using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Models
{
    internal class Student
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }   
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        public short Year { get; set; }
        public string Email { get; set; }   
        public string Phone { get; set; }   
        public DateTime RegisterDate { get; set; }   
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public virtual ICollection<ExamMark> ExamMarks { get; set; }
    }
}
