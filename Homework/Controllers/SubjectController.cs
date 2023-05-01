using advanceProgramingProject.Models;
using advanceProgramingProject.Services;

namespace advanceProgramingProject.Controllers
{
    internal class SubjectController
    {
        ISubjectService service = new SubjectService();
        IDepartmentService departmentService = new DepartmentService();

        public int ValidateDepartmentId(string method)
        {
            Console.WriteLine("Chosse Id of Department");
            List<Department> depts = departmentService.Index().ToList();
            foreach (Department item in depts)
            {
                Console.WriteLine("Id: " + item.Id + "\t Name:" + item.Name);
            }
            int id;
            Department d;
            do
            {
                Console.Write("Id: ");
                string temp = Console.ReadLine();
                if (method == "update" && temp == "")
                {
                    return 0;
                }
                id = temp == "" ? -1 : Convert.ToInt32(temp);
                d = depts.FirstOrDefault(d => d.Id == id);
                if (d == null || id == -1)
                {
                    Console.WriteLine("Choose a Correct Id");
                }
            } while (d == null || id == -1);
            return id;
        }


        public int ValidateInt(string method)
        {   
            string temp;
            do
            {
                temp = Console.ReadLine();
                if (temp == "" && method == "update")
                    return -1;
                if (temp == "")
                {
                    Console.WriteLine("Please Enter Data");
                }
            } while (temp == "");
            return int.Parse(temp);
        }
        public short ValidateShort(string method,string validate)
        {
            string temp;
            short t;
            do
            {
                Console.Write($"{validate}: ");
                temp = Console.ReadLine();
                if (temp == "" && method == "update")
                    return -1;
                if (temp == "")
                {
                    Console.WriteLine($"Please Enter {validate}");
                }

                t = short.Parse(temp);
                if (t < 0)
                {
                    Console.WriteLine("Please Enter Correct Data");
                }
            } while (temp == "" || t <0);

            return t;
        }



