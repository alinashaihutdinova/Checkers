using Checkers.Data;
using Checkers.Core.Services;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Checkers.Services;

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

            var container = new WindsorContainer();
            container.Register(
                Component.For<CheckersDbContext>().LifestyleTransient(),
                Component.For<IUserService>().ImplementedBy<UserService>().LifestyleTransient(),
                Component.For<IGameService>().ImplementedBy<GameService>().LifestyleTransient()
            );
            var entranceForm = new EntranceForm(container);
            Application.Run(entranceForm);
            
        }
    }
}