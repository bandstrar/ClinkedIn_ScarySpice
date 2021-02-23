using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class Clinker
    {
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public List<string> Interests { get; set; }
        public List<string> Services { get; set; }
        public List<Clinker> Friends { get; set; }
        public List<Clinker> Enemies { get; set; }
    }
}
