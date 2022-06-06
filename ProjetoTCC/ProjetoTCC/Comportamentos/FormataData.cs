using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ProjetoTCC.Comportamentos
{
    public class FormataData : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnTextChanged;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnTextChanged;

            base.OnDetachingFrom(bindable);
        }

        void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = (Entry)sender;

            entry.Text = FormatarData(entry.Text);
        }

        private string FormatarData(string input)
        {
            var digitsRegex = new Regex(@"[^\d]");
            var digits = digitsRegex.Replace(input ?? "", "");


            if (digits.Length <= 2)
            {
                return digits;
            }

            if (digits.Length <= 4)
            {
                return $"{digits.Substring(0, 2)}/{digits.Substring(2)}"; //digits[2..] é o mesmo que digits.Substring(2)
            }

            return $"{digits.Substring(0, 2)}/{digits.Substring(2, 2)}/{digits.Substring(4)}"; //digits[4..] é o mesmo que digits.Substring(4)
        }
    }
}
