using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Entites
{
    public class TaskComment
    {
        public Guid Id { get; set; }
        public string Comment { get; set; } = string.Empty;

        public Guid TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = null!;

        public Guid CreatedByUserId { get; set; }
        public User CreatedBy { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }

}
