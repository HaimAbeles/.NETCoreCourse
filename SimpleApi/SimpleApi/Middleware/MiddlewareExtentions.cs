namespace SimpleApi.Middleware
{
    public static class MiddlewareExtentions
    {
        public static void UseMyCustomMiddleware(this WebApplication app)
        {
            app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
