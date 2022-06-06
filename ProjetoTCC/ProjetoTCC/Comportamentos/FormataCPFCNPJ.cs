using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace ProjetoTCC.Comportamentos
{
    public class FormataCPFCNPJ : Behavior<Entry>
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

            entry.Text = FormatarCPF(entry.Text);
        }

        private string FormatarCPF(string input)
        {
            var digits = Util.Rotinas.RetiraCaracteresNaoNumericos(input);
            
            if(digits.Length == 14)
            {
                return $"{digits.Substring(0, 2)}.{digits.Substring(2, 3)}.{digits.Substring(5, 3)}/{digits.Substring(8, 4)}-{digits.Substring(12)}"; //digits[12..] é o mesmo que digits.Substring(12)
            }
            else
            {
                if (digits.Length <= 3)
                {
                    return digits;
                }

                if (digits.Length <= 6)
                {
                    return $"{digits.Substring(0, 3)}.{digits.Substring(3)}"; //digits[3..] é o mesmo que digits.Substring(3)
                }

                if (digits.Length <= 9)
                {
                    return $"{digits.Substring(0, 3)}.{digits.Substring(3, 3)}.{digits.Substring(6)}"; //digits[6..] é o mesmo que digits.Substring(6)
                }
;
                return $"{digits.Substring(0, 3)}.{digits.Substring(3, 3)}.{digits.Substring(6, 3)}-{digits.Substring(9)}"; //digits[9..] é o mesmo que digits.Substring(9)
            }
        }
    }
}
