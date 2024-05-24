namespace TourismFormsAPI.Tools
{
    public class ErrorTools
    {
        public static string GetInfo(Exception ex)
        {
            if (ex.InnerException is not null)
                return ex.Message + "\r\n" + ex.InnerException.Message;
            else
                return ex.Message;
        }
    }
}
