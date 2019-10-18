using AutoMapper;
using HMS.Api.Models.parameters;
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
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationRepository reservationRepository, IMapper mapper) {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            try {
                return Ok(_mapper.Map<List<Reservation>>(
                    await _reservationRepository.GetAllAsync()));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("CheckReservation")]
        public async Task<IActionResult> CheckReservation(CheckReservation checkReservation) {
            try {
                // get any rooms available
                var availableRooms =
                    await _reservationRepository.CheckReservation(checkReservation);

                return Ok(availableRooms);
            } catch (Exception e) {
                return BadRequest(new { message = e.Message });
            }
        }
    }
}
