using ManageHighSchool.Abstract.Services;
using ManageHighSchool.IdProvider;
using ManageHighSchool.InMemoryDatas;
using ManageHighSchool.Models;
using ManageSchool.InMemoryDatas;


namespace ManageHighSchool.Services
{
    public class TeacherService : ITeacherService
    {
        private List<Teacher> teacherList = TeacherList.GetTeacherListInstance();
        private List<Homework> homeworkList = HomeworkList.GetHomeworkListInstance();
        private List<Classroom> classroomList = ClassroomList.GetClassroomListInstance();
        private readonly IIdProvider _teacherIdProvider;
        public TeacherService(IIdProvider idProvider)
        {
            _teacherIdProvider = idProvider;
        }

        public bool Add(Teacher item)
        {
            item.Id=_teacherIdProvider.GetId();
            if (item.Classroom != null)
            {
                Classroom classroom = classroomList.FirstOrDefault(c => c.Id == item.Classroom.Id);

                if (classroom.Teacher != null)
                {
                    Console.WriteLine("Kayıt oluşturulamadı. Bu sınıfın sınıf öğretmeni bulunmaktadır.");
                }

                else
                {
                    item.Classroom.Teacher = item;
                    teacherList.Add(item);
                    Console.WriteLine("Öğretmen kaydı başarıyla oluşturuldu.");
                    Console.WriteLine($"{item.FullName} isimli öğretmen {item.Classroom.Name} sınıfına sınıf öğretmeni olarak eklendi");
                }
            }

            else
            {
                teacherList.Add(item);
                Console.WriteLine("Öğretmen kaydı başarıyla oluşturuldu.");
            }
            return true;
        }

        public void Delete(int id)
        {
            Teacher teacher = GetById(id);
            teacherList.Remove(teacher);
        }

        public void Update(Teacher teacher1)
        {
            Teacher teacher = teacherList.FirstOrDefault(n => n.Id == teacher1.Id);
            teacher.FullName = teacher1.FullName;
            teacher.Branch = teacher1.Branch;
        }

        public List<Teacher> GetAll()
        {
            Console.WriteLine("Öğretmenler");
            if (teacherList.Count() == 0)
            {
                Console.WriteLine("Listelenecek öğretmen bulunmamaktadır.");
            }

            foreach (var item in teacherList)
            {
                Console.WriteLine($"--- Öğretmen numarası: {item.Id} {item.FullName} - {item.Branch} - {item.Id}");
            }
            return teacherList;
        }

        public void GetWhere(Func<Teacher, bool> method)
        {
            IList<Teacher> teachers = teacherList.Where(method).ToList();
            foreach (var item in teachers)
            {
                Console.WriteLine($"{item.FullName} - {item.Classroom.Name}? öğretmeni");
            }
        }

        public Teacher GetById(int id)
        {
            Teacher teacher = teacherList.FirstOrDefault(n => n.Id == id);
            //Console.WriteLine($"{teacher.FullName} - {teacher.ClassRoom.Name}? öğretmeni");
            return teacher;
        }

        public void PrintAllHomeworks()
        {
            string noGrade = "Henüz notu girilmemiştir.";
            foreach (var homework in homeworkList)
            {
                Console.WriteLine($"{homework.Student.StudentNumber} - {homework.Student.FullName} öğrencinin ödevi:  {homework.HomeWork} öğrencinin notu: {(homework.Grade == null ? noGrade : homework.Grade)}");

            }

        }


        public void GradeAHomework()
        {
            string noGrade = "Henüz notu girilmemiştir.";

            foreach (var homework in homeworkList)
            {
                Console.WriteLine($"{homework.Student.StudentNumber} - {homework.Student.FullName} öğrencinin ödevi:  {homework.HomeWork} öğrencinin notu: {(homework.Grade == null ? noGrade : null)}");

                Console.WriteLine("Öğrencinin notunu giriniz.");
                int grade = int.Parse(Console.ReadLine());

                homework.Grade = grade;

            }
            PrintAllHomeworks();
            
        }


    }
}
