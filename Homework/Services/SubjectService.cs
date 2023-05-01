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
    internal class SubjectService : ISubjectService
    {
        AppDb db = new AppDb();
        public ICollection<Subject> Index()
        {
            return db.Subjects.Include(s=>s.Department).Include(s=>s.SubjectLectures).ToList();
        }

        public async Task<bool> Create(Subject s)
        {
            try
            {
                await db.Subjects.AddAsync(s);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        public async Task<Subject> Update(Subject s)
        {
            db.Update(s);
            db.SaveChanges();
            return s;
        }


        public async Task<bool> Delete(Subject s)
        {

            db.Subjects.Remove(s);
            db.SaveChanges();
            return true;

        }

        public Subject? Show(int id)
        {
            return db.Subjects
                .Include(s => s.Department)
                .Include(s => s.SubjectLectures)
                .FirstOrDefault(s => s.Id == id);
        }

        public List<SubjectLecture> ViewLectures(int id)
        {
            return db.SubjectLectures
                .Where(l => l.SubjectId == id)
                .ToList();
        }

        public List<Subject> ShowByDepartment(int dept_id)
        {
            return db.Subjects
                .Include(s => s.Department)
                .Include(s => s.SubjectLectures)
                .Where(s => s.DeptId == dept_id).ToList();
        }
        public List<Subject> ShowByYear(int year)
        {
            return db.Subjects
                .Include(s => s.Department)
                .Include(s => s.SubjectLectures)
                .Where(s => s.Year == year).ToList();
        }
        public List<Subject> ShowByTerm(int term)
        {
            return db.Subjects
                .Include(s => s.Department)
                .Include(s => s.SubjectLectures)
                .Where(s => s.Term == term).ToList();
        }


    }
}
