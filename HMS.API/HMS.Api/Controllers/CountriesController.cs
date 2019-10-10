using AutoMapper;
using HMS.Api.Repositories.Interfaces;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountriesController(ICountryRepository countryRepository, IMapper mapper) {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            return Ok(_mapper.Map<List<Country>>(await _countryRepository.GetAllAsync()));
        }
    }
}
