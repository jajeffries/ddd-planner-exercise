namespace Planner
{
    public class HttpContext
    {
        public int StatusCode {get; private set;}
        public string Path {get; private set;}
        public void Redirect(int statusCode, string path)
        {
            StatusCode = statusCode;
            Path = path;
        }
    }
}