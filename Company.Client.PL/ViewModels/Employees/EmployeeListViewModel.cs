namespace Company.Client.PL.ViewModels.Employees
{
    public class EmployeeListViewModel
    {
        //Collection of Employees to Display
        public IEnumerable<EmployeeViewModel> EmployeeList { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public string? SearchTerm { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }
    }
}
