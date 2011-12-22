using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenericRepoWebApp.Data.DataAccess.SqlServer;

namespace GenericRepoWebApp.Data.DataAccess.Infrastructure {

    public interface IFooRepository : IGenericRepository<Foo> {

        Foo GetSingle(int fooId);
    }
}
