using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XupApi.Entities;
using XupApi.Helpers;
using XupApi.Models;

namespace XupApi.Services
{
    public class CheckRegisterService : ICheckRegisterService
    {
        private DataContext _context;
        public CheckRegisterService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CheckRegister>> ViewAllCheck()
        {
           
            return await _context.CheckRegister.ToListAsync();
        }
        public async Task AddCheck(CheckRegister check)
        {
            if (_context.CheckRegister.Any(x => x.Url == check.Url && x.Name == check.Name))
                throw new AppException("Name \"" + check.Name + "\" and Url \"" + check.Url + "\" is already exists.");


            check.IsScheduled = false;
            check.IsActive = true;
            check.Id = Guid.NewGuid();
            check.CreatedOn = DateTime.UtcNow;
            _context.CheckRegister.Add(check);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCheck(UpdateCheckModel check)
        {

            var result = _context.CheckRegister.Where(x => x.Name == check.Name && x.Url == check.Url);
            if (!result.Any())
                throw new AppException("Name \"" + check.Name + "\" and Url \"" + check.Url + "\"  does not exists.");

            result.FirstOrDefault(x => x.Name == check.Name && x.Url == check.Url).IsActive = check.IsActive;

            if(!check.IsActive)//disable job
            {
                result.FirstOrDefault(x => x.Name == check.Name && x.Url == check.Url).IsScheduled = false;
            }
          
            _context.CheckRegister.Update(result.FirstOrDefault());
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<CheckRegister>> GetCheckByFilter(string name, int frequency)
        {
            var result = await  _context.CheckRegister.Where(x => x.Name.Contains(name) || 
                                                     x.Frequency.ToString().Contains(frequency.ToString())).ToListAsync();
            return result;
        }

        public async Task<CheckStatusModel> CheckStatus(string Name, string Url)
        {
            
           var result= await ( from cr in _context.CheckRegister
                        join  crun in _context.CheckRun
                         on cr.Id equals crun.CheckRegister.Id
                          where cr.Name == Name && cr.Url == Url
                             orderby crun.LastRunOn descending
                          select new{ Status=crun.Status,                                                                      
                                      RunTime=TimeSpan.Parse(crun.RunTime),
                                      LastRunOn=crun.LastRunOn}).ToListAsync();

            var checkStatus = new CheckStatusModel();

            if (result.Any())
            { 
          
               var lastRun = result.Where(x => x.Status != result.FirstOrDefault().Status).FirstOrDefault();
                checkStatus.Name = Name;
                checkStatus.Url = Url;
                checkStatus.Status = result.FirstOrDefault().Status;
                checkStatus.StatusSince = lastRun == null ? result.FirstOrDefault().LastRunOn : lastRun.LastRunOn;
                checkStatus.AveragResoponseTime = result.Average(x => x.RunTime.TotalMilliseconds).ToString();
           
            }

            return checkStatus;
        }
    }
}
