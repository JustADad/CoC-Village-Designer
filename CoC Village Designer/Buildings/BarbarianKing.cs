namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Windows.Media;

    public class BarbarianKing : ArmyBuilding
    {
        public BarbarianKing()
            : base("BARBARIANKING", "Barbarian King", 3, 3)
        {
            this.Color = new SolidColorBrush(Colors.OrangeRed);
        }
    }
}
