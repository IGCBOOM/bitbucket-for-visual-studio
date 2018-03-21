﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BitBucket.REST.API.Exceptions;
using log4net;

namespace GitClientVS.Infrastructure.Utils
{
    public static class ExceptionMapper
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string Map(Exception ex)
        {
            if (ex is GitClientVsException gitEx)
                return gitEx.Message;
            if (ex is WebException webExc)
                return webExc.Message;
            if (ex is AuthorizationException)
                return "Incorrect credentials";
            if (ex is ForbiddenException)
                return "Operation is forbidden";
            if (ex is RequestFailedException reqFailedEx)
                return reqFailedEx.IsFriendlyMessage ? ex.Message : "Wrong request";

            if (ex is UnauthorizedAccessException)
                return "Unauthorized";

            Logger.Error("Unknown error: " + ex);

            return $"Unknown error. ({ex.Message}). Check logs for more info";
        }
    }
}
