using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class CompatibleFileResultAttribute : ActionFilterAttribute
    {
        private static bool CanExecute(ResultExecutedContext filterContext)
        {
            if (!(filterContext.Result is FileResult))
            {
                return false;
            };

            var browser = filterContext.HttpContext.Request.Browser;

            // レガシーブラウザ
            return browser.Browser == "IE" && browser.MajorVersion < 9;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (!CanExecute(filterContext))
            {
                return;
            }

            var response = filterContext.HttpContext.Response;

            // ####################
            // # Download will fail when the ssl and no-cache.
            // ####################

            var cacheControl = response.Headers["Cache-Control"];
            if (!string.IsNullOrEmpty(cacheControl))
            {
                response.Headers["Cache-Control"] = RemoveNoCache(cacheControl);
            }

            var pragma = response.Headers["Pragma"];
            if (!string.IsNullOrEmpty(cacheControl))
            {
                response.Headers["Pragma"] = RemoveNoCache(pragma);
            }

            // ####################
            // # Not supported RFC 6266 (RFC 2231/RFC 5987).
            // ####################

            var contentDisposition = response.Headers["Content-Disposition"];
            if (!string.IsNullOrEmpty(contentDisposition))
            {
                response.Headers["Content-Disposition"] = ReplaceFileName(contentDisposition);
            }
        }

        private static readonly Regex FileNameRegex = new Regex(@"filename(\*)?=""?(UTF-8'')?([^;""]+)""?;?", RegexOptions.Compiled);

        private static string ReplaceFileName(string contentDisposition)
        {
            // filename*=UTF-8'' 形式を filename= 形式に置換
            return FileNameRegex.Replace(
                contentDisposition,
                x => string.Format(@"filename=""{0}""", x.Groups[3].Value.Replace(" ", "%20")));
        }

        private static readonly Regex NoCacheRegex = new Regex(@" ?no-cache,?", RegexOptions.Compiled);

        private static string RemoveNoCache(string cache)
        {
            // no-cache の指定を削除
            return NoCacheRegex.Replace(
                cache,
                _ => string.Empty);
        }
    }
}