using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LINQJoin
{
    public class Class2
    {
        public Guid Id { get; set; }
        public string GenderName { get; set; }
    }

    public class Genders
    {
        public static readonly List<Class2> genderList = new List<Class2>
        {
            new Class2()
            {
                Id = Guid.Parse("9df54680-4f76-4c26-a55a-92cf012fa71b"),
                GenderName = "mees"
            },
            new Class2()
            {
                Id = Guid.Parse("fe7f4110-d1b9-4ebc-8219-b48c852be3e6"),
                GenderName = "naine"
            }
        };
    }
}
