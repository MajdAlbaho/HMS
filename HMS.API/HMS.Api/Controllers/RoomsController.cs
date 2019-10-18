using AutoMapper;
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
    public class RoomsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;

        public RoomsController(IMapper mapper, IRoomRepository roomRepository) {
            _mapper = mapper;
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                return Ok(await _roomRepository.GetAllAsync());
            } catch (Exception e) {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
