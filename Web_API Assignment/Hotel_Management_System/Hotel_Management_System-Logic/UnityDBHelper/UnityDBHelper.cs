using Hotel_Management_System_Database;
using Hotel_Management_System_Database.Interface_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Extension;


namespace Hotel_Management_System_Logic.UnityDBHelper
{
    public class UnityDBHelper : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IHotelManagerDB, HotelManagerDB>();

        }
    }
}
