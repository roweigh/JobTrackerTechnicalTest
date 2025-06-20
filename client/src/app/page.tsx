'use client';

import React, { useEffect, useState } from 'react';
import { SortingState, ColumnFiltersState } from '@tanstack/react-table';
import { columns } from '@/components/table/Columns';

import { handle } from '@/api/api-utils';
import { getApplications, Application, Pagination } from '@/api/applications';

import DataTable from '@/components/table/DataTable';
import BulkUploadDialog from '@/components/dialog/BulkuploadDialog';
import AddApplicationDialog from '@/components/dialog/AddApplicationDialog';
import EditApplicationDialog from '@/components/dialog/EditApplicationDialog';

// Fetch applications when table paginates, then update pagination information
function useGetApplications() {
  const [applications, setApplications] = useState([]);
  const [paginated, setPaginated] = useState<Pagination>({
    page: 1,
    size: 20,
    totalElements: 0,
    totalPages: 1,
    first: true,
    last: true
  });

  const fetchApplications = async (pagination: Pagination, sortBy: SortingState, statuses: ColumnFiltersState) => {
    try {
      const { data } = await getApplications(pagination, sortBy, statuses);
      setApplications(data.content);
      setPaginated(data.pagination);
    } catch (error) {
      handle(error);
    }
  };

  return { fetchApplications, applications, paginated };
}

export default function Home() {
  // CRUD Overlays
  const [appId, setAppId] = useState<string | null>(null);
  const [editDialog, setEditDialog] = useState<boolean>(false);

  // Paginated query data
  const { fetchApplications, applications, paginated } = useGetApplications();
  const [statuses, setStatuses] = useState<ColumnFiltersState>([{id: 'status', value: []}]);
  const [sortBy, setSortBy] = useState<SortingState>([{id: 'dateApplied', desc: true}]);
  const [pagination, setPagination] = useState({
    page: 1,
    size: 20,
    totalElements: 0,
    totalPages: 1,
    first: true,
    last: true
  });

  // Fetch applications on paginaton/sort
  useEffect(() => {
    fetchApplications(pagination, sortBy, statuses);
  }, [sortBy, pagination, statuses]);

  // Fetch applications after each update
  const refresh = () => {
    fetchApplications(pagination, sortBy, statuses);
    setEditDialog(false);
  };

  // Opens/closes edit application dialog
  const setEdit = (v: Application) => {
    setAppId(v.id ?? null);
    setEditDialog(!!v);
  };

  return (
    <main className="min-w-2xs mx-[5%] my-[40px] lg:mx-auto lg:my-[60px] lg:w-1/2 flex flex-col gap-[8px]">
      <div className="flex">
        <span className="mr-auto text-3xl font-bold select-none">
          Applications
        </span>

        {/* CRUD Dialogs */}
        <BulkUploadDialog refresh={refresh} />
        <AddApplicationDialog refresh={refresh} />
        <EditApplicationDialog
          id={appId}
          open={editDialog}
          setOpen={setEditDialog}
          refresh={refresh}
        />
      </div>

      <DataTable
        columns={columns}
        data={applications}
        setEdit={setEdit}
        pagination={paginated}
        onPagination={setPagination}
        sorting={sortBy}
        onSortingChange={setSortBy}
        columnFilters={statuses}
        onColumnFiltersChange={setStatuses}
      />
    </main>
  );
}
