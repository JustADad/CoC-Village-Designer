namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;

    public class Cannon : DefensiveBuilding
    {
        public Cannon()
            : base("CANNON", "Cannon", 3, 3, 9, AttackTargets.Ground)
        {
        }
    }
}
