using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrownGlobal.ViewModels.TestimonyGroup
{
    public class TestimonyGroupPostVM
    {
        [Required]
        public string Name { get; set; }
    }
}
