using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management_System_Models
{
    public class Rooms
    {
         public int Room_Id { get; set; }
        public int Hotel_Id { get; set; }
        public string RoomName { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public string IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    

    }
}
