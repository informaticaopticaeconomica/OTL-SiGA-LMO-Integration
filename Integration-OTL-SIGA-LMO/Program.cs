using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using Services;

namespace Integration_OTL_SIGA_LMO
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //********************************************************************************************************
            //Created by: Marvin Garmendia
            //This method was created to implement Inyection Dependency
            //Decembre 17, 2021.
            //
            //You can see in: https://www.youtube.com/watch?v=LII7g9g4Og8
            // Multiples Forms: https://www.youtube.com/watch?v=7ILodxJ7lNg (.AddScoped<Form1>() .AddScoped<Form2>() .AddScoped<Fomr3>() .....)
            //
            var services = new ServiceCollection();

            //Sending to ConfigureServices method
            ConfigureServices(services);

            //This is to the formulary take the inyection
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var formulary = serviceProvider.GetRequiredService<frmLayoutDesktop>();
                Application.Run(formulary);
            }
            //********************************************************************************************************
                
        }

        //********************************************************************************************************
        //Created by: Marvin Garmendia
        //This method was created to implement Inyection Dependency
        //Decembre 17, 2021.
        // Multiples Forms: https://www.youtube.com/watch?v=7ILodxJ7lNg (.AddScoped<Form1>() .AddScoped<Form2>() .AddScoped<Fomr3>() .....)
        //
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<IServiceDB, ServiceDB>()
                .AddScoped<frmLayoutDesktop>();
        }
        //********************************************************************************************************

    }
}
