using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Model
{
    public class CheckedListItem<T> : ModelBase
    {
        private bool isChecked;
        private T item;

        public CheckedListItem()
        { 
        }

        public CheckedListItem(T item, bool isChecked = false)
        {
            this.item = item;
            this.isChecked = isChecked;
        }

        public T Item
        {
            get { return this.item; }
            set
            {
                this.item = value;
                OnPropertyChanged("Item");
            }
        }

        public bool IsChecked
        {
            get { return this.isChecked; }
            set
            {
                this.isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
    }
}
