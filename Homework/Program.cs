using advanceProgramingProject.Controllers;
using advanceProgramingProject.Services;

namespace advanceProgramingProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice;
            while (true)
            {

                Console.WriteLine("Hello... Which Table Do You Want Display?");
                Console.WriteLine(" 1. Student \n 2. Subject \n 3. Subject Lecture \n 4. Department \n 5. Exam \n 6. Exam Mark \n 7. Exit");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        {
                            StudentController st = new StudentController();
                            st.Index();
                            break;
                        }
                    case 2:
                        {
                            SubjectController su = new SubjectController();
                            su.Index();
                            break;
                        }
                    case 3:
                        {
                            SubjectLecturesController sl = new SubjectLecturesController();
                            sl.Index();
                            break;
                        }
                    case 4:
                        {
                            DepartmentController d = new DepartmentController();
                            d.Index();
                            break;
                        }
                    case 5:
                        {
                            ExamController e = new ExamController();
                            e.Index();
                            break;
                        }
                    case 6:
                        {
                            ExamMarkController em = new ExamMarkController();
                            em.Index();
                            break;
                        }
                    case 7:
                        {
                            Console.WriteLine("Have A Good day....");
                            return;                          
                        }
                    default:
                        {
                            Console.WriteLine("Choose A Correct Choice!!");
                            break;
                        }

                }
            }

        }
    }
}