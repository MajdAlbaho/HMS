using HMS.Api.Repositories.Interfaces;
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

        public CountriesController(ICountryRepository countryRepository) {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            return Ok(await _countryRepository.GetAllAsync());
        }
    }
}
