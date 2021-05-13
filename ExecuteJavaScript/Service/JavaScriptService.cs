using ExecuteJavaScript.Model;
using Microsoft.ClearScript.V8;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ExecuteJavaScript.Service
{
    /// <summary>
    /// JavaScriptService
    /// 创建人：赵迪
    /// 创建时间：2021/5/12 17:44:25
    /// </summary>
    public class JavaScriptService
    {
        private static JavaScriptService _JavaScriptService = null;

        private static V8ScriptEngine _V8ScriptEngine = null;

        private JavaScriptService() { }

        static JavaScriptService()
        {
            _JavaScriptService = new JavaScriptService();
            _V8ScriptEngine = new V8ScriptEngine();
        }
        /// <summary>
        /// 获取单例
        /// </summary>
        /// <returns></returns>
        public static JavaScriptService GetInstance()
        {
            return _JavaScriptService;
        }
        //JS代码
/*        var jsCode = @"
                var field1 = '2021-05-01';
                var field2 = '2021-05-01';

                if (field1 != null && field2 != null && field1.trim() != '' && field2.trim() != '') {

                    var nowdate = new Date(field1);
                    var fydate = new Date(field2);
                    var maxtime = new Date(fydate.getDate() + 1728);
                    var mintime = new Date(fydate.getDate() + 1548);

                    var nowyear = nowdate.getYear();
                    var nowmonth = nowdate.getMonth() + 1;
                    var nowday = nowdate.getDate();
                    var date = nowyear + '/' + nowmonth + '/' + nowday;

                    var str = '';
                    if (nowyear < 2015) {
                        str += `检查日期年份 (${date})数据低于合理值范围，应在2015年~2023年范围内`;
                    } else if (nowmonth > 2023) {
                        str += `检查日期年份 (${date})数据高于合理值范围，应在2015年~2023年范围内`;
                    } else if (nowmonth < 1 || nowmonth > 12) {
                        str += `检查日期月份 (${date})数据超出合理值范围，应在1月~12月范围内`;
                    } else if (nowday < 1 || nowday > 31) {
                        str += `检查日期日期 (${date})数据超出合理值范围，应在1天~31天范围内`;
                    }

                    if (nowdate > maxtime) {
                        var day = Math.abs(parseInt((maxtime.getTime() - nowdate.getTime()) / 1000 / (24 * 60 * 60)));
                        str += `检查日期不在服药时间+1638±90天，访视超窗${day}天`;
                    } else if (nowdate < mintime) {
                        var day = Math.abs(parseInt((maxtime.getTime() - nowdate.getTime()) / 1000 / (24 * 60 * 60)));
                        str += `检查日期不在服药时间+1638±90天，提前访视${day}天`;
                    }

                    return str;
                }
                return '';";*/

        /// <summary>
        /// 运行Js
        /// </summary>
        /// <param name="param">js代码块</param>
        /// <returns></returns>
        public dynamic ExecuteJavaScript(JavaScriptParam param)
        {
            using (var engine = new V8ScriptEngine())
            {
                byte[] bytes = Convert.FromBase64String(param.JavaScriptCode);
                var code = Encoding.UTF8.GetString(bytes);
                var script = engine.Compile($"function run() {{{code}}}");
                engine.Execute(script);
                var result = engine.Script.run();
                return result;
            }
        }

        /// <summary>
        /// 运行Js
        /// </summary>
        /// <param name="param">js代码块</param>
        /// <returns></returns>
        public dynamic ExecuteJavaScriptStatic(JavaScriptParam param)
        {
            byte[] bytes = Convert.FromBase64String(param.JavaScriptCode);
            var code = Encoding.UTF8.GetString(bytes);
            var script = _V8ScriptEngine.Compile($"function run() {{{code}}}");
            _V8ScriptEngine.Execute(script);
            var result = _V8ScriptEngine.Script.run();
            return result;
        }
    }
}
