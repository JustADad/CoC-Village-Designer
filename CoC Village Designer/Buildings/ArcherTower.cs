namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;

    public class ArcherTower : DefensiveBuilding
    {
        public ArcherTower()
            : base("ARCHERTOWER", "Archer Tower", 3, 3, 10, AttackTargets.AirAndGround)
        {
        }
    }
}
