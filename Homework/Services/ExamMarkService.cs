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
    internal class ExamMarkService : IExamMarkService
    {
        AppDb db = new AppDb();

        public ICollection<ExamMark> Index()
        {
            return db.ExamMarks.Include(e=>e.Student).Include(e=>e.Exam).ThenInclude(e=>e.Subject).ToList();
        }


        public async Task<bool> Create(ExamMark em)
        {
            try
            {
                await db.ExamMarks.AddAsync(em);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        public async Task<ExamMark> Update(ExamMark em)
        {
            db.Update(em);
            db.SaveChanges();
            return em;
        }

        public async Task<bool> Delete(ExamMark em)
        {
            db.ExamMarks.Remove(em);
            db.SaveChanges();
            return true;
        }
    }
}
