using ModelsB.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationB.Contracts_B;

namespace InfrastructureB
{
    public class GenericRepositoryWithLogging<T> : IGenericRepositoryB<T> where T : BaseEntityB
    {
    }
}
