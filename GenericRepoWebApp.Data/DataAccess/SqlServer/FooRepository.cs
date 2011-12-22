using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericRepoWebApp.Data.DataAccess.Infrastructure;

namespace GenericRepoWebApp.Data.DataAccess.SqlServer {

    public class FooRepository :
        GenericRepository<FooBarEntities1, Foo>, IFooRepository {

        public Foo GetSingle(int fooId) {

            var query = GetAll().FirstOrDefault(x => x.FooId == fooId);
            return query;
        }
    }
}
