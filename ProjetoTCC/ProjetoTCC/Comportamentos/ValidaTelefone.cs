using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProjetoTCC.Comportamentos
{
    public class ValidaTelefone : Behavior<Entry>
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

            var isValid = IsValidPhone(entry.Text);
            entry.TextColor = isValid ? Color.Default : Color.Red;
            entry.PlaceholderColor = isValid ? Color.Default : Color.Red;
        }


        private bool IsValidPhone(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return Util.Rotinas.CelularValido(input ?? "");
            }

            return true;
        }
    }
}
