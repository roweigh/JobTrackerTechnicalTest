using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationApi.Models
{
    public class JobApplication
    {
        [Key]
        [Required]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName ="nvarchar(255)")]
        public string companyName { get; set; }

        [Required]
        [StringLength(255)]
        [Column(TypeName ="nvarchar(255)")]
        public string position { get; set; }

        [Required]
        [StringLength(100)]
        [Column(TypeName ="nvarchar(100)")]
        public string status { get; set; }

        [Required]
        [Column(TypeName ="datetime2")]
        public DateTime dateApplied {  get; set; }
    }
}
