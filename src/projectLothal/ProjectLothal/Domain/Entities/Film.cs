using Core.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Film : Entity<Guid>
    {
        public string Title { get; set; }
        public string Producer { get; set; }

        public Film()
        {
            Characters = new HashSet<Character>();
        }
        public Film(string title, string producer) : this()
        {
            Title = title;
            Producer = producer;
        }

        public virtual ICollection<Character>? Characters { get; set; }
    }
}
