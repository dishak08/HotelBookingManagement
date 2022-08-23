using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using UserMicroservice.Exceptions;
using UserMicroservice.Models;

namespace UserMicroservice.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case ConnectedServiceException e:
                        // custom application error
                        response.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
                        break;
                    case UserNotFoundException e:
                        // not found error
                        response.StatusCode = (int) HttpStatusCode.NotFound;
                        break;

                    case InvalidEmailException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case InvalidPhoneException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new Response(error.Message, false));
                await response.WriteAsync(result);
            }
        }
    }
}
