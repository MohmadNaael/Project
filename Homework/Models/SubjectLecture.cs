using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Models
{
    internal class SubjectLecture
    {
        [Key]
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
    }
}
