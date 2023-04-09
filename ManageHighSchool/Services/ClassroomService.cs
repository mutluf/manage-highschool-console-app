using ManageHighSchool.Abstract.Services;
using ManageHighSchool.IdProvider;
using ManageHighSchool.InMemoryDatas;
using ManageHighSchool.Models;
using ManageSchool.InMemoryDatas;

namespace ManageHighSchool.Services
{

    public class ClassroomService : IClassroomService
    {
        private List<Classroom> _classrooms = ClassroomList.GetClassroomListInstance();
        private List<Teacher> _teacherList = TeacherList.GetTeacherListInstance();
        public readonly IIdProvider _provider;

        public ClassroomService(IIdProvider idProvider)
        {
            _provider = idProvider;   
        }
        public bool Add(Classroom item)
        {           
            item.Id =_provider.GetId();
            _classrooms.Add(item);
            return true;
        }

        public void Delete(int id)
        {
            Classroom classroom = _classrooms.FirstOrDefault(x => x.Id == id);
            _classrooms.Remove(classroom);
        }

        public List<Classroom> GetAll()
        {
            string noTeacher = "Henüz eklenmemiştir.";
            Console.WriteLine("\nSınıflar listeleniyor...");
            foreach (var item in _classrooms)
            {
                Console.WriteLine($"--- Sınıf Numarası: {item.Id} Şubesi: {item.Name} Öğretmeni: {(item.Teacher == null ? noTeacher : item.Teacher.FullName)}");
            }
            if (_classrooms.Count == 0)
            {
                Console.WriteLine("Herhangi bir şube henüz oluşturulmamıştır.!");
            }
            return _classrooms;
        }

        public void Update(Classroom item)
        {
            Classroom classroom = _classrooms.FirstOrDefault(y => y.Id == item.Id);
            classroom.Teacher = item.Teacher;
            classroom.Name = item.Name;
        }

        public void GetWhere(Func<Classroom, bool> method)
        {
            List<Classroom> classrooms = new List<Classroom>();
            classrooms.Where(method).ToList();
            foreach (var item in classrooms)
            {
                Console.WriteLine($"{item.Id} - {item.Name}");
            }
        }

        public Classroom GetById(int id)
        {
            Classroom room = _classrooms.FirstOrDefault(x => x.Id == id);
            if (room == null)
            {
                Console.WriteLine("Böyle bir sınıf bulunmamaktadır.");
            }
            return room;
        }

    }
}
