using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.BLL.FormFactory;
using XMIS.Report.Domain;
using XMIS.Report.Core.DAL.Contract;
using XMIS.Report.Core.DAL;
using Microsoft.Office.Interop.Excel;
using XMIS.Report.Core.Processor;
using XMIS.Report.Core.Processor.Contract;

namespace XMIS.Report.Core.BLL.FormFactory.Forms
{
    public class Form7x : FormFactory
    {
        public Form7x()
        {
            this.tableWidth = 18;
        }

        public override IExcelApp GetApp(List<ServiceDescriptorBase> serv, DateTime fromDate, DateTime toDate)
        {
            var app = new ExcelApp();
            var ioManager = new ExcelDataWriter(app.Cells);

            this.queryProcessor = new QueryProcessor(serv);
            this.fromDate = fromDate;
            this.toDate = toDate;

            var fullBorder = new CellBorder();
            fullBorder.ToDefaultAll();

            this.FillDefaultText(ioManager);
            this.FillData(ioManager);
            this.DrawBorders(ioManager);
            this.FormatText(ioManager);

            return app;
        } 

        private void FillDefaultText(IExcelDataWriter ioManager)
        {
            //header
            ioManager.MoveRight(12);
            ioManager.CreateCell(@"Код форми за ЗКУД", 6);
            ioManager.NextRow();
            ioManager.MoveRight(12);
            ioManager.CreateCell(@"Код закладу за ЗКПО", 6);
            ioManager.NextRow();
            ioManager.NextRow();
            ioManager.CreateCell(@"Міністерство охорони здоров'я України", 2);
            ioManager.CreateCell("", 10, 2);
            ioManager.CreateCell(@"МЕДИЧНА ДОКУМЕНТАЦІЯ", 6);
            ioManager.NextRow();
            ioManager.CreateCell(@"Больница № 6", 2);
            ioManager.MoveRight(10);
            ioManager.CreateCell(@"ФОРМА № 007/о", 6);
            ioManager.NextRow();
            ioManager.CreateCell(@"Листок обліку руху хворих і ліжкового фонду стаціонару за 23.01.03", 18, 2);
            ioManager.NextRow();
            ioManager.NextRow();
            //fields
            ioManager.CreateCell(@"Відділеня", 1, 4);
            ioManager.CreateCell(@"Код", 1, 4);
            ioManager.CreateCell(@"Фактично розгорнуто ліжок, включаючи ліжка, згорнуті на ремонт", 1, 4);
            ioManager.CreateCell(@"В т.ч. ліжка згорнуті на ремонт", 1, 4);
            ioManager.CreateCell(@"Рух хворих за минулу добу", 9);
            ioManager.CreateCell(@"На початок поточного дняу", 5);
            ioManager.NextRow();
            ioManager.MoveRight(4);
            ioManager.CreateCell(@"знаходилось на початок минулої доби", 1, 3);
            ioManager.CreateCell(@"Поступило хворих (без переведених всередені лікарні)", 3, 1);
            ioManager.CreateCell(@"Переведено хворих в середені лікарні", 2, 1);
            ioManager.CreateCell(@"Виписано хворих", 2, 1);
            ioManager.CreateCell(@"Померло", 1, 3);
            ioManager.CreateCell(@"Знаходилось хворих", 2, 1);
            ioManager.CreateCell(@"перебуває матерів при хворих дітях", 1, 3);
            ioManager.CreateCell(@"кількість вільних місць", 2, 1);
            ioManager.NextRow();
            ioManager.MoveRight(5);
            ioManager.CreateCell(@"всього", 1, 2);
            ioManager.CreateCell(@"із них", 2, 1);
            ioManager.CreateCell(@"із інших відділень", 1, 2);
            ioManager.CreateCell(@"в інші відділення", 1, 2);
            ioManager.CreateCell(@"всьогоя", 1, 2);
            ioManager.CreateCell(@"в т.ч. переведені в інші стаціонари", 1, 2);
            ioManager.MoveRight(1);
            ioManager.CreateCell(@"всього", 1, 2);
            ioManager.CreateCell(@"в т.ч. сільских жителів", 1, 2);
            ioManager.MoveRight(1);
            ioManager.CreateCell(@"чоловічихя", 1, 2);
            ioManager.CreateCell(@"жіночих", 1, 2);
            ioManager.NextRow();
            ioManager.MoveRight(6);
            ioManager.CreateCell(@"сільских жителів");
            ioManager.CreateCell(@"дітей до 14 років включно");
            ioManager.NextRow();
            for (int i = 1; i <= this.tableWidth; i++)
                ioManager.CreateCell(i.ToString());
            ioManager.NextRow();
        }

