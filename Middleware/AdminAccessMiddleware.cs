//using BackEnd_Camping.Models;
//using BackEnd_Camping.Utils;
//namespace BackEnd_Camping.Middleware
//{
//    public class AdminAccessMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public AdminAccessMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            try
//            {
//                if (!context.Session.IsAvailable)
//                {
//                    await _next(context);
//                    return;
//                }

//                var userInfo = context.Session.Get<User>("userInfo");

//                if (context.Request.Path.StartsWithSegments("/admin") && userInfo == null)
//                {
//                    context.Response.Redirect("/");
//                    return;
//                }

//                await _next(context);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error in AdminAccessMiddleware: {ex.Message}");
//                throw;
//            }
//        }
//    }
//}
