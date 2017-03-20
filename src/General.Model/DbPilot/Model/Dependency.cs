using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralModel.Model
{
    class Dependency
    {
        public string ChildName { get; set; }
        public string ParentName { get; set; }
        public int Level { get; set; }
    }
}
