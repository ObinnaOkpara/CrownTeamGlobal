using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrownGlobal.ViewModels.TestimonyGroup
{
    public class TestimonyGroupListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "No of Testimonies")]
        public int TestimonyCount { get; set; }
    }
}
