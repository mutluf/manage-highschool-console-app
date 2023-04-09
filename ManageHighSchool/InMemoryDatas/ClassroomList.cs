using ManageHighSchool.Models;

namespace ManageHighSchool.InMemoryDatas
{
    public class ClassroomList
    {
        public static List<Classroom> classroomListsInstance;

        private ClassroomList()
        {

        }

        public static List<Classroom> GetClassroomListInstance()
        {
            if (classroomListsInstance == null)
            {
                classroomListsInstance = new List<Classroom>();
            }
            return classroomListsInstance;
        }
    }
}
