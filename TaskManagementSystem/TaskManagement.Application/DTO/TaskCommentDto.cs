using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.DTO
{
    public class TaskCommentDto
    {
        public Guid Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Guid CreatedByUserId { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
    }
}
