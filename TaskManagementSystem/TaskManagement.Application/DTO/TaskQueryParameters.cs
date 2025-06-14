using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.DTO
{
    public class TaskQueryParameters
    {
        public string? Search { get; set; }          
        public Guid? ProjectId { get; set; }        

        public string? SortBy { get; set; } = "CreatedAt";   
        public bool IsDescending { get; set; } = false;      

        public int PageNumber { get; set; } = 1;     
        public int PageSize { get; set; } = 10;      
    }

}
