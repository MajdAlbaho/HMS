using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HMS.Api.Repositories.Interfaces;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public GuestsController(IPersonRepository personRepository, IMapper mapper) {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Get() {
            try {
                return Ok(_mapper.Map<List<Person>>((await _personRepository.GetAllAsync())
                    .Take(4)
                    .ToList()));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
