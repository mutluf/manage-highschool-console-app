using ManageHighSchool.Models;


namespace ManageSchool.InMemoryDatas
{
    public class HomeworkList
    {
        public static List<Homework> homeworkListInstance;

        public static List<Homework> GetHomeworkListInstance()
        {
            return homeworkListInstance = homeworkListInstance ?? new List<Homework>();
        }
    }
}
