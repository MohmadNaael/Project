using advanceProgramingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Services
{
    internal interface IExamService
    {
        public ICollection<Exam> Index();

        public Task<bool> Create(Exam exam);

        public Task<Exam> Update(Exam exam);

        public Task<bool> Delete(Exam exam);

        public Exam? Show(int id);
        public List<ExamMark> ShowMarks(int id);

        public List<Student> GetStudentsWithoutExam(int id);
        public List<Student> GetStudentsWithExam(int id);
    }
}
