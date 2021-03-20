using ClinkedIn.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ClinkedIn.DataAccess
{
    public class ClinkerRepository
    {
        const string ConnectionString = "Server=localhost;Database=ClinkedIn;Trusted_Connection=True;";
        static List<Clinker> _clinkers = new List<Clinker>
        {
            new Clinker {Id = 1, Name = "Joe Exotic", DaysRemaining = 1 },
            new Clinker {Id = 2, Name = "Al Capone", DaysRemaining = 12  },
            new Clinker {Id = 3, Name = "John Dillinger", DaysRemaining = 1000  },
            new Clinker {Id = 4, Name = "Fred Flintstone", DaysRemaining = 200  },
            new Clinker {Id = 5, Name = "Clyde Barrow", DaysRemaining = 420  },
            new Clinker {Id = 6, Name = "Frank Abagnale", DaysRemaining = 12983  },
            new Clinker {Id = 7, Name = "Charlie Manson", DaysRemaining = 329  },
            new Clinker {Id = 8, Name = "George Bluth", DaysRemaining = 2  },
            new Clinker {Id = 9, Name = "Hannibal Lecter", DaysRemaining = 1  }
        };

        public List<Clinker> GetAll()
        {
            using var db = new SqlConnection(ConnectionString);
            var sql = @"Select *
                        from Clinkers";

            return db.Query<Clinker>(sql).ToList();
        }

        public Clinker Get(int id)
        {
            using var db = new SqlConnection(ConnectionString);
            var sql = @"Select *
                        from Clinkers
                        where Id = @id";

            var clinker = db.QueryFirstOrDefault<Clinker>(sql, new { id = id });

            return clinker;
        }

        public void Add(Clinker clinker)
        {
            using var db = new SqlConnection(ConnectionString);
            var sql = @"INSERT INTO [Clinkers] ([Name],[DaysRemaining])
                        OUTPUT inserted.Id
                        VALUES(@Name, @DaysRemaining)";

            var id = db.ExecuteScalar<int>(sql, clinker);

            clinker.Id = id;
        }

        public List<Interest> GetAllInterests()
        {
            using var db = new SqlConnection(ConnectionString);
            var sql = @"Select *
                        from Interests";

            return db.Query<Interest>(sql).ToList();
        }

        /*public void removeInterest(int serialNumber, string interest)
        {
            var clinker = Get(serialNumber);
            clinker.Interests.RemoveAt(clinker.Interests.FindIndex(n => n.Equals(interest, StringComparison.OrdinalIgnoreCase)));
        }*/

        /*public void removeService(int serialNumber, string service)
        {
            var clinker = Get(serialNumber);
            clinker.Services.RemoveAt(clinker.Services.FindIndex(n => n.Equals(service, StringComparison.OrdinalIgnoreCase)));
        }*/

        public List<Service> GetAllServices()
        {
            using var db = new SqlConnection(ConnectionString);
            var sql = @"Select *
                        from Services";

            return db.Query<Service>(sql).ToList();
        }

        // Crew: Friends of Friends
        /*public Dictionary<Clinker, List<Clinker>> GetFriendsOfFriends(Clinker clinker)
        {
            var friendsOfFriends = new Dictionary<Clinker, List<Clinker>>();
            var clinkersFriends = clinker.Friends;
            foreach (var clinkerfriend in clinkersFriends)
            {
                friendsOfFriends.Add(clinkerfriend, clinkerfriend.Friends);
            }

            return friendsOfFriends;
        }*/
        /*public void AddFriend(int serialNumber, int friendId)
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
        }*/

        /*public void AddEnemy(int serialNumber, int enemyId)
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
        }*/

        /*public void RemoveClinker(int serialNumber)
        {
            var clinker = Get(serialNumber);

            _clinkers.Remove(clinker);
        }*/

        /*public void RemoveFriend(int serialNumber, int friendSerial)
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
        }*/

        public int DaysLeft(int serialNumber)
        {
            var clinker = Get(serialNumber);
            return clinker.DaysRemaining;
        }
    }

}
