using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Client.DAL.Persistence.Common
{

    /*
         * if a method returns paginated data means it returns an object with 4 properties :
         * 1.page size
         * 2.page num
         * 3.data
         * 4.total count
 */
    public class PaginationParameters
    {
        const int MaxPageSize = 20;

        private int _pageIndex;
        public int PageIndex 
        {
            get => _pageIndex;
            set => _pageIndex = value < 1 ? 1 : value;
        }

        private int _pageSize;
        public int PageSize 
        {
            get => _pageSize; 
            set => _pageSize = value > MaxPageSize? MaxPageSize : value;
        }

        private string _searchTerm;
        public string SearchTerm
        {
            get => _searchTerm;
            set => _searchTerm = value.ToLower();
        }

        public string SortBy { get; set; }
        public bool SortAscending { get; set; }
    }
}
