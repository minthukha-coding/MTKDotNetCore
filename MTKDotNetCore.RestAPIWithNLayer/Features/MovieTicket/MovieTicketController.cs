using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MTKDotNetCore.RestAPIWithNLayer.Features.MovieTicket
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieTicketController : ControllerBase
    {
        private MovieTicket _movieTicket;

        private async Task<MovieTicket> GetDataAsync()
        {
            var jsonStr = await System.IO.File.ReadAllTextAsync("MovieTicket.json");
            var model = JsonConvert.DeserializeObject<MovieTicket>(jsonStr);
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
        [HttpGet("GetMovieShowTime")]
        public async Task<IActionResult> GetMovieShowTime(int movieid, int cinemaId, int roomId)
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_MovieShowDate
                .Where(x => x.MovieId == movieid && x.CinemaId == cinemaId && x.RoomId == roomId));
        }

        [HttpGet("GetMovieschedule")]
        public async Task<IActionResult> GetMovieschedule(int showDataId)
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_MovieSchedule.Where(x => x.ShowDateId == showDataId).ToList());
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
        public async Task<IActionResult> GetInvoice(string movieName, string movieTime, string roomSeat, string seatPrice)
        {
            var invoice = new
            {
                MovieName = movieName,
                ShowTime = movieTime,
                Seat = roomSeat,
                Price = seatPrice
            };
            return Ok(invoice);
        }

        #region Model

        public class MovieTicket
        {
            public Tbl_Cinemalist[] Tbl_CinemaList { get; set; }
            public Tbl_Cinemaroom[] Tbl_CinemaRoom { get; set; }
            public Tbl_Movielist[] Tbl_MovieList { get; set; }
            public Tbl_Roomseat[] Tbl_RoomSeat { get; set; }
            public Tbl_Movieshowdate[] Tbl_MovieShowDate { get; set; }
            public Tbl_Movieschedule[] Tbl_MovieSchedule { get; set; }
            public Tbl_Seatprice[] Tbl_SeatPrice { get; set; }
        }

        public class Tbl_Cinemalist
        {
            public int CinemaId { get; set; }
            public string CinemaName { get; set; }
            public string CinemaLocation { get; set; }
        }

        public class Tbl_Cinemaroom
        {
            public int RoomId { get; set; }
            public int CinemaId { get; set; }
            public int RoomNumber { get; set; }
            public string RoomName { get; set; }
            public int SeatingCapacity { get; set; }
        }

        public class Tbl_Movielist
        {
            public int MovieId { get; set; }
            public string MovieTitle { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string Duration { get; set; }
            public string MoviePhoto { get; set; }
        }

        public class Tbl_Roomseat
        {
            public int SeatId { get; set; }
            public int RoomId { get; set; }
            public int? SeatNo { get; set; }
            public string RowName { get; set; }
            public string SeatType { get; set; }
        }

        public class Tbl_Movieshowdate
        {
            public int ShowDateId { get; set; }
            public int CinemaId { get; set; }
            public int RoomId { get; set; }
            public int MovieId { get; set; }
        }

        public class Tbl_Movieschedule
        {
            public int ShowId { get; set; }
            public int ShowDateId { get; set; }
            public DateTime ShowDateTime { get; set; }
        }

        public class Tbl_Seatprice
        {
            public int SeatPriceId { get; set; }
            public int RoomId { get; set; }
            public string RowName { get; set; }
            public int SeatPrice { get; set; }
        }

        #endregion

    }

}
