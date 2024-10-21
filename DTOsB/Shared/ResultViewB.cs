using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.Shared
{
    public class ResultViewB<T>
    {
        public T Entity { get; set; }
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
    }
}
