using ManageHighSchool.Models;

namespace ManageSchool.InMemoryDatas
{
    public class StudentList
    {
        public static List<Student> studentListsInstance;

        private StudentList()
        {

        }

        public static List<Student> GetStudentListInstance()
        {
            if (studentListsInstance == null)
            {
                studentListsInstance = new List<Student>();
            }
            return studentListsInstance;
        }
    }
}
