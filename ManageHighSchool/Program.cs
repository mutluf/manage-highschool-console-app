using ManageHighSchool.Abstract.Services;
using ManageHighSchool.IdProvider;
using ManageHighSchool.InMemoryDatas;
using ManageHighSchool.Models;
using ManageHighSchool.Services;
using ManageSchool.InMemoryDatas;

List<Teacher> teacherList = TeacherList.GetTeacherListInstance();
List<Student> studentList = StudentList.GetStudentListInstance();
List<Homework> homeworkList = HomeworkList.GetHomeworkListInstance();
List<Classroom> classroomList = ClassroomList.GetClassroomListInstance();

ITeacherService teacherService = new TeacherService(new TeacherIDProvider());
IStudentService studentService = new StudentService(new TeacherService(new TeacherIDProvider()),new StudentIDProvider());
IClassroomService roomService = new ClassroomService(new ClassroomIDProvider());
Admin admin = new Admin() { FullName = "admin", Id = 1122 };

teacherService.Add(new() { Branch = "Matematik", FullName = "Nur Banu" });
roomService.Add(new Classroom() { Name = "A", Teacher = teacherList[0] });
studentService.Add(new Student() { FullName = "Fatma Mutlu", Classroom = classroomList[0] });

Console.WriteLine("öğrenci" + studentList[0].StudentNumber);
Console.WriteLine("sınıf" + classroomList[0].Id);
Console.WriteLine("öğretmen" + teacherList[0].Id);
void Back(string operation)
{
    Console.WriteLine("\nAna menüye dönmek için 1 \nBir önceki sayfaya dönmek için 2 tuşlayınız.\nÇıkış yapmak için 3 tuşlayınız.");
    string choice = Console.ReadLine();
    try
    {

        if (choice == "2")
        {
            switch (operation)
            {
                case "student":
                    AdminStudentOperations();
                    break;

                case "teacher":
                    AdminTeacherOperations();
                    break;

                case "classroom":
                    AdminClassroomOperation();
                    break;

                case "studentOperation":
                    StudentOperations();
                    break;
                case "teacherOperation":
                    TeacherOperations();
                    break;

                default:
                    MainMenu();
                    break;
            }
        }

        else if (choice == "1")
        {
            Console.Clear();
            MainMenu();
        }

        else if (choice == "3")
        {
            Console.Clear();
            Console.WriteLine("öğrenci numarası " + studentList[0].StudentNumber);
            Console.WriteLine("sınıf numarası " + classroomList[0].Id);
            Console.WriteLine("öğretmen numarası " + teacherList[0].Id + "\n");
            Login();
        }
    }
    catch (Exception)
    {

        Console.WriteLine("Giriş başarısız.");
        Login();
    }
}

void Login()
{
    Console.WriteLine("Öğrenciyseniz   - 1");
    Console.WriteLine("Yöneticiyseniz  - 2");
    Console.WriteLine("Öğretmenseniz   - 3");
    string who = Console.ReadLine();
    switch (who)
    {
        case "1":
            Console.WriteLine("Öğrenci Numaranızı Giriniz");
            int studentNumber = int.Parse(Console.ReadLine());
            var student = studentService.GetById(studentNumber);
            if (student == null)
            {
                Console.WriteLine("Böyle bir öğrenci yoktur.");
                Login();
            }
            else
            {
                Console.WriteLine($"Sisteme hoş geldiniz {student.FullName} ");
                StudentOperations();
            }
            break;
        case "2":
            Console.WriteLine("Yönetici Numaranızı Giriniz");
            int adminNo = Convert.ToInt32(Console.ReadLine());
            if (adminNo == 1122)
            {
                Console.WriteLine("Yönetici olarak giriş yaptınız.");
                MainMenu();
            }

            break;
        case "3":
            Console.WriteLine("Öğretmen Numaranızı Giriniz");
            int teacherNo = Convert.ToInt32(Console.ReadLine());
            var teacher = teacherService.GetById(teacherNo);
            if (teacher == null)
            {

                Console.WriteLine("Böyle bir öğretmen bulunamadı.");
                Login();
            }
            else
            {
                Console.WriteLine($"Sisteme hoş geldiniz {teacher.FullName}");
                TeacherOperations();
            }
            break;
        default:
            break;
    }
}



