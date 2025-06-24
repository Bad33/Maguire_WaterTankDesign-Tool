using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WaterTankTool_WFA.Designer_Notes;

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

            _diContainer = new DIContainer();
            NotesManager.LoadNotes();

            TankType selectedType = TankType.None;
            using (var dlg = new TankTypeSelectionForm())
            {
                var result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                    selectedType = dlg.SelectedTankType;
                else
                    MessageBox.Show("You must select a tank type to proceed.");
            }

            if (selectedType != TankType.None)
            {
                try
                {
                    Application.Run(new StartupForm(_diContainer, selectedType));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Application crashed: " + ex);
                }
            }
        }

        public static DIContainer GetContainer()
        {
            return _diContainer;
        }
    
    }
}
