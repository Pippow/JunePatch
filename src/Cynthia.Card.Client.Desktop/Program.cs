﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Alsein.Utilities;
using Autofac;

namespace Cynthia.Card.Client
{
    class Program
    {
        static async Task Main(string[] args) => await ConfiguerServices().Resolve<MainService>().Main(args);

        private static IContainer ConfiguerServices()
        {
            var container = default(IContainer);
            var builder = new ContainerBuilder();
            builder.RegisterType<HubConnectionBuilder>().SingleInstance();
            builder.Register(x => container.Resolve<HubConnectionBuilder>().WithUrl("http://localhost:5000/hub/gwent").Build()).SingleInstance();
            builder.Register(x => container);
            builder.RegisterAllServices(option => option.PreserveExistingDefaults());
            return container = builder.Build(); ;
        }
    }
}
