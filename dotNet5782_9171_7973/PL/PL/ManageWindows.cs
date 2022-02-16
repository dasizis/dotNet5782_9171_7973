using System.Linq;
using System.Windows;

namespace PL
{
    static public class ManageWindows
    {
        const string AppWindowTitle = "Workspace Window";
        const string RegisterWindowTitle = "Welcome Window";

        public static void OpenRegisterWindow()
        {
            new Views.WelcomeWindow().Show();
        }

        public static void CloseRegisterWindow()
        {
            var registerWindow = App.Current.Windows.Cast<Window>().Single(w => w.Title == RegisterWindowTitle);
            registerWindow.Close();
        }

        public static void OpenAppWindow(int? id)
        {
            if (id == null)
            {
                new Views.WorkspaceWindow().Show();
            }
            else
            {
                new Views.WorkspaceWindow((int)id).Show();
            }
        }

        public static void CloseAppWindow()
        {
            if (!CanCloseAppWindow()) return;
            var appWindow = App.Current.Windows.Cast<Window>().Single(w => w.Title == AppWindowTitle);
            appWindow.Close();
        }

        public static bool CanCloseAppWindow()
        {
            foreach (var simulator in PLSimulators.Simulators.Values)
            {
                if (simulator.IsBusy)
                {
                    MessageBox.Show(MessageBox.BoxType.Error, "There is One Or More Active Simulator");
                    return false;
                }
            }
            return true;
        }
    }
}
