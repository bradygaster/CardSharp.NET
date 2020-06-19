using System.Reflection;
using CardSharp.Abstractions;
using CardSharp.GameProviders.HiLo;
using CardSharp.GameProviders.Standard;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

[assembly: TestFramework("CardSharp.HiLo.Tests.Startup", "CardSharp.HiLo.Tests")]

namespace CardSharp.HiLo.Tests
{
    public class Startup : DependencyInjectionTestFramework
    {
        public Startup(IMessageSink messageSink) : base(messageSink) { }

        protected void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<StandardDeckDealer>();
            services.AddTransient<StandardDeckProvider>();
            services.AddSingleton<HiLoGame>();
            services.AddSingleton<HiLoGameManager>();
        }

        protected override IHostBuilder CreateHostBuilder(AssemblyName assemblyName) =>
            base.CreateHostBuilder(assemblyName)
                .ConfigureServices(ConfigureServices);
    }
}