namespace JustADadSoftware.VillageDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;
    using System.Windows;

    public class ToolbarGroupItemButton : ImageButton
    {
        public static readonly DependencyProperty ItemIDProperty = DependencyProperty.Register("ItemID", typeof(string), typeof(ToolbarGroupItemButton));

        public string ItemID
        {
            get { return (string)this.GetValue(ToolbarGroupItemButton.ItemIDProperty); }
            set { this.SetValue(ToolbarGroupItemButton.ItemIDProperty, value); }
        }
    }
}
