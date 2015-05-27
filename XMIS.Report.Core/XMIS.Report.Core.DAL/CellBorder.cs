using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Core.DAL
{
    public sealed class CellBorder
    {
        public double Top { get; set; }
        public double Left { get; set; }
        public double Bottom { get; set; }
        public double Right { get; set; }
        public double Weight { get; set; }

        public CellBorder(double left, double top, double right, double bottom)
        {
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
            this.Left = left;
            this.Weight = 2;
        }

        public CellBorder() : this(0,0,0,0) { }

        public CellBorder(bool left, bool top, bool right, bool bottom) : this(
            left ? 2 : 0, 
            top ? 2 : 0, 
            right ? 2 : 0, 
            bottom ? 2 : 0)
        {
            if (this.Weight != 1)
                this.ToDefault();
        }

        public void ToDefault()
        {
            this.Top = this.Top == 0 ? 0 : this.Weight;
            this.Left = this.Left == 0 ? 0 : this.Weight;
            this.Bottom = this.Bottom == 0 ? 0 : this.Weight;
            this.Right = this.Right == 0 ? 0 : this.Weight;
        }

        public void ToDefaultAll()
        {
            this.Top = this.Weight;
            this.Left = this.Weight;
            this.Bottom = this.Weight;
            this.Right = this.Weight;
        }

        public void ToZero()
        {
            this.Top = 0;
            this.Bottom = 0;
            this.Left = 0;
            this.Right = 0;
        }

        public bool IsZero()
        {
            return ((int)this.Top | (int)this.Bottom | (int)this.Left | (int)this.Right) == 0;
        }
    }
}
