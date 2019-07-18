using Microsoft.Extensions.DependencyInjection;
using System;

namespace DIConditionalResolve
{
    class Program
    {
        public static void Main(string[] args)
        {
            // create service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // create service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // entry to run app
            serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add services
            serviceCollection.AddSingleton(new CosaPar());
            serviceCollection.AddSingleton(new CosaImpar());

            // add app
            serviceCollection.AddTransient<App>();

            serviceCollection.AddTransient<Func<int, ICosa>>(serviceProvider => input =>
            {
                
                switch (input%2==0)
                {
                    case true:
                        {
                            return serviceProvider.GetService<CosaPar>();
                        }
                    case false:
                        {
                            var x = serviceProvider.GetService<CosaImpar>();
                            return x;
                        }
                    default:
                        throw new InvalidOperationException();
                }
            });

        }
    }

    public interface ICosa
    {
        void Print();
    }

    public class CosaPar : ICosa
    {
        public void Print()
        {
            Console.WriteLine("Es par");
        }
    }

    public class CosaImpar : ICosa
    {
        public void Print()
        {
            Console.WriteLine("Es impar");
        }
    }
}
