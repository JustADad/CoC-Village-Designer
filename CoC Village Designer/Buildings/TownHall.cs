namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Media;
    using JustADadSoftware.VillageDesigner.Data;
    using System.Windows.Controls;
    using System.Windows;

    public class TownHall : Building
    {
        public readonly static RoutedEvent TownHallLevelChangedEvent = EventManager.RegisterRoutedEvent("TownHallLevelChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TownHall));

        TownHallLevels townHallLevels = TownHallLevels.Initialize("JustADadSoftware.VillageDesigner.Data.TownHallLevels.xml");

        public TownHall()
            : base("TOWNHALL", "Town Hall", Colors.Violet, 4, 4)
        {
            if (null != this.townHallLevels)
            {
                ContextMenu contextMenu = new ContextMenu();

                foreach (int level in this.townHallLevels.Keys)
                {
                    MenuItem menuItem = new MenuItem();
                    menuItem.Header = String.Format("Level {0}", level);
                    menuItem.Tag = this.townHallLevels[level];
                    menuItem.IsCheckable = true;

                    menuItem.Checked += new RoutedEventHandler(this.TownHallLevelMenuItem_Checked);

                    contextMenu.Items.Add(menuItem);
                }

                this.ContextMenu = contextMenu;

                ((MenuItem)this.ContextMenu.Items[0]).IsChecked = true;
            }
        }

        public event RoutedEventHandler TownHallLevelChanged
        {
            add { this.AddHandler(TownHall.TownHallLevelChangedEvent, value); }
            remove { this.RemoveHandler(TownHall.TownHallLevelChangedEvent, value); }
        }

        public TownHallLevel TownHallLevel
        {
            get;
            private set;
        }

        void TownHallLevelMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (null != menuItem)
            {
                if (null != this.TownHallLevel)
                {
                    ((MenuItem)this.ContextMenu.Items[this.TownHallLevel.Level - 1]).IsChecked = false;
                }

                this.TownHallLevel = menuItem.Tag as TownHallLevel;

                this.RaiseEvent(new RoutedEventArgs(TownHall.TownHallLevelChangedEvent));
            }
        }
    }
}
