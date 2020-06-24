using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using XupMonitor.DataAccess.Entities;
using XupMonitor.DataAccess.Repositories.Base;

namespace XupMonitor.DataAccess.Repositories
{
    internal class CheckRunRepository : RepositoryBase, ICheckRunRepository
    {
        public CheckRunRepository(IDbTransaction transaction): base(transaction)
        {
        }
        public void AddCheckRun(CheckRun data)
        {
            Connection.Execute(
             "INSERT INTO CheckRun(Id,CheckId,Status,RunTime,LastRunOn)" +
             "    VALUES(@Id, @CheckId, @Status, @RunTime,@LastRunOn)",
                param: new { Id = Guid.NewGuid(), CheckId= data.CheckId, 
                             Status=data.Status,RunTime=data.RunTime, 
                             LastRunOn=data.LastRunOn },
                transaction: Transaction
        );
        }
    }
}
