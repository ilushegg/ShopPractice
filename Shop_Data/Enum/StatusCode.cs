using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Enum
{
    public enum StatusCode
    {
        UserNotFound = 0,

        ProductNotFound = 10,

        OK = 200,

        InternalServerError = 500,

        CommonError = 1000

    }
}
