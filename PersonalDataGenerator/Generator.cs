using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PersonalDataGenerator
{
    class Generator
    {
        private Parser _parser;
        private Random _rng;

        public List<Person> People { get; }

        public Generator(Parser parser)
        {
            _parser = parser;
            _rng = new Random();
            People = new List<Person>(100);
        }

        public void CreatePeople(int count)
        {
            for (int i = 0; i < count; i++)
            {
                People.Add(CreatePerson());
            }
        }

        private Person CreatePerson()
        {
            char[] sexes = { 'M', 'F' };

            char sex = sexes[_rng.Next(2)];
            string firstName = _parser.FirstNames[sex][_rng.Next(_parser.FirstNames[sex].Count)];
            string lastName = _parser.LastNames[_rng.Next(_parser.LastNames.Count)];
            if (sex == 'F') lastName += 'а';
            string city = _parser.Cities[_rng.Next(_parser.Cities.Count)];
            string street = _parser.Streets[_rng.Next(_parser.Streets.Count)];

            int age = 20 + _rng.Next(71);
            int year = DateTime.Now.Year - age;
            int month = 1 + _rng.Next(12);
            int day = 1 + _rng.Next(28);  // Для простоты
            DateTime birth = new DateTime(year, month, day);

            return new Person()
                {
                    Name = lastName + " " + firstName,
                    Sex = sex,
                    Birth = birth,
                    Country = "Россия",
                    Address = city + ", " + street
                };
        }

        public void WritePeopleToXml(string path)
        {
            var doc = new XDocument();
            var root = new XElement("people");

            int id = 0;
            foreach (var p in People)
            {
                id++;
                XElement person =
                    new XElement("person", new XAttribute("id", id),
                        new XElement("name", p.Name),
                        new XElement("sex", p.Sex),
                        new XElement("birth", p.Birth.ToShortDateString()),
                        new XElement("country", p.Country),
                        new XElement("address", p.Address)
                    );
                root.Add(person);
            }
            doc.Add(root);
            doc.Save(path);
        }
    }
}