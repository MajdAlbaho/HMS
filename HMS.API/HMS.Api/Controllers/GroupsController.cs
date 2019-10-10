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
            return Ok(_mapper.Map<List<Group>>(await _groupRepository.GetAllAsync()));
        }

        [HttpGet("{reservationStartDate}")]
        public async Task<IActionResult> GetGroupsByReservationStartDate(DateTime reservationStartDate) {
            try{
                return Ok(_mapper.Map<List<Reservation>>(
                    await _groupRepository.GetGroupsByReservationStartDate(reservationStartDate)));
            } catch (Exception e) {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
