using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;

namespace PL.Views.Behaviors
{
    class DoubleInputBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewTextInput += (s, e) =>
            {
                if (!double.TryParse(e.Text, out double number))
                    e.Handled = true;
            };
        }
    }
}
