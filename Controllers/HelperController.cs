using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace hotel_management.Controllers
{
    public static class HelperController
    {
        public static bool LoginSessionVerification(ISession session)
        {
            if(session.GetString("UserLogin") == null)
                return false;
            else
                return true;
        }

    }
}