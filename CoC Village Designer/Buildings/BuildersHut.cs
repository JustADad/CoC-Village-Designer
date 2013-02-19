namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Windows.Media;

    public class BuildersHut : ResourceBuilding
    {
        public BuildersHut()
            : base("BUILDERSHUT", "Builder's Hut", 2, 2)
        {
            this.Color = new SolidColorBrush(Colors.SaddleBrown);
        }
    }
}
