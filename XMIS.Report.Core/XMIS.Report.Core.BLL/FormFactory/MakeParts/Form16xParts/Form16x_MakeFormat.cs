using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.BLL.FormFactory.MakeParts;
using XMIS.Report.Core.DAL.Contract;

namespace XMIS.Report.Core.BLL.FormFactory.MakeParts.Parts
{
    public class Form16x_MakeFormat : PartMaker
    {
        private IExcelDataWriter writer;

        public override void Do(IExcelDataWriter writer)
        {
            this.writer = writer;

            this.RotateText();
            this.AlignText();
            this.MakeTitles();
        } 

        private void RotateText()
        {
            for (int i = 1; i <= writer.MaxColumnIdx; i++)
            {
                writer.FormatText(11, i, 90);
            }
        }

        private void AlignText()
        {
            for (int i = 1; i < writer.RowIdx; i++)
                for (int j = 1; j <= writer.MaxColumnIdx; j++)
                {
                    var halign = XlHAlign.xlHAlignCenter;
                    if (i > 12 && j == 1)
                        halign = XlHAlign.xlHAlignLeft;
                    writer.FormatText(i, j, halign, XlVAlign.xlVAlignCenter);
                }

            writer.FormatText(6, 1, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop);
        }

        private void MakeTitles()
        {
            writer.FormatText(5, 1, true, false, false);
            writer.FormatText(5, 19, true, false, false);
            writer.FormatText(6, 1, true, false, false);
        }
    }
}
