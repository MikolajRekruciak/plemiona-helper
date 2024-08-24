using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_ThirdParty.Utils
{
    public static class WPF_Helpers
    {
        public static void ScrollDownScrollbar(this DataGrid dataGrid)
        {
            if (!(
                GetDescendantByTypeOnly(dataGrid,
                new List<Type> { typeof(ScrollViewer) })
                is ScrollViewer scrollViewer))
            {
                return;
            }

            scrollViewer.ScrollToVerticalOffset(5000);
        }

        public static Visual GetDescendantByTypeOnly(Visual element, Type type) =>
            GetDescendantByTypeOnly(element, new List<Type> { type });

        public static Visual GetDescendantByTypeOnly(Visual element, List<Type> types)
        {
            if (element == null)
                return null;

            if (types.Contains(element.GetType()) &&
                element is FrameworkElement fe &&
                fe.Visibility == Visibility.Visible)
                return fe;

            Visual foundElement = null;
            if (element is FrameworkElement)
                (element as FrameworkElement).ApplyTemplate();

            for (int i = 0;
                i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByTypeOnly(visual, types);
                if (foundElement != null)
                    break;
            }
            return foundElement;
        }

    }
}
