using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPart.Models
{
    public class Part
    {
        public int Id { get; set; }

        [Required, StringLength(25)]
        public string Name { get; set; }

        public DateTime Now { get; set; } = DateTime.Now;
        [Required, StringLength(25)]
        public string UsedFor { get; set; }

        public string Flagged { get; set; } = "N";

        public string Color { get; set; } = "White";

        public string TextColor { get; set; } = "Black";
    }
}
