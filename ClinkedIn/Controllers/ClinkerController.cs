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

        //GetListOfMyServices /api/clinker/services/serialNumber
        [HttpGet("services/{serialNumber}")]
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

        //AddService /api/clinker/services/serialNumber
        [HttpPut("services/{serialNumber}")]
        public IActionResult AddService(int serialNumber, List<string> services)
        {
            var clinker = _repo.Get(serialNumber);
            clinker.Services.AddRange(services);
            return Ok(clinker.Services);

        }

        // API Post to /api/Clinkers
        [HttpPost]
        public IActionResult AddAClinker(Clinker clinker)
        {
            _repo.Add(clinker);
            return Created($"/api/Clinker/{clinker.SerialNumber}", clinker);
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

        [HttpGet("interests/{serialNumber}")]
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
        // PUT /api/Clinkers/{serialNumber} {interest = }
        [HttpPut("interests/{serialNumber}")]
        public IActionResult EditInterest(int serialNumber, List<string> interests)
        {
            var clinker = _repo.Get(serialNumber);
            clinker.Interests.AddRange(interests);
            return Ok(clinker.Interests);
        }



        }
    }
