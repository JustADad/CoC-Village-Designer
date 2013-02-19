namespace JustADadSoftware.VillageDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;
    using System.Windows;
    using System.Collections.ObjectModel;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Collections.Specialized;
    using System.Windows.Input;

    public class ToolbarGroupButton : ImageButton
    {
        Popup groupItemsPopup;

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<ToolbarGroupItemButton>), typeof(ToolbarGroupButton), new PropertyMetadata(new ObservableCollection<ToolbarGroupItemButton>()));

        public ToolbarGroupButton()
        {
            this.Items = new ObservableCollection<ToolbarGroupItemButton>();
        }

        public ObservableCollection<ToolbarGroupItemButton> Items
        {
            get { return (ObservableCollection<ToolbarGroupItemButton>)this.GetValue(ToolbarGroupButton.ItemsProperty); }
            set { this.SetValue(ToolbarGroupButton.ItemsProperty, value); }
        }

        Popup GroupItemsPopup
        {
            get
            {
                if (null == this.groupItemsPopup)
                {
                    if (null != this.Items &&
                        0 < this.Items.Count)
                    {
                        WrapPanel wrapPanel = new WrapPanel();
                        wrapPanel.Orientation = Orientation.Vertical;
                        wrapPanel.Background = new SolidColorBrush(Colors.Transparent);

                        foreach (ToolbarGroupItemButton itemButton in this.Items)
                        {
                            itemButton.Click += new RoutedEventHandler(this.ItemButton_Click);
                            wrapPanel.Children.Add(itemButton);
                        }

                        this.groupItemsPopup = new Popup();
                        this.groupItemsPopup.Child = wrapPanel;
                        this.groupItemsPopup.StaysOpen = false;
                    }
                }

                return this.groupItemsPopup;
            }
        }

        void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            if (null != this.GroupItemsPopup)
            {
                this.GroupItemsPopup.IsOpen = false;
            }
        }

        protected override void OnClick()
        {
            if (null != this.GroupItemsPopup)
            {
                this.groupItemsPopup.Placement = PlacementMode.Relative;
                this.groupItemsPopup.HorizontalOffset = (this.ActualWidth / 2.0);
                this.groupItemsPopup.VerticalOffset = (this.ActualHeight / 2.0);
                this.groupItemsPopup.PlacementTarget = this;

                foreach (ToolbarGroupItemButton itemButton in this.Items)
                {
                    itemButton.Width = this.ActualHeight;
                    itemButton.Height = this.ActualHeight;
                }

                this.GroupItemsPopup.IsOpen = true;
            }
        }
    }
}
