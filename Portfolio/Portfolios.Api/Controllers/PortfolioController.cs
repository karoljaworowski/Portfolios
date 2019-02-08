using Microsoft.AspNetCore.Mvc;
using Portfolios.Common.Enums;
using Portfolios.Dto;
using Portfolios.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Portfolios.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            this.portfolioService = portfolioService;
        }

        [HttpGet("{isin}/date/{date}")]
        public async Task<PortfolioDto> Get(string isin, DateTime date)
        {
            return await portfolioService.GetAsync(isin, date);
        }

        [HttpGet("{isin}")]
        public async Task<PortfolioDto> GetLast(string isin)
        {
            return await portfolioService.GetLastByIsinAsync(isin);
        }

        [HttpGet("{isin}/date/{date}/aggregate/{aggregationType}")]
        public async Task<object> GetAggregated(string isin, DateTime date, AggregationType aggregationType)
        {
            return await portfolioService.GetAggregatedPositionsAsync(isin, date, aggregationType);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PortfolioDto portfolio)
        {
            var result = await portfolioService.AddAndDeleteExistingAsync(portfolio);
            if (result.ResultType == Common.Enums.ServiceResultType.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPut("{isin}")]
        public async Task<IActionResult> Put(string isin, [FromBody] PortfolioDto portfolio)
        {
            if (isin != portfolio.ISIN)
            {
                return BadRequest($"isin code {isin} and portfolio ISIN code {portfolio.ISIN} are not equal");
            }

            var result = await portfolioService.AddAndDeleteExistingAsync(portfolio);
            if (result.ResultType == Common.Enums.ServiceResultType.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("{isin}/date/{date}")]
        public async Task<IActionResult> Delete(string isin, DateTime date)
        {
            var result = await portfolioService.DeleteAsync(isin, date);
            if (result.ResultType == Common.Enums.ServiceResultType.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}
