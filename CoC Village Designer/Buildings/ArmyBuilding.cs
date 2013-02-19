namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Windows.Media;

    public abstract class ArmyBuilding : Building
    {
        public ArmyBuilding(string id, string label, int gridWidth, int gridHeight)
            : base(id, label, Colors.Orange, gridWidth, gridHeight)
        {
        }
    }
}
