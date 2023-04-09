using ManageHighSchool.Models;
using ManageSchool.InMemoryDatas;

namespace ManageHighSchool.IdProvider
{
    public class HomeworkIDProvider:IIdProvider
    {
        List<Homework> homeworkList = HomeworkList.GetHomeworkListInstance();

        Random random = new Random();
        public int GetId()
        {
            int rnd;

            do
            {
                rnd = random.Next(100, 1000);
            }
            while (homeworkList.Any(r => r.Id == rnd));

            return rnd;
        }
    }
}
