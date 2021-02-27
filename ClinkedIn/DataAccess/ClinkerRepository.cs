using ClinkedIn.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClinkedIn.DataAccess
{
    public class ClinkerRepository
    {
        static List<Clinker> _clinkers = new List<Clinker>
        {
            new Clinker {SerialNumber = 1, Name = "Joe Exotic", Interests = new List<string> {"Exotic animals", "Cowboy hats" }, Services = new List<string> {"Relationship advice", "Hiring hitmen" }, DaysRemaining = 1 },
            new Clinker {SerialNumber = 2, Name = "Al Capone", Interests = new List<string> {"Tax evasion", "Al Pacino movies" }, Services = new List<string> {"Hiring bodyguards", "Smuggling goods" }, DaysRemaining = 1  },
            new Clinker {SerialNumber = 3, Name = "John Dillinger", Interests = new List<string> {"Banks", "Broads" }, Services = new List<string> {"Making escape plans", "Stealing cafeteria food" }, DaysRemaining = 1  },
            new Clinker {SerialNumber = 4, Name = "Fred Flintstone", Interests = new List<string> {"DIY Vehicles", "Rock bowling" }, Services = new List<string> {"Shank sharpening", "Selling exotic animals" }, DaysRemaining = 1  },
            new Clinker {SerialNumber = 5, Name = "Clyde Barrow", Interests = new List<string> {"Jazz Music", "Robbery" }, Services = new List<string> {"Hideouts", "Apple Pie" }, DaysRemaining = 1  },
            new Clinker {SerialNumber = 6, Name = "Frank Abagnale", Interests = new List<string> {"Traveling", "Fine food" }, Services = new List<string> {"Disgusies", "Counterfeiting" }, DaysRemaining = 1  },
            new Clinker {SerialNumber = 7, Name = "Charlie Manson", Interests = new List<string> {"Rock N Roll", "Knives" }, Services = new List<string> {"Organizing Retreats", "Face tattoos" }, DaysRemaining = 1  },
            new Clinker {SerialNumber = 8, Name = "George Bluth", Interests = new List<string> {"The Banana Stand", "Cornballers" }, Services = new List<string> {"Motivational Tapes", "Smuggling goods" }, DaysRemaining = 1  },
            new Clinker {SerialNumber = 9, Name = "Hannibal Lecter", Interests = new List<string> {"Fine Cuisine", "Face Masks" }, Services = new List<string> {"Psychological Advice", "Cooking" }, DaysRemaining = 1  }
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

        public Dictionary<string, List<string>> GetAllInterests()
        {
            var myReturnList = new Dictionary<string, List<string>>();
            foreach (var clinker in _clinkers)
            {
                if (clinker.Interests != null) myReturnList.Add(clinker.Name, clinker.Interests);
            }
            return myReturnList;
        }

        public void removeInterest(int serialNumber, List<string> interest)
        {
            var clinker = Get(serialNumber);
            clinker.Interests.Remove(interest[0]);
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
            foreach (var clinkerfriend in clinkersFriends)
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
            if (!friend.Friends.Contains(clinker))
            {
                friend.Friends.Add(clinker);
            }
        }

        public void AddEnemy(int serialNumber, int enemyId)
        {
            var clinker = Get(serialNumber);
            var enemy = Get(enemyId);

            if (!clinker.Enemies.Contains(enemy))
            {
                clinker.Enemies.Add(enemy);
            }
            if (!enemy.Enemies.Contains(clinker))
            {
                enemy.Enemies.Add(clinker);
            }
        }

        public void RemoveClinker(int serialNumber)
        {
            var clinker = Get(serialNumber);

            _clinkers.Remove(clinker);
        }

        public void RemoveFriend(int serialNumber, int friendSerial)
        {
            var clinker = Get(serialNumber);
            var friend = Get(friendSerial);

            if (clinker.Friends.Contains(friend))
            {
                clinker.Friends.Remove(friend);
            }
            if (friend.Friends.Contains(clinker))
            {
                friend.Friends.Remove(clinker);
            }
        }

        public void RemoveEnemy(int serialNumber, int enemySerial)
        {
            var clinker = Get(serialNumber);
            var enemy = Get(enemySerial);

            if (clinker.Enemies.Contains(enemy))
            {
                clinker.Enemies.Remove(enemy);
            }
            if (enemy.Enemies.Contains(clinker))
            {
                enemy.Enemies.Remove(clinker);
            }
        }

        public void RemoveAll(int serialNumber)
        {
            for (var i = 0; i < _clinkers.Count; i++)
            {
                RemoveFriend(serialNumber, _clinkers[i].SerialNumber);
                RemoveEnemy(serialNumber, _clinkers[i].SerialNumber);
            }
        }


    }

}
