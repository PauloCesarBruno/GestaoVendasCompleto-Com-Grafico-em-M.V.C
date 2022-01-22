using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Treinando_MVC_e_Sessao;

namespace SistemaVendas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseIISIntegration() // Define o IIS como Proxy
                .Build();
    }
}
