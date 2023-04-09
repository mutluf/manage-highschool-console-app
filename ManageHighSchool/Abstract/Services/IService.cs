namespace ManageHighSchool.Abstract.Services
{
    public interface IService<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        bool Add(T item);
        void Update(T item);
        void Delete(int id);
        void GetWhere(Func<T, bool> method);
    }
}
