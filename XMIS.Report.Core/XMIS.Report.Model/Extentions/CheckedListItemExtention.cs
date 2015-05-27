using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XMIS.Report.Model;

namespace XMIS.Report.Model.Extentions
{
    public static class CheckedListItemExtention
    {
        public static List<T> GetChecked<T>(this List<CheckedListItem<T>> list)
        {
            return list.Where(s => s.IsChecked == true).Select(i => i.Item).ToList();
        }
    }
}
