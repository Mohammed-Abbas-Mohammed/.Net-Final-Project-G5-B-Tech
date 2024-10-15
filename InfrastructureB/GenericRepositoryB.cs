using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationB.Contracts_B;

namespace InfrastructureB
{
    public class GenericRepositoryB<T>: IGenericRepositoryB<T> where T :class
    {
    }
}
