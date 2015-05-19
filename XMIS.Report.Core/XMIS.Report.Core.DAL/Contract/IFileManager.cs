using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMIS.Report.Core.DAL.Contract
{
    public interface IFileManager
    {
        void Open(string path);
        DataTable Read();
        void Write(int rCnt, int cCnt, string data);
        void Write(int rCnt, string[] data);
        void SaveAs(string fullFilePath);
    }
}
