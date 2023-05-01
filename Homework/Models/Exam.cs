using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Models
{
    internal class Exam
    {
        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public short Term { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }

        public virtual ICollection<ExamMark> ExamMarks { get; set; }
    }
}
