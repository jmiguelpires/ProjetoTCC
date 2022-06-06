using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ProjetoTCC.Comportamentos
{
    public class FormataTelefone : Behavior<Entry>
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

            entry.Text = FormatPhoneNumber(entry.Text);
        }

        private string FormatPhoneNumber(string input)
        {
            var digitsRegex = new Regex(@"[^\d]");
            var digits = digitsRegex.Replace(input ?? "", "");

            if (digits.Length <= 2)
            {
                return digits;
            }

            if (digits.Length <= 3)
            {
                return $"({digits.Substring(0, 2)}) {digits.Substring(2)}"; //digits[2..] é o mesmo que digits.Substring(2)
            }

            if (digits.Length <= 7)
            {
                return $"({digits.Substring(0, 2)}) {digits.Substring(2, 1)} {digits.Substring(3)}"; //digits[3..] é o mesmo que digits.Substring(3)
            }

            return $"({digits.Substring(0, 2)}) {digits.Substring(2, 1)} {digits.Substring(3, 4)}-{digits.Substring(7)}"; //digits[7..] é o mesmo que digits.Substring(7)
        }
    }
}
