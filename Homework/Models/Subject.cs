using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Models
{
    internal class Subject
    {
        [Key]
        public int Id { get; set; }

        public int DeptId { get; set; }

        public string Name { get; set; }

        public int MinDegree { get; set; }

        public short Term { get; set; }
        public short Year { get; set; }

        [ForeignKey("DeptId")]
        public virtual Department Department { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<SubjectLecture> SubjectLectures { get; set; }
    }
}
