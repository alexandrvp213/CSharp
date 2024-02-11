using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EntryDecimal
{
    /// <summary>
    /// Нужно для корректного ввода дробных чисел, в SDK андроида на эту тему баг, невозможно ввести разделитель в дробное число в неанглийской версии андроида
    /// </summary>
    public class MyCustomEntry : Entry
    {
    }
}
