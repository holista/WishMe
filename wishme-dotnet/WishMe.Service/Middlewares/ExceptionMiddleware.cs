using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WishMe.Service.Exceptions;
using WishMe.Service.Exceptions.Factories;

namespace WishMe.Service.Middlewares
{
  public sealed class ExceptionMiddleware
  {
    private readonly RequestDelegate fNext;

    public ExceptionMiddleware(RequestDelegate next)
    {
      fNext = next;
    }

    public async Task InvokeAsync(HttpContext context, IStatusFactory statusFactory)
    {
      try
      {
        await fNext(context);
      }
      catch (Exception ex)
      {
        statusFactory.Create(ex, out var statusCode, out var message);

        await WriteResponseAsync(context, statusCode, message);
      }
    }

    private static async Task WriteResponseAsync(HttpContext context, int statusCode, string? message)
    {
      context.Response.StatusCode = statusCode;

      if (message == null)
        return;

      var model = new MessageModel { Message = message };

      string json = JsonConvert.SerializeObject(model);

      context.Response.ContentType = "application/json";
      await context.Response.WriteAsync(json, context.RequestAborted);
    }
  }
}