void MainMenu()
{
    Console.WriteLine("*******************");
    Console.WriteLine("Okul yönetim sistemine hoş geldiniz!");
    Console.WriteLine("*******************");
    Console.WriteLine("Öğrenci işlemleri için - 1");
    Console.WriteLine("Öğretmen işlemleri için - 2");
    Console.WriteLine("Sınıf işlemleri için - 3");

    string input = Console.ReadLine();

    switch (input)
    {
        case "1":


            Console.WriteLine("Öğrenci işlemleri listeleniyor...");
            Task.Delay(1000).Wait();
            AdminStudentOperations();
            break;
        case "2":
            Console.WriteLine("Öğretmen işlemleri listeleniyor...");
            Task.Delay(1000).Wait();
            AdminTeacherOperations();
            break;
        case "3":
            Console.WriteLine("Sınıf işlemleri listeleniyor...");
            Task.Delay(1000).Wait();
            AdminClassroomOperation();
            break;
        default:
            Console.WriteLine("Lütfen ekrandaki işlemlerden birini seçiniz.");
            MainMenu();
            break;
    }
}
Login();





void AdminStudentOperations()
{
    string operationStudent = "student";

    Console.WriteLine("Öğrenci Eklemek İçin - 1\n" +
        "Öğrenci Güncellemek İçin - 2\n" +
        "Bütün Öğrencileri Listelemek İçin - 3\n" +
        "Öğrenci Silmek İçin - 4\n" +
        "Öğrenci Aramak İçin - 5"

        );
    string inputStudentChoice = Console.ReadLine();

    try
    {
        switch (inputStudentChoice)
        {
            case "1":
                Console.WriteLine("Öğrenci Adı Soyadı Giriniz: ");
                string studentName = Console.ReadLine();

                List<Classroom> classRooms = roomService.GetAll();

                if (classRooms.Count == 0)
                {
                    Console.WriteLine("Herhangi bir sınıf kaydı yok, önce bir sınıf oluşturmalısınız");
                }
                else
                {
                    Console.WriteLine("Hangi sınıfa eklemek istiyorsunuz? Numarasını Giriniz.");
                    string classRoom = Console.ReadLine();

                    studentService.Add(new() { FullName = studentName, Classroom = roomService.GetById(int.Parse(classRoom)) });
                }

                Back(operationStudent);

                break;
            case "2":
                if (studentService.GetAll().Count == 0)
                {
                    Console.WriteLine("Kayıtlı öğrenci bulunmamaktadır.!");


                }
                else
                {
                    Student updatedStudent = new();
                    Console.WriteLine("Güncellemek istediğiniz öğrencinin numarasını giriniz.!");

                    updatedStudent.StudentNumber = int.Parse(Console.ReadLine());
                    Console.WriteLine("Öğrencinin güncellenmiş adını giriniz...");

                    updatedStudent.FullName = Console.ReadLine();
                    Console.WriteLine("Öğrencinin güncellenmiş sınıfının numarasını listeden seçiniz ve giriniz...");

                    roomService.GetAll();

                    updatedStudent.Classroom = roomService.GetById(int.Parse(Console.ReadLine()));
                    studentService.Update(updatedStudent);

                }

                Back(operationStudent);
                break;

            case "3":
                Console.WriteLine("Öğrenciler listeleniyor...");
                Task.Delay(1000).Wait();

                studentService.GetAll();

                Back(operationStudent);

                break;

            case "4":
                studentService.GetAll();
                Console.WriteLine("Silmek istediğiniz öğrencinin numarasını girin.");
                int studentNum = int.Parse(Console.ReadLine());

                studentService.Delete(studentNum);

                Back(operationStudent);
                break;

            case "5":
                Console.WriteLine("Aramak istediğiniz öğrencinin adını giriniz.");
                string studentNamee = Console.ReadLine();

                studentService.GetWhere(s => s.FullName.Contains(studentNamee));
                break;

            default:
                MainMenu();
                break;
        }
    }
    catch (Exception)
    {

        Console.WriteLine("İşlem gerçekleştirilememiştir.");
        Back(operationStudent);
    }

}
void AdminTeacherOperations()
{
    string operationTeacher = "teacher";

    Console.WriteLine("Öğretmen Eklemek İçin - 1\n" +
        "Öğretmen Güncellemek İçin - 2\n" +
        "Bütün Öğretmenleri Listelemek İçin - 3\n" +
        "Öğretmen Silmek İçin - 4\n" +
        "Öğretmen Aramak için - 5"
        );
    string inputTeacherChoice = Console.ReadLine();

    try
    {
        switch (inputTeacherChoice)
        {
            case "1":

                Console.WriteLine("Eklemek istediğiniz öğretmenin tam adını giriniz.");
                string teacherName = Console.ReadLine();

                Console.WriteLine("Öğretmenin branşını giriniz.");
                string branch = Console.ReadLine();

                Console.WriteLine("İlgili öğretmeni sınıf öğretmeni yapmak istiyor musunuz? ( evet/hayır )");
                string answer = Console.ReadLine();

                Teacher teacher1 = new Teacher() { FullName = teacherName, Branch = branch };


                if (answer == "evet")
                {
                    roomService.GetAll();
                    Console.WriteLine("Eklemek istediğiniz sınıfın numarasını girin.");
                    int number1 = int.Parse(Console.ReadLine());

                    teacher1.Classroom = roomService.GetById(number1);
                }

                teacherService.Add(teacher1);

                Back(operationTeacher);

                break;

            case "2":

                if (teacherService.GetAll().Count == 0)
                {
                    Console.WriteLine("Kayıtlı öğretmen bulunmamaktadır.!");

                    Back(operationTeacher);
                }

                Console.WriteLine("Güncellemek istediğiniz öğretmenin id'sini girin.");
                int number = int.Parse(Console.ReadLine());

                Teacher teacherr = teacherList.First(t => t.Id == number);

                Teacher updatedTeacher = new Teacher();
                Console.WriteLine("Öğretmen için tam ad girin.");

                updatedTeacher.FullName = Console.ReadLine();

                Console.WriteLine("Öğretmen için branş girin.");
                updatedTeacher.Branch = Console.ReadLine();


                if (teacherr.Classroom == null)
                {
                    Console.WriteLine("İlgili öğretmeni sınıf öğretmeni yapmak istiyor musunuz? ( evet/hayır )");
                    string answer2 = Console.ReadLine();

                    if (answer2 == "evet")
                    {
                        Console.WriteLine("Eklemek istediğiniz sınıfın numarasını girin.");
                        int number2 = int.Parse(Console.ReadLine());

                        updatedTeacher.Classroom = roomService.GetById(number2);
                    }
                }

                teacherService.Update(updatedTeacher);

                Back(operationTeacher);

                break;

            case "3":

                Console.WriteLine("Bütün öğretmenler listeleniyor...");
                Task.Delay(1000).Wait();

                teacherService.GetAll();

                Back(operationTeacher);

                break;

            case "4":

                Console.WriteLine("Silmek istediğiniz öğretmenin id'sini girin.");
                int teacherId = int.Parse(Console.ReadLine());

                teacherService.Delete(teacherId);

                Back(operationTeacher);

                break;

            case "5":
                Console.WriteLine("Aramak istediğiniz öğretmenin adını giriniz.");
                string teacherNamee = Console.ReadLine();

                teacherService.GetWhere(s => s.FullName.Contains(teacherNamee));
                break;

            default:
                MainMenu();
                break;
        }
    }
    catch (Exception)
    {

        Console.WriteLine("İşlem gerçekleştirilememiştir.");
        Back(operationTeacher);
    }

}

