using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditScoresController : Controller
    {


        ICreditScoreService _creditScoreService;


        public CreditScoresController(ICreditScoreService creditScoreService)
        {
            _creditScoreService = creditScoreService;
        }


        [HttpPost("add")]
        public IActionResult Add(CreditScore creditScore)
        {
            var result = _creditScoreService.Add(creditScore);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpPost("delete")]
        public IActionResult Delete(CreditScore creditScore)
        {
            var result = _creditScoreService.Delete(creditScore);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpPost("update")]
        public IActionResult Update(CreditScore creditScore)
        {
            var result = _creditScoreService.Update(creditScore);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _creditScoreService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


    }
}