
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace WebAPI_Filters.Filters
{
    public class CustomExceptionFilter:IExceptionFilter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var msg = "500-Internal Server Error";
            logger.Error(context.Exception);
            var exception = context.Exception;

            //writing exception content to a file
            string filepath = @"Logs/";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);

            }
            filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //File Name
            if (!File.Exists(filepath))
            {


                File.Create(filepath).Dispose();

            }
            using (StreamWriter sw = File.AppendText(filepath))
            {
                string error = "Log Written at " + DateTime.Now.ToString();
                sw.WriteLine(error);
                sw.WriteLine(exception);
                sw.WriteLine("************************");
                sw.Flush();
                sw.Close();
            }



            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new ObjectResult(new ApiResponse { Message = msg });
        }
    }

    internal class ApiResponse
    {
        public string Message { get; set; }
    }
}
