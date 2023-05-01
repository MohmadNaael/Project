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
    internal class ExamService : IExamService
    {
        AppDb db = new AppDb();

        public ICollection<Exam> Index()
        {
            return db.Exams.Include(e => e.Subject).ToList();
        }


        public async Task<bool> Create(Exam exam)
        {
            try
            {
                await db.Exams.AddAsync(exam);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        public async Task<Exam> Update(Exam exam)
        {
            db.Update(exam);
            db.SaveChanges();
            return exam;
        }

        public async Task<bool> Delete(Exam exam)
        {
            db.Exams.Remove(exam);
            db.SaveChanges();
            return true;
        }

        public Exam? Show(int id)
        {
            return db.Exams.Include(e => e.ExamMarks).Include(e => e.Subject).FirstOrDefault(e => e.Id == id);
        }

        public List<ExamMark> ShowMarks(int id)
        {
            return db.Exams
                .Include(e => e.ExamMarks)
                .ThenInclude(m => m.Student)
                .First(e => e.Id == id).ExamMarks.ToList();
        }

        public List<Student> GetStudentsWithoutExam(int id)
        {
            var studentsWithoutExam = (from s in db.Students
                                      where !db.ExamMarks.Any(em => em.StudentId == s.Id && em.ExamId == id)
                                      select s).ToList();

            return studentsWithoutExam;
        }
        public List<Student> GetStudentsWithExam(int id)
        {
            var studentsWithExam = (from s in db.Students
                                      where db.ExamMarks.Any(em => em.StudentId == s.Id && em.ExamId == id)
                                      select s).ToList();

            return studentsWithExam;
        }

       
    }
}
