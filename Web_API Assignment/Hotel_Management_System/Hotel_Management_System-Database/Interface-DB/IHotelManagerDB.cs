using Hotel_Management_System_Database.DB_Context;
using Hotel_Management_System_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_Database.Interface_DB
{
    public interface IHotelManagerDB
    {
        // TODO: Add new hotels
        String AddHotel(Hotels model);

        
        // TODO: Add new hotels
        String AddRoom(Rooms model);


        // TODO: Get All hotes by Alphabetic order
        List<Hotels> GetAllHotels();


        // TODO: Get All Room by default sort by price
        List<HotelsRoomsModel> GetAllRooms();

        // TODO: Get available room
        String GetAvailableRoom(Rooms date);

        // TODO: Book room
        String BookRoom(Bookings model);

        // TODO: Update booking date only
        String UpdateBookingDate(Bookings model);


        // TODO: Update booking status only
        String UpdateBookingStatus(Bookings model);


        // TODO: Delete room (Soft Delete)
        String DeleteBookings(Bookings model);
    }
}
