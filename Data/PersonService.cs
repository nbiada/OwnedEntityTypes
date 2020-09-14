using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnedEntityTypes.Data
{
    public class PersonService : IPersonService
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Person> Add(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> Delete(int id)
        {
            var person = await _context.People.FindAsync(id);
            _context.People.Remove(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<List<Person>> Get()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person> Get(int id)
        {
            var person = await _context.People.FindAsync(id);
            return person;
        }

        public async Task<Person> Update(Person person)
        {
            if (_context.Entry(person).IsKeySet)
            {
                _context.Entry(person).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(person).State = EntityState.Added;
            }
            await _context.SaveChangesAsync();
            return person;
        }
    }
}
