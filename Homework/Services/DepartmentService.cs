using advanceProgramingProject.Data;
using advanceProgramingProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Services
{
    internal class DepartmentService : IDepartmentService
    {
        AppDb db = new AppDb();
        public ICollection<Department> Index()
        {
            return db.Departments.Include(d => d.Students).Include(d=>d.Subjects).ToList();
        }

        public async Task<bool> Create(Department student)
        {
            try
            {
                await db.Departments.AddAsync(student);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<Department> Update(Department dept)
        {
            db.Update(dept);
            db.SaveChanges();
            return dept;
        }


        public async Task<bool> Delete(Department dept)
        {         
            db.Departments.Remove(dept);
            db.SaveChanges();
            return true;

        }

        public Department? Show(int id)
        {
            return db.Departments.Include(d => d.Students).Include(d => d.Subjects).FirstOrDefault(d => d.Id == id);
        }

        public ICollection<Student> ShowStudents(int id)
        {
            Department d = db.Departments.Include(d => d.Students).First(d => d.Id == id);
            return d.Students;
        }

        public ICollection<Subject> ShowSubjects(int id)
        {
            Department d = db.Departments.Include(d => d.Subjects).ThenInclude(s => s.SubjectLectures).First(d => d.Id == id);
            return d.Subjects;
        }

        public List<Student> ViewStudentsByYear(int id, int year)
        {
            return db.Students.Include(s => s.Department).Where(s => s.DepartmentId == id && s.Year == year).ToList();
        }
    }
}