void AdminClassroomOperation()
{
    string operationClassroom = "classroom";

    Console.WriteLine("Sınıf Eklemek İçin - 1\n" +
        "Sınıf Güncellemek İçin - 2\n" +
        "Bütün Sınıfları Listelemek İçin - 3\n" +
        "Sınıf Silmek İçin - 4\n" + "");

    string inputClassroomChoice = Console.ReadLine();

    try
    {
        switch (inputClassroomChoice)
        {
            case "1":

                Console.WriteLine("Sınıf adı giriniz. (Karakter)");
                string classroomName = Console.ReadLine();

                var teachers = teacherService.GetAll();

                Console.WriteLine("Sınıf öğretmeni olarak atamak istediğiniz öğretmenin numarasını girin. Girmek istemiyorsanız 0 tuşlayınız");
                int teacherNumber = Convert.ToInt32(Console.ReadLine());
                if (teacherNumber == 0)
                {
                    roomService.Add(new() { Name = classroomName });
                }
                else
                {
                    Teacher teacher = teacherList.FirstOrDefault(t => t.Id == teacherNumber);

                    roomService.Add(new() { Name = classroomName, Teacher = teacher });
                }







                Back(operationClassroom);
                break;

            case "2":

                Console.WriteLine("Güncellemek istediğiniz sınıfın numarasını girin.");
                int classroomNumber2 = int.Parse(Console.ReadLine());

                Console.WriteLine("Güncellemek istediğiniz sınıfın adını girin.");
                string classroomName2 = Console.ReadLine();

                Console.WriteLine("Güncellemek istediğiniz sınıfın öğretmeni için öğretmen numarası girin");
                int teacherNum = int.Parse(Console.ReadLine());

                Teacher teach = teacherList.FirstOrDefault(t => t.Id == teacherNum);

                Classroom classroom = new() { Id = classroomNumber2, Name = classroomName2, Teacher = teach };

                Back(operationClassroom);

                break;

            case "3":

                Console.WriteLine("Sınıflar listeleniyor...");
                roomService.GetAll();

                Back(operationClassroom);
                break;

            case "4":

                Console.WriteLine("Silmek istediğiniz sınıfın numarasını girin.");
                int classroomNum = int.Parse(Console.ReadLine());

                Classroom classRoom = roomService.GetById(classroomNum);
                roomService.Delete(classroomNum);

                Back(operationClassroom);

                break;

            default:
                MainMenu();
                break;
        }
    }
    catch (Exception)
    {
        Console.WriteLine("İşlem gerçekleştirilememiştir.");
        Back(operationClassroom);
    }


}

