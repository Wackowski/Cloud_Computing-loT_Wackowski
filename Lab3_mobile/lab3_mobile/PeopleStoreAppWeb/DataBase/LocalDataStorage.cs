using PeopleStoreAppDataCoontracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeopleStoreAppWeb.DataBase
{
    public class LocalDataStorage
    {
        private List<Person> people = new List<Person>();
        public IReadOnlyCollection<Person> People => people;
        public void AddPerson(Person p)
        {
            p.ID = people.OrderByDescending(by => by.ID).FirstOrDefault()?.ID + 1 ?? 1;
            people.Add(p);
        }
    }
}
