using System;

namespace DIConditionalResolve
{
    class App
    {
        private Func<int, IDivisibilityPrinter> _serviceAccessor;
        public App(Func<int, IDivisibilityPrinter> serviceAccessor)
        {
            _serviceAccessor = serviceAccessor;
        }
        public void Run()
        {
            var collection = new[] { 1, 2, 3,4,5,6,7,8,9,15 };

            for (int i = 0; i < collection.Length; i++)
            {
                var printerService = _serviceAccessor(collection[i]);
                printerService.Print(collection[i]);
            }
        }
    }
}
