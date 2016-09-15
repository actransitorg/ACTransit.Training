namespace ACTransit.Training.Web.Models
{
    public class JsonGeneric
    {
        public bool Success { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public string UrlReferrer { get; set; }
        public object Data { get; set; }

        public int ErrorNumber { get; set; }                


        public JsonGeneric(bool success)
        {
            Success = success;
        }
        public JsonGeneric(bool success, string message):this(success)
        {
            Message = message;
        }
        public JsonGeneric(bool success, string message, object data):this(success, message)
        {
            Data = data;
        }

        public JsonGeneric(bool error, string message,string urlReferrer)
        {
            Error = error;
            Message = message;
            UrlReferrer = urlReferrer;
        }


    }
}