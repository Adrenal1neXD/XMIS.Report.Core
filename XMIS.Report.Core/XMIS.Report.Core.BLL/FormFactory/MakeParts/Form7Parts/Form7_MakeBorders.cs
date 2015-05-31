using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.BLL.FormFactory.MakeParts;
using XMIS.Report.Core.DAL.Contract;

namespace XMIS.Report.Core.BLL.FormFactory.MakeParts.Parts
{
    public class Form7_MakeBorders : PartMaker
    {
        public override void Do(IExcelDataWriter writer)
        {
            writer.AddBorders(1, 13, 6);
            writer.AddBorders(2, 13, 6);
            for (int i = 4; i < 13; i++)
                writer.AddBorders(i, 1, writer.MaxColumnIdx);
            //data
            for (int j = 13; j < writer.RowIdx; j++)
                writer.AddBorders(j, 1, writer.MaxColumnIdx);
            //all
            writer.AddBorders(writer.RowIdx, 1, writer.MaxColumnIdx);
        }
    }
}
