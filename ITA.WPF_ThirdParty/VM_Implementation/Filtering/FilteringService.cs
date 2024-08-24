using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ThirdParty.VM_Implementation.Filtering
{
    public class FilteringService
    {
        private string _valueToFilterBy;
        private PropertyInfo[] _filtrableProps;

        public void SetValueToFilterBy(string valueToFilterBy)
        {
            _valueToFilterBy = valueToFilterBy;
        }

        public void SetPropertiesToFilterBy(System.Windows.Controls.ItemsControl itemsControl)
        {
            _filtrableProps = (itemsControl.ItemsSource.GetType()).GenericTypeArguments[0].GetProperties()
                .Where(q => q.GetCustomAttribute(typeof(Filtrable)) != null).ToArray();
        }

        public void SetPropertiesToFilterBy(Type type)
        {
            _filtrableProps = type.GetProperties()
                .Where(q => q.GetCustomAttribute(typeof(Filtrable)) != null).ToArray();
        }

        public virtual bool FrameworkFilter(object item)
        {
            return String.IsNullOrEmpty(_valueToFilterBy) ? true :
                _valueToFilterBy.Split(' ').All(p =>
                 _filtrableProps
                    .Where(prop => item.GetType().GetProperty(prop.Name).GetValue(item, null)?.ToString() != null)
                    .Any(prop => prop.PropertyType != typeof(DateTime)
                        ? item.GetType().GetProperty(prop.Name).GetValue(item, null).ToString().IndexOf(p, StringComparison.OrdinalIgnoreCase) >= 0
                        : ((DateTime)item.GetType().GetProperty(prop.Name).GetValue(item, null)).ToString(Constants.DateTimeUiFormat).IndexOf(p, StringComparison.OrdinalIgnoreCase) >= 0));
        }
    }
}
