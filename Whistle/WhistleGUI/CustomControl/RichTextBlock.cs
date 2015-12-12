using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WhistleGUI.CustomControl
{
    public class RichTextBlock : TextBlock
    {


        public InlineCollection RichText
        {
            get { return (InlineCollection)GetValue(RichTextProperty); }
            set { SetValue(RichTextProperty, value); }
        }

        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.Register("RichText", typeof(InlineCollection), typeof(RichTextBlock),
                new FrameworkPropertyMetadata(null, (dpObj, args) =>
                {
                    if (args.NewValue != args.OldValue)
                    {
                        var textBlock = dpObj as RichTextBlock;
                        var inlines = args.NewValue as InlineCollection;
                        Inline[] array = inlines.ToArray();

                        textBlock.Inlines.Clear();

                        foreach (var inline in array)
                        {
                            textBlock.Inlines.Add(inline);
                        }
                    }
                })
            );


    }
}
