using System;
using NServiceBus.MessageRouting.RoutingSlips.Samples.Messages;
using log4net;

namespace NServiceBus.MessageRouting.RoutingSlips.Samples.Sender 
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://nservicebus.com/GenericHost.aspx
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
    }

    public class RoutingConfiguration : IWantCustomInitialization
    {
        public void Init()
        {
            Configure.Instance.RoutingSlips();
        }
    }

    public class Startup : IWantToRunAtStartup
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Startup));

        public IBus Bus { get; set; }

        public void Run()
        {
            bool toggle = false;

            while (Console.ReadLine() != null)
            {
                if (toggle)
                {
                    var messageABC = new SequentialProcess();

                    Logger.Info("Sending message for step A, B, C");
                    Bus.SendToFirstStep(messageABC, new[]
                    {
                        "NServiceBus.MessageRouting.RoutingSlips.Samples.StepA",
                        "NServiceBus.MessageRouting.RoutingSlips.Samples.StepB",
                        "NServiceBus.MessageRouting.RoutingSlips.Samples.StepC",
                        "NServiceBus.MessageRouting.RoutingSlips.Samples.ResultHost",
                    });
                }
                else
                {
                    var messageAC = new SequentialProcess();

                    Logger.Info("Sending message for step A, C");
                    Bus.SendToFirstStep(messageAC, new[]
                    {
                        new RouteDefinition("NServiceBus.MessageRouting.RoutingSlips.Samples.StepA", false),
                        new RouteDefinition("NServiceBus.MessageRouting.RoutingSlips.Samples.StepC", false),
                        new RouteDefinition("NServiceBus.MessageRouting.RoutingSlips.Samples.ResultHost", false),
                    });
                }

                toggle = !toggle;
            }


        }

        public void Stop()
        {
        }
    }

}