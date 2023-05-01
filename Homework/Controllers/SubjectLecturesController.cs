using advanceProgramingProject.Models;
using advanceProgramingProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace advanceProgramingProject.Controllers
{
    internal class SubjectLecturesController
    {
        ISubjectLecturesService service = new SubjectLectureService();
        ISubjectService subjectservice = new SubjectService();

public void ShowLecture()
        {
            Console.WriteLine("Enter Lecture Id");
            int id = Convert.ToInt32(Console.ReadLine());
            SubjectLecture? lecture = service.Show(id);
            if (lecture == null)
            {
                Console.WriteLine("Couldn't find lecture!");
                return;
            }
            Console.WriteLine("Title:\t"+lecture.Title);
            Console.WriteLine("Content:");
            Console.WriteLine(lecture.Content);
        }

        public string ValidateString(string validate, string method)
        {
            string item;
            do
            {
                Console.Write(validate + ": ");
                item = Console.ReadLine();
                if (item == "" && method == "update")
                {
                    return item;
                }
                if (item == "")
                {
                    Console.WriteLine($"Enter {validate} Please!");
                }
            } while (item == "");
            return item;
        }


        public int ValidateSubjectId(string method)
        {
            Console.WriteLine("Chosse Id of Subject");
            List<Subject> subjects = subjectservice.Index().ToList();
            foreach (Subject item in subjects)
            {
                Console.WriteLine("Id: " + item.Id + "\t Name:" + item.Name);
            }
            int id;
            Subject d;
            do
            {
                Console.Write("Id: ");
                string temp = Console.ReadLine();
                if (method == "update" && temp == "")
                {
                    return 0;
                }
                id = temp == "" ? -1 : Convert.ToInt32(temp);
                d = subjects.FirstOrDefault(d => d.Id == id);
                if (d == null || id == -1)
                {
                    Console.WriteLine("Choose a Correct Id");
                }
            } while (d == null || id == -1);
            return id;
        }



        public async void Index()
        {
            List<SubjectLecture> subjectLectures = service.Index().ToList();
            Console.WriteLine("*********************************************************************************\r\n|\tId\t|\tTitle\t\t|\tContent\t\t|\tSubject\t|");

            foreach (SubjectLecture item in subjectLectures)
            {
                Console.WriteLine(String.Format("---------------------------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t\t|\t{2}\t\t|\t{3}\t|",
                    item.Id,
                    item.Title,
                    item.Content,
                    item.Subject.Name
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
                case 4:
                    {
                        ShowLecture();
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
            string title, content;
            title = ValidateString("Titel","create");
            content = ValidateString("Content", "create");
            int subjectId = ValidateSubjectId("create");

            SubjectLecture s = new SubjectLecture()
            {
                SubjectId = subjectId,
                Content = content,
                Title = title
            };

            await service.Create(s);
            Index();
        }


        public async void Update()
        {
            Console.WriteLine("Enter Lecture Id");
            int id = Convert.ToInt32(Console.ReadLine());
            SubjectLecture? lecture = service.Show(id);
            if (lecture == null)
            {
                Console.WriteLine("Couldn't find lecture!");
                return;
            }

            string title, content;
            title = ValidateString("Titel", "update");
            content = ValidateString("Content", "update");
            int subjectId = ValidateSubjectId("update");

            if (title != ".")
                lecture.Title = title;

            if (content != ".")
                lecture.Content = content;

            if (subjectId != 0)
                lecture.SubjectId = subjectId;
            await service.Update(lecture);
            Index();
        }

        public async void Delete()
        {
            Console.WriteLine("Enter Lecture Id");
            int id = Convert.ToInt32(Console.ReadLine());
            SubjectLecture? lecture = service.Show(id);
            if (lecture == null)
            {
                Console.WriteLine("Couldn't find lecture!");
                return;
            }
            await service.Delete(lecture);
            Index();
        }

    }
}
