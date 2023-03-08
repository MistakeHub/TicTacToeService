﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTTService.HttpApi.Helpers.Statuses
{
  public  enum Statuse : int
    {
        Success = 200,
        NotFound = 404,
        Conflicted = 409,
        BadGateway = 502,
        ServiceUnavalible = 503

    }
}
