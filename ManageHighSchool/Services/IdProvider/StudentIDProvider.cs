using ManageHighSchool.Models;
using ManageSchool.InMemoryDatas;

namespace ManageHighSchool.IdProvider
{
    public class StudentIDProvider:IIdProvider
    {
        List<Student> studentList = StudentList.GetStudentListInstance();

        Random random = new Random();
        public int GetId()
        {
            int rnd;

            do
            {
                rnd = random.Next(100, 1000);
            }
            while (studentList.Any(r => r.StudentNumber == rnd));

            return rnd;
        }
    }
}
