using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CassaAssistenzaSanitaria.API.Data
{
    public class RequestResponseLogging
    {
        private readonly RequestDelegate _next;
        private log4net.ILog log;

        public RequestResponseLogging(RequestDelegate next)
        {
            log = Logger.GetLogger(typeof(RequestResponseLogging));
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //First, get the incoming request
            FormatRequest(context.Request);

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //Format the response from the server
                FormatResponse(context.Response);

                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async void FormatRequest(HttpRequest request)
        {
            string bodyAsText = await GetRequestBodyAsync(request);

            //Log the request...
            //log.Info($"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}");
            foreach (var header in request.Headers)
            {
                log.Info("header(" + header.Key + "): " + header.Value);
            }
        }

        public async Task<string> GetRequestBodyAsync(HttpRequest request)
        {
            string strRequestBody = "";

            // IMPORTANT: Ensure the requestBody can be read multiple times.
            HttpRequestRewindExtensions.EnableBuffering(request);

            // IMPORTANT: Leave the body open so the next middleware can read it.
            using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true))
            {
                strRequestBody = await reader.ReadToEndAsync();

                // IMPORTANT: Reset the request body stream position so the next middleware can read it
                request.Body.Position = 0;
            }

            return strRequestBody;
        }

        private async void FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Log the response...
            log.Info($"{response.StatusCode}: {text}");
        }
    }
}
