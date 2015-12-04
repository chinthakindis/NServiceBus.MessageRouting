
namespace NServiceBus.MessageRouting.RoutingSlips.Samples.StepC
{
    using System;
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;
    using NServiceBus.Logging;

    class Program
    {
        static void Main(string[] args)
        {
            RunBus().GetAwaiter().GetResult();
        }

        static async Task RunBus()
        {
            IEndpointInstance endpoint = null;
            try
            {
                DefaultFactory defaultFactory = LogManager.Use<DefaultFactory>();

                var configuration = new BusConfiguration();
                configuration.EndpointName("NServiceBus.MessageRouting.RoutingSlips.Samples.StepC");

                configuration.UseTransport<MsmqTransport>();
                configuration.UsePersistence<InMemoryPersistence>();
                configuration.EnableFeature<RoutingSlips>();

                endpoint = await Endpoint.Start(configuration);

                Console.ReadLine();
            }
            finally
            {
                await endpoint.Stop();
            }
        }
    }
}
