using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrownGlobal.Data
{
    public class TestimonyGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Testimony> Testimonies { get; set; }
    }
}
