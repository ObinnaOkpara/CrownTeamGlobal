using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrownGlobal.Data
{
    public class Testimony
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Video { get; set; }
        public int TestimonyGroupId { get; set; }

        public TestimonyGroup TestimonyGroup { get; set; }
    }
}
