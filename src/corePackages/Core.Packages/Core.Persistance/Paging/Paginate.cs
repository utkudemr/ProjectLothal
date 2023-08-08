using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistance.Paging
{
    public class Paginate<T>
    {
        public Paginate()
        {
            Items = new List<T>();
        }

        public int PageIndex { get; set; } // Selected Index
        public int Size { get; set; }  // Page Size
        public int Count { get; set; } // Record count
        public int Pages { get; set; }  // Page Count
        public IList<T> Items { get; set; }
        public bool HasPrevious => PageIndex > 0;
        public bool HasNext => PageIndex + 1 < Pages; //
    }
}
