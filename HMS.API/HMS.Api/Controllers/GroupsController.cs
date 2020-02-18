using AutoMapper;
using HMS.Api.Repositories.Interfaces;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Api.Repositories.HMSDb;

namespace HMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GroupsController(IMapper mapper, IGroupRepository groupRepository) {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                return Ok(_mapper.Map<List<Group>>(await _groupRepository.GetAllAsync()));
            } catch (Exception e) {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpGet("{reservationStartDate}")]
        public async Task<IActionResult> GetGroupsByReservationStartDate(DateTime reservationStartDate) {
            try {
                return Ok(_mapper.Map<List<Reservation>>(
                    await _groupRepository.GetGroupsByReservationStartDate(reservationStartDate)));
            } catch (Exception e) {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost]
        [Route("SaveGroup")]
        public async Task<IActionResult> SaveGroup([FromBody]Group group) {
            try {
                return Ok(await _groupRepository.AddAsync(_mapper.Map<Groups>(group)));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
