using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWebApp
{
    public class Search
    {
        public Media Model = new Media();
        public string? Category { get; set; }
        public List<Media> MediaList { get; set; } = new List<Media>();
    }
}
