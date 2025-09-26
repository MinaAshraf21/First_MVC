using System.ComponentModel;

namespace Company.Client.PL.ViewModels.Departments
{
    //View Models is used for displaying the data to the user in some way
    //we are not using DTO for displaying data because we're using it for transfering data between layers
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
        [DisplayName("Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
