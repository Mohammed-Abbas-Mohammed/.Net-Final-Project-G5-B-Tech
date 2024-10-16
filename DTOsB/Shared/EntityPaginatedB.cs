using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.Shared
{
    public class EntityPaginatedB<T>
    {
        public List<T> Data { get; set; }
        public int CountAllItems { get; set; }
    }
}
