using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Responses
{
    public class GetListResponse<T>: BasePageableModel
    {
        private IList<T> _items { get; set; }

        public IList<T> Items
        {
            get { return _items ?? new List<T>(); }
            set { _items = value; }
        }
    }
}
