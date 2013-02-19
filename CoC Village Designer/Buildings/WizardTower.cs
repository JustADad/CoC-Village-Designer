namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;

    public class WizardTower : DefensiveBuilding
    {
        public WizardTower()
            : base("WIZARDTOWER", "Wizard Tower", 3, 3, 7, AttackTargets.AirAndGround)
        {
        }
    }
}
