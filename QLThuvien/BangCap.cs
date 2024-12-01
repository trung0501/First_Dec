using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLThuvien
{
    class BangCap
    {
        Database db;
        public BangCap()
        {
            db = new Database();
        }
        public DataTable LayBangcap()
        {
            string strSQL = "Select * from bangcap";
            DataTable dt = db.Execute(strSQL);
            return dt;
        }
    }
}
