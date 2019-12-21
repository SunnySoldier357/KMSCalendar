using Autofac;

using KMSCalendar.Models.Settings;
using KMSCalendar.Services.Email;

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
            // Models -> Settings
            builder.RegisterInstance(AppSettings.InitSingleton())
                .AsSelf()
                .SingleInstance();

            builder.RegisterInstance(UserSettings.InitSingleton())
                .AsSelf()
                .SingleInstance();

            // Services -> Email
            builder.RegisterType<EmailService>()
                .As<IEmailService>();
        }
    }
}