using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace XupMonitor.DataAccess.Repositories.Base
{
    internal abstract class RepositoryBase
    {
        protected IDbTransaction Transaction { get; }
        protected IDbConnection Connection => Transaction.Connection;

        protected RepositoryBase(IDbTransaction transaction) => Transaction = transaction;
    }
}
