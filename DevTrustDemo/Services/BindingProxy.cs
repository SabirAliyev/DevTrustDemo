using System.Windows;

namespace DevTrustDemo.Services
{
    /// <summary>
    /// is <see cref="System.Windows.Freezable"/> class that defines objects that have a modifiable and a read-only state.
    /// Can inherit the DataContext even when they’re not in the visual or logical tree.
    /// Particularly useful when it is not possible to reference from a View to a corresponding View Model.
    /// </summary>
    public class BindingProxy : Freezable
    {
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        public object Data {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));

    }
}
