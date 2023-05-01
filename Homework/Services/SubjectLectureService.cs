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
    internal class SubjectLectureService : ISubjectLecturesService
    {
        AppDb db = new AppDb();

        public ICollection<SubjectLecture> Index()
        {
            return db.SubjectLectures.Include(sl=>sl.Subject).ToList();
        }


        public async Task<bool> Create(SubjectLecture sl)
        {
            try
            {
                await db.SubjectLectures.AddAsync(sl);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        public async Task<SubjectLecture> Update(SubjectLecture sl)
        {
            db.Update(sl);
            db.SaveChanges();
            return sl;
        }


        public async Task<bool> Delete(SubjectLecture s)
        {
            db.SubjectLectures.Remove(s);
            db.SaveChanges();
            return true;
        }

        public SubjectLecture? Show(int id)
        {
            return db.SubjectLectures.FirstOrDefault(l => l.Id == id);
        } 
    }
}