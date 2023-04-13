using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bai4
{
    [Serializable()]
    public class Student
    {
        public string name { get; set; }
        public string id { get; set; }
        public string phone { get; set; }
        public string score1 { get; set; }
        public string score2 { get; set; }
        public string score3 { get; set; }
        public string avg { get; set; }
    }
}
