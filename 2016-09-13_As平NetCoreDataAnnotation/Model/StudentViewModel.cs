using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2016_09_13_As平NetCoreDataAnnotation.Model
{
    public class StudentViewModel
    {
        [Required]
        [Range(2, 3)]
        public string Name { get; set; }
    }
}
