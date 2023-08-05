namespace Studenda.Service
{
    //класс для отправки ошибок в клиент в виде JSON'а 
    public class ExceptionCatcher
    {
        //делегат для инициализации запроса с сервера
        private readonly RequestDelegate _deleg;

        public ExceptionCatcher(RequestDelegate deleg)
        {
            _deleg = deleg;
        }
        /// <summary>
        /// выполнение запросов. Если все хорошо, то все выполнится , в противном случае вернет сообщение в виде JSON'а об ошибке
        /// </summary>
        /// <param name="context">запрос</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _deleg.Invoke(context);
            }
            catch (Exception ex)
            {
                string errorType = ex.GetType().ToString();
                string errorMessage = ex.Message;
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { ErrorType = errorType, ErrorMessage = errorMessage });
            }
        }
    }
}
