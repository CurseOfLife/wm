using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class RequestParams
    {
        //maximum page size a client can request
        const int maxPageSize = 100;

        //default page size 10
        private int _pageSize = 10;

        //default page number 1
        public int PageNumber { get; set; } = 1;

        //if user requests more than 100 we send 100 as its max
        //otherwise send back the number of items requested
        public int PageSize {
            get
            {
                return _pageSize;          
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            } }


      
    }
}
