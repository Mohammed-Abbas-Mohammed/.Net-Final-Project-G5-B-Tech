using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsB.Shared
{
    public class BaseEntityB
    {
        public int CreatedBy { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
