namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Windows.Controls;
    using System.Windows;

    public class XBow : DefensiveBuilding
    {
        public XBow()
            : base("XBOW", "X-Bow", 3, 3, 14, AttackTargets.Ground)
        {
            ContextMenu contextMenu = new ContextMenu();

            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Target Ground Only";
            menuItem.Tag = AttackTargets.Ground;
            menuItem.IsCheckable = true;
            menuItem.IsChecked = true;
            menuItem.Checked += new RoutedEventHandler(this.ChangeTargetMenuItem_Checked);

            contextMenu.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Target Air and Ground";
            menuItem.Tag = AttackTargets.AirAndGround;
            menuItem.IsCheckable = true;
            menuItem.Checked += new RoutedEventHandler(this.ChangeTargetMenuItem_Checked);

            contextMenu.Items.Add(menuItem);

            this.ContextMenu = contextMenu;
        }

        void ChangeTargetMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            MenuItem changeTargetMenuItem = sender as MenuItem;
            if (null != changeTargetMenuItem)
            {
                this.Targets = (AttackTargets)changeTargetMenuItem.Tag;

                foreach (MenuItem menuItem in this.ContextMenu.Items)
                {
                    if (this.Targets != (AttackTargets)menuItem.Tag)
                    {
                        menuItem.IsChecked = false;
                    }
                }
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (DefensiveBuilding.TargetsProperty == e.Property)
            {
                switch ((AttackTargets)e.NewValue)
                {
                    case(AttackTargets.Ground):
                        this.MaxAttackRange = 14;
                        break;

                    case(AttackTargets.AirAndGround):
                        this.MaxAttackRange = 11;
                        break;
                }

                this.InvalidateVisual();
            }

            base.OnPropertyChanged(e);
        }
    }
}
