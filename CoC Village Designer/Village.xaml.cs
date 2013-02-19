namespace JustADadSoftware.VillageDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using JustADadSoftware.VillageDesigner.Buildings;
    using JustADadSoftware.VillageDesigner.Data;

    public partial class Village : UserControl
    {
        public class VillageDetail
        {
            public VillageDetail(string buildingId, string buildingName, int currentCount, int maxCount)
            {
                this.BuildingId = buildingId;
                this.BuildingName = buildingName;
                this.CurrentCount = currentCount;
                this.MaxCount = maxCount;
            }

            public string BuildingId { get; set; }
            public string BuildingName { get; set; }
            public int CurrentCount { get; set; }
            public int MaxCount { get; set; }

            public override string ToString()
            {
                return String.Format(
                    "{0} : {1} / {2}",
                    this.BuildingName,
                    this.CurrentCount,
                    this.MaxCount);
            }
        }

        public const int VillageGridSize = 40;
        const double GridSquareSize = 18.0;

        Binding attackRangeLayerBinding;
        Dictionary<string, int> buildingCounts = new Dictionary<string, int>();
        ClanCastle clanCastle;
        Building draggingBuilding = null;
        int draggingBuildingInitialColumn;
        int draggingBuildingInitialRow;
        Key lastDirectionKey = Key.Right;
        Building selectedBuilding = null;
        Binding spawnShieldLayerBinding;
        TownHall townHall;
        List<Border> villageGridSquares;

        public static readonly DependencyProperty TownHallLevelLabelProperty = DependencyProperty.Register("TownHallLevelLabel", typeof(string), typeof(Village));
        public static readonly DependencyProperty ShowAttackRangeLayerProperty = DependencyProperty.Register("ShowAttackRangeLayer", typeof(AttackTargets), typeof(Village), new PropertyMetadata(AttackTargets.AirAndGround));
        public static readonly DependencyProperty ShowSpawnShieldLayerProperty = DependencyProperty.Register("ShowSpawnShieldLayer", typeof(bool), typeof(Village));
        public static readonly DependencyProperty ShowVillageGridLinesProperty = DependencyProperty.Register("ShowVillageGridLines", typeof(bool), typeof(Village));
        public static readonly DependencyProperty VillageDetailsProperty = DependencyProperty.Register("VillageDetails", typeof(List<VillageDetail>), typeof(Village));

        public Village()
        {
            InitializeComponent();

            this.DataContext = this;

            App.Current.MainWindow.KeyDown += this.UserControl_KeyDown;
            this.CreateNewVillage();
        }

        Binding AttackRangeLayerBinding
        {
            get
            {
                if (null == this.attackRangeLayerBinding)
                {
                    this.attackRangeLayerBinding = new Binding("ShowAttackRangeLayer");
                    this.attackRangeLayerBinding.Source = this;
                }

                return this.attackRangeLayerBinding;
            }
        }

        public AttackTargets ShowAttackRangeLayer
        {
            get { return (AttackTargets)this.GetValue(Village.ShowAttackRangeLayerProperty); }
            set { this.SetValue(Village.ShowAttackRangeLayerProperty, value); }
        }

        public bool ShowSpawnShieldLayer
        {
            get { return (bool)this.GetValue(Village.ShowSpawnShieldLayerProperty); }
            set { this.SetValue(Village.ShowSpawnShieldLayerProperty, value); }
        }

        public bool ShowVillageGridLines
        {
            get { return (bool)this.GetValue(Village.ShowVillageGridLinesProperty); }
            set { this.SetValue(Village.ShowVillageGridLinesProperty, value); }
        }

        Binding SpawnShieldLayerBinding
        {
            get
            {
                if (null == this.spawnShieldLayerBinding)
                {
                    this.spawnShieldLayerBinding = new Binding("ShowSpawnShieldLayer");
                    this.spawnShieldLayerBinding.Source = this;
                }

                return this.spawnShieldLayerBinding;
            }
        }

        public string TownHallLevelLabel
        {
            get { return (string)this.GetValue(Village.TownHallLevelLabelProperty); }
            set { this.SetValue(Village.TownHallLevelLabelProperty, value); }
        }

        public List<VillageDetail> VillageDetails
        {
            get { return (List<VillageDetail>)this.GetValue(Village.VillageDetailsProperty); }
            set { this.SetValue(Village.VillageDetailsProperty, value); }
        }

        public Building AddBuilding(string buildingID)
        {
            Building newBuilding = null;

            if(String.IsNullOrEmpty(buildingID))
            {
                throw new NullReferenceException("buildingID");
            }
            else if(String.IsNullOrEmpty(BuildingFactory.GetBuildingName(buildingID)))
            {
                throw new ArgumentException(String.Format("Unknown BuildingID: '{0)'.", buildingID));
            }

            if (!this.buildingCounts.ContainsKey(buildingID))
            {
                this.buildingCounts.Add(buildingID, 0);
            }

            if(this.buildingCounts[buildingID] < this.townHall.TownHallLevel.Details[buildingID].MaxAllowed)
            {
                newBuilding = BuildingFactory.CreateBuilding(buildingID);

                newBuilding.IsDraggingChanged += new RoutedEventHandler(this.Building_IsDraggingChanged);
                newBuilding.IsSelectedChanged += new RoutedEventHandler(this.Building_IsSelectedChanged);

                newBuilding.SetBinding(Building.ShowSpawnShieldLayerProperty, this.SpawnShieldLayerBinding);

                DefensiveBuilding newDefensiveBuilding = newBuilding as DefensiveBuilding;
                if (null != newDefensiveBuilding)
                {
                    newDefensiveBuilding.SetBinding(DefensiveBuilding.ShowAttackRangeLayerProperty, this.AttackRangeLayerBinding);
                }

                this.villageLayoutGrid.Children.Add(newBuilding);

                if (null != this.selectedBuilding)
                {
                    int selectedColumn = Grid.GetColumn(this.selectedBuilding);
                    int selectedRow = Grid.GetRow(this.selectedBuilding);

                    int initialColumn = selectedColumn;
                    int initialRow = selectedRow;

                    switch (this.lastDirectionKey)
                    {
                        case(Key.Right):
                            initialColumn = (Village.VillageGridSize < selectedColumn + this.selectedBuilding.GridWidth + newBuilding.GridWidth) ? selectedColumn - newBuilding.GridWidth : selectedColumn + this.selectedBuilding.GridWidth;
                            break;

                        case(Key.Left):
                            initialColumn = (0 > selectedColumn - newBuilding.GridWidth) ? selectedColumn + this.selectedBuilding.GridWidth : selectedColumn - newBuilding.GridWidth;
                            break;

                        case(Key.Up):
                            initialRow = (0 > selectedRow - newBuilding.GridHeight) ? selectedRow + this.selectedBuilding.GridHeight : selectedRow - newBuilding.GridHeight;
                            break;

                        case(Key.Down):
                            initialRow = (Village.VillageGridSize < selectedRow + this.selectedBuilding.GridHeight + newBuilding.GridHeight) ? selectedRow - newBuilding.GridHeight : selectedRow + this.selectedBuilding.GridHeight;
                            break;
                    }

                    Grid.SetColumn(newBuilding, initialColumn);
                    Grid.SetRow(newBuilding, initialRow);
                }
                else
                {
                    Grid.SetColumn(newBuilding, newBuilding.GridWidth);
                    Grid.SetRow(newBuilding, newBuilding.GridHeight);
                }

                newBuilding.IsSelected = true;
                this.buildingCounts[buildingID]++;
            }

            this.RefreshVillageDetails();

            return newBuilding;
        }

        int selectedBuildingZOrder = 1;

        void Building_IsDraggingChanged(object sender, RoutedEventArgs e)
        {
            Building targetBuilding = sender as Building;
            if (targetBuilding.IsDragging)
            {
                this.draggingBuilding = targetBuilding;
                this.draggingBuilding.IsSelected = true;
                this.draggingBuildingInitialColumn = Grid.GetColumn(this.draggingBuilding);
                this.draggingBuildingInitialRow = Grid.GetRow(this.draggingBuilding);
            }
            else
            {
                this.draggingBuilding = null;
            }
        }

        void Building_IsSelectedChanged(object sender, RoutedEventArgs e)
        {
            Building building = sender as Building;
            if (null != building)
            {
                if (building.IsSelected)
                {
                    if (null != this.selectedBuilding)
                    {
                        this.selectedBuilding.IsSelected = false;
                    }

                    this.selectedBuilding = building;
                    Panel.SetZIndex(this.selectedBuilding, ++this.selectedBuildingZOrder);
                }
                else
                {
                    if (building == this.selectedBuilding)
                    {
                        this.selectedBuilding = null;
                    }
                }
            }
        }

        public void CreateNewVillage()
        {
            this.RefreshVillageDetails();
            this.buildingCounts = new Dictionary<string, int>();
            this.selectedBuilding = null;

            this.villageLayoutGrid.Children.Clear();
            this.villageLayoutGrid.ColumnDefinitions.Clear();
            this.villageLayoutGrid.RowDefinitions.Clear();

            for (int i = 0; i < Village.VillageGridSize; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                this.villageLayoutGrid.ColumnDefinitions.Add(columnDefinition);

                RowDefinition rowDefinition = new RowDefinition();
                this.villageLayoutGrid.RowDefinitions.Add(rowDefinition);
            }

            this.villageGridSquares = new List<Border>(Village.VillageGridSize * Village.VillageGridSize);
            for (int x = 0; x < Village.VillageGridSize; x++)
            {
                for (int y = 0; y < Village.VillageGridSize; y++)
                {
                    BitmapImage bitmapImage = new BitmapImage(new Uri("Images\\VillageBackground.png", UriKind.RelativeOrAbsolute));

                    Image cellImage = new Image();
                    cellImage.Source = bitmapImage;
                    cellImage.Stretch = Stretch.Uniform;
                    
                    Border villageGridSquare = new Border();
                    villageGridSquare.Child = cellImage;

                    if (this.ShowVillageGridLines)
                    {
                        villageGridSquare.BorderBrush = new SolidColorBrush(Colors.Black);
                        villageGridSquare.BorderThickness = new Thickness(1.0);
                    }
                    
                    this.villageGridSquares.Add(villageGridSquare);

                    this.villageLayoutGrid.Children.Add(villageGridSquare);
                    Grid.SetColumn(villageGridSquare, x);
                    Grid.SetRow(villageGridSquare, y);
                }
            }

            this.UpdateGridSize();

            this.townHall = new TownHall();
            this.townHall.TownHallLevelChanged += new RoutedEventHandler(this.TownHallLevelChanged);
            this.townHall.IsDraggingChanged += new RoutedEventHandler(this.Building_IsDraggingChanged);
            this.townHall.IsSelectedChanged += new RoutedEventHandler(this.Building_IsSelectedChanged);

            this.townHall.SetBinding(Building.ShowSpawnShieldLayerProperty, this.SpawnShieldLayerBinding);

            this.villageLayoutGrid.Children.Add(this.townHall);

            Grid.SetColumn(this.townHall, (Village.VillageGridSize / 2) - (this.townHall.GridWidth / 2));
            Grid.SetRow(this.townHall, (Village.VillageGridSize / 2) - (this.townHall.GridHeight / 2));

            this.clanCastle = new ClanCastle();
            this.clanCastle.IsDraggingChanged += new RoutedEventHandler(this.Building_IsDraggingChanged);
            this.clanCastle.IsSelectedChanged += new RoutedEventHandler(this.Building_IsSelectedChanged);

            this.clanCastle.SetBinding(DefensiveBuilding.ShowAttackRangeLayerProperty, this.AttackRangeLayerBinding);
            this.clanCastle.SetBinding(Building.ShowSpawnShieldLayerProperty, this.SpawnShieldLayerBinding);

            this.villageLayoutGrid.Children.Add(this.clanCastle);

            Grid.SetColumn(this.clanCastle, Grid.GetColumn(this.townHall) + 5);
            Grid.SetRow(this.clanCastle, Grid.GetRow(this.townHall) + 5);

            this.RefreshVillageDetails();
        }

        void RefreshVillageDetails()
        {
            this.VillageDetails = new List<VillageDetail>();

            foreach (string buildingID in BuildingFactory.BuildingIDs)
            {
                this.VillageDetails.Add(new VillageDetail(
                    buildingID,
                    BuildingFactory.GetBuildingName(buildingID),
                    (this.buildingCounts.ContainsKey(buildingID)) ? this.buildingCounts[buildingID] : 0,
                    (null != this.townHall) ? this.townHall.TownHallLevel.Details[buildingID].MaxAllowed : 0));
            }

            if (null != this.townHall &&
                null != this.townHall.TownHallLevel)
            {
                this.TownHallLevelLabel = String.Format(
                    "TownHall Level {0}",
                    this.townHall.TownHallLevel.Level);
            }
        }

        void MoveBuilding(Building building, int xOffset, int yOffset)
        {
            int currentColumn = Grid.GetColumn(building);
            int currentRow = Grid.GetRow(building);

            int newColumn = currentColumn + xOffset;
            int newRow = currentRow + yOffset;

            if (-1 < newColumn &&
                -1 < newRow &&
                Village.VillageGridSize >= newColumn + building.GridWidth &&
                Village.VillageGridSize >= newRow + building.GridHeight)
            {
                Grid.SetColumn(building, newColumn);
                Grid.SetRow(building, newRow);
            }
        }

        public void OpenVillage(string fileName)
        {
            this.CreateNewVillage();

            using (System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(fileName))
            {
                xmlReader.Read();

                while (System.Xml.XmlNodeType.Element != xmlReader.NodeType)
                {
                    xmlReader.MoveToContent();
                }

                xmlReader.MoveToAttribute("townHallLevel");
                int townHallLevel = Int32.Parse(xmlReader.Value);
                ((MenuItem)this.townHall.ContextMenu.Items[townHallLevel - 1]).IsChecked = true;
                xmlReader.MoveToElement();

                xmlReader.Read();

                while (System.Xml.XmlNodeType.EndElement != xmlReader.NodeType)
                {
                    if (System.Xml.XmlNodeType.Element == xmlReader.NodeType)
                    {
                        if (xmlReader.Name.Equals("building", StringComparison.Ordinal))
                        {
                            xmlReader.MoveToAttribute("id");
                            string buildingId = xmlReader.Value;

                            xmlReader.MoveToAttribute("xPosition");
                            int column = Int32.Parse(xmlReader.Value);

                            xmlReader.MoveToAttribute("yPosition");
                            int row = Int32.Parse(xmlReader.Value);

                            if (this.townHall.BuildingID.Equals(buildingId, StringComparison.Ordinal))
                            {
                                Grid.SetColumn(this.townHall, column);
                                Grid.SetRow(this.townHall, row);
                            }
                            else if (this.clanCastle.BuildingID.Equals(buildingId, StringComparison.Ordinal))
                            {
                                Grid.SetColumn(this.clanCastle, column);
                                Grid.SetRow(this.clanCastle, row);
                            }
                            else
                            {
                                Building newBuilding = this.AddBuilding(buildingId);
                                if (null != newBuilding)
                                {
                                    Grid.SetColumn(newBuilding, column);
                                    Grid.SetRow(newBuilding, row);
                                }
                            }
                        }

                        xmlReader.MoveToElement();
                        xmlReader.Read();
                    }
                    else
                    {
                        xmlReader.MoveToContent();
                    }
                }
            }
        }

        void RemoveBuilding(Building targetBuilding)
        {
            if (targetBuilding != this.townHall &&
                targetBuilding != this.clanCastle)
            {
                if (targetBuilding == this.selectedBuilding)
                {
                    this.selectedBuilding = null;
                }

                this.buildingCounts[targetBuilding.BuildingID]--;
                this.villageLayoutGrid.Children.Remove(targetBuilding);
            }

            this.RefreshVillageDetails();
        }

        public void SaveVillage(string fileName)
        {
            using (System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(fileName))
            {
                xmlWriter.WriteStartElement("village");

                xmlWriter.WriteAttributeString("townHallLevel", this.townHall.TownHallLevel.Level.ToString());

                foreach (UIElement uiElement in this.villageLayoutGrid.Children)
                {
                    Building building = uiElement as Building;
                    if (null != building)
                    {
                        xmlWriter.WriteStartElement("building");

                        xmlWriter.WriteAttributeString("id", building.BuildingID);
                        xmlWriter.WriteAttributeString("xPosition", Grid.GetColumn(building).ToString());
                        xmlWriter.WriteAttributeString("yPosition", Grid.GetRow(building).ToString());
                        xmlWriter.WriteEndElement();
                    }
                }

                xmlWriter.WriteEndElement();
            }
        }

        void TownHallLevelChanged(object sender, RoutedEventArgs e)
        {
            this.RefreshVillageDetails();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Village.ShowVillageGridLinesProperty == e.Property)
            {
                if (null != this.villageGridSquares)
                {
                    if ((bool)e.NewValue)
                    {
                        foreach (Border villageGridSquare in this.villageGridSquares)
                        {
                            villageGridSquare.BorderBrush = new SolidColorBrush(Colors.Black);
                            villageGridSquare.BorderThickness = new Thickness(1.0);
                        }
                    }
                    else
                    {
                        foreach (Border villageGridSquare in this.villageGridSquares)
                        {
                            villageGridSquare.BorderBrush = null;
                            villageGridSquare.BorderThickness = new Thickness(0.0);
                        }
                    }
                }
            }

            base.OnPropertyChanged(e);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            this.UpdateGridSize();
            base.OnRenderSizeChanged(sizeInfo);
        }

        void UpdateGridSize()
        {
            if (0 < this.RenderSize.Height &&
                0 < this.RenderSize.Width)
            {
                double availableWidth = this.RenderSize.Width - this.villageDetailsPanel.ActualWidth - (this.villageBorderGrid.ColumnDefinitions[0].Width.Value + this.villageBorderGrid.ColumnDefinitions[2].Width.Value);
                double availableHeight = this.RenderSize.Height - (this.villageBorderGrid.RowDefinitions[0].Height.Value + this.villageBorderGrid.RowDefinitions[2].Height.Value);

                double gridSize = Math.Min(
                    availableHeight / (double)Village.VillageGridSize,
                    availableWidth / (double)Village.VillageGridSize);

                for (int i = 0; i < Village.VillageGridSize; i++)
                {
                    this.villageLayoutGrid.ColumnDefinitions[i].Width = new GridLength(gridSize);
                    this.villageLayoutGrid.RowDefinitions[i].Height = new GridLength(gridSize);
                }
            }
        }

        void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = false;

            if (null != this.selectedBuilding)
            {
                switch (e.Key)
                {
                    case (Key.Add):
                    case (Key.OemPlus):
                        this.AddBuilding(this.selectedBuilding.BuildingID);
                        e.Handled = true;
                        break;

                    case (Key.Subtract):
                    case (Key.OemMinus):
                    case (Key.Delete):
                        this.RemoveBuilding(this.selectedBuilding);
                        e.Handled = true;
                        break;

                    case(Key.Right):
                    case (Key.D):
                        this.MoveBuilding(this.selectedBuilding, 1, 0);
                        this.lastDirectionKey = Key.Right;
                        e.Handled = true;
                        break;

                    case(Key.Left):
                    case (Key.A):
                        this.MoveBuilding(this.selectedBuilding, -1, 0);
                        this.lastDirectionKey = Key.Left;
                        e.Handled = true;
                        break;

                    case (Key.Up):
                    case(Key.W):
                        this.MoveBuilding(this.selectedBuilding, 0, -1);
                        this.lastDirectionKey = Key.Up;
                        e.Handled = true;
                        break;

                    case (Key.Down):
                    case(Key.S):
                        this.MoveBuilding(this.selectedBuilding, 0, 1);
                        this.lastDirectionKey = Key.Down;
                        e.Handled = true;
                        break;

                    case(Key.Escape):
                        if (null != this.selectedBuilding)
                        {
                            this.selectedBuilding.IsSelected = false;
                        }
                        e.Handled = true;
                        break;

                    default:
                        e.Handled = false;
                        break;
                }
            }
        }

        void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateGridSize();
        }

        void VillageLayoutGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (null != this.draggingBuilding &&
                this.draggingBuilding.IsDragging)
            {
                Point currentMousePosition = e.GetPosition(null);
                Point initialMousePosition = this.draggingBuilding.GetInitialMouseLocationForDrag();

                Vector mouseDelta = Point.Subtract(currentMousePosition, initialMousePosition);

                double gridSquareSize = this.villageLayoutGrid.ColumnDefinitions[0].Width.Value;

                int xOffsetFromOriginal = (int)(mouseDelta.X / gridSquareSize);
                int yOffsetFromOriginal = (int)(mouseDelta.Y / gridSquareSize);

                int currentColumn = Grid.GetColumn(this.draggingBuilding);
                int currentRow = Grid.GetRow(this.draggingBuilding);

                int xOffsetFromCurrent = xOffsetFromOriginal - (currentColumn - this.draggingBuildingInitialColumn);
                int yOffsetFromCurrent = yOffsetFromOriginal - (currentRow - this.draggingBuildingInitialRow);

                this.MoveBuilding(this.draggingBuilding, xOffsetFromCurrent, yOffsetFromCurrent);
            }
        }
    }
}
