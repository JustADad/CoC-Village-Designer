namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System.Windows.Media;

    public class ClanCastle : DefensiveBuilding
    {
        public ClanCastle()
            : base("CLANCASTLE", "Clan Castle", 3, 3, 12, 0, AttackTargets.AirAndGround)
        {
            this.Color = new SolidColorBrush(Colors.Violet);
        }
    }
}
