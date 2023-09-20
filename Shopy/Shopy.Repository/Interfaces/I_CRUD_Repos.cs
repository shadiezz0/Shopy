using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopy.Repository.Interfaces
{
    public interface I_CRUD_Repos<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int Id);
        Task UpdateItem(T NewItem);
        Task AddNewItem(T NewItem);
        Task DeleteItem(int Id);
    }
}
