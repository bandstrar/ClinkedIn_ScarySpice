using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIn.DataAccess;
using ClinkedIn.Models;

namespace ClinkedIn.Controllers
{
    [Route("api/Clinker")]
    [ApiController]
    public class ClinkerController : ControllerBase
    {
        ClinkerRepository _repo;

        public ClinkerController()
        {
            _repo = new ClinkerRepository();
        }

        //GetAllClinkers /api/Clinkers/warden/allClinkers

        [HttpGet("warden/allClinkers")]
        public IActionResult GetAllClinkers()
        {
            return Ok(_repo.GetAll());
        }

        //Delete clinker from list of Clinkers
        [HttpDelete("warden/deleteClinker")]
        public IActionResult DeleteClinker(List<int> serialNumber)
        {
            var clinker = _repo.Get(serialNumber[0]);

            if (clinker == null)
            {
                return NotFound("This clinker does not exist");
            }
            else
            {
                _repo.RemoveAll(serialNumber[0]);
                _repo.RemoveClinker(serialNumber[0]);

                return Ok(_repo.GetAll());
            }
            
        }

        //GetClinker /api/Clinkers/{serialNumber}
        //            /api/Clinkers/1
        [HttpGet("{serialNumber}")]
        public IActionResult GetClinker(int serialNumber)
        {
            var clinker = _repo.Get(serialNumber);

            if (clinker == null)
            {
                return NotFound("This clinker does not exist");
            }

            return Ok(clinker);
        }

        //GetListOfMyServices /api/clinker/services/serialNumber
        [HttpGet("{serialNumber}/services")]
        public IActionResult GetListOfMyServices(int serialNumber)
        {
            var clinker = _repo.Get(serialNumber);
            if (clinker == null)
            {
                return NotFound("This clinker does not exist");
            }
            if (clinker.Services.Count == 0)
            {
                return NotFound($"{clinker.Name} does not have any services");
            }

            return Ok(clinker.Services);
        }

        //GetListOfAllServices /api/clinker/services
        [HttpGet("services")]
        public IActionResult GetListOfAllServices()
        {
            return Ok(_repo.GetAllServices());
        }

        // API Post to /api/Clinkers
        [HttpPost]
        public IActionResult AddAClinker(Clinker clinker)
        {
            _repo.Add(clinker);
            return Created($"/api/Clinker/{clinker.SerialNumber}", clinker);
        }

        // Show all Clinker's friends
        [HttpGet("{serialNumber}/friends")]
        public IActionResult GetFriends(int serialNumber)
        {
            var clinker = _repo.Get(serialNumber);

            if (clinker == null)
            {
                return NotFound("This clinker does not exist");
            }
            else if (clinker.Friends == null || clinker.Friends.Count == 0)
            {
                return NotFound("This clinker does not have any friends. How sad.");
            }
            return Ok(clinker.Friends);
        }

        //Add Clinker friend
        [HttpPut("{serialNumber}/friends/addFriend")]
        public IActionResult AddFriend(int serialNumber, List<int> friendId)
        {
            var clinker = _repo.Get(serialNumber);

            _repo.AddFriend(serialNumber, friendId[0]);

            return Ok(clinker.Friends);
        }

        // Show all Clinker's enemies
        [HttpGet("{serialNumber}/enemies")]
        public IActionResult GetEnemies(int serialNumber)
        {
            var clinker = _repo.Get(serialNumber);

            if (clinker == null)
            {
                return NotFound("This clinker does not exist");
            }
            else if (clinker.Enemies == null || clinker.Enemies.Count == 0)
            {
                return NotFound("This clinker does not have any enemies. Likely story.");
            }
            return Ok(clinker.Enemies);
        }

        // Add Clinker enemy

        [HttpPut("{serialNumber}/enemies/addEnemy")]
        public IActionResult AddEnemy(int serialNumber, List<int> enemyId)
        {
            var clinker = _repo.Get(serialNumber);

            _repo.AddEnemy(serialNumber, enemyId[0]);

            return Ok(clinker.Enemies);
        }

        // GET all the users and their interests
        // GET All + return only names and interests
        [HttpGet("interests")]
        public IActionResult GetAllClinkersInterests()
        {
            return Ok(_repo.GetAllInterests());
        }

        // Get all User's interests
        // GET /api/Clinker/{serialNumber}/interests

        [HttpGet("{serialNumber}/interests")]
        public IActionResult GetInterestsById(int serialNumber)
        {
            var clinker = _repo.Get(serialNumber);
            if (clinker == null)
            {
                return NotFound("This clinker does not exist");
            }
            else if (clinker.Interests == null)
            {
                return NotFound("This clinker exists but does not have any interests yet.");
            }
            else return Ok(clinker.Interests);

        }

        // Add an interest to a user
        // PUT /api/Clinkers/{serialNumber} + Interest Body / This is case insensitive
        [HttpPut("{serialNumber}/interests")]
        public IActionResult EditInterest(int serialNumber, List<string> interests)
        {
            var clinker = _repo.Get(serialNumber);
            if (clinker.Interests.FindAll(x => x.IndexOf(interests[0],
                       StringComparison.OrdinalIgnoreCase) >= 0).Count == 0)
                
            {
                clinker.Interests.AddRange(interests);
            }
                
            return Ok(clinker.Interests);
        }

        //Put /api/Clinkers/{serialNumber}/removeInterest + Interest Body / This is case insensitive
        [HttpPut("{serialNumber}/interests/removeInterest")]
        public IActionResult DeleteInterest(int serialNumber, List<string> interests)
        {
            var clinker = _repo.Get(serialNumber);
            if (clinker.Interests.FindAll(x => x.IndexOf(interests[0],
                       StringComparison.OrdinalIgnoreCase) >= 0).Count > 0)
            {
                _repo.removeInterest(serialNumber, interests[0]);
                return Ok(clinker.Interests);
            }
            else
            {
                return NotFound("This interest is not in the clinkers interest.");
            }

        }

        // Add an Service to a user
        // PUT /api/Clinkers/{serialNumber} + Service Body / This is case insensitive
        [HttpPut("{serialNumber}/services")]
        public IActionResult EditService(int serialNumber, List<string> services)
        {
            var clinker = _repo.Get(serialNumber);
            if (clinker.Services.FindAll(x => x.IndexOf(services[0],
                       StringComparison.OrdinalIgnoreCase) >= 0).Count == 0)

            {
                clinker.Services.AddRange(services);
            }

            return Ok(clinker.Services);
        }

        //Put /api/Clinkers/{serialNumber}/removeService + Service Body / This is case insensitive
        [HttpPut("{serialNumber}/services/removeService")]
        public IActionResult DeleteService(int serialNumber, List<string> services)
        {
            var clinker = _repo.Get(serialNumber);
            if (clinker.Services.FindAll(x => x.IndexOf(services[0],
                       StringComparison.OrdinalIgnoreCase) >= 0).Count > 0)
            {
                _repo.removeService(serialNumber, services[0]);
                return Ok(clinker.Services);
            }
            else
            {
                return NotFound("This Service is not in the clinkers Service.");
            }

        }

        // Crew: Returns friends of friends

        [HttpGet("{serialNumber}/friends/crew")]
        public IActionResult FriendsOfFriends(int serialNumber)
        {
            var clinker = _repo.Get(serialNumber);
            if (clinker.Friends == null || clinker.Friends.Count == 0)
            {
                return NotFound("This clinker does not have any friends. How sad.");
            }
            var friendsOfFriends = _repo.GetFriendsOfFriends(clinker);
            return Ok(friendsOfFriends);

        }
    }
}