using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericRepoWebApp.Data.DataAccess.Infrastructure;

namespace GenericRepoWebApp.Data.DataAccess.SqlServer {

    public class BarReposiltory : 
        GenericRepository<FooBarEntities1, Bar>, IBarRepository  {

        public Bar GetSingle(int barId) {

            var query = Context.Bars.FirstOrDefault(x => x.BarId == barId);
            return query;
        }
    }
}