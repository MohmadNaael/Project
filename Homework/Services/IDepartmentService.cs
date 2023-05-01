using advanceProgramingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Services
{
    internal interface IDepartmentService
    {
        public ICollection<Department> Index();

        public Task<bool> Create(Department department);

        public Task<Department> Update(Department department);


        public Department? Show(int id);

        public ICollection<Student> ShowStudents(int id);
        public ICollection<Subject> ShowSubjects(int id);
        public Task<bool> Delete(Department dept);
        public List<Student> ViewStudentsByYear(int id,int year);
    }
}
