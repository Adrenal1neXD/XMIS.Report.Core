using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Core.BLL.FormFactory.MakeParts;
using XMIS.Report.Core.DAL.Contract;

namespace XMIS.Report.Core.BLL.FormFactory.MakeParts.Parts
{
    public class Form16x_MakeText : PartMaker
    {
        public override void Do(IExcelDataWriter writer)
        {
            //header
            writer.MoveRight(19);
            writer.CreateCell(@"Код форми за ЗКУД", 4);
            writer.NextRow();
            writer.MoveRight(19);
            writer.CreateCell(@"Код закладу за ЗКПО", 4);
            writer.NextRow();
            writer.NextRow();
            writer.CreateCell(@"Міністерство охорони здоров'я України", 3);
            writer.CreateCell("", 16, 2);
            writer.CreateCell(@"МЕДИЧНА ДОКУМЕНТАЦІЯ", 4);
            writer.NextRow();
            writer.CreateCell(@"Больница № 6", 3);
            writer.MoveRight(16);
            writer.CreateCell(@"ФОРМА № 016/У", 4);
            writer.NextRow();
            writer.CreateCell(@"Листок обліку руху хворих і ліжкового фонду стаціонару за 23.01.03", 23);
            writer.NextRow();
            writer.CreateCell(@"за период с 01.11.2005 по 01.03.2006", 23);
            writer.NextRow();
            //fields
            writer.CreateCell(@"Профіль ліжок", 1, 4);
            writer.CreateCell(@"Код", 1, 4);
            writer.CreateCell(@"Фактично розгорнуто ліжок, включаючи ліжка, згорнуті на ремонт", 1, 4);
            writer.CreateCell(@"В т.ч. ліжка згорнуті на ремонт", 1, 4);
            writer.CreateCell(@"перебувало хворихна початок звітнього періоду", 1, 4);
            writer.CreateCell(@"Поступило хворих (без переведених всередені лікарні)", 4, 2);
            writer.CreateCell(@"Переведено хворих всередені лікарні", 3, 2);
            writer.CreateCell(@"Виписано хворих", 3, 2);
            writer.CreateCell(@"Померло", 2, 2);
            writer.CreateCell(@"Перебувало хворих", 2, 2);
            writer.CreateCell(@"Виконувано койкоднів ", 2, 2);
            writer.CreateCell(@"Число койкоднів згортання на ремонт", 1, 4);
            writer.CreateCell(@"Проведено койкоднів матерями при дітях ", 1, 4);
            writer.NextRow();
            writer.NextRow();
            writer.MoveRight(4);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"із них", 3, 1);
            writer.CreateCell(@"із інших відділень", 1, 2);
            writer.CreateCell(@"в інші відділення", 1, 2);
            writer.CreateCell(@"у тому числі до суток", 1, 2);
            writer.CreateCell(@"в т.ч. переведені в інші стаціонари", 1, 2);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"у т.ч. переведені в інші стаціонари", 1, 2);
            writer.CreateCell(@"у тому числі до суток", 1, 2);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"у тому числі до суток", 1, 2);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"в т.ч. сільских жителів", 1, 2);
            writer.CreateCell(@"всього", 1, 2);
            writer.CreateCell(@"сільскими жителями", 1, 2);
            writer.NextRow();
            writer.MoveRight(5);
            writer.CreateCell(@"сільских жителів");
            writer.CreateCell(@"дітей до 17 років включно");
            writer.CreateCell(@"дітей до 14 років включно");
            writer.NextRow();

            for (int i = 1; i <= writer.MaxColumnIdx; i++)
                writer.CreateCell(i.ToString());
            writer.NextRow();
        }
    }
}
