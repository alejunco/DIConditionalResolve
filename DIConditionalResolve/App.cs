using System;

namespace DIConditionalResolve
{
    class App
    {
        private Func<int, ICosa> _serviceAccessor;
        public App(Func<int, ICosa> serviceAccessor)
        {
            _serviceAccessor = serviceAccessor;
        }
        public void Run()
        {
            var collection = new[] { 1, 2, 3 };

            for (int i = 0; i < collection.Length; i++)
            {
                var printerService = _serviceAccessor(collection[i]);
                printerService.Print();
            }
        }
    }
}
