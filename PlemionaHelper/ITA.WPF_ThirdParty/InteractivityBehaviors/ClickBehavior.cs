using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace WPF_ThirdParty.InteractivityBehaviours
{
    public class ClickBehavior : Behavior<Control>
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        public ClickBehavior()
        {
            _timer.Interval = TimeSpan.FromSeconds(0.2);
            _timer.Tick += Timer_Tick;
        }
        public static readonly DependencyProperty ClickCommandPropery =
             DependencyProperty.Register(nameof(ClickCommand), typeof(ICommand), typeof(ClickBehavior));
        public ICommand ClickCommand
        {
            get => (ICommand)GetValue(ClickCommandPropery);
            set => SetValue(ClickCommandPropery, value);
        }
        public static readonly DependencyProperty DoubleClickCommandPropery =
            DependencyProperty.Register(nameof(DoubleClickCommand), typeof(ICommand), typeof(ClickBehavior));
        public ICommand DoubleClickCommand
        {
            get => (ICommand)GetValue(DoubleClickCommandPropery);
            set => SetValue(DoubleClickCommandPropery, value);
        }

        public static readonly DependencyProperty ClickCommandParameterProperty =
            DependencyProperty.RegisterAttached(nameof(ClickCommandParameter),
                typeof(object),
                typeof(ClickBehavior));

        public object ClickCommandParameter
        {
            get => GetValue(ClickCommandParameterProperty);
            set => SetValue(ClickCommandParameterProperty, value);
        }

        public static readonly DependencyProperty DoubleClickCommandParameterProperty =
            DependencyProperty.RegisterAttached(nameof(DoubleClickCommandParameter),
                typeof(object),
                typeof(ClickBehavior));

        public object DoubleClickCommandParameter
        {
            get => GetValue(DoubleClickCommandParameterProperty);
            set => SetValue(DoubleClickCommandParameterProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObject_Loaded;
            AssociatedObject.Unloaded += AssociatedObject_Unloaded;
        }
        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObject_PreviewMouseLeftButtonDown;
        }
        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Unloaded -= AssociatedObject_Unloaded;
            AssociatedObject.PreviewMouseLeftButtonDown -= AssociatedObject_PreviewMouseLeftButtonDown;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            ClickCommand?.Execute(ClickCommandParameter);
        }
        private void AssociatedObject_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                _timer.Stop();
                DoubleClickCommand?.Execute(DoubleClickCommandParameter);
            }
            else
            {
                _timer.Start();
            }
        }
    }
}
