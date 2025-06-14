using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.DTO
{
    public class TaskUpdateDto
    {
        public Guid Id { get; set; }    
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public Guid ProjectId { get; set; }
        public Guid AssignedToUserId { get; set; }

        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }

}
