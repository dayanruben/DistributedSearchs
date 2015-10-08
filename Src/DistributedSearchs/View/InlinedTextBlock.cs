// Developed by doiTTeam => devdoiTTeam@gmail.com

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DistributedSearchs
{
    public class InlinedTextBlock : TextBlock
    {
        #region IEnumerable<Inline> InlineSource (dependency property with callback)

        public static readonly DependencyProperty InlineSourceProperty =
            DependencyProperty.Register("InlineSource", typeof (IEnumerable<Inline>), typeof (InlinedTextBlock),
                                        new PropertyMetadata(Enumerable.Empty<Inline>(), OnInlineSourcePropertyChanged));

        public IEnumerable<Inline> InlineSource
        {
            get { return (IEnumerable<Inline>) GetValue(InlineSourceProperty); }
            set { SetValue(InlineSourceProperty, value); }
        }

        /// <summary>
        ///   Called when the value of the InlineSource property changes.
        /// </summary>
        /// <param name = "d">Control that changed its InlineSource.</param>
        /// <param name = "e">Event arguments.</param>
        private static void OnInlineSourcePropertyChanged(DependencyObject d,
                                                          DependencyPropertyChangedEventArgs e)
        {
            var source = (InlinedTextBlock) d;
            var oldValue = (IEnumerable<Inline>) e.OldValue;
            var newValue = (IEnumerable<Inline>) e.NewValue;
            source.OnInlineSourcePropertyChanged(oldValue, newValue);
        }

        /// <summary>
        ///   Called when the value of the InlineSource property changes.
        /// </summary>
        /// <param name = "oldValue">The value to be replaced.</param>
        /// <param name = "newValue">The new value.</param>
        protected virtual void OnInlineSourcePropertyChanged(IEnumerable<Inline> oldValue, IEnumerable<Inline> newValue)
        {
            Inlines.Clear();
            Inlines.AddRange(newValue);
        }

        #endregion IEnumerable<Inline> InlineSource (dependency property)
    }
}