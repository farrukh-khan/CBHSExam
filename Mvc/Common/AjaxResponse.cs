namespace Mvc.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class AjaxResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public AjaxResponseStatus Status { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Html { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string RedirectUrl { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public static AjaxResponse GetResponse(string message, string details = "", AjaxResponseStatus status = AjaxResponseStatus.Warning, string html = "", string redirectUrl = "")
        {
            return new AjaxResponse { Status = status, Message = message, Details = details, Html = html, RedirectUrl = redirectUrl };
        }

    }

    public enum AjaxResponseStatus
    {
        Error = 0,
        Success = 1,
        Warning = 2
    }
}