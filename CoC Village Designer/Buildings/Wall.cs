namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Windows.Media;

    public class Wall : DefensiveBuilding
    {
        public Wall()
            : base("WALL", "Wall", 1, 1)
        {
            this.Color = new SolidColorBrush(Colors.Gray);
        }
    }
}
