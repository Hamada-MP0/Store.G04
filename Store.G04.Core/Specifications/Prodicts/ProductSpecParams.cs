using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Specifications.Prodicts
{
    public class ProductSpecParams
    {
        private string? _search;
        public string? Search
        {
            get { return _search; }
            set { _search = value?.ToLower( ); }
        }
        public string? sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
