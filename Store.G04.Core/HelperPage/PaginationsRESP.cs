using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.HelperPage
{
    public class PaginationsRESP<TEntity>
    {
        public PaginationsRESP(int pageIndex, int pageSize, int count, IEnumerable<TEntity> data)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.count = count;
            Data = data;
        }

        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int count { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
}
