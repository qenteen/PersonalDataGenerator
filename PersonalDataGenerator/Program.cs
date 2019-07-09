using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var gen = new Generator(new Parser());
            gen.CreatePeople(2000000);  // ~516mb
            gen.WritePeopleToXml(@"G:\People.xml");
        }
    }
}
