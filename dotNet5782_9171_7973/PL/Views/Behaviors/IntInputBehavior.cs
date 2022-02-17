using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;

namespace PL.Views.Behaviors
{
    class IntInputBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewTextInput += (s, e) =>
            {
                if (!int.TryParse(e.Text, out int number))
                    e.Handled = true;
            };
        }
    }
}
