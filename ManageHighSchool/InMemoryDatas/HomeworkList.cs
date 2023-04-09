using ManageHighSchool.Models;


namespace ManageSchool.InMemoryDatas
{
    public class HomeworkList
    {
        public static List<Homework> homeworkListInstance;
        private HomeworkList()
        {

        }
        public static List<Homework> GetHomeworkListInstance()
        {
            //return homeworkListInstance ??= new List<Homework>();
            return homeworkListInstance = homeworkListInstance ?? new List<Homework>();
        }
    }
}
