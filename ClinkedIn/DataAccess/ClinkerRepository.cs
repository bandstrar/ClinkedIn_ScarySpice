using ClinkedIn.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClinkedIn.DataAccess
{
    public class ClinkerRepository
    {
        static List<Clinker> _clinkers = new List<Clinker>
        {
            new Clinker {SerialNumber = 1, Name = "Joe Exotic", Interests = new List<string> {"Exotic animals", "Cowboy hats" }, Services = new List<string> {"Relationship advice", "Hiring hitmen" } },
            new Clinker {SerialNumber = 2, Name = "Al Capone", Interests = new List<string> {"Tax evasion", "Al Pacino movies" }, Services = new List<string> {"Hiring bodyguards", "Smuggling goods" } },
            new Clinker {SerialNumber = 3, Name = "John Dillinger", Interests = new List<string> {"Banks", "Broads" }, Services = new List<string> {"Making escape plans", "Stealing cafeteria food" } },
            new Clinker {SerialNumber = 4, Name = "Fred Flintstone", Interests = new List<string> {"DIY Vehicles", "Rock bowling" }, Services = new List<string> {"Shank sharpening", "Selling exotic animals" } },
            new Clinker {SerialNumber = 5, Name = "Clyde Barrow", Interests = new List<string> {"Jazz Music", "Robbery" }, Services = new List<string> {"Hideouts", "Apple Pie" } },
            new Clinker {SerialNumber = 6, Name = "Frank Abagnale", Interests = new List<string> {"Traveling", "Fine food" }, Services = new List<string> {"Disgusies", "Counterfeiting" } },
            new Clinker {SerialNumber = 7, Name = "Charlie Manson", Interests = new List<string> {"Rock N Roll", "Knives" }, Services = new List<string> {"Organizing Retreats", "Face tattoos" } }
        };

        public List<Clinker> GetAll()
        {
            return _clinkers;
        }

        public Clinker Get(int serialNumber)
        {
            var clinker = _clinkers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            return clinker;
        }

        public void Add(Clinker clinker)
        {
            var biggestExistingInt = _clinkers.Max(clinker => clinker.SerialNumber);
            clinker.SerialNumber = biggestExistingInt + 1;
            _clinkers.Add(clinker);
        }

        public Dictionary<string,List<string>> GetAllInterests()
        {
            var myReturnList = new Dictionary<string, List<string>>();
            foreach(var clinker in _clinkers)
            {
                if (clinker.Interests != null) myReturnList.Add(clinker.Name, clinker.Interests);
            }
            return myReturnList;
        }
        public Dictionary<string, List<string>> GetAllServices()
        {
            var allServices = new Dictionary<string, List<string>>();
            foreach (var clinker in _clinkers)
            {
                if (clinker.Services != null) allServices.Add(clinker.Name, clinker.Services);
            }
            return allServices;
        }

        // Crew: Friends of Friends
        public Dictionary<Clinker, List<Clinker>> GetFriendsOfFriends(Clinker clinker)
        {
            var friendsOfFriends = new Dictionary<Clinker, List<Clinker>>();
            var clinkersFriends = clinker.Friends;
            foreach(var clinkerfriend in clinkersFriends)
            {
                friendsOfFriends.Add(clinkerfriend, clinkerfriend.Friends);
            }
           
            return friendsOfFriends;
        }
        public void AddFriend(int serialNumber, int friendId)
        {
            var clinker = Get(serialNumber);
            var friend = Get(friendId);

            if (!clinker.Friends.Contains(friend))
            {
                clinker.Friends.Add(friend);
            }
            
        }
    }

}
