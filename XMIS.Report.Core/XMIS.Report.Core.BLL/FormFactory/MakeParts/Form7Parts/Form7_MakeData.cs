using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.BLL.FormFactory.MakeParts;
using XMIS.Report.Core.DAL.Contract;
using XMIS.Report.Domain;
using XMIS.Report.Domain.Extentions;

namespace XMIS.Report.Core.BLL.FormFactory.MakeParts.Parts
{
    public class Form7_MakeData : DataMaker
    {
        public override List<Func<int>> GetDataFunc(DepartmentDescriptorBase info, DateTime fromDate, DateTime toDate)
        {
            long dayTicks = 864000000000;
            DateTime oneDayAgo = new DateTime(toDate.Ticks - dayTicks);
            DateTime twoDaysAgo = new DateTime(oneDayAgo.Ticks - dayTicks);
            List<Func<int>> result = new List<Func<int>>();

            result.Add(() => info.Rooms.Beds(BedStatus.Free, BedStatus.Broken, BedStatus.Busy).Count);
            result.Add(() => info.Rooms.Beds(BedStatus.Broken).Count);
            result.Add(() => info.Services.FindAll(s => s.InDate < twoDaysAgo && (s.OutDate == null || s.OutDate > oneDayAgo)).Count);
            var inDep = info.Services.FindAll(s => s.Stay.FactStay.InDate == s.InDate);
            result.Add(() => inDep.Count);
            result.Add(() => inDep.Where(s => s.Patient.IsVillager == true).Count());
            result.Add(() => inDep.Where(s => s.Patient.Age <= 14).Count());
            result.Add(() => info.Services.FindAll(s => s.Stay.FactStay.InDate < oneDayAgo && s.Stay.FactStay.InDate > twoDaysAgo).Count);
            result.Add(() => info.Services.FindAll(s => s.Stay.FactStay.OutDate < oneDayAgo && s.Stay.FactStay.OutDate > twoDaysAgo).Count);
            var outDep = info.Services.FindAll(s => s.OutDate > twoDaysAgo && s.OutDate < oneDayAgo);
            result.Add(() => outDep.Count);
            result.Add(() => outDep.Count); // ????
            result.Add(() => info.Services.FindAll(s => s.Patient.Dead != null && s.Patient.Dead > twoDaysAgo && s.Patient.Dead < oneDayAgo).Count);
            var curDep = info.Services.FindAll(s => s.InDate < oneDayAgo && (s.OutDate == null || s.OutDate > toDate));
            result.Add(() => curDep.Count);
            result.Add(() => curDep.Where(s => s.Patient.IsVillager).Count());
            result.Add(() => info.Services.Where(s => s.Patient.Age < 18 && s.Stay.Divergence == 0).Count()); // ??? moms
            result.Add(() => info.Rooms.Where(s => s.Gender == 1 && s.Beds.Where(b => b.Status == BedStatus.Free).Count() > 0).Count());
            result.Add(() => info.Rooms.Where(s => s.Gender == 2 && s.Beds.Where(b => b.Status == BedStatus.Free).Count() > 0).Count()); 
       
            return result;
        }
    }
}
 