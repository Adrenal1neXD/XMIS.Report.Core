using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Transform.Contract;
using XMIS.Report.Domain;
using System.Data;

namespace XMIS.Report.Transform
{
    public class RoomDescriptrorTransformer : IDescriptorTransformer<RoomBase>
    {
        public RoomBase Transform(DataRow dataRow)
        {
            var room = new RoomBase();
            room.Beds = new List<BedBase>(); //------------------------> bed transform

            room.Gender = Convert.ToInt32(dataRow["roomGender"]);
            room.RoomId = Convert.ToInt32(dataRow["roomId"]);

            return room;
        }
    }
}
