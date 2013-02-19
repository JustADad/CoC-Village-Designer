namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;

    public class HiddenTesla : DefensiveBuilding
    {
        public HiddenTesla()
            : base("HIDDENTESLA", "Hidden Tesla", 3, 3, 7, AttackTargets.Ground)
        {
        }
    }
}
