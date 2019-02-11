using System;
using System.Collections.Generic;

namespace IBSANBR.Extensions
{
    public class SearchResult<T>
    {
        public int CurrentPage { get; set; }
        public string Query { get; set; }
        public List<T> Results { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling((decimal)TotalResults / PageSize);
        public int TotalResults { get; set; }
        public virtual int Next() => this.CurrentPage >= this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
        public virtual int Previous() => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;
    }
}


