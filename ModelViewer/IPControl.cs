using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModelViewer
{
    public class IPControl : TextBox
    {
        private const string IpRegex =
            @"([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\.([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\.([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])\.([0-9]|[0-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5]|[*])";

        public IPControl()
        {
            this.PreviewTextInput += this.TextBoxPreviewTextInput;
            this.PreviewKeyDown += this.HandleHandledKeyDown;
            DataObject.AddPastingHandler(this, this.IPControl_Pasting);
        }

        public void HandleHandledKeyDown(object sender, RoutedEventArgs e)
        {
            var ke = e as KeyEventArgs;
            if (ke != null && ke.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private static bool ValidCharacter(string enteredValue)
        {
            Match valRes = Regex.Match(enteredValue, "[0-9.]", RegexOptions.IgnoreCase);
            if (!valRes.Success)
            {
                return false;
            }

            return true;
        }

        private static int CountOfDot(string text)
        {
            int dotCount = text.Count(c => c == '.');
            return dotCount;
        }

        private string AlterValue(TextCompositionEventArgs e, TextBox text, string val, int insertIndex, bool replace, out bool moveNext, int replaceIndex = 0)
        {
            string textVal = text.Text;
            string oldValue = textVal;
            if (replace)
            {
                textVal = textVal.Remove(replaceIndex, 1);
            }

            textVal = textVal.Insert(insertIndex, val);

            if (!this.ValidIpFragment(textVal, out moveNext))
            {
                return this.Manipulate(e, text, oldValue, val, insertIndex);
            }

            return textVal;
        }

        private void Do(TextCompositionEventArgs e, string val, TextBox tx, int ind, int nextIndexOfDot, int indexDiff, int dotCount)
        {
            bool moveNext;

            switch (indexDiff)
            {
                case 0:
                    int previousIndexOfDot = this.FindPreviousIndexOf(".", tx.Text, ind - 1);
                    int indexDiff2 = Math.Abs(previousIndexOfDot - nextIndexOfDot);

                    if (4 > indexDiff2)
                    {
                        this.Do(e, val, tx, ind, nextIndexOfDot, indexDiff2, dotCount);
                        return;
                    }
                    else
                    {
                        this.HandleTextInput(e, ind + 1, val, tx);
                    }

                    break;
                case 4:
                    if (tx.Text.Length <= ind)
                    {
                        if (dotCount >= 3)
                        {
                            e.Handled = true;
                            tx.CaretIndex = ind + 1;
                            return;
                        }

                        if (val != ".")
                        {
                            tx.Text = this.AlterValue(e, tx, ".", ind, false, out moveNext);
                            tx.Text = this.AlterValue(e, tx, val, ind + 1, false, out moveNext);
                        }
                        else
                        {
                            tx.Text = this.AlterValue(e, tx, val, ind, false, out moveNext);
                        }

                        tx.CaretIndex = ind + 2;
                    }
                    else
                    {
                        string ret1 = this.AlterValue(e, tx, val, ind, true, out moveNext, ind);
                        if (!moveNext)
                        {
                            tx.Text = ret1;
                            tx.CaretIndex = ind + 1;
                        }
                        else
                        {
                            int carInd = tx.CaretIndex;
                            tx.Text = ret1;
                            tx.CaretIndex = carInd + 1;
                        }
                    }

                    e.Handled = true;

                    break;
                default:
                    string ret = this.AlterValue(e, tx, val, ind, false, out moveNext);

                    e.Handled = true;
                    if (!moveNext)
                    {
                        tx.Text = ret;
                        tx.CaretIndex = ind + 1;
                    }
                    else
                    {
                        int carInd = tx.CaretIndex;
                        tx.Text = ret;
                        tx.CaretIndex = carInd + 1;
                    }

                    break;
            }
        }

        private int FindNextIndexOf(string p, string text, int ind)
        {
            int index = text.IndexOf(p, ind);
            if (index == -1)
            {
                return text.Length;
            }

            return index;
        }

        private int FindPreviousIndexOf(string p, string text, int ind)
        {
            if (ind < 0)
            {
                return ind;
            }

            return text.LastIndexOf(p, ind);
        }

        private void HandleExcessDot(TextCompositionEventArgs e, int ind, TextBox textBox)
        {
            if (textBox.Text.Count() > ind)
            {
                int findNextIndexOf = this.FindNextIndexOf(".", textBox.Text, ind);
                if (findNextIndexOf != textBox.Text.Count())
                {
                    int count = findNextIndexOf - ind;
                    textBox.Text = textBox.Text.Remove(ind, count);
                    textBox.CaretIndex = ind + 1;
                }
                else
                {
                    textBox.CaretIndex = findNextIndexOf;
                }
            }

            e.Handled = true;
        }

        private void HandleTextInput(TextCompositionEventArgs e, int ind, string enteredValue, TextBox textBox)
        {
            if (!ValidCharacter(enteredValue))
            {
                e.Handled = true;
                textBox.CaretIndex = ind;
                return;
            }

            int dotCount = CountOfDot(textBox.Text);

            if (enteredValue == ".")
            {
                if (dotCount >= 3)
                {
                    this.HandleExcessDot(e, ind, textBox);
                    return;
                }
            }

            int previousIndexOfDot = this.FindPreviousIndexOf(".", textBox.Text, ind);
            int nextIndexOfDot = this.FindNextIndexOf(".", textBox.Text, ind);
            int indexDiff = nextIndexOfDot - previousIndexOfDot;
            if (4 >= indexDiff)
            {
                this.Do(e, enteredValue, textBox, ind, nextIndexOfDot, indexDiff, dotCount);
            }
            else
            {
                e.Handled = true;
            }
        }

        private void IPControl_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            var textBox = (TextBox)sender;
            textBox.Clear();
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));

                bool res = Regex.IsMatch(text, IpRegex);
                if (!res)
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private string Manipulate(TextCompositionEventArgs e, TextBox text, string oldValue, string val, int insertIndex)
        {
            int previousIndexOfDot = this.FindPreviousIndexOf(".", oldValue, insertIndex);
            int nextIndexOfDot = this.FindNextIndexOf(".", oldValue, insertIndex);
            int indexDiff = nextIndexOfDot - previousIndexOfDot;

            int countOfDot = CountOfDot(oldValue);

            if (0 < indexDiff)
            {
                if (insertIndex != nextIndexOfDot)
                {
                    oldValue = oldValue.Remove(previousIndexOfDot + 1, indexDiff - 1);
                    oldValue = oldValue.Insert(previousIndexOfDot + 1, val);
                    text.CaretIndex = previousIndexOfDot + 1;
                }
                else
                {
                    if (countOfDot < 3)
                    {
                        this.HandleTextInput(e, text.CaretIndex, ".", text);
                        this.HandleTextInput(e, text.CaretIndex, val, text);
                        oldValue = text.Text;
                    }
                }
            }
            else
            {
                this.HandleTextInput(e, nextIndexOfDot + 1, val, text);
                text.CaretIndex = text.CaretIndex - 1;
                oldValue = text.Text;
            }

            return oldValue;
        }

        private void TextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            string enteredValue = e.Text;

            this.HandleTextInput(e, textBox.CaretIndex, enteredValue, textBox);
        }

        private bool ValidIpFragment(string textVal, out bool moveToNext)
        {
            moveToNext = false;
            string[] arr = textVal.Split('.');
            int count = 0;
            foreach (string item in arr)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    int val = int.Parse(item);
                    if (val > 255)
                    {
                        if (count < 3)
                        {
                            moveToNext = true;
                        }

                        return false;
                    }
                }

                count++;
            }

            return true;
        }
    }
}
