using System.Collections.Generic;

namespace Infnet.Aspnet.Assessment.Data
{
    public interface IData<T>
    {
        List<T> GetAll();

        T Get(int id);

        bool Update(int id, T entry);

        bool Delete(int id);

        bool Insert(T entry);
    }
}
