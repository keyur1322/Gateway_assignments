using Hotel_Management_System_Database.DB_Context;
using Hotel_Management_System_Logic.Interface;
using Hotel_Management_System_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hotel_Management_System.Controllers
{
    public class HotelController : ApiController
    {
        private readonly IHotelManager _hotelManager;

        public HotelController(IHotelManager hotelManager)
        {
            _hotelManager = hotelManager;
        }

        // TODO: Update booking status only
        [Route("api/Hotel/UpdateBookingStatus")]
        public String UpdateBookingStatus([FromBody] Bookings model)
        {
            return _hotelManager.UpdateBookingStatus(model);
        }

        // TODO: Update booking date only
        [Route("api/Hotel/UpdateBookingDate")]
        public String UpdateBookingDate([FromBody] Bookings model)
        {
            return _hotelManager.UpdateBookingDate(model);
        }


        // TODO: Add new hotels
        [Route("api/Hotel/AddHotel")]
        public String AddHotel([FromBody] Hotels model)
        {
            return _hotelManager.AddHotel(model);
        }


        // TODO: Add new rooms
        [Route("api/Hotel/AddRoom")]
        public String AddRoom([FromBody] Rooms model)
        {
            return _hotelManager.AddRoom(model);

        }

        // TODO: Get All hotels by Alphabetic order
        [Route("api/Hotel/GetAllHotel")]
        public List<Hotels> GetAllHotel()
        {
            List<Hotels> resultlist = _hotelManager.GetAllHotels();
            return resultlist;
        }



        // TODO: Get All Room by default sort by price
        [Route("api/Hotel/GetAllRoom")]
        public List<HotelsRoomsModel> GetAllRoom()
        {
            List<HotelsRoomsModel> resultlist = _hotelManager.GetAllRooms();
            return resultlist;
        }



        // TODO: Get available room
        [Route("api/Hotel/AvailableRoom")]
        public String AvailableRoom([FromBody] Rooms date)
        {
            return _hotelManager.GetAvailableRoom(date);
        }



        // TODO: Book room
        [Route("api/Hotel/BookRoom")]
        public String BookRoom([FromBody] Bookings model)
        {
            return _hotelManager.BookRoom(model);
        }



        // TODO: Delete room (Soft Delete)
        public String DeleteBooking([FromBody] Bookings model)
        {
            return _hotelManager.DeleteBookings(model);
        }
    }
}
