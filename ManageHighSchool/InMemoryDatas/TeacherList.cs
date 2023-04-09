using ManageHighSchool.Models;

namespace ManageSchool.InMemoryDatas
{
    public class TeacherList
    {
        public static List<Teacher> teacherListsInstance;

        public static List<Teacher> GetTeacherListInstance()
        {
            if (teacherListsInstance == null)
            {
                teacherListsInstance = new List<Teacher>();
            }

            return teacherListsInstance;
        }
    }
}
