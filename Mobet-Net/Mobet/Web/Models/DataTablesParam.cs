using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mobet.Authorization.Controllers.Shared
{
    public class DataTablesParam<TEntity> where TEntity : class
    {
        public DataTablesParam()
        {
            this.Start = 1;
            this.Length = 10;
            this.Columns = new List<string>();
            this.Search = new List<string>();
            this.Order = new List<string>();

            this.Data = default(TEntity);
        }

        public TEntity Data { get; set; }

        public int Start { get; set; }
        public int Length { get; set; }
        public List<string> Columns { get; set; }
        public List<string> Search { get; set; }
        public List<string> Order { get; set; }
    }
}