using Autofac;

namespace KMSCalendar
{
    public class AppSetup
    {
        //* Public Methods
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();

            RegisterDependencies(containerBuilder);

            return containerBuilder.Build();
        }

        //* Protected Methods
        protected virtual void RegisterDependencies(ContainerBuilder builder)
        {

        }
    }
}