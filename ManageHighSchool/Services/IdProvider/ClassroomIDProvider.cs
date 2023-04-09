using ManageHighSchool.InMemoryDatas;
using ManageHighSchool.Models;

namespace ManageHighSchool.IdProvider
{
    public  class ClassroomIDProvider : IIdProvider
    {
        List<Classroom> clasroomList = ClassroomList.GetClassroomListInstance();

        Random random = new Random();
        public int GetId()
        {
            int rnd;

            do
            {
                rnd = random.Next(100, 1000);               
            }
            while (clasroomList.Any(r => r.Id == rnd));

            return rnd;
        }
    }
}
