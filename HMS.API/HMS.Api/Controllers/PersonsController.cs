using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HMS.Api.Repositories.HMSDb;
using HMS.Api.Repositories.Interfaces;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public PersonsController(IMapper mapper, IPersonRepository personRepository) {
            _mapper = mapper;
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            try {
                return Ok(_mapper.Map<List<Person>>(
                    await _personRepository.GetAllAsync()));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person) {
            try {
                return Ok(await _personRepository.AddAsync(_mapper.Map<Persons>(person)));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }



    }
}
