using advanceProgramingProject.Models;
using advanceProgramingProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Controllers
{
    internal class ExamMarkController
    {
        IExamMarkService service = new ExamMarkService();
        IExamService examService = new ExamService();
        IStudentService studentService = new StudentService();


        public int ValidateStudentId(string method)
        {
            Console.WriteLine("Chosse Id of Student");
            List<Student> students = studentService.Index().ToList();
            foreach (Student item in students)
            {
                Console.WriteLine($"Id:  {item.Id} \t First Name: {item.FirstName} \t Last Name: {item.LastName}");
            }
            int id;
            Student d;
            do
            {
                Console.Write("Id: ");
                string temp = Console.ReadLine();
                if (method == "update" && temp == "")
                {
                    return 0;
                }
                id = temp == "" ? -1 : Convert.ToInt32(temp);
                d = students.FirstOrDefault(d => d.Id == id);
                if (d == null || id == -1)
                {
                    Console.WriteLine("Choose a Correct Id");
                }
            } while (d == null || id == -1);
            return id;
        }


        public int ValidateExamtId(string method)
        {
            Console.WriteLine("Chosse Id of Exame");
            List<Exam> exams = examService.Index().ToList();
            foreach (Exam item in exams)
            {
                Console.WriteLine($"Id:  {item.Id} \t Name:{item.Subject.Name} \t Term: {item.Term}");
            }
            int id;
            Exam d;
            do
            {
                Console.Write("Id: ");
                string temp = Console.ReadLine();
                if (method == "update" && temp == "")
                {
                    return 0;
                }
                id = temp == "" ? -1 : Convert.ToInt32(temp);
                d = exams.FirstOrDefault(d => d.Id == id);
                if (d == null)
                {
                    Console.WriteLine("Choose a Correct Id");
                }
            } while (d == null);
            return id;
        }


        public int ValidateMark(string method)
        {
            string temp;
            do
            {
                Console.Write("Mark: ");
                temp = Console.ReadLine();
                if (temp == "" && method == "update")
                    return -1;
                if (temp == "")
                {
                    Console.WriteLine("Enter Mark Please!");
                }
            } while (temp == "");
            return int.Parse(temp);
        }

        public async void Index()
        {
            List<ExamMark> examMarks = service.Index().ToList();
            Console.WriteLine("*********************************************************************************\r\n|\tId\t|\tStudent\t|\tExam\t|\tTerm\t|\tMark\t|");

            foreach (ExamMark item in examMarks)
            {
                Console.WriteLine(String.Format("-----------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t|\t{2}\t|\t{3}\t|\t{4}\t",
                    item.Id,
                    (item.Student.FirstName + item.Student.LastName),
                    item.Exam.Subject.Name,
                    item.Exam.Term,
                    item.Mark
                    ));
            }

            Console.WriteLine();
            Console.WriteLine("choose options");
            Console.WriteLine("1. Create \n2. Update \n3. Delete \n4. Show \n5. back");
            Console.WriteLine();
            Console.Write("Choose: ");
            int chosse = Convert.ToInt32(Console.ReadLine());

            switch (chosse)
            {
                case 1:
                    {
                        Create();
                        break;
                    }
                case 2:
                    {
                        Update();
                        break;
                    }
                case 3:
                    {
                        Delete();
                        break;
                    }
                default:
                    {
                        return;
                    }

            }
        }


        public async void Create()
        {
            int studentId = ValidateStudentId("create");
            int examId = ValidateExamtId("create");
            int mark = ValidateMark("create");
   

            ExamMark e = new ExamMark()
            {
                StudentId = studentId,
                ExamId = examId,
                Mark = mark
            };


            await service.Create(e);
            Index();
        }


        public async void Update()
        {
            Console.WriteLine("Enter Exam Mark Id");
            int id = Convert.ToInt32(Console.ReadLine());
            ExamMark? examMark = service.Index().FirstOrDefault(d => d.Id == id);
            if (examMark == null)
            {
                Console.WriteLine("Couldn't find Exam Mark!");
                return;
            }

            int studentId = ValidateStudentId("update");
            int examId = ValidateExamtId("update");
            int mark = ValidateMark("update");

            if (studentId != 0)
                examMark.StudentId = studentId;

            if (examId != 0)
                examMark.ExamId = examId;

            if (mark != -1)
                examMark.Mark = mark;

            await service.Update(examMark);
            Index();
        }


        public async void Delete()
        {
            Console.WriteLine("Enter Exam Mark Id");
            int id = Convert.ToInt32(Console.ReadLine());
            ExamMark? examMark = service.Index().FirstOrDefault(d => d.Id == id);
            if (examMark == null)
            {
                Console.WriteLine("Couldn't find Exam Mark!");
                return;
            }
            await service.Delete(examMark);
            Index();
        }


    }
}
