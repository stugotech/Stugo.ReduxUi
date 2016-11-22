using System.Globalization;
using System.Windows.Data;

namespace Stugo.ReduxUi.TodoApp
{
    public class Bind : Binding
    {
        public Bind()
        {
            ConverterCulture = CultureInfo.CurrentCulture;
            Mode = BindingMode.OneWay;
        }

        public Bind(string path)
            : base(path)
        {
            ConverterCulture = CultureInfo.CurrentCulture;
            Mode = BindingMode.OneWay;
        }
    }
}
