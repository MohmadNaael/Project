using advanceProgramingProject.Models;
using advanceProgramingProject.Services;

namespace advanceProgramingProject.Controllers
{
    internal class StudentController
    {
        IStudentService service = new StudentService();
        IDepartmentService departmentService = new DepartmentService();

        public async void Index()
        {
            List<Student> students = service.Index().ToList();
            Console.WriteLine("**************************************************************************************************************\r\n|Id\t|First Name\t|Last Name\t|Year\t|Username\t\t|Email\t\t|Phone\t|Register Date\t|Department\t|\r\n-------------------------------------------------------------------------------------------------------------------------------------------------");

            foreach (Student item in students)
            {
                Console.WriteLine(String.Format("|{0}\t|{1}\t|{2}\t|{3}\t|{4}\t\t|{5}\t|{6}\t|{7}\t|{8}\t|",
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


        public string ValidateString(string validate, string method)
        {
            string item;
            do
            {
                Console.Write(validate + ": ");
                item = Console.ReadLine();
                if(item == "" && method == "update")
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

        public string ValidatePhoneNumber(string method)
        {
            string phone;
            do
            {
                Console.Write("Phone: ");
                phone = Console.ReadLine();

                 if (method == "update" && phone == "")
                {                    
                        return phone;
                }

                if (phone == "")
                {
                    Console.WriteLine("Enter Phone Number Please!");
                }

                else
                {
                    for (int i = 0; i < phone.Length; i++)
                    {
                        if (!Char.IsDigit(phone[i]))
                        {
                            Console.WriteLine("Please Enter A Correct Phone Number");
                            phone = "";
                            break;
                        }
                    }
                }
            } while (phone == "");
            return phone;
        }

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

        public async void Create()
        {
            string firstName, lastName, userName, email, phone;
            short year;
            firstName = ValidateString("First Name","create");
            lastName = ValidateString("Last Name", "create");
            userName = ValidateString("User Name", "create");
            email = ValidateString("Email", "create");
            phone = ValidatePhoneNumber("create");

            do
            {
                Console.Write("Year:");
                year = Convert.ToInt16(Console.ReadLine());

                if(year < 0 || year > 6)
                {
                    Console.WriteLine("Please Enter A Correct Year");
                }
            } while (year < 0 || year > 6);

            int deptId = ValidateDepartmentId("create");


            Student std = new Student()
            {
                DepartmentId = deptId,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Username = userName,
                Phone = phone,
                Year = year,
                RegisterDate = DateTime.Now
            };
            await service.Create(std);
            Index();
        }
        public void Show()
        {
            Console.WriteLine("Enter Student Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Student? student = service.Show(id);
            if (student == null)
            {
                Console.WriteLine("Couldn't find Studnet!");
                return;
            }
            Console.WriteLine("*******************************************************************************************************************\r\n|Id\t|First Name\t|Last Name\t|Username\t|Email\t\t\t|Phone\t\t|Register Date\t|Department\t|\r\n---------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(
                String.Format("|{0}\t|{1}\t\t|{2}\t\t|{3}\t|{4}\t\t|{5}\t|{6}\t|{7}\t|\r\n---------------------------------------------------------------------------------------------------------------------------------",
                student.Id,
                student.FirstName, student.LastName,
                student.Username, student.Email,
                student.Phone,student.RegisterDate.ToShortDateString(),
                student.Department.Name
                ));
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Get Marks");
            Console.WriteLine("2. Get Subjects");
            Console.WriteLine("3. Calculate Avarage");
            Console.WriteLine("4. Go Back");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    GetStudentMarks(id);
                    break;
                case 2:
                    GetStudentSubjects(id);
                    break;
                case 3:
                    CalculateAvarage(id);
                    break;
                default:
                    return;
            }

        }

        public void GetStudentMarks(int id)
        {
            Console.WriteLine("|\tDate\t\t|\tExam\t\t|\tMark\t|\r\n-----------------------------------------------------------------");
            var Marks = service.showMarks(id);
            foreach (var item in Marks)
            {
                Console.WriteLine(String.Format(
                    "|\t{0}\t|\t{1}\t|\t{2}\t|",
                    item.Exam.Date.ToShortDateString(),
                    item.Exam.Subject.Name,
                    item.Mark
                    ));
            }
            Console.WriteLine("-----------------------------------------------------------------");
        }
        
        public void GetStudentSubjects(int id)
        {
            List<Subject> subjects = service.ViewSubjects(id);
            Console.WriteLine("|Id\t|Name\t\t|Min Degree\t|Term\t|Year\t|Department\t|Number of Lectures\t|\r\n-------------------------------------------------------------------------------------------------");
            foreach (var item in subjects)
            {
                Console.WriteLine(String.Format("|{0}\t|{1}\t|\t{2}\t|{3}\t|{4}\t|{5}\t|\t{6}\t\t|",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Term,
                    item.Year,
                    item.Department.Name,
                    item.SubjectLectures.Count
                ));
            }

            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. filter subjects by department");
            Console.WriteLine("2. filter subjects by year");
            Console.WriteLine("3. filter subjects by term");
            Console.WriteLine("4. Go Back");

            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    FilterSubjectsByDepartment(id);
                    break;
                case 2:
                    FilterSubjectsByYear(id);
                    break;
                case 3:
                    FilterSubjectsByTerm(id);
                    break;
                default:
                    break;
            }

        }

        public void FilterSubjectsByYear(int id)
        {
            int year = Convert.ToInt32(Console.ReadLine());

            List<Subject> subjects = service.ViewSubjectsByYear(id, year);
            Console.WriteLine("|Id\t|Name\t\t|Min Degree\t|Term\t|Year\t|Department\t|Number of Lectures\t|\r\n-------------------------------------------------------------------------------------------------");
            foreach (var item in subjects)
            {
                Console.WriteLine(String.Format("|{0}\t|{1}\t|\t{2}\t|{3}\t|{4}\t|{5}\t|\t{6}\t\t|",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Term,
                    item.Year,
                    item.Department.Name,
                    item.SubjectLectures.Count
                ));
            }

        }
        public void FilterSubjectsByTerm(int id)
        {
            short term = Convert.ToInt16(Console.ReadLine());

            List<Subject> subjects = service.ViewSubjectsByTerm(id, term);
            Console.WriteLine("|Id\t|Name\t\t|Min Degree\t|Term\t|Year\t|Department\t|Number of Lectures\t|\r\n-------------------------------------------------------------------------------------------------");
            foreach (var item in subjects)
            {
                Console.WriteLine(String.Format("|{0}\t|{1}\t|\t{2}\t|{3}\t|{4}\t|{5}\t|\t{6}\t\t|",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Term,
                    item.Year,
                    item.Department.Name,
                    item.SubjectLectures.Count
                ));
            }

        }

        public void FilterSubjectsByDepartment(int id)
        {
            int dept_id = ValidateDepartmentId("filter");

            List<Subject> subjects = service.ViewSubjectsByDepartment(id, dept_id);
            Console.WriteLine("|Id\t|Name\t\t|Min Degree\t|Term\t|Year\t|Department\t|Number of Lectures\t|\r\n-------------------------------------------------------------------------------------------------");
            foreach (var item in subjects)
            {
                Console.WriteLine(String.Format("|{0}\t|{1}\t|\t{2}\t|{3}\t|{4}\t|{5}\t|\t{6}\t\t|",
                    item.Id,
                    item.Name,
                    item.MinDegree,
                    item.Term,
                    item.Year,
                    item.Department.Name,
                    item.SubjectLectures.Count
                ));
            }

        }

        public async void CalculateAvarage(int id)
        {
            double avg =await service.CalculateAvarege(id);
            Console.WriteLine("This Student has the avarage of " +avg);
        }


        public async void Update()
        {
            Console.WriteLine("Enter Student Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Student? student = service.Show(id);
            if (student == null)
            {
                Console.WriteLine("Couldn't find Studnet!");
                return;
            }

            string firstName, lastName, userName, email, phone;
            short year;
            firstName = ValidateString("First Name","update");
            lastName = ValidateString("Last Name","update");
            userName = ValidateString("User Name", "update");
            email = ValidateString("Email", "update");
            phone = ValidatePhoneNumber("update");
            do
            {
                Console.Write("Year:");
                string temp =Console.ReadLine();

                if(temp == "")
                {
                    year = student.Year;
                }
                else
                {
                    year = Convert.ToInt16(temp);
                }

                if (year < 0 || year > 6)
                {
                    Console.WriteLine("Please Enter A Correct Year");
                }
            } while (year < 0 || year > 6);

            int deptId = ValidateDepartmentId("update");
            if (firstName != "")
                student.FirstName = firstName;
            if (lastName != "")
                student.LastName = lastName;
            if (userName != "")
                student.Username = userName;
            if (email != "")
                student.Email = email;
            if (phone != "")
                student.Phone = phone;
            if (deptId != 0)
                student.DepartmentId = deptId;
            student.Year = year;
            await service.Update(student);
            Index();
        }


        public async void Delete()
        {
            Console.WriteLine("Enter Student Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Student? student = service.Show(id);
            if (student == null)
            {
                Console.WriteLine("Couldn't find Studnet!");
                return;
            }
            await service.Delete(student);
            Index();
        }

    }
}
