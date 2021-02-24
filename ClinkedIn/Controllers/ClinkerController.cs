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

        //GetAllClinkers /api/Clinkers

        [HttpGet]
        public IActionResult GetAllClinkers()
        {
            return Ok(_repo.GetAll());
        }

        //GetClinker /api/Clinkers/{serialNumber}
        //            /api/Clinkers/1
        [HttpGet("{serialNumber}")]
        public IActionResult GetClinker(int serialNumber)
        {
            var clinker = _repo.Get(serialNumber);

            if(clinker == null)
            {
                return NotFound("This clinker does not exist");
            }

            return Ok(clinker);
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

        [HttpPut("{serialNumber}/friends/addFriend/{friendId}")]
        public IActionResult AddFriend(int serialNumber, int friendId)
        {
            var clinker = _repo.Get(serialNumber);

            _repo.AddFriend(serialNumber, friendId);

            return Ok(clinker.Friends);
        }


    }
}
