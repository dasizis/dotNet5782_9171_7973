using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public void AddDisplayTab(string header, Control content)
        //{
        //    DisplayPanel.Items.Add(new TabItem() { Header = header, Content = content });
        //    DisplayPanel.SelectedIndex = DisplayPanel.Items.Count - 1;
        //}

        //public void AddListTab(string header, Control content)
        //{
        //    ListsPanel.Items.Add(new TabItem() { Header = header, Content = content });
        //    ListsPanel.SelectedIndex = ListsPanel.Items.Count - 1;

        //}
        //check if that works when x sign is in header
        public void CloseMyTab()
        {
            //DisplayPanel.Items.RemoveAt(DisplayPanel.SelectedIndex);
        }

        //public void CloseMyTab(object content)
        //{
        //    Object tab = content;
        //    while(content.GetType() != typeof(TabControl))
        //    {
        //        content = ((FrameworkElement)content).Parent;
        //    }
        //    (content as TabControl).Items.Remove(tab as TabItem);
        //}

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
