using ExecuteJavaScript.Model;
using ExecuteJavaScript.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExecuteJavaScript.Controllers
{
    /// <summary>
    /// Js代码运行
    /// </summary>
    [Route("api/[controller]")]
    public class JavaScriptController : Controller
    {
        private readonly ILogger<JavaScriptController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public JavaScriptController(ILogger<JavaScriptController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// C#执行Js代码块(需要Base64加密)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("ExecuteJavaScript")]
        public dynamic ExecuteJavaScript([FromBody] JavaScriptParam param)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var res = JavaScriptService.GetInstance().ExecuteJavaScript(param);
            stopwatch.Stop();
            _logger.LogInformation($"ExecuteJavaScript--运行时间{stopwatch.ElapsedMilliseconds}ms");
            return res;
        }

        /// <summary>
        /// C#执行Js代码块(需要Base64加密)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("ExecuteJavaScriptStatic")]
        public dynamic ExecuteJavaScriptStatic([FromBody] JavaScriptParam param)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var res = JavaScriptService.GetInstance().ExecuteJavaScriptStatic(param);
            stopwatch.Stop();
            _logger.LogInformation($"ExecuteJavaScriptStatic--运行时间{stopwatch.ElapsedMilliseconds}ms");
            return res;
        }
    }
}
