using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTKDotNetCore.RestAPIWithNLayer.Models;
using Newtonsoft.Json;
using static MTKDotNetCore.RestAPIWithNLayer.Models.MovieTicket;

namespace MTKDotNetCore.RestAPIWithNLayer.Features.MovieTicket
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieTicketController : ControllerBase
    {
        private MovieTicketContainner _MovieTicket;

        private async Task<MovieTicketContainner> GetDataAsync()
        {
            var jsonStr = await System.IO.File.ReadAllTextAsync("MovieTicket.json");
            var model = JsonConvert.DeserializeObject<MovieTicketContainner>(jsonStr);
            return model;
        }

        [HttpGet("getMovies")]
        public async Task<IActionResult> GetMovies()
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_MovieList);
        }

        [HttpGet("getCinemaList")]
        public async Task<IActionResult> GetCinemaList()
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_CinemaList);
        }

        [HttpGet("getCinemaRoomWithCinemaId")]
        public async Task<IActionResult> GetCinemaRoomWithCinemaId(int cinemaId)
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_CinemaRoom.Where(x => x.CinemaId == cinemaId).ToList());
        }

        [HttpGet("GetMovieShowDate")]
        public async Task<IActionResult> GetMovieShowDate()
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_MovieShowDate);
         }

        [HttpGet("GetMovieschedule")]
        public async Task<IActionResult> GetMovieschedule(int showDateId)
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_MovieSchedule.Where(x => x.ShowDateId == showDateId).ToList());
        }

        [HttpGet("GetRoomSeat")]
        public async Task<IActionResult> GetRoomSeat(int roomId)
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_RoomSeat.Where(x => x.RoomId == roomId).ToList());
        }

        [HttpGet("GetSeatprice")]
        public async Task<IActionResult> GetSeatprice(int roomId, string rowName)
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_SeatPrice.Where(x => x.RoomId == roomId && x.RowName == rowName).ToList());
        }
        
        [HttpGet("GetInvoice")]
        public async Task<IActionResult> GetInvoice(int movieId, int cinemaId, int roomId, string rowName)
        {
            var model = await GetDataAsync();

            // Fetch movie details
            var movie = model.Tbl_MovieList.FirstOrDefault(m => m.MovieId == movieId);
            if (movie == null)
                return NotFound("Movie not found");

            // Fetch cinema details
            var cinema = model.Tbl_CinemaList.FirstOrDefault(c => c.CinemaId == cinemaId);
            if (cinema == null)
                return NotFound("Cinema not found");

            // Fetch room details
            var room = model.Tbl_CinemaRoom.FirstOrDefault(r => r.RoomId == roomId && r.CinemaId == cinemaId);
            if (room == null)
                return NotFound("Room not found");

            // Fetch seat details
            var seat = model.Tbl_RoomSeat.FirstOrDefault(s => s.RoomId == roomId && s.RowName == rowName);
            if (seat == null)
                return NotFound("Seat not found");

            // Fetch seat price
            var seatPrice = model.Tbl_SeatPrice.FirstOrDefault(sp => sp.RoomId == roomId && sp.RowName == rowName);
            if (seatPrice == null)
                return NotFound("Seat price not found");

            // Fetch showtime
            var showDate = model.Tbl_MovieShowDate.FirstOrDefault(sd => sd.MovieId == movieId && sd.CinemaId == cinemaId && sd.RoomId == roomId);
            if (showDate == null)
                return NotFound("Show time not found");

            var showTime = model.Tbl_MovieSchedule.FirstOrDefault(ms => ms.ShowDateId == showDate.ShowDateId);
            if (showTime == null)
                return NotFound("Show schedule not found");
            var invoice = new
            {
                MovieName = movie.MovieTitle,
                ShowTime = showTime.ShowDateTime,
                CinemaName = cinema.CinemaName,
                RoomName = room.RoomName,
                Seat = seat.RowName + seat.SeatNo,
                Price = seatPrice.SeatPrice
            };
            return Ok(invoice);
        }
    }

}
