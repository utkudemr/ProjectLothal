using Core.Persistance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Planet : Entity<Guid>
    {
        public Planet()
        {
            Characters = new HashSet<Character>();
        }
        public Planet(string name, string imageUrl) : this()
        {
            Name = name;
            ImageUrl = imageUrl;
        }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
    }
}
