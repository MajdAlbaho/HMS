using AutoMapper;
using HMS.Api.Models.parameters;
using HMS.Api.Repositories.HMSDb;
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
                return Ok(_mapper.Map<List<HMS.Models.Reservation>>(
                    await _reservationRepository.GetAllAsync()));
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("Check")]
        public async Task<IActionResult> Check([FromBody]Reservation reservation) {
            try {
                if (reservation == null)
                    return BadRequest(new { message = "Invalid arguments" });
                if (reservation.StartDate == DateTime.MinValue || reservation.EndDate == DateTime.MinValue)
                    return BadRequest(new { message = "Invalid start or end date" });

                // get any rooms available
                var availableRooms =
                    _mapper.Map<List<Room>>(await _reservationRepository.CheckReservation(reservation));

                return Ok(availableRooms.Where(e => reservation.RoomType == 0 || e.TotalBeds == reservation.RoomType));
            } catch (Exception e) {
                return BadRequest(new { message = e.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SaveReservation(SingleReservationParam param) {
            try {
                await _reservationRepository.SaveReservation(param.Reservation,
                    param.Person, param.Group);

                return Ok();
            } catch (Exception e) {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid id) {
            try {
                await _reservationRepository.DeleteAsync(id);
                return Ok();
            } catch (Exception e) {
                return BadRequest(new { message = e.Message });
            }
        }

    }
}
