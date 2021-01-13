using Hotel_Management_System_Database.DB_Context;
using Hotel_Management_System_Database.Interface_DB;
using Hotel_Management_System_Logic.Interface;
using Hotel_Management_System_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_Logic
{
    public class HotelManager : IHotelManager
    {
        private readonly IHotelManagerDB _hotelManagerDB;

        public HotelManager(IHotelManagerDB hotelManagerDB)
        {
            _hotelManagerDB = hotelManagerDB;
        }



        // TODO: Add new hotels
        public string AddHotel(Hotels model)
        {
            return _hotelManagerDB.AddHotel(model);
        }


        // TODO: Add new rooms
        public string AddRoom(Rooms model)
        {
            return _hotelManagerDB.AddRoom(model);
        }

        // TODO: Get All hotes by Alphabetic order
        public List<Hotels> GetAllHotels()
        {
            return _hotelManagerDB.GetAllHotels();
        }

        // TODO: Get All Room by default sort by price
        public List<HotelsRoomsModel> GetAllRooms()
        {
            return _hotelManagerDB.GetAllRooms();
        }


        // TODO: Get available room
        public String GetAvailableRoom(Rooms date)
        {
            return _hotelManagerDB.GetAvailableRoom(date);
        }


        // TODO: Book room
        public string BookRoom(Bookings model)
        {
            return _hotelManagerDB.BookRoom(model);
        }


        // TODO: Update booking date only
        public string UpdateBookingDate(Bookings model)
        {
            return _hotelManagerDB.UpdateBookingDate(model);
        }


        // TODO: Update booking status only
        public string UpdateBookingStatus(Bookings model)
        {
            return _hotelManagerDB.UpdateBookingStatus(model);
        }


        // TODO: Delete room (Soft Delete)
        public string DeleteBookings(Bookings model)
        {
            return _hotelManagerDB.DeleteBookings(model);
        }
    }
}
