using Portfolios.Dto;
using System;
using System.Threading.Tasks;

namespace Portfolios.Service.Interfaces
{
    public interface IPortfolioService
    {
        Task<PortfolioDto> GetLastByIsinAsync(string isin);
        Task<PortfolioDto> GetAsync(string isin, DateTime date);
        Task<ServiceResult> AddAndDeleteExistingAsync(PortfolioDto dto);
        Task<ServiceResult> DeleteAsync(string isin, DateTime date);
    }
}
