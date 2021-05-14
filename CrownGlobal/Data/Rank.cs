using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrownGlobal.Data
{
    public class Rank
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }

        public List<TeamMember> Teams { get; set; }
    }
}
