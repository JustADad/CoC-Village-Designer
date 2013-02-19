namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Windows.Media;

    public class Bomb : DefensiveBuilding
    {
        public Bomb()
            : base("BOMB", "Bomb", 1, 1)
        {
            this.Color = new SolidColorBrush(Colors.PaleVioletRed);
        }
    }
}
