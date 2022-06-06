using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ProjetoTCC.Comportamentos
{
    public class ValidaData : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Unfocused += OnUnfocused;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Unfocused -= OnUnfocused;

            base.OnDetachingFrom(bindable);
        }

        private void OnUnfocused(object sender, FocusEventArgs e)
        {
            Entry entry = ((Entry)sender);

            var isValid = IsValidEmail(entry.Text);
            entry.TextColor = isValid ? Color.Default : Color.Red;
            entry.PlaceholderColor = isValid ? Color.Default : Color.Red;
        }


        private bool IsValidEmail(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return DateTime.TryParseExact(input, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _);
            }

            return true;
        }
    }
}
