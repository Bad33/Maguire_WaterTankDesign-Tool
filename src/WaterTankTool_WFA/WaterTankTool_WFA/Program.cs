using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WaterTankTool_WFA
{
    static class Program
    {

        public static DIContainer _diContainer;

        [DllImport("Shcore.dll")]
        private static extern int SetProcessDpiAwareness(int awareness);

        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //ApplicationConfiguration.Initialize();

            //// Step 1: Create the splash screen and show it
            //SplashScreen splash = new SplashScreen();
            //splash.Show();
            //splash.Refresh();

            //// Step 2: Create the background worker for initialization
            //BackgroundWorker bgWorker = new BackgroundWorker();
            //bgWorker.DoWork += (sender, e) =>
            //{
            //    // Simulate initialization tasks (e.g., loading resources)
            //    System.Threading.Thread.Sleep(1000); // Replace with actual initialization code
            //};

            //bgWorker.RunWorkerCompleted += (sender, e) =>
            //{
            //    // Step 3: Close the splash screen and show the main form
            //    splash.Close();
            //    WaterTank mainForm = new WaterTank();
            //    mainForm.Show(); // Use Show() instead of Application.Run()

            //    // No need for Application.Run() here as it was already called in Main()
            //};

            //// Step 4: Start the background worker
            //bgWorker.RunWorkerAsync();

            //// Start the application message loop
            //Application.Run(); // This starts the main event loop only once



            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //ApplicationConfiguration.Initialize();
            //Application.Run(new StartupForm());
            //Application.Run(new WaterTank());


            SetProcessDpiAwareness(2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize the DI container
            _diContainer = new DIContainer();

            // Create and initialize DbContext with project path
            //string projectPath = "path/to/your/project_data.db"; // Replace this with the actual path
            //DbContextProvider.InitializeContext($"Data Source={projectPath};");

            // Register the DbContext instance in the DI container
            //_diContainer.Register<WaterTankDbContext>(DbContextProvider.GetContext());

            //// Run the main form with dependency injection
            //Application.Run(new StartupForm(_diContainer));


            // Run the StartupForm
            Application.Run(new StartupForm(_diContainer));
        }

        public static DIContainer GetContainer()
        {
            return _diContainer;
        }
    
    }
}
