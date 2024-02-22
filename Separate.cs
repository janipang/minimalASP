using System;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace MinAPISeparateFile
{
    [ApiController]
    [Route("[controller]")]
    public static class HelloHandler
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/", () => "Hello named route");

            app.MapGet("/newpost", () => "GET /newpost");
            app.MapPost("/newpost", () => Console.WriteLine("received something"));
            // app.MapPost("/newpost", CreatePost);

            app.MapGet("/love", async context =>
            {
                await context.Response.WriteAsync("love");
            });

            app.MapGet("/users/{id:int}", (int id) => $"The user id is {id} na kub");
        }

        [HttpPost]
        public static IActionResult CreatePost([FromBody] Post post)
        {
            Console.WriteLine("received something");
            if (post == null)
            {
                Console.WriteLine("send not OK response");
                return new BadRequestResult();
            }

            Console.WriteLine(post);
            string json = JsonSerializer.Serialize(post);
            File.WriteAllText("./datacenter/post.json", json);

            return new OkResult();
        }
    }

    public record Post(
        int Id,
        string User,
        string Privacy,
        string Status
    );
}
