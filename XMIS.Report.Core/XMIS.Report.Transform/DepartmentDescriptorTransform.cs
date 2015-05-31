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
    public class DepartmentDescriptorTransform : IDepartmentDescriptorTransformer
    {
        private int departmentID;

        public DepartmentDescriptorTransform(int id)
        {
            this.departmentID = id;
        }

        public DepartmentDescriptorBase Transform(DataTable dataTable)
        {
            DepartmentDescriptorBase descriptor = new DepartmentDescriptorBase();
            descriptor.Services = this.GetServices(dataTable);
            descriptor.Rooms = this.GetRooms(dataTable);
            descriptor.DepartmenID = this.departmentID;
            descriptor.DepartmentName = this.GetDepartmentName(dataTable);
            descriptor.DepertmantType = this.GetDepartmentType(dataTable);

            return descriptor;
        }

        private List<ServiceDescriptorBase> GetServices(DataTable dataTable)
        {
            ServiceDescriptorTransformer serviceTransform = new ServiceDescriptorTransformer();
            var tmpServices = new List<ServiceDescriptorBase>();
            foreach (DataRow row in dataTable.AsEnumerable())
                if (row["departmentID"].ToString() == this.departmentID.ToString())
                    tmpServices.Add(serviceTransform.Transform(row));
            return tmpServices;
        }

        private List<RoomBase> GetRooms(DataTable dataTable)
        {
            RoomDescriptrorTransformer serviceTransform = new RoomDescriptrorTransformer();
            var tmpServices = new List<RoomBase>();
            foreach (DataRow row in dataTable.AsEnumerable())
                if (row["departmentID"].ToString() == this.departmentID.ToString())
                    tmpServices.Add(serviceTransform.Transform(row));
            return tmpServices;
        }

        private string GetDepartmentName(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.AsEnumerable())
                if (row["departmentID"].ToString() == this.departmentID.ToString())
                    return row["departmentName"].ToString();
            return string.Empty;
        }

        private string GetDepartmentType(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.AsEnumerable())
                if (row["departmentID"].ToString() == this.departmentID.ToString())
                    return row["departmentType"].ToString();
            return string.Empty;
        }
    }
}
