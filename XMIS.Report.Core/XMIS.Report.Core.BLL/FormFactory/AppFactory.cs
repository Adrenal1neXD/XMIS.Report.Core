using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

using XMIS.Report.Core.BLL.FormFactory;
using XMIS.Report.Domain;
using XMIS.Report.Core.DAL.Contract;
using XMIS.Report.Core.DAL;
using XMIS.Report.Domain.Extentions;
using XMIS.Report.Core.BLL.FormFactory.MakeParts.Parts;
using XMIS.Report.Core.BLL.FormFactory.MakeParts;


namespace XMIS.Report.Core.BLL.FormFactory
{
    public class AppFactory<TTextMaker, TDataMaker, TBorderMaker, TFormatMaker> 
    {
        private PartMaker BorderMaker;
        private DataMaker DataMaker;
        private PartMaker TextMaker;
        private PartMaker FormatMaker;
        private int tableWidth;
        private List<DepartmentDescriptorBase> descriptor;

        public AppFactory(int tableWidth)
        {
            this.tableWidth = tableWidth;
        }

        public IExcelApp GetApp(List<DepartmentDescriptorBase> descr, DateTime fromDate, DateTime toDate)
        {
            var app = new ExcelApp();
            var ioManager = new ExcelDataWriter(app.Cells);
            this.descriptor = descr;

            this.BorderMaker = (PartMaker)Activator.CreateInstance(typeof(TBorderMaker));
            this.TextMaker = (PartMaker)Activator.CreateInstance(typeof(TTextMaker));
            this.FormatMaker = (PartMaker)Activator.CreateInstance(typeof(TFormatMaker));
            this.DataMaker = (DataMaker)Activator.CreateInstance(typeof(TDataMaker));

            var fullBorder = new CellBorder();
            fullBorder.ToDefaultAll();
            var funcList = new List<List<Func<int>>>();
            foreach (DepartmentDescriptorBase d in descr)
                funcList.Add(this.DataMaker.GetDataFunc(d, fromDate, toDate));

            this.TextMaker.Do(ioManager);
            this.InsertData(ioManager, funcList.ToArray());
            this.BorderMaker.Do(ioManager);
            this.FormatMaker.Do(ioManager);

            return app;
        }

        private void InsertData(IExcelDataWriter writer, List<Func<int>>[] func)
        {
            int[] allSum = new int[this.tableWidth - 2];
            int[] localSum = new int[this.tableWidth - 2];
            int[] thisSum = new int[this.tableWidth - 2];
            int xRow = writer.RowIdx;
            this.descriptor.Sort(delegate(DepartmentDescriptorBase x, DepartmentDescriptorBase y)
            {
                if (x.DepertmantType == null && y.DepertmantType == null) return 0;
                else if (x.DepertmantType == null) return -1;
                else if (y.DepertmantType == null) return 1;
                else return x.DepertmantType.CompareTo(y.DepertmantType);
            });
            string prevDep = string.Empty;
            for (int rows = 0; rows < this.descriptor.Count; rows++)
            {
                var info = this.descriptor[rows];

                if (prevDep != info.DepertmantType)
                {
                    allSum = this.SequenceSum(allSum, localSum);
                    for (int col = 0; col < localSum.Length; col++)
                    {
                        if (prevDep != string.Empty)
                            writer.SetValue(xRow - 1, col + 3, localSum[col].ToString());
                        if (col == 0)
                            writer.CreateCell(info.DepertmantType);
                        else
                            writer.CreateCell();
                    }
                    writer.NextRow();
                    xRow = writer.RowIdx;
                    prevDep = info.DepertmantType;
                }

                writer.CreateCell(info.DepartmentName);
                writer.CreateCell(info.DepartmenID.ToString());

                for (int col = 0; col < thisSum.Length; col++)
                    try
                    {
                        writer.CreateCell((thisSum[col] = func[rows][col].Invoke()).ToString());
                    }
                    catch 
                    {
                        writer.CreateCell("0");
                    }
                writer.NextRow();

                localSum = this.SequenceSum(localSum, thisSum);
            }

            for (int col = 0; col < localSum.Length; col++)
                writer.SetValue(xRow - 1, col + 3, localSum[col].ToString());
            allSum = this.SequenceSum(allSum, localSum);

            writer.CreateCell("Всего");
            writer.CreateCell();
            for (int col = 0; col < allSum.Length; col++)
                writer.CreateCell(allSum[col].ToString());
        }

        private int[] SequenceSum(int[] src1, int[] src2)
        {
            var res = new int[src1.Length];
            for (int i = 0; i < src1.Length; i++)
                res[i] = src1[i] + src2[i];
            return res;
        }
    }
}
