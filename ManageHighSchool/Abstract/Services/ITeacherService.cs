using ManageHighSchool.Models;

namespace ManageHighSchool.Abstract.Services
{
    public interface ITeacherService : IService<Teacher>
    {
        void PrintAllHomeworks();
        void GradeAHomework();

    }
}
