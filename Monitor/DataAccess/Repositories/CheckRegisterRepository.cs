using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using XupMonitor.DataAccess.Entities;
using XupMonitor.DataAccess.Repositories.Base;

namespace XupMonitor.DataAccess.Repositories
{
    internal class CheckRegisterRepository : RepositoryBase, ICheckRegisterRepository
    {
        public CheckRegisterRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public IEnumerable<CheckRegister> GetCheckUpdates()
        {
            return Connection.Query<CheckRegister>(
                 $"SELECT * FROM CheckRegister ",
                 transaction: Transaction
             ).ToList();
        }
        public void UpdateCheckSchedule(Guid id,bool IsScheduled)
        {
            Connection.Execute(
               "UPDATE CheckRegister SET IsScheduled = @IsScheduled WHERE Id = @Id",
               param: new { IsScheduled = IsScheduled, Id = id },
               transaction: Transaction
           );
        }

       
    }
}
