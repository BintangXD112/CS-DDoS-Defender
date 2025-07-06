using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CSharpDefender.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly bool _logToFile;
        private readonly string _logFilePath;

        public LoggerMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            var section = configuration.GetSection("AntiDDoS:Logger");
            _logToFile = section.GetValue<bool>("LogToFile", false);
            _logFilePath = section.GetValue<string>("LogFilePath", "logs/requests.log");
            if (_logToFile && !Directory.Exists(Path.GetDirectoryName(_logFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath));
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var path = context.Request.Path;
            var method = context.Request.Method;
            var reqMsg = $"Request: {method} {path} from {ip}";
            var resMsg = $"Response: {context.Response.StatusCode} for {method} {path} from {ip}";
            Console.WriteLine(reqMsg);
            if (_logToFile)
            {
                await File.AppendAllTextAsync(_logFilePath, reqMsg + "\n");
            }
            await _next(context);
            Console.WriteLine(resMsg);
            if (_logToFile)
            {
                await File.AppendAllTextAsync(_logFilePath, resMsg + "\n");
            }
        }
    }
} 