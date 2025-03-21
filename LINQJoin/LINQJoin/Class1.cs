using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQJoin
{
    public class Class1
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Guid GenderId { get; set; }
    }

    public class Humans
    {
        public static readonly List<Class1> peoples = new List<Class1>
        {
            new Class1()
            {
                Id = 1,
                Name = "Joonas",
                GenderId = Guid.Parse("9df54680-4f76-4c26-a55a-92cf012fa71b")
            },
            new Class1()
            {
                Id = 2,
                Name = "Moona",
                GenderId = Guid.Parse("fe7f4110-d1b9-4ebc-8219-b48c852be3e6")
            },
            new Class1()
            {
                Id = 3,
                Name = "Ron",
                GenderId = Guid.Parse("9df54680-4f76-4c26-a55a-92cf012fa71b")
            },
            new Class1()
            {
                Id = 4,
                Name = "Mary",
                GenderId = Guid.Parse("fe7f4110-d1b9-4ebc-8219-b48c852be3e6")
            },
            new Class1()
            {
                Id = 5,
                Name = "Mari",
                GenderId = Guid.Parse("fe7f4110-d1b9-4ebc-8219-b48c852be3e6")
            },
            
        };
    }
}
