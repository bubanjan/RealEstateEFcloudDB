using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realestateBubanjaEF.Models
{
    public class Estate
    {
        [Key]
        public int ID { get; set; }
        public Type Type { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }

    }
}
