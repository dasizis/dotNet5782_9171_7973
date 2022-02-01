﻿using Microsoft.Xaml.Behaviors;
using System.Windows.Controls;

namespace PL.Views.Behaviors
{
    class DoubleInputBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.PreviewTextInput += (s, e) =>
            {
                TextBox textBox = s as TextBox;

                if (e.Text == ".")
                {
                    e.Handled = !int.TryParse(textBox.Text, out int _);
                }
                
                if (!double.TryParse(textBox.Text + e.Text, out double __))
                {
                    e.Handled = true;
                }
            };
        }
    }
}
