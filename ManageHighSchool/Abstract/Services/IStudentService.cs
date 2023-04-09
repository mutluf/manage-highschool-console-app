using ManageHighSchool.Models;

namespace ManageHighSchool.Abstract.Services
{
    public interface IStudentService : IService<Student>
    {
        void SendHomeworkToTeacher(int teacherId, string Homework, int studentNumber);

        void PrintAllHomeworks(int studentNumber);

    }
}
