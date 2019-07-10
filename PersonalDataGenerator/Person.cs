using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDataGenerator
{
    class Person
    {
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public string Address { get; set; }
        public char Sex { get; set; }
        public string Country { get; set; }

        public void Print()
        {
            Console.WriteLine($"{Name}, {Sex}, {Birth}");
            Console.WriteLine($"{Country}, {Address}");
        }
    }
}
