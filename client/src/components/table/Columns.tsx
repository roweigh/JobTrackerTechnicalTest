import React from 'react';
import { ColumnDef } from '@tanstack/react-table';
import { Application } from '@/api/applications';

import { Checkbox } from '@/components/ui/checkbox';
import { Label } from '@/components/ui/label';
import { Badge } from '@/components/ui/badge';
import { Button } from '@/components/ui/button';
import {
  HoverCard,
  HoverCardContent,
  HoverCardTrigger
} from '@/components/ui/hover-card';

import {
  Funnel,
  ChevronUp,
  ChevronDown,
  ChevronsUpDown
} from 'lucide-react';

// Define table columns
export const columns: ColumnDef<Application>[] = [
  {
    accessorKey: 'companyName',
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          className="cursor-pointer"
          onClick={() => column.toggleSorting(column.getIsSorted() === 'asc')}
        >
          Company Name
          {column.getIsSorted() === 'asc' ? (
            <ChevronUp />
          ) : column.getIsSorted() === 'desc' ? (
            <ChevronDown />
          ) : (
            <ChevronsUpDown className="opacity-[0.4]" />
          )}
        </Button>
      );
    }
  },
  {
    accessorKey: 'position',
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          className="cursor-pointer"
          onClick={() => column.toggleSorting(column.getIsSorted() === 'asc')}
        >
          Position
          {column.getIsSorted() === 'asc' ? (
            <ChevronUp />
          ) : column.getIsSorted() === 'desc' ? (
            <ChevronDown />
          ) : (
            <ChevronsUpDown className="opacity-[0.4]" />
          )}
        </Button>
      );
    }
  },
  {
    accessorKey: 'status',
    header: ({ column }) => {
      const filterValue = column.getFilterValue() as string[] | undefined;
      const filterSet = new Set(filterValue ?? []);
      
      return (
        <HoverCard 
          openDelay={200}
          closeDelay={200}
        >
          <HoverCardTrigger>
            <div className={'text-center'}>
              <Button
                variant="ghost"
                className="cursor-pointer"
                onClick={() => column.toggleSorting(column.getIsSorted() === 'asc')}
              >
                {filterSet.size > 0 ? (
                  <Funnel />
                ) : (
                  <Funnel className="opacity-[0.4]"/>
                )}

                Status

                {column.getIsSorted() === 'asc' ? (
                  <ChevronUp />
                ) : column.getIsSorted() === 'desc' ? (
                  <ChevronDown />
                ) : (
                  <ChevronsUpDown className="opacity-[0.4]" />
                )}
              </Button>
            </div>
          </HoverCardTrigger>
          <HoverCardContent 
            sideOffset={10}
            align={'start'} className="flex flex-col gap-[16px]">
            {['Applied', 'Interview', 'Offer', 'Rejected'].map(value => (
              <div key={value} className="flex gap-[8px]">
                <Checkbox 
                  checked={filterSet.has(value)}
                  onCheckedChange={(v) => {
                    if (v) {
                      filterSet.add(value);
                    } else {
                      filterSet.delete(value);
                    }
                    column.setFilterValue([...filterSet]);
                  }}
                />
                <Label>{value}</Label>
              </div>
            ))}
          </HoverCardContent>
        </HoverCard>
      );
    },
    cell: ({ row }) => {
      return (
        <div className="text-center">
          <Status status={row.original.status} />
        </div>
      );
    }
  },
  {
    accessorKey: 'dateApplied',
    header: ({ column }) => {
      return (
        <Button
          variant="ghost"
          className="cursor-pointer"
          onClick={() => column.toggleSorting(column.getIsSorted() === 'asc')}
        >
          Date Applied
          {column.getIsSorted() === 'asc' ? (
            <ChevronUp />
          ) : column.getIsSorted() === 'desc' ? (
            <ChevronDown />
          ) : (
            <ChevronsUpDown className="opacity-[0.4]" />
          )}
        </Button>
      );
    },
    cell: ({ row }) => {
      return <span>{generateDateTime(row.original.dateApplied)}</span>;
    }
  },
  {
    accessorKey: 'actions',
    header: 'Actions'
  }
];

// Convert ISO DateTime string to human-readable format
function generateDateTime(dateStr: string) {
  const dateTime = new Date(dateStr);
  const date = dateTime.toLocaleDateString();
  const time = dateTime.toLocaleTimeString('en-NZ', {
    hour12: true,
    hour: 'numeric',
    minute: '2-digit'
  });
  return `${date} ${time}`;
}

// Generate status + colour
function Status({ status }: { status: string }) {
  let className = 'select-none w-[75px] ';
  switch (status) {
    case 'Applied':
      className = className.concat('bg-yellow-500');
      break;
    case 'Interview':
      className = className.concat('bg-blue-400');
      break;
    case 'Rejected':
      className = className.concat('bg-red-400');
      break;
    case 'Offer':
      className = className.concat('bg-green-500');
      break;
    default:
      break;
  }

  return <Badge className={className}>{status}</Badge>;
}
