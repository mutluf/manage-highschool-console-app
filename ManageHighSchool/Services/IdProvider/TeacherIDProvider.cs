using ManageHighSchool.Models;
using ManageSchool.InMemoryDatas;

namespace ManageHighSchool.IdProvider
{
    public class TeacherIDProvider:IIdProvider
    {
        List<Teacher> teacherList = TeacherList.GetTeacherListInstance();

        Random random = new Random();
        public int GetId()
        {
            int rnd;

            do
            {
                rnd = random.Next(100, 1000);
            }
            while (teacherList.Any(r => r.Id == rnd));

            return rnd;
        }
    }
}
