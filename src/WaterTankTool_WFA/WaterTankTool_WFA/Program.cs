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
  
            SetProcessDpiAwareness(2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize the DI container
            _diContainer = new DIContainer();


            // Run the StartupForm
            Application.Run(new StartupForm(_diContainer));
        }

        public static DIContainer GetContainer()
        {
            return _diContainer;
        }
    
    }
}
