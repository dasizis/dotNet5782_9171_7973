I'm the first
# dotNet5782_9171_7973

https://s3.amazonaws.com/files2.syncfusion.com/Installs/v19.4.0.38/syncfusionessentialstudiowebinstaller.exe

https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-wpf-apps-with-the-model-view-viewmodel-design-pattern
לדעתי זה לא מדי יעזור לך

read [here](https://www.markdownguide.org/basic-syntax/) about markdown format.

hello world
I'm here to write a peace of code

My name is Yael

hi key
this is an extra line

## this is the end!

 > #### to do:
 > - check what works and what does not.
 > - see string added to "getInput". is it OK?
 > - place the while loop of main menu in a more propar place. I didn't find one yet.
 > - place init func in a propar place too.
 > - edit printing function to the wanted format
 > - command
 > - ##check validation - in all functions- very important (can't be done by delegates since we havn't learnt it yet)
 > - arrange random func od Parcel- few statuses and dates.
 > - separate dalObject to files. It's too long
 > - go over the code and see if logic is correct
 > - anything More?

 to do files:
 ### ConsoleUI
 - [X] Activate - in my opinion id does not must be in a sequence
 - [X] AddMenu
 - [X] OptionEnum
 - [X] Program

 ### DAL
 - [X] BaseStation
 - [X] Customer
 - [ ] DalObject - if not found
 - [X] DataSource
 - [X] Drone
 - [X] DroneCharge
 - [X] Enum
 - [ ] Parcel - check the date 
 - [X] Random

the tree view code
```cs
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTrial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public object List { get; set; } = new List<School>()
        {
            new School(),
            new School(),
            new School(),
            new School(),
            new School(),
        };

        public MainWindow()
        {
            
            InitializeComponent();


            treeFileSystem.Items.Add(createProp("Schools", List));

            
        }

        private TreeViewItem createProp(string name, object value = null)
        {           
            Type type = value.GetType();
            TreeViewItem treeViewItem = new TreeViewItem();

            if (value == null)
            {
                treeViewItem.Header = name;
            }
            else if (type.IsValueType || type == typeof(string))
            {
                treeViewItem.Header = $"{name}\t{value}";
            }
            else
            {
                treeViewItem.Header = name;
                treeViewItem.Items.Add(value);
            }

            return treeViewItem;
        }

        private void item_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem parent = e.OriginalSource as TreeViewItem;
            var content = parent.Items[0];
            parent.Items.Clear();
            Type contentType = content.GetType();

            // The content is an object
            if (contentType.GetInterface("IEnumerable") == null)
            {
                foreach (var prop in contentType.GetProperties())
                {
                    parent.Items.Add(createProp(prop.Name, prop.GetValue(content)));
                }
            }
            // The content is a list
            else
            {
                int count = (int)contentType.GetProperty("Count").GetValue(content);
                var getItemAt = contentType.GetProperty("Item");

                for (int i = 0; i < count; i++)
                {
                    var item = getItemAt.GetValue(content, new object[] { i });
                    Type itemType = item.GetType();

                    if (itemType.IsValueType || itemType == typeof(string))
                    {
                        parent.Items.Add(createProp(item.ToString()));
                    }
                    // the list items are objects
                    else
                    {

                        var attributeData = itemType.GetCustomAttributes(false).OfType<UniqeKeyAttribute>().Single();
                        string uniqeValue = itemType.GetProperty(attributeData.UniqeProp).GetValue(item).ToString();
                        parent.Items.Add(createProp(uniqeValue, item));
                    }
                }
            }

        }  

        //private TreeViewItem BuildTree(string name, object value)
        //{
        //    Type type = value.GetType();
        //    if (HasSimpleType(value)) return new TreeViewItem() { Header = $"{name}\t{value}" };

        //    // Is the value a list?
        //    if (type.GetInterface("IEnumerable") != null)
        //    {

        //        var treeView = new TreeViewItem()  {  Header = name };
        //        int count = (int)type.GetProperty("Count").GetValue(value);
        //        var itemProp = type.GetProperty("Item");

        //        for (int i = 0; i < count; i++)
        //        {
        //            var item = itemProp.GetValue(value, new object[] { i });

        //            treeView.Az
        //        }
        //    }

        //}

        //private bool HasSimpleType(object value)
        //{            
        //    Type valueType = value.GetType();

        //    return valueType.IsValueType || valueType == typeof(string);
        //}
    }
}

```


the material design code
in the App.xaml
```xaml
<Application . . .>
  <Application.Resources>
    <ResourceDictionary>
      <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml" />
        <ResourceDictionary Source="pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml" />
        <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml" />
        <ResourceDictionary Source="pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml" />
      </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
  </Application.Resources>
</Application>
```

in MainWindow.xaml
```xaml
<Window . . .
     xmlns:materialDesign="http://materialdesigninxaml.net/winfx/xaml/themes"
     TextElement.Foreground="{DynamicResource MaterialDesignBody}"
     TextElement.FontWeight="Regular"
     TextElement.FontSize="13"
     TextOptions.TextFormattingMode="Ideal"
     TextOptions.TextRenderingMode="Auto"
     Background="{DynamicResource MaterialDesignPaper}"
     FontFamily="{DynamicResource MaterialDesignFont}">
  <Grid>
    <materialDesign:Card Padding="32" Margin="16">
      <TextBlock Style="{DynamicResource MaterialDesignTitleTextBlock}">My First Material Design App</TextBlock>
    </materialDesign:Card>
  </Grid>
</Window>
```
