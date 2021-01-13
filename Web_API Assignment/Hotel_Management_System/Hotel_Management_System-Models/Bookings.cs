using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_Models
{
    public class Bookings
    {
        public int Booking_Id { get; set; }
        public int Room_Id { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public string status { get; set; }

    }
}
