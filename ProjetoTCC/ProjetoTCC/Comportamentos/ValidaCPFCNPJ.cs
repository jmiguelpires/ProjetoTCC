using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ProjetoTCC.Comportamentos
{
    public class ValidaCPFCNPJ : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Unfocused += Bindable_Unfocused;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Unfocused -= Bindable_Unfocused;

            base.OnDetachingFrom(bindable);
        }

        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            Entry entry = (Entry)sender;

            var isValid = IsValidCPF(entry.Text);

            entry.TextColor = isValid ? Color.Default : Color.Red;
            entry.PlaceholderColor = isValid ? Color.Default : Color.Red;
        }

        private bool IsValidCPF(string input)
        {
            input = Util.Rotinas.RetiraCaracteresNaoNumericos(input);
            if (input.Length > 11)
            {
                return Util.Rotinas.CnpjValido(input);
            }
            else
            {
                return Util.Rotinas.CpfValido(input);
            }
        }
    }
}

