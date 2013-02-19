namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Windows.Media;

    public class GiantBomb : DefensiveBuilding
    {
        public GiantBomb()
            : base("GIANTBOMB", "Giant Bomb", 2, 2)
        {
            this.Color = new SolidColorBrush(Colors.PaleVioletRed);
        }
    }
}
