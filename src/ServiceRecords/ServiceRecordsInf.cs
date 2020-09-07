using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceRecords
{
    public class ServiceRecordsInf
    {
        public int id { get; set; }
        public decimal sumGetMinusReturn { get; set; }
        public decimal sumGetCashMinusReturn { get; set; }
        public decimal sumGetNonCashMinusReturn { get; set; }
        public string Valuta { get; set; }
        public decimal oldSummaReport { get; set; }
        public decimal oldSummaReportCash { get; set; }
        public decimal oldSummaReportNonCash { get; set; }
        public int typeCashNonCash { get; set; } // 0 - нал, 1 - безнал
        public int typeSZONTime { get; set; } // 1- разовая, 2 - ежемесячная
    }
}
