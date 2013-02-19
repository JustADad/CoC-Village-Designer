namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;

    [Flags]
    public enum AttackTargets
    {
        None = 0,
        Ground = 1,
        Air = 2,
        AirAndGround = 3
    }
}
