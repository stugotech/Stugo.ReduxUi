using System.Windows;

namespace Stugo.ReduxUi.TodoApp
{
    static class Redux
    {
        public static readonly DependencyProperty StoreProperty =
            DependencyProperty.RegisterAttached("Store", typeof(TodoAppStore), typeof(Redux),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));


        public static TodoAppStore GetStore(DependencyObject obj)
        {
            return (TodoAppStore)obj.GetValue(StoreProperty);
        }


        public static void SetStore(DependencyObject obj, TodoAppStore value)
        {
            obj.SetValue(StoreProperty, value);
        }


        public static TodoAppStore GetTodoAppStore(this FrameworkElement element)
        {
            return GetStore(element);
        }
    }
}
