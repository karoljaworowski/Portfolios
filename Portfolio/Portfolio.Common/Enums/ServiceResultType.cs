using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolios.Common.Enums
{
    public enum ServiceResultType : byte
    {
        Undefined = 0,

        Success = 1,

        Error,

        NotFound,

        Unauthorized
    }

}