        public async void Index()
        {
            List<Subject> subjects = service.Index().ToList();
            Console.WriteLine("**************************************************************************************************************\r\n|\tid\t|\tName\t|\tMinDegree\t|\tyear\t|\tDepartment\t|\tterm\t|\tNumber Lecture\t|");

            foreach (Subject item in subjects)
            { 
                Console.WriteLine(String.Format("---------------------------------------------------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t|\t{2}\t\t|\t{3}\t|\t{4}\t\t|\t{5}\t|\t{6}\t  |",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Year,
                    item.Department.Name,
                    item.Term,
                    item.SubjectLectures.Count
                    ));
            }
            Console.WriteLine();
            Console.WriteLine("choose options");
            Console.WriteLine("1. Create \n2. Update \n3. Delete \n4. Show \n5. Show By Department \n6. Show By Year \n7.Show By Term \n8.Back");
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
                case 5:
                    {
                        ShowByDepartment();
                        break;
                    } 
                case 6:
                    {
                        ShowByYear();
                        break;
                    }
                case 7:
                    {
                        ShowByTerm();
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

            Console.Write("Minum Degree: ");
            int min = ValidateInt("Create");
            Console.Write("Year: ");
            short year = ValidateShort("create","Year");
            Console.Write("Term: ");
            short term = ValidateShort("create","Term");

            int id = ValidateDepartmentId("create");


            Subject s = new Subject()
            {
                DeptId = id,
                Name = name,
                Term = term,
                Year = year,
                MinDegree = min,
            };
            await service.Create(s);
            Index();
        }



        public async void Update()
        {
            Console.WriteLine("Enter Subject Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Subject? subject = service.Show(id);
            if (subject == null)
            {
                Console.WriteLine("Couldn't find lecture!");
                return;
            }

            string name = Console.ReadLine();
            Console.Write("Minum Degree: ");
            int min = ValidateInt("update");
            Console.Write("Year: ");
            short year = ValidateShort("update","Year");
            Console.Write("Term: ");
            short term = ValidateShort("update","Term");

            int deptId = ValidateDepartmentId("update");
            if (name != "")
                subject.Name = name;
            if (min != -1)
                subject.MinDegree = min;
            if (year != -1)
                subject.Year = year;
            if (term != -1)
                subject.Term = term;
            if (deptId != 0)
                subject.DeptId = deptId;
            await service.Update(subject);
            Index();
        }

        public async void Delete()
        {
            Console.WriteLine("Enter Subject Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Subject? subject = service.Show(id);
            if (subject == null)
            {
                Console.WriteLine("Couldn't find lecture!");
                return;
            }
            await service.Delete(subject);
            Index();
        }

        public void Show()
        {
            Console.WriteLine("Enter Subject Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Subject? subject = service.Show(id);
            if (subject == null)
            {
                Console.WriteLine("Couldn't find lecture!");
                return;
            }
            Console.WriteLine("|Id\t|Name\t\t|Min Degree\t|Term\t|Year\t|Department\t|Number of Lectures\t|\r\n-------------------------------------------------------------------------------------------------");
            Console.WriteLine(String.Format("|{0}\t|{1}\t|\t{2}\t|{3}\t|{4}\t|{5}\t|\t{6}\t\t|",
                subject.Id,
                subject.Name,
                subject.MinDegree,
                subject.Term,
                subject.Year,
                subject.Department.Name,
                subject.SubjectLectures.Count
                ));

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Get Lectures");
            Console.WriteLine("3. Go Back");

            int option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {
                case 1:
                    GetLectures(id);
                    break;
                default:
                    return;
            }
        }


        public void ShowByDepartment()
        {
            int deptId = ValidateDepartmentId("show");
            List<Subject> subjects =  service.ShowByDepartment(deptId);
            Console.WriteLine("**************************************************************************************************************\r\n|\tid\t|\tName\t|\tMinDegree\t|\tyear\t|\tDepartment\t|\tterm\t|\tNumber Lecture\t|");

            foreach (Subject item in subjects)
            {
                Console.WriteLine(String.Format("---------------------------------------------------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t|\t{2}\t\t|\t{3}\t|\t{4}\t\t|\t{5}\t|\t{6}\t  |",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Year,
                    item.Department.Name,
                    item.Term,
                    item.SubjectLectures.Count
                    ));
            }
        }

        public void ShowByYear()
        {
            short year = ValidateShort("show","Year");
            List<Subject> subjects = service.ShowByYear(year);
            Console.WriteLine("**************************************************************************************************************\r\n|\tid\t|\tName\t|\tMinDegree\t|\tyear\t|\tDepartment\t|\tterm\t|\tNumber Lecture\t|");

            foreach (Subject item in subjects)
            {
                Console.WriteLine(String.Format("---------------------------------------------------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t|\t{2}\t\t|\t{3}\t|\t{4}\t\t|\t{5}\t|\t{6}\t  |",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Year,
                    item.Department.Name,
                    item.Term,
                    item.SubjectLectures.Count
                    ));
            }
        }
        public void ShowByTerm()
        {
            short term = ValidateShort("show","Term");
            List<Subject> subjects = service.ShowByTerm(term);
            Console.WriteLine("**************************************************************************************************************\r\n|\tid\t|\tName\t|\tMinDegree\t|\tyear\t|\tDepartment\t|\tterm\t|\tNumber Lecture\t|");

            foreach (Subject item in subjects)
            {
                Console.WriteLine(String.Format("---------------------------------------------------------------------------------------------------------\r\n|\t{0}\t|\t{1}\t|\t{2}\t\t|\t{3}\t|\t{4}\t\t|\t{5}\t|\t{6}\t  |",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Year,
                    item.Department.Name,
                    item.Term,
                    item.SubjectLectures.Count
                    ));
            }
        }

        public void GetLectures(int id)
        {
            var lectures = service.ViewLectures(id);
            Console.WriteLine("|Id\t|Title\r\n-----------------------------------------------------------------");
            foreach (var item in lectures)
            {
                Console.WriteLine(String.Format("|{0}\t|{1}",item.Id,item.Title));
            }
            Console.WriteLine("-----------------------------------------------------------------");
        }
    }
}
