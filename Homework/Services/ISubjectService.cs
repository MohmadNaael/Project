using advanceProgramingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Services
{
    internal interface ISubjectService
    {
        public ICollection<Subject> Index();

        public Task<bool> Create(Subject subject);

        public Task<Subject> Update(Subject subject);

        public Task<bool> Delete(Subject s);

        public Subject? Show(int id);

        public List<SubjectLecture> ViewLectures(int id);

        public List<Subject> ShowByDepartment(int dept_id);

        public List<Subject> ShowByYear(int year);

        public List<Subject> ShowByTerm(int term);
    }
}
