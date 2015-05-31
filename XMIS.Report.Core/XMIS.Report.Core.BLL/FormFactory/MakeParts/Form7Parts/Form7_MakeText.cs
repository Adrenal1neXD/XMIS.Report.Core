using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.BLL.FormFactory.MakeParts;
using XMIS.Report.Core.DAL.Contract;

namespace XMIS.Report.Core.BLL.FormFactory.MakeParts.Parts
{
    public class Form7_MakeText : PartMaker
    {
        public override void Do(IExcelDataWriter writer)
        {
            //header
            writer.MoveRight(12);
            writer.CreateCell(@"Код форми за ЗКУД", 6);
            writer.NextRow();
            writer.MoveRight(12);
            writer.CreateCell(@"Код закладу за ЗКПО", 6);
            writer.NextRow();
            writer.NextRow();
            writer.CreateCell(@"Міністерство охорониздоров'я України", 2);
            writer.CreateCell("", 10, 2);
            writer.CreateCell(@"МЕДИЧНА ДОКУМЕНТАЦІЯ", 6);
            writer.NextRow();
            writer.CreateCell(@"Больница № 6", 2);
            writer.MoveRight(10);
            writer.CreateCell(@"ФОРМА № 007/о", 6);
            writer.NextRow();
            writer.CreateCell(string.Format("Листок обліку руху хворих і ліжкового фонду стаціонару за {0}", "_"), 18, 2);
            writer.NextRow();
            writer.NextRow();
            //fields
            writer.CreateCell(@"Відділеня", 1, 4);
            writer.CreateCell(@"Код", 1, 4);
            writer.CreateCell(@"Фактично розгорнуто ліжок,включаючи ліжка, згорнуті на ремонт", 1, 4);
            writer.CreateCell(@"В т.ч. ліжка згорнуті на ремонт", 1, 4);
            writer.CreateCell(@"Рух хворих за минулу добу", 9);
            writer.CreateCell(@"На початок поточного дняу", 5);
            writer.NextRow();
            writer.MoveRight(4);
            writer.CreateCell(@"знаходилось на початокминулої доби", 1, 3);
            writer.CreateCell(@"Поступило хворих (безпереведених всередені лікарні)", 3, 1);
            writer.CreateCell(@"Переведено хворих в середені лікарні", 2, 1);
            writer.CreateCell(@"Виписано хворих", 2, 1);
            writer.CreateCell(@"Померло", 1, 3);
            writer.CreateCell(@"Знаходилось хворих", 2, 1);
            writer.CreateCell(@"перебуває матерів при хворих дітях", 1, 3);
            writer.CreateCell(@"кількість вільних місць", 2, 1);
            writer.NextRow();
            writer.MoveRight(5);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"із них", 2, 1);
            writer.CreateCell(@"із інших відділень", 1, 2);
            writer.CreateCell(@"в інші відділення", 1, 2);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"в т.ч. переведені в іншістаціонари", 1, 2);
            writer.MoveRight(1);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"в т.ч. сільских жителів", 1, 2);
            writer.MoveRight(1);
            writer.CreateCell(@"чоловічихя", 1, 2);
            writer.CreateCell(@"жіночих", 1, 2);
            writer.NextRow();
            writer.MoveRight(6);
            writer.CreateCell(@"сільских жителів");
            writer.CreateCell(@"дітей до 14 років включно");
            writer.NextRow();

            for (int i = 1; i <= writer.MaxColumnIdx; i++)
                writer.CreateCell(i.ToString());
            writer.NextRow();
        }
    }
}
