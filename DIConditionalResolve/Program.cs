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
            serviceCollection.AddSingleton(new DividedByThree());
            serviceCollection.AddSingleton(new DividedByFive());
            serviceCollection.AddSingleton(new DividedByThreeAndFive());
            serviceCollection.AddSingleton(new DefaultPrinter());

            // add app
            serviceCollection.AddTransient<App>();

            serviceCollection.AddTransient<Func<int, IDivisibilityPrinter>>(serviceProvider => input =>
            {
                if(input%3==0 &&input%5==0)
                    return serviceProvider.GetService<DividedByThreeAndFive>();

                if (input % 3 == 0)
                    return serviceProvider.GetService<DividedByThree>();

                if (input % 5 == 0)
                    return serviceProvider.GetService<DividedByFive>();

                return serviceProvider.GetService<DefaultPrinter>();
            });

        }
    }

    public interface IDivisibilityPrinter
    {
        void Print(int n);
    }

    public class DefaultPrinter : IDivisibilityPrinter
    {
        public void Print(int n)
        {
            Console.WriteLine(n);
        }
    }

    public class DividedByThree : IDivisibilityPrinter
    {
        public void Print(int n)
        {
            Console.WriteLine("tic");
        }
    }

    public class DividedByFive : IDivisibilityPrinter
    {
        public void Print(int n)
        {
            Console.WriteLine("tac");
        }
    }

    public class DividedByThreeAndFive : IDivisibilityPrinter
    {
        public void Print(int n)
        {
            Console.WriteLine("tic tac");
        }
    }
}
