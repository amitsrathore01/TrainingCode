using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XupApi.Entities;
using XupApi.Models;

namespace XupApi.Services
{
    public interface ICheckRegisterService
    {
        Task<IEnumerable<CheckRegister>> ViewAllCheck();
        Task<IEnumerable<CheckRegister>> GetCheckByFilter(string name, int frequency);
        Task AddCheck(CheckRegister check);
        Task UpdateCheck(UpdateCheckModel check);
        Task <CheckStatusModel> CheckStatus(string Name, string Url);
    }
}
