using System.Linq;
using System.Windows;

namespace PL
{
    /// <summary>
    /// A class to manage windows in the application
    /// </summary>
    static public class ManageWindows
    {
        const string AppWindowTitle = "WorkspaceWindow";
        const string RegisterWindowTitle = "Welcome Window";

        /// <summary>
        /// Open register window
        /// </summary>
        public static void OpenRegisterWindow()
        {
            new Views.WelcomeWindow().Show();
        }

        /// <summary>
        /// Close register window
        /// </summary>
        public static void CloseRegisterWindow()
        {
            var registerWindow = App.Current.Windows.Cast<Window>().Single(w => w.Title == RegisterWindowTitle);
            registerWindow.Close();
        }

        /// <summary>
        /// Open application window
        /// as manager (id is null) or as customer 
        /// </summary>
        /// <param name="id">id of customer to sign in for</param>
        public static void OpenAppWindow(int? id = null)
        {
            if (id == null)
            {
                PLService.IsManangerMode = true;
                new Views.WorkspaceWindow().Show();
            }
            else
            {
                PLService.IsCustomerMode = true;
                new Views.WorkspaceWindow((int)id).Show();
            }
        }

        /// <summary>
        /// Close application window
        /// </summary>
        public static void CloseAppWindow()
        {
            if (!CanCloseAppWindow()) return;
            var appWindow = App.Current.Windows.Cast<Window>().Single(w => w.Title == AppWindowTitle);
            appWindow.Close();
        }

        /// <summary>
        /// Return wheather application can be closed or not
        /// </summary>
        /// <returns>Wheather application can be closed or not</returns>
        public static bool CanCloseAppWindow()
        {
            foreach (var simulator in PLSimulators.Simulators.Values)
            {
                if (simulator.IsBusy)
                {
                    MessageBox.Show(MessageBox.BoxType.Error, "There are one or more simulators running.");
                    return false;
                }
            }
            return true;
        }
    }
}
