using System.ComponentModel;

namespace Company.Client.PL.ViewModels.Departments
{
    public class DepartmentDetailsViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        [DisplayName("Creation Date")]
        public DateOnly CreationDate { get; set; }
        public required string Description { get; set; }

        [DisplayName("Created By")]
        public required string CreatedBy { get; set; }
        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Last Modified By")]
        public required string LastModifiedBy { get; set; }
        [DisplayName("Last Modified On")]
        public DateTime LastModifiedOn { get; set; }
    }
}
