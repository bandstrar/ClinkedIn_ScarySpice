using ClinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.DataAccess
{
    public class ClinkerRepository
    {
        static List<Clinker> _loaves = new List<Clinker>
        {
            new Clinker {SerialNumber = 1, Name = "Joe Exotic", Interests = new List<string> {"Exotic animals", "Cowboy hats" }, Services = new List<string> {"Relationship advice", "Hiring hitmen" } },
            new Clinker {SerialNumber = 2, Name = "Al Capone", Interests = new List<string> {"Tax evasion", "Al Pacino movies" }, Services = new List<string> {"Hiring bodyguards", "Smuggling goods" } },
            new Clinker {SerialNumber = 3, Name = "John Dillinger", Interests = new List<string> {"Banks", "Broads" }, Services = new List<string> {"Making escape plans", "Stealing cafeteria food" } },
            new Clinker {SerialNumber = 4, Name = "Fred Flintstone", Interests = new List<string> {"DIY Vehicles", "Rock bowling" }, Services = new List<string> {"Shank sharpening", "Selling exotic animals" } }
        };
    }
}
