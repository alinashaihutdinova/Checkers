using Checkers.Data;
using Checkers.Core.Services;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Checkers.Services;
using Checkers.Forms;

namespace Checkers
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = new WindsorContainer();//инициализация контейнера
            container.Register(Component.For<CheckersDbContext>().LifestyleSingleton());
            container.Register(
                Component.For<IUserService>().ImplementedBy<UserService>().LifestyleSingleton()
            );
            container.Register(Component.For<EntranceForm>().LifestyleSingleton());
            container.Register(Component.For<MainForm>().LifestyleSingleton());
            container.Register(Component.For<RegistrationForm>().LifestyleSingleton());
            var entranceForm = container.Resolve<EntranceForm>();
            Application.Run(entranceForm);
            
        }
    }
}