void StudentOperations()
{
    string operation = "studentOperation";
    Console.WriteLine("Ödev göndermek için - 1 \nÖdevlerini görüntülemek için - 2");
    string homeworkChoice = Console.ReadLine();

    switch (homeworkChoice)
    {
        case "1":
            teacherService.GetAll();
            Console.WriteLine("Ödevi göndermek istediğiniz öğretmenin numarasını giriniz.");
            int teacherId = int.Parse(Console.ReadLine());
            Console.WriteLine("Öğrenci numaranızı giriniz.");
            int studentNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Ödevinizi yazınız.");
            string homework = Console.ReadLine();
            studentService.SendHomeworkToTeacher(teacherId, homework, studentNumber);
            Back(operation);
            break;
        case "2":
            Console.WriteLine("Öğrenci Numaranızı Giriniz.");
            studentNumber = int.Parse(Console.ReadLine());
            studentService.PrintAllHomeworks(studentNumber);
            Back(operation);
            break;
        default:
            break;
    }
}
void TeacherOperations()
{
    string operation = "teacherOperation";
    Console.WriteLine("Ödevleri listelemek için - 1\nNot girmek için - 2");
    string teacherChoice = Console.ReadLine();

    switch (teacherChoice)
    {
        case "1":
            teacherService.PrintAllHomeworks();
            Back(operation);
            break;

        case "2":
            teacherService.GradeAHomework();
            Back(operation);
            break;
        default:
            break;
    }

}