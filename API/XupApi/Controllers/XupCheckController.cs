using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using XupApi.Entities;
using XupApi.Helpers;
using XupApi.Models;
using XupApi.Services;

namespace XupApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class XupCheckController : Controller
    {

        private readonly ICheckRegisterService _checkregisterService;
        private IMapper _mapper;
        public XupCheckController(ICheckRegisterService checkregisterService, IMapper mapper)
        {
            _checkregisterService = checkregisterService;
            _mapper = mapper;
        }
                     

        [HttpGet("ViewAllCheck")]
        public async Task<IActionResult> ViewAllCheck() 
        {
            try
            {           
         
            var result = await _checkregisterService.ViewAllCheck();
            var model = _mapper.Map<IList<CheckRegisterModel>>(result);

            return Ok(model);
            }
            catch (AppException ex)
            {                
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetCheckByFilter")]
        public async Task<IActionResult> GetCheckByFilter(string name,int frequency=0)
        {
            try
            {
                if (frequency<0 || frequency>59 || frequency.ToString().Length>2)
                {
                    return BadRequest(new { message = "Frequency should be 2 characters int format and range between 1 to 59" });
                }
                 
               var result= await _checkregisterService.GetCheckByFilter(name,frequency);
               var model = _mapper.Map<IList<CheckRegisterModel>>(result);
               
                return Ok(model);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetCheckStatus")]
        public async Task<IActionResult> GetCheckStatus(string name,string url)
        {
            try
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(url))
                {
                    return BadRequest(new { message = "Name and Url cannot be blank" });
                }

              var result=  await _checkregisterService.CheckStatus(name,url);

                if(string.IsNullOrEmpty(result.Name))
                {
                    return Ok("There is no record exists.");
                }
              return Ok(result);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("AddCheck")]
        public async Task<IActionResult> AddCheck([FromBody]AddCheckModel newcheck)        
        {
            try
            {
                if (newcheck.FrequencyType!="mm" && newcheck.FrequencyType != "hh")
                {
                    return BadRequest(new { message = "FrequencyType should be 2 characters format eg: hh or mm" });
                }

                if (newcheck.FrequencyType=="hh" && newcheck.Frequency>24)
                {
                    return BadRequest(new { message = "Frequency for FrequencyType 'hh' can not be greater than 24" });
                }
                

                var model =  _mapper.Map<CheckRegister>(newcheck);
                await _checkregisterService.AddCheck(model);
                return Ok();

            }
            catch (AppException ex)
            {              
                return BadRequest(new { message = ex.Message });
            }           
        }

        [HttpPut("UpdateCheck")]
        public async Task<IActionResult> UpdateCheck([FromBodyAttribute] UpdateCheckModel updatedata)
        {
            try
            {               
                await _checkregisterService.UpdateCheck(updatedata);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

    }
}