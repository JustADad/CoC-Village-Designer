namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Windows.Media;

    public class SpringTrap : DefensiveBuilding
    {
        public SpringTrap()
            : base("SPRINGTRAP", "Spring Trap", 1, 1)
        {
            this.Color = new SolidColorBrush(Colors.PaleVioletRed);
        }
    }
}
   