using AutoMapper;
using Grpc.Service.Core.Reflection;
using Microsoft.Extensions.Configuration;
using RepeatedToken.Service.AutoMap;
using RepeatedToken.Service.GrpcImpl;
using SP.Service;
using System;
using System.IO;
using System.Linq;

namespace RepeatedToken.Service
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        static void Main(string[] args)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new TokenProfile());
            });
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
            AssemblyRegistHelper.Register(Configuration);

            var host = Configuration.GetSection(CommonKeys.AppHost).Value;
            var port = Configuration.GetSection(CommonKeys.AppPort).Value;
            var service = new CacheService(host, port);
            service.Start();
        }
    }

    public class CacheService
    {
        readonly Grpc.Core.Server server;
        public CacheService(string host, string port)
        {
            int tempPort = 0;
            int.TryParse(port, out tempPort);
            server = new Grpc.Core.Server
            {
                Services = { RepeatedTokenService.BindService(new TokenServiceImpl(tempPort)) },
                Ports = { new Grpc.Core.ServerPort(host, tempPort, Grpc.Core.ServerCredentials.Insecure) }
            };
        }
        public void Start()
        {
            server.Start();
            server.Ports.ToList().ForEach(a => Console.WriteLine($"token server listening on {a.Host}port {a.Port}..."));
            server.ShutdownTask.Wait();
        }
        public void Stop() { server.ShutdownAsync(); }
    }
}
