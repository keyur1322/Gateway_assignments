using Hotel_Management_System_Database.DB_Context;
using Hotel_Management_System_Database.Interface_DB;
using Hotel_Management_System_Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_Database
{
    public class HotelManagerDB : IHotelManagerDB
    {
        private readonly DB_Context.WebAPI_DatabaseEntities _webAPI_DatabaseEntities;

        public HotelManagerDB(DB_Context.WebAPI_DatabaseEntities webAPI_DatabaseEntities)
        {
            _webAPI_DatabaseEntities = webAPI_DatabaseEntities;
        }


        // TODO: Add new hotels
        public string AddHotel(Hotels model)
        {
            if(model != null)
            {
                DB_Context.tbl_hotels tbl_Hotels = new DB_Context.tbl_hotels();

                tbl_Hotels.HotelName = model.HotelName;
                tbl_Hotels.Address = model.Address;
                tbl_Hotels.City = model.City;
                tbl_Hotels.Pincode = model.Pincode;
                tbl_Hotels.ContactNo = model.ContactNo;
                tbl_Hotels.ContactPerson = model.ContactPerson;
                tbl_Hotels.Website = model.Website;
                tbl_Hotels.Facebook = model.Facebook;
                tbl_Hotels.Twitter = model.Twitter;
                tbl_Hotels.IsActive = model.IsActive;
                tbl_Hotels.CreatedDate = model.CreatedDate;
                tbl_Hotels.CreatedBy = model.CreatedBy;
                tbl_Hotels.UpdatedDate = model.UpdatedDate;
                tbl_Hotels.UpdateBy = model.UpdateBy;

                _webAPI_DatabaseEntities.tbl_hotels.Add(tbl_Hotels);
                _webAPI_DatabaseEntities.SaveChanges();

                return "Hotel added Successfully.";
            }
            return "Not Found.";
        }




        // TODO: Add new rooms
        public string AddRoom(Rooms model)
        {
            if(model != null)
            {
                DB_Context.tbl_rooms tbl_Rooms = new DB_Context.tbl_rooms();

                tbl_Rooms.Hotel_Id = model.Hotel_Id;
                tbl_Rooms.RoomName = model.RoomName;
                tbl_Rooms.Category = model.Category;
                tbl_Rooms.Price = model.Price;
                tbl_Rooms.IsActive = model.IsActive;
                tbl_Rooms.CreatedDate = model.CreatedDate;
                tbl_Rooms.CreatedBy = model.CreatedBy;
                tbl_Rooms.UpdatedDate = model.UpdatedDate;
                tbl_Rooms.UpdatedBy = model.UpdatedBy;

                _webAPI_DatabaseEntities.tbl_rooms.Add(tbl_Rooms);
                _webAPI_DatabaseEntities.SaveChanges();

                return "Room added Successfully.";
            }
            return "Not Found.";
        }

      
        // TODO: Get All hotes by Alphabetic order
        public List<Hotels> GetAllHotels()
        {
            List<Hotels> resultlist = new List<Hotels>();

            var result1 = _webAPI_DatabaseEntities.tbl_hotels.OrderBy(x => x.HotelName).ToList();

            foreach (var item in result1)
            {
                Hotels hotels = new Hotels();

                hotels.Hotel_Id = item.Hotel_Id;
                hotels.HotelName = item.HotelName;
                hotels.Address = item.Address;
                hotels.City = item.City;
                hotels.Pincode = item.Pincode;
                hotels.ContactNo = item.ContactNo;
                hotels.ContactPerson = item.ContactPerson;
                hotels.Website = item.Website;
                hotels.Facebook = item.Facebook;
                hotels.Twitter = item.Twitter;
                hotels.IsActive = item.IsActive;
                hotels.CreatedDate = item.CreatedDate;
                hotels.CreatedBy = item.CreatedBy;
                hotels.UpdatedDate = item.UpdatedDate;
                hotels.UpdateBy = item.UpdateBy;

                resultlist.Add(hotels);

            }
            return resultlist;

        }


        // TODO: Get All Room by default sort by price
        public List<HotelsRoomsModel> GetAllRooms()
        {
            var resultlist = (from x in _webAPI_DatabaseEntities.tbl_rooms.OrderBy( x => x.Price )
                          
                          select new HotelsRoomsModel
                          {
                             HotelName = x.tbl_hotels.HotelName,
                             Address = x.tbl_hotels.Address,
                             City = x.tbl_hotels.City,
                             Pincode = x.tbl_hotels.Pincode,

                             RoomName = x.RoomName,
                             Category = x.Category,
                             Price = x.Price
                          }).ToList();

            return resultlist;
        }

        // TODO: Book room
        public string BookRoom(Bookings model)
        {
            if (model != null)
            {
                string bookingdate = model.BookingDate?.ToString("yyyy-MM-dd");

                var roomdate = _webAPI_DatabaseEntities.tbl_rooms.Where(x => x.UpdatedDate.ToString() == bookingdate).ToList();
                if(roomdate.Count == 0)
                {
                    DB_Context.tbl_bookings tbl_Bookings1 = new DB_Context.tbl_bookings();

                    tbl_Bookings1.Room_Id = model.Room_Id;
                    tbl_Bookings1.BookingDate = model.BookingDate;
                    tbl_Bookings1.Status = model.status;

                    _webAPI_DatabaseEntities.tbl_bookings.Add(tbl_Bookings1);
                    _webAPI_DatabaseEntities.SaveChanges();

                    return "Room Booked Successfully.";
                }
                else
                {
                    return "Room is not available";
                }
               
               
            }
            return "Data Not Found";
        }


        // TODO: Get available room
        public String GetAvailableRoom(Rooms date)
        {
          
            //String date1 = string.Format("{0}-{1}-{2}", date.UpdatedDate.Value.Year, date.UpdatedDate.Value.Month, date.UpdatedDate.Value.Day);
            string dateString = date.UpdatedDate?.ToString("yyyy-MM-dd");

            List<tbl_rooms> value = _webAPI_DatabaseEntities.tbl_rooms.Where(x => x.UpdatedDate.ToString() == dateString).ToList();
            if(value.Count == 0)
            {
                return "True";
            }
            return "False";
        }


        // TODO: Update booking date only
        public string UpdateBookingDate(Bookings model)
        {
            var result = _webAPI_DatabaseEntities.tbl_bookings.Find(model.Booking_Id);
            if (result != null)
            {
                result.Room_Id = model.Room_Id;
                result.BookingDate = model.BookingDate;


                _webAPI_DatabaseEntities.SaveChanges();

                return "Booking Updated Successfully.";
            }

            return "Booking not found.";
           

        }


        // TODO: Update booking status only
        public string UpdateBookingStatus(Bookings model)
        {
            var result = _webAPI_DatabaseEntities.tbl_bookings.Find(model.Booking_Id);
            if (result != null)
            {
                result.Status = model.status;

                _webAPI_DatabaseEntities.SaveChanges();

                return "Booking Status Updated Successfully.";
            }

            return "Booking not found.";

        }


        // TODO: Delete room (Soft Delete)
        public string DeleteBookings(Bookings model)
        {
            var result = _webAPI_DatabaseEntities.tbl_bookings.Find(model.Booking_Id);
            if (result != null)
            {
                result.Status = model.status;

                _webAPI_DatabaseEntities.SaveChanges();

                return "Booking Status Deleted Successfully.";
            }

            return "Booking not found.";

        }
    }
}

