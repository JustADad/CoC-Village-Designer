namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Windows.Media;

    public abstract class ResourceBuilding : Building
    {
        public ResourceBuilding(string id, string label, int gridWidth, int gridHeight)
            : base(id, label, Colors.PaleGreen, gridWidth, gridHeight)
        {
        }
    }
}
