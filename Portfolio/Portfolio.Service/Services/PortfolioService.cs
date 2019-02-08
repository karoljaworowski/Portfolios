using Microsoft.EntityFrameworkCore;
using Portfolios.Common.Validators;
using Portfolios.Dto;
using Portfolios.Repository.Models;
using Portfolios.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolios.Service.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IRepository<Portfolio> portfolioRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly log4net.ILog logger;

        public PortfolioService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            portfolioRepository = unitOfWork.GetRepository<Portfolio>();
            logger = log4net.LogManager.GetLogger(GetType());
        }

        public async Task<ServiceResult> AddAndDeleteExistingAsync(PortfolioDto portfolioDto)
        {
            ServiceResult result = ValidatePortfolio(portfolioDto);
            try
            {
                if (result.ResultType != Common.Enums.ServiceResultType.Success)
                {
                    return result;
                }

                Portfolio portfolio = await portfolioRepository.GetFirstOrDefaultAsync(
                    predicate: p => p.ISIN == portfolioDto.ISIN && p.Date == portfolioDto.Date.Date);

                if (portfolio != null)
                {
                    portfolioRepository.Delete(portfolio.Id);
                }

                portfolio = new Portfolio
                {
                    ISIN = portfolioDto.ISIN,
                    Date = portfolioDto.Date.Date,
                    Currency = portfolioDto.Currency.ToUpper(),
                    MarketValue = portfolioDto.Positions.Select(p => p.MarketValue).Sum(),
                    Positions = new List<Position>()
                };

                await portfolioRepository.InsertAsync(portfolio);

                foreach (var positionDto in portfolioDto.Positions)
                {
                    result = ValidatePosition(positionDto);
                    if (result.ResultType != Common.Enums.ServiceResultType.Success)
                    {
                        return result;
                    }
                    Position position = new Position
                    {
                        ISIN = positionDto.ISIN,
                        Country = positionDto.Country.ToUpper(),
                        Currency = positionDto.Currency.ToUpper(),
                        Name = positionDto.Name,
                        Type = positionDto.Type,
                        MarketValue = positionDto.MarketValue,
                        SharePercentage = Math.Round((double)((positionDto.MarketValue / portfolio.MarketValue) * 100), 2)
                    };

                    portfolio.Positions.Add(position);
                }

                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.Fatal(e.Message);
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = "Something went wrong during adding portfolio";
            }

            return result;
        }

        private ServiceResult ValidatePortfolio(PortfolioDto portfolioDto)
        {
            ServiceResult result = new ServiceResult(Common.Enums.ServiceResultType.Success);
            if (string.IsNullOrEmpty(portfolioDto.ISIN))
            {
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = $"ISIN code of portfolio is empty";
                return result;
            }

            if (!IsinValidator.Validate(portfolioDto.ISIN))
            {
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = $"ISIN code ('{portfolioDto.ISIN}') of portfolio is not valid";
                return result;
            }

            if (string.IsNullOrEmpty(portfolioDto.Currency))
            {
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = $"Currency code of portfolio is empty";
                return result;
            }

            if (!CurrencyCodeValidator.Validate(portfolioDto.Currency))
            {
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = $"Currency code ('{portfolioDto.Currency}') of portfolio is not valid";
                return result;
            }

            return result;
        }

        private ServiceResult ValidatePosition(PositionDto positionDto)
        {
            ServiceResult result = new ServiceResult(Common.Enums.ServiceResultType.Success);
            if (string.IsNullOrEmpty(positionDto.ISIN))
            {
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = $"ISIN code of portfolios position is empty";
                return result;
            }

            if (!IsinValidator.Validate(positionDto.ISIN))
            {
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = $"ISIN code ('{positionDto.ISIN}') of portfolios position is not valid";
                return result;
            }

            if (string.IsNullOrEmpty(positionDto.Currency))
            {
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = $"Currency code of portfolios position is empty";
                return result;
            }

            if (!CurrencyCodeValidator.Validate(positionDto.Currency))
            {
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = $"Currency code ('{positionDto.Currency}') of portfolios position is not valid";
                return result;
            }

            if (string.IsNullOrEmpty(positionDto.Country))
            {
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = $"Country code of portfolios position is empty";
                return result;
            }

            if (!CountryCodeValidator.Validate(positionDto.Country))
            {
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = $"Country code ('{positionDto.Country}') of portfolios position is not valid";
                return result;
            }

            return result;
        }

        public async Task<ServiceResult> DeleteAsync(string isin, DateTime date)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                Portfolio portfolio = await portfolioRepository.GetFirstOrDefaultAsync(
                    predicate: p => p.ISIN == isin && p.Date == date.Date);

                if (portfolio != null)
                {
                    portfolioRepository.Delete(portfolio.Id);
                }

                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                logger.Fatal(e.Message);
                result.ResultType = Common.Enums.ServiceResultType.Error;
                result.Message = "Something went wrong during portfolio deleting";
            }

            return result;
        }

        public async Task<PortfolioDto> GetAsync(string isin, DateTime date)
        {
            var result = await portfolioRepository.GetFirstOrDefaultAsync(p => PortfolioToDto(p),
                predicate: p => p.ISIN == isin && p.Date == date.Date,
                include: source => source.Include(p => p.Positions));

            return result;
        }

        public async Task<PortfolioDto> GetLastByIsinAsync(string isin)
        {
            var result = await portfolioRepository.GetFirstOrDefaultAsync(p => PortfolioToDto(p),
                predicate: p => p.ISIN == isin,
                orderBy: source => source.OrderByDescending(position => position.Date),
                include: source => source.Include(p => p.Positions));

            return result;
        }

        private PortfolioDto PortfolioToDto(Portfolio p)
        {
            return new PortfolioDto
            {
                Currency = p.Currency,
                Date = p.Date,
                ISIN = p.ISIN,
                MarketValue = p.MarketValue,
                Positions = p.Positions.Select(pos => new PositionDto
                {
                    ISIN = pos.ISIN,
                    Country = pos.Country,
                    Currency = pos.Currency,
                    MarketValue = pos.MarketValue,
                    Name = pos.Name,
                    SharePercentage = pos.SharePercentage,
                    Type = pos.Type
                }).ToList()
            };
        }
    }
}
