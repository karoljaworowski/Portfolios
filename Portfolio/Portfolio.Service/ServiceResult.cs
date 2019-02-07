using Portfolios.Common.Enums;

namespace Portfolios.Service
{
    public class ServiceResult
    {
        public string Message { get; set; }

        public ServiceResultType ResultType { get; set; }

        public ServiceResult(ServiceResultType type = ServiceResultType.Success, string message = "")
        {
            ResultType = type;
            Message = message;
        }
    }
}
