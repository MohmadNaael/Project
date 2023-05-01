using advanceProgramingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Services
{
    internal interface ISubjectLecturesService
    {
        public ICollection<SubjectLecture> Index();

        public Task<bool> Create(SubjectLecture subject_lecture);

        public Task<SubjectLecture> Update(SubjectLecture subject_lecture);

        public Task<bool> Delete(SubjectLecture sl);
        public SubjectLecture? Show(int id);
    }
}
