using MassTransit;
using Trading.Api.Models;

class Program
{
    public static async Task Main()
    {
        var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host("rabbitmq", "/", h =>
            {
                h.Username("guest");
                h.Password("guest");
            });

            cfg.ReceiveEndpoint("trade_queue", e =>
            {
                e.Handler<Trade>(context =>
                {
                    Console.WriteLine($"Trade received: {context.Message.Symbol}, Amount: {context.Message.Price}");
                    return Task.CompletedTask;
                });
            });
        });

        await bus.StartAsync();
        Console.WriteLine("Listening for trades...");

        // Keep the app running in Docker
        await Task.Delay(Timeout.Infinite);

        // await bus.StopAsync();  // optional, only if you want graceful shutdown
    }
}
