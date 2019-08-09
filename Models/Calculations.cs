﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPart.Models
{
    public class Calculations
    {
        public int Id { get; set; }

        [Required, StringLength(25)]
        public string Name { get; set; }

        [Required]
        public double Percent { get; set; }
    }
}
