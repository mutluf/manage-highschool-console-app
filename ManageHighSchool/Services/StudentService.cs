using ManageHighSchool.Abstract.Services;
using ManageHighSchool.IdProvider;
using ManageHighSchool.Models;
using ManageSchool.InMemoryDatas;

namespace ManageHighSchool.Services
{
    public class StudentService : IStudentService
    {

        private List<Student> studentList = StudentList.GetStudentListInstance();
        private List<Homework> homeworkList = HomeworkList.GetHomeworkListInstance();
        private readonly ITeacherService _teacherService;

        public readonly IIdProvider _provider; 
        public StudentService(ITeacherService teacherService,IIdProvider idProvider)
        {
            _teacherService = teacherService;
            _provider = idProvider;
        }
        public bool Add(Student item)
        {
            item.StudentNumber = _provider.GetId();
           

            studentList.Add(item);
            if (item.Classroom != null)
            {

                Console.WriteLine($"Öğrenci numarası: {item.StudentNumber} - {item.FullName}, {item.Classroom?.Name} sınıfına başarıyla eklendi.");
            }
            else
            {
                Console.WriteLine("Kayıt işlemi başarısız. Böyle bir sınıf bulunmamaktadır.");
            }
            return true;
        }

        public void Delete(int studentNumber)
        {

            Student student = studentList.FirstOrDefault(s => s.StudentNumber == studentNumber);
            studentList.Remove(student);
        }

        public void Update(Student item)
        {
            Student student = studentList.FirstOrDefault(n => n.StudentNumber == item.StudentNumber);
            student.FullName = item.FullName;
            student.Classroom = item.Classroom;
        }

        public List<Student> GetAll()
        {
            Console.WriteLine("\nÖğrenciler Listesi");
            if (studentList.Count != 0)
            {
                foreach (var item in studentList)
                {
                    Console.WriteLine($"--- {item.StudentNumber} {item.FullName} - {item.Classroom?.Name}");
                }
            }
            else
            {
                Console.WriteLine("Listelenecek öğrenci bulunmamaktadır.");
            }

            return studentList;
        }

        public void GetWhere(Func<Student, bool> method)
        {
            List<Student> students = studentList.Where(method).ToList();
            foreach (var item in students)
            {
                Console.WriteLine($"Öğrenci numarası: {item.StudentNumber} {item.FullName} - {item.Classroom?.Name}");
            }
        }

        public Student GetById(int id)
        {
            Student student = studentList.FirstOrDefault(n => n.StudentNumber == id);
            return student;
        }


        public void SendHomeworkToTeacher(int teacherId, string Homework, int studentNumber)
        {
            Student student = studentList.FirstOrDefault(s => s.StudentNumber == studentNumber);
            Teacher teacher = _teacherService.GetById(teacherId);
            Homework homework = new()
            {
                Teacher=teacher,
                HomeWork = Homework,
                Student=student
            };
            homeworkList.Add(homework);
        }
        public void PrintAllHomeworks(int studentNumber)
        {
            string noGrade = "Henüz notu girilmemiştir.";
            var myHomeworks=homeworkList.Where(a => a.Student.StudentNumber == studentNumber);
            foreach (var homework in myHomeworks)
            {
                Console.WriteLine($"{homework.Student.StudentNumber} - {homework.Student.FullName} öğrencinin ödevi:  {homework.HomeWork} öğrencinin notu: {(homework.Grade == null ? noGrade : null)}");

            }
        }
    }
}
