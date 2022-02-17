using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
