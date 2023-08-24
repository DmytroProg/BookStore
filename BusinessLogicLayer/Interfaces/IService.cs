using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IService<T>
    {
        void Add(T value);
        void Remove(T value);
        void Update(T value);
        IEnumerable<T> GetAll();
        T FindOne(int id);
    }
}
