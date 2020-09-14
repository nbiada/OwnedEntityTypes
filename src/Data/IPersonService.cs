using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnedEntityTypes.Data
{
    public interface IPersonService
    {
        Task<List<Person>> Get();
        Task<Person> Get(int id);
        Task<Person> Add(Person person);
        Task<Person> Update(Person person);
        Task<Person> Delete(int id);
    }
}
