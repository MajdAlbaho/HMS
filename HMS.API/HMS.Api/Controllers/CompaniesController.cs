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
    public class CompaniesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public CompaniesController(IMapper mapper, ICompanyRepository companyRepository) {
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            try {
                return Ok(_mapper.Map<List<Company>>(await _companyRepository.GetAllAsync()));
            } catch (Exception e) {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
