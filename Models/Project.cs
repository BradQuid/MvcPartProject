using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPart.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required, StringLength(25)]
        public string Name { get; set; }
    }
}
