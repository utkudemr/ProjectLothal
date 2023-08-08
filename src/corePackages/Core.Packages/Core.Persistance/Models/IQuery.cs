using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance.Models
{
    public class IQuery<T>
    {
        IQueryable<T> Query();
    }
}
