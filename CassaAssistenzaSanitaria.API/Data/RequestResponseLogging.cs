using System;
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
            this.log = Logger.GetLogger(typeof(RequestResponseLogging)); 
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
            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableBuffering();
            
            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body item for the retrieve in controller
            request.HttpContext.Items.Add("Body", bodyAsText);

            //Log the request...
            this.log.Info($"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}");

            foreach (var header in request.Headers)
            {
                //Log the request...
                this.log.Info("header(" + header.Key + "): " + header.Value);
            }
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
            this.log.Info($"{response.StatusCode}: {text}");
        }
    }
}
