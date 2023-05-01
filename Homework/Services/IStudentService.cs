using advanceProgramingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Services
{
    internal interface IStudentService
    {
        public ICollection<Student> Index();

        public  Task<bool> Create(Student student);

        public  Task<Student> Update(Student student);

        public  Task<bool> Delete(Student s);

        public Student? Show(int id);
        
        public ICollection<ExamMark> showMarks(int id);

        public Task<double> CalculateAvarege(int id);

        public List<Subject> ViewSubjects(int id);

        public List<Subject> ViewSubjectsByDepartment(int id,int dept_id);
        
        public List<Subject> ViewSubjectsByTerm(int id, short term);
        
        public List<Subject> ViewSubjectsByYear(int id, int year);

    }
}
