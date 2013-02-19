namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;

    public class Mortar : DefensiveBuilding
    {
        public Mortar()
            : base("MORTAR", "Mortar", 3, 3, 11, 4, AttackTargets.Ground)
        {
        }
    }
}
