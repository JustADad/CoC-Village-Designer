namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;

    public class AirDefense : DefensiveBuilding
    {
        public AirDefense()
            : base("AIRDEFENSE", "Air Defense", 3, 3, 10, AttackTargets.Air)
        {
        }
    }
}
