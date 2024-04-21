using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamPy.SDK.Models.Domain
{
    public class Contents<T>
    {
        public List<T> Content { get; set; }

        public int totalElements { get; set; }

        public int totalPages { get; set; }

        public bool last { get; set; }

        public bool first { get; set; }

        public int numberOfElements { get; set; }

        public int size { get; set; }

        public int number { get; set; }

        public bool empty { get; set; }

        public Pageable pageable { get; set; }

        public Sort sort { get; set; }

        public class Pageable
        {
            public int pageNumber { get; set; }

            public int pageSize { get; set; }

            public int offset { get; set; }

            public bool unpaged { get; set; }

            public bool paged { get; set; }

            public Sort sort { get; set; }
        }


        public class Sort
        {
            public bool sorted { get; set; }

            public bool unsorted { get; set; }

            public bool empty { get; set; }


        }
    }
}
