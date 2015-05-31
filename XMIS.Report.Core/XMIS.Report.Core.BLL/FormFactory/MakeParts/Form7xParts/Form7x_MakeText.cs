using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.BLL.FormFactory.MakeParts;
using XMIS.Report.Core.DAL.Contract;

namespace XMIS.Report.Core.BLL.FormFactory.MakeParts.Parts
{
    public class Form7x_MakeText : PartMaker
    {
        public override void Do(IExcelDataWriter writer)
        {
            //header
            writer.MoveRight(25);
            writer.CreateCell(@"Код форми за ЗКУД", 4);
            writer.NextRow();
            writer.MoveRight(25);
            writer.CreateCell(@"Код закладу за ЗКПО", 4);
            writer.NextRow();
            writer.NextRow();
            writer.CreateCell(@"Міністерство охорони здоров'я України", 3);
            writer.CreateCell("", 22, 2);
            writer.CreateCell(@"МЕДИЧНА ДОКУМЕНТАЦІЯ", 4);
            writer.NextRow();
            writer.CreateCell(@"Больница № 6", 3);
            writer.MoveRight(22);
            writer.CreateCell(@"ФОРМА № 007/У", 4);
            writer.NextRow();
            writer.CreateCell(@"Листок обліку руху хворих і ліжкового фонду стаціонару за 23.01.03", 29, 2);
            writer.NextRow();
            writer.NextRow();
            //fields
            writer.CreateCell(@"Профіль ліжок", 1, 4);
            writer.CreateCell(@"Код", 1, 4);
            writer.CreateCell(@"Фактично розгорнуто ліжок, включно ліжка, згорнуті на ремонт", 1, 4);
            writer.CreateCell(@"В т.ч. ліжка згорнуті на ремонт", 1, 4);
            writer.CreateCell(@"Рух хворих за минулу добу", 12);
            writer.CreateCell(@"На початок поточного дняу", 2);
            writer.CreateCell(@"З початку місяця", 11);
            writer.NextRow();
            writer.MoveRight(4);
            writer.CreateCell(@"перебувало хворих на початок минулої доби", 1, 3);
            writer.CreateCell(@"Поступило хворих (без переведених всередені лікарні)", 3, 1);
            writer.CreateCell(@"Переведено хворих в середені лікарні", 3, 1);
            writer.CreateCell(@"Виписано хворих", 3, 1);
            writer.CreateCell(@"Померло", 2, 1);
            writer.CreateCell(@"Перебувало хворих", 2, 1);
            writer.CreateCell(@"Поступило хворих", 2, 1);
            writer.CreateCell(@"Переведено", 3, 1);
            writer.CreateCell(@"Виписано хворих", 1, 3);
            writer.CreateCell(@"Померло", 1, 3);
            writer.CreateCell(@"План койкоднів", 1, 3);
            writer.CreateCell(@"Виконувано койкоднів", 2, 1);
            writer.CreateCell(@"Виконувано койкоднів(%)", 1, 3);
            writer.NextRow();
            writer.MoveRight(5);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"із них", 2, 1);
            writer.CreateCell(@"із інших відділень", 1, 2);
            writer.CreateCell(@"в інші відділення", 1, 2);
            writer.CreateCell(@"у тому числі до суток", 1, 2);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"в т.ч. переведені в інші стаціонари", 1, 2);
            writer.CreateCell(@"у тому числі до суток", 1, 2);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"у тому числі до суток", 1, 2);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"в т.ч. сільских жителів", 1, 2);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"сільских жителів", 1, 2);
            writer.CreateCell(@"із інших відділень", 1, 2);
            writer.CreateCell(@"в інші відділення", 1, 2);
            writer.CreateCell(@"у тому числі до суток", 1, 2);
            writer.MoveRight(3);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"сільскими жителями", 1, 2);
            writer.NextRow();
            writer.MoveRight(6);
            writer.CreateCell(@"сільских жителів");
            writer.CreateCell(@"дітей до 17 років включно");
            writer.NextRow();
            for (int i = 1; i <= writer.MaxColumnIdx; i++)
                writer.CreateCell(i.ToString());
            writer.NextRow();
        }
    }
}
