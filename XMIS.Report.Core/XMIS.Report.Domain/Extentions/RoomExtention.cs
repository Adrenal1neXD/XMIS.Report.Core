using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Domain.Extentions
{
    public static class RoomExtention
    {
        public static List<BedBase> Beds(this List<RoomBase> roomList, params BedStatus[] status)
        {
            List<BedBase> beds = new List<BedBase>(); 
            foreach (BedStatus stat in status)
                beds.AddRange((from room in roomList
                   from bed in room.Beds
                   where bed.Status == stat
                   select bed).ToList());
            return beds;
        }
    }
}
