namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Windows.Media;

    public class ArcherQueen : ArmyBuilding
    {
        public ArcherQueen()
            : base("ARCHERQUEEN", "Archer Queen", 3, 3)
        {
            this.Color = new SolidColorBrush(Colors.OrangeRed);
        }
    }
}
