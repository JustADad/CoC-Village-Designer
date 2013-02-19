namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;

    public static class BuildingFactory
    {
        public static string[] BuildingIDs
        {
            get
            {
                return new string[]
                {
                    "ARMYCAMP",
                    "BARRACKS",
                    "LABORATORY",
                    "SPELLFACTORY",
                    "BARBARIANKING",
                    "ARCHERQUEEN",
                    "ELIXIRCOLLECTOR",
                    "ELIXIRSTORAGE",
                    "GOLDMINE",
                    "GOLDSTORAGE",
                    "BUILDERSHUT",
                    "DARKELIXIRPUMP",
                    "DARKELIXIRSTORAGE",
                    "CANNON",
                    "ARCHERTOWER",
                    "WALL",
                    "MORTAR",
                    "BOMB",
                    "AIRDEFENSE",
                    "SPRINGTRAP",
                    "WIZARDTOWER",
                    "GIANTBOMB",
                    "HIDDENTESLA",
                    "XBOW"
                };
            }
        }

        public static Building CreateBuilding(string buildingID)
        {
            Building building = null;

            switch (buildingID)
            {
                case ("ARMYCAMP"):
                    building = new ArmyCamp();
                    break;

                case ("BARRACKS"):
                    building = new Barracks();
                    break;

                case ("LABORATORY"):
                    building = new Laboratory();
                    break;

                case ("SPELLFACTORY"):
                    building = new SpellFactory();
                    break;

                case ("BARBARIANKING"):
                    building = new BarbarianKing();
                    break;

                case ("ARCHERQUEEN"):
                    building = new ArcherQueen();
                    break;

                case ("ELIXIRCOLLECTOR"):
                    building = new ElixirCollector();
                    break;

                case ("ELIXIRSTORAGE"):
                    building = new ElixirStorage();
                    break;

                case ("GOLDMINE"):
                    building = new GoldMine();
                    break;

                case ("GOLDSTORAGE"):
                    building = new GoldStorage();
                    break;

                case ("BUILDERSHUT"):
                    building = new BuildersHut();
                    break;

                case ("DARKELIXIRPUMP"):
                    building = new DarkElixirDrill();
                    break;

                case ("DARKELIXIRSTORAGE"):
                    building = new DarkElixirStorage();
                    break;

                case ("CANNON"):
                    building = new Cannon();
                    break;

                case ("ARCHERTOWER"):
                    building = new ArcherTower();
                    break;

                case ("WALL"):
                    building = new Wall();
                    break;

                case ("MORTAR"):
                    building = new Mortar();
                    break;

                case ("BOMB"):
                    building = new Bomb();
                    break;

                case ("AIRDEFENSE"):
                    building = new AirDefense();
                    break;

                case ("SPRINGTRAP"):
                    building = new SpringTrap();
                    break;

                case ("WIZARDTOWER"):
                    building = new WizardTower();
                    break;

                case ("GIANTBOMB"):
                    building = new GiantBomb();
                    break;

                case ("HIDDENTESLA"):
                    building = new HiddenTesla();
                    break;

                case ("XBOW"):
                    building = new XBow();
                    break;

                default:
                    building = new Building();
                    building.BuildingID = buildingID;
                    break;
            }

            return building;
        }

        public static string GetBuildingName(string buildingID)
        {
            string buildingName = String.Empty;

            switch (buildingID)
            {
                case ("ARMYCAMP"):
                    buildingName = "Army Camp";
                    break;

                case ("BARRACKS"):
                    buildingName = "Barracks";
                    break;

                case ("LABORATORY"):
                    buildingName = "Laboratory";
                    break;

                case ("SPELLFACTORY"):
                    buildingName = "Spell Factory";
                    break;

                case ("BARBARIANKING"):
                    buildingName = "Barbarian King";
                    break;

                case ("ARCHERQUEEN"):
                    buildingName = "Archer Queen";
                    break;

                case ("ELIXIRCOLLECTOR"):
                    buildingName = "Elixir Pump";
                    break;

                case ("ELIXIRSTORAGE"):
                    buildingName = "Elixir Storage";
                    break;

                case ("GOLDMINE"):
                    buildingName = "Gold Mine";
                    break;

                case ("GOLDSTORAGE"):
                    buildingName = "Gold Storage";
                    break;

                case ("BUILDERSHUT"):
                    buildingName = "Builder's Hut";
                    break;

                case ("DARKELIXIRPUMP"):
                    buildingName = "Dark Elixir Drill";
                    break;

                case ("DARKELIXIRSTORAGE"):
                    buildingName = "Dark Elixir Storage";
                    break;

                case ("CANNON"):
                    buildingName = "Cannon";
                    break;

                case ("ARCHERTOWER"):
                    buildingName = "Archer Tower";
                    break;

                case ("WALL"):
                    buildingName = "Wall";
                    break;

                case ("MORTAR"):
                    buildingName = "Mortar";
                    break;

                case ("BOMB"):
                    buildingName = "Bomb";
                    break;

                case ("AIRDEFENSE"):
                    buildingName = "Air Defense";
                    break;

                case ("SPRINGTRAP"):
                    buildingName = "Spring Trap";
                    break;

                case ("WIZARDTOWER"):
                    buildingName = "Wizard Tower";
                    break;

                case ("GIANTBOMB"):
                    buildingName = "Giant Bomb";
                    break;

                case ("HIDDENTESLA"):
                    buildingName = "Hidden Tesla";
                    break;

                case ("XBOW"):
                    buildingName = "X-Bow";
                    break;
            }

            return buildingName;
        }
    }
}
