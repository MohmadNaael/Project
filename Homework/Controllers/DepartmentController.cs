using advanceProgramingProject.Models;
using advanceProgramingProject.Services;

namespace advanceProgramingProject.Controllers
{
    internal class DepartmentController
    {
        IDepartmentService service = new DepartmentService();

        public void Show()
        {
            Console.WriteLine("Enter Department Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Department? department = service.Show(id);
            if (department == null)
            {
                Console.WriteLine("Couldn't find department!");
                return;
            }
            Console.WriteLine("\r\n|Id\t|Name\t\t| Students Count| Subjects Count|\r\n---------------------------------------------------------");
            Console.WriteLine(String.Format(
                "|{0}\t|{1}\t|\t{2}\t|\t{3}\t|\r\n---------------------------------------------------------",
                department.Id,
                department.Name,
                department.Students.Count,
                department.Subjects.Count
                ));

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Get Students");
            Console.WriteLine("2. Get Subjects");
            Console.WriteLine("3. Go Back");

            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    GetStudents(id);
                    break;
                case 2:
                    GetSubjects(id);
                    break;
                default:
                    return;
            }
        }

        public void GetStudents(int id)
        {
            Console.WriteLine("************************************************************************************\r\n|Id     |First Name     |Last Name      |Year   |Username       \t|Email                  |Phone          |Register Date |Department\t|\r\n***********************************************************************************************************");
            var students = service.ShowStudents(id);
            foreach (Student item in students)
            {
                Console.WriteLine(String.Format("|{0}      |{1}           |{2}         |{3}      |{4}                |{5} \t|{6}     |{7}    |{8}\t|",
                        item.Id,
                        item.FirstName,
                        item.LastName,
                        item.Year,
                        item.Username,
                        item.Email,
                        item.Phone,
                        item.RegisterDate.ToShortDateString(),
                        item.Department.Name
                    ));
            }
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
            FilterStudentsByYear(id);
        }

        public void FilterStudentsByYear(int id)
        {
            Console.WriteLine("Enter a year to filter student or enter . if you don't want to filter:\t");
            string ans = Console.ReadLine();
            if (ans == ".")
            {
                return;
            }
            int year;
            while (!int.TryParse(ans, out year))
            {
                Console.WriteLine("enter a valid year:\t");
                ans = Console.ReadLine();
            }
            List<Student> filted_students = service.ViewStudentsByYear(id, year);
            Console.WriteLine("*************************************************************************************************************************\r\n|Id     |First Name     |Last Name      |Year   |Username       \t|Email                  |Phone          |Register Date |Department\t|\r\n-------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (Student item in filted_students)
            {
                Console.WriteLine(String.Format("|{0}      |{1}           |{2}         |{3}      |{4}                |{5} \t|{6}     |{7}    |{8}\t|",
                        item.Id,
                        item.FirstName,
                        item.LastName,
                        item.Year,
                        item.Username,
                        item.Email,
                        item.Phone,
                        item.RegisterDate.ToShortDateString(),
                        item.Department.Name
                    ));
            }
        }

        public void GetSubjects(int id)
        {
            Console.WriteLine("|Id\t|Name\t\t|Min Degree\t|Term\t|Year\t|Lectures Count\t|\r\n-------------------------------------------------------------------------");
            var subjects = service.ShowSubjects(id);
            foreach (var item in subjects)
            {
                Console.WriteLine(String.Format("|{0}\t|{1}\t|{2}\t\t|{3}\t|{4}\t|\t{5}\t|",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Term,
                    item.Year,
                    item.SubjectLectures.Count
                    ));
            }
            Console.WriteLine("-------------------------------------------------------------------------");
        }


        public async void Index()
        {
            List<Department> departments = service.Index().ToList();
            Console.WriteLine("**************************************************\r\n|\tid\t|\t\tName\t\t|");
            foreach (Department item in departments)
            {
                Console.WriteLine(String.Format("-------------------------------------------------\r\n|\t{0}\t|\t{1}\t\t|", item.Id, item.Name));
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
                        Show();
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
            string name;
            do
            {
                Console.Write("Name: ");
                name = Console.ReadLine();
                if (name == "")
                {
                    Console.WriteLine("Enter Name Please!");
                }
            } while (name == "");

            Department d = new Department()
            {
                Name = name
            };

            await service.Create(d);
            Index();

        }


        public async void Update()
        {
            Console.WriteLine("Enter Department Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Department? department = service.Show(id);
            if (department == null)
            {
                Console.WriteLine("Couldn't find department!");
                return;
            }
            Console.Write("Name: ");
            string name = Console.ReadLine();

            if (name != "")
                department.Name = name;

            await service.Update(department);
            Index();
        }



        public async void Delete()
        {
            Console.WriteLine("Enter Department Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Department? department = service.Show(id);
            if (department == null)
            {
                Console.WriteLine("Couldn't find department!");
                return;
            }
            await service.Delete(department);
            Index();
        }

    }
}