        private void FillData(IExcelDataWriter ioManager)
        {
            var servs = this.conditionController.GetConditionFunction("depatment:", "in", this.fromDate, this.toDate);
            int maxCnt = this.queryProcessor.GetCount(servs);
            int rowsCurr = ioManager.RowIdx;

            for (int rows = 0; rows < maxCnt; rows++)
            {
                //WHAAAT
                //ioManager.CreateCell(this.queryProcessor.DoQuery(servs)[rows].Patient.Age.ToString());
                //ioManager.CreateCell(this.queryProcessor.DoQuery(servs)[rows].Patient.AgeGroup.ToString());
                //ioManager.CreateCell(this.queryProcessor.DoQuery(servs)[rows].Patient.AgeSubGroup.ToString());
                //ioManager.CreateCell(this.queryProcessor.DoQuery(servs)[rows].Patient.Dob.ToShortDateString());
                //ioManager.CreateCell(this.queryProcessor.DoQuery(servs)[rows].Patient.Gender.ToString());
                //ioManager.CreateCell(this.queryProcessor.DoQuery(servs)[rows].Patient.IsAlive.ToString());
                //ioManager.CreateCell(this.queryProcessor.DoQuery(servs)[rows].Patient.IsVillager.ToString());
                //ioManager.CreateCell(this.queryProcessor.DoQuery(servs)[rows].Patient.RegionId.ToString());
                //ioManager.NextRow();
            }
            //all
            ioManager.CreateCell(@"Всего");
            for (int i = 2; i <= this.tableWidth; i++)
                ioManager.CreateCell("result: " + i.ToString());
        }

        private void DrawBorders(IExcelDataWriter ioManager)
        {
            ioManager.AddBorders(1, 13, 6);
            ioManager.AddBorders(2, 13, 6);
            for (int i = 4; i < 13; i++)
                ioManager.AddBorders(i, 1, this.tableWidth);
            //data
            for (int j = 13; j < ioManager.RowIdx - 1; j++)
                ioManager.AddBorders(j, 1, this.tableWidth);
            //all
            ioManager.AddBorders(ioManager.RowIdx, 1, this.tableWidth);
        }

        private void FormatText(IExcelDataWriter ioManager)
        {
            this.AlignText(ioManager);
            this.RotateText(ioManager);
            this.MakeTitles(ioManager);
        }

        private void RotateText(IExcelDataWriter ioManager)
        {
            for (int i = 1; i <= this.tableWidth; i++)
            {
                if (i < 5)
                    ioManager.FormatText(8, i, 90);
                if (i < 6 || i == 13 || i == 16)
                    ioManager.FormatText(9, i, 90);
                if (i < 7 || i > 8)
                    ioManager.FormatText(10, i, 90);
                ioManager.FormatText(11, i, 90);
            }
        }

        private void AlignText(IExcelDataWriter ioManager)
        {
            for (int i = 1; i < ioManager.RowIdx; i++)
                for (int j = 1; j <= this.tableWidth; j++)
                {
                    var halign = XlHAlign.xlHAlignCenter;
                    if (((i == 8) && j < 5)
                        ||((i == 9) && (j < 6 || j != 13 || j != 16))
                        ||((i == 10) && (j < 7 || j > 8)))
                        halign = XlHAlign.xlHAlignLeft;
                    ioManager.FormatText(i, j, halign, XlVAlign.xlVAlignCenter);
                }
                    
            ioManager.FormatText(6, 1, XlHAlign.xlHAlignCenter, XlVAlign.xlVAlignTop);
        }

        private void MakeTitles(IExcelDataWriter ioManager)
        {
            ioManager.FormatText(5, 1, true, false, false);
            ioManager.FormatText(5, 13, true, false, false);
            ioManager.FormatText(6, 1, true, false, false);
        }
    }
}
