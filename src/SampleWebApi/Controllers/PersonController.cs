using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SampleWebApi.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleWebApi.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private SampleDbContext _context;

        public PersonController(SampleDbContext context)
        {
            _context = context; 
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            var people = _context.People
                .Include(c => c.Addresses);

            return people;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Person Get(int id)
        {
            var person = _context.People
                .Include(c => c.Addresses)
                .FirstOrDefault(c => c.Id == id);
            

            return person;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Person value)
        {

            _context.People.Add(value);
            _context.SaveChanges();
        }
        
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Person value)
        {
            // return;
            var existingPerson = _context.People.Include(c => c.Addresses).FirstOrDefault(t => t.Id == id);

            if (existingPerson != null)
            {
                //existingPerson.Addresses = value.Addresses;
                existingPerson.FirstName = value.FirstName;
                existingPerson.LastName = value.LastName;
                //_context.SaveChanges();
            }

            //foreach (var add in value.Addresses)
            //{
            //    existingPerson.Addresses.Add(add);
            //}
            // _context.SaveChanges();

            var dbAddList = existingPerson.Addresses.Select(a => a.Id).ToList<int>();
            var inpuAddList = value.Addresses.Select(a => a.Id).ToList<int>();
            var toRemoveAddress = dbAddList.Except(inpuAddList).ToList<int>();
            //insert or update
            foreach (var add in value.Addresses)
            {
                if (add.Id == 0)
                {
                    var address = new Address()
                    {
                        Street = add.Street,
                        City = add.City
                    };
                    existingPerson.Addresses.Add(address);
                }
                else
                {
                    var existAddress = existingPerson.Addresses.FirstOrDefault(t => t.Id == add.Id);
                    existAddress.City = add.City;
                    existAddress.Street = add.Street;
                }
            }
            //delete previous which is not supplied
            foreach (var removeAdd in toRemoveAddress)
            {
                var exitAdd = _context.Address.FirstOrDefault(a => a.Id == removeAdd);
                _context.Address.Remove(exitAdd);
            }
            _context.SaveChanges();

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var person = _context.People.Include(c => c.Addresses).FirstOrDefault(t => t.Id == id);
            if (person != null)
            {
                _context.Remove(person);
                _context.SaveChanges();
            }
        }
    }
}
