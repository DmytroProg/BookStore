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
        void Update(T value, int id);
        void Remove(int id);
        List<T> GetAll();
    }
}
