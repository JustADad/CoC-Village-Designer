namespace JustADadSoftware.VillageDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    public partial class Building : UserControl
    {
        static Point UninitializeMousePosition = new Point(-100, -100);

        AdornerLayer adornerLayer;
        Point initialDragMousePosition = Building.UninitializeMousePosition;
        BuildingSpawnShieldAdorner spawnShieldAdorner;

        public static readonly RoutedEvent IsSelectedChangedEvent = EventManager.RegisterRoutedEvent("IsSelectedChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Building));
        public static readonly RoutedEvent IsDraggingChangedEvent = EventManager.RegisterRoutedEvent("IsDraggingChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Building));

        public static readonly DependencyProperty BuildingIDProperty = DependencyProperty.Register("BuildingID", typeof(string), typeof(Building));
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(Building));
        public static readonly DependencyProperty GridHeightProperty = DependencyProperty.Register("GridHeight", typeof(int), typeof(Building), new PropertyMetadata(1));
        public static readonly DependencyProperty GridWidthProperty = DependencyProperty.Register("GridWidth", typeof(int), typeof(Building), new PropertyMetadata(1));
        public static readonly DependencyProperty IsDragggingProperty = DependencyProperty.Register("IsDraggging", typeof(bool), typeof(Building), new PropertyMetadata(false));
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(Building), new PropertyMetadata(false));
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(Building));
        public static readonly DependencyProperty ShowSpawnShieldLayerProperty = DependencyProperty.Register("ShowSpawnShieldLayer", typeof(bool), typeof(Building));

        public Building()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public Building(string id, string label, Color color, int gridWidth, int gridHeight)
            : this()
        {
            this.BuildingID = id;
            this.Label = label;
            this.Color = new SolidColorBrush(color);
            this.GridWidth = gridWidth;
            this.GridHeight = gridHeight;
        }

        public event RoutedEventHandler IsDraggingChanged
        {
            add { this.AddHandler(Building.IsDraggingChangedEvent, value); }
            remove { this.RemoveHandler(Building.IsDraggingChangedEvent, value); }
        }

        public event RoutedEventHandler IsSelectedChanged
        {
            add { this.AddHandler(Building.IsSelectedChangedEvent, value); }
            remove { this.RemoveHandler(Building.IsSelectedChangedEvent, value); }
        }

        protected AdornerLayer AdornerLayer
        {
            get
            {
                if (null == this.adornerLayer)
                {
                    this.adornerLayer = AdornerLayer.GetAdornerLayer(this.buildingAdornderDecorator);
                }

                return this.adornerLayer;
            }
        }

        public string BuildingID
        {
            get { return (string)this.GetValue(Building.BuildingIDProperty); }
            set { this.SetValue(Building.BuildingIDProperty, value); }
        }

        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)this.GetValue(Building.ColorProperty); }
            set { this.SetValue(Building.ColorProperty, value); }
        }

        public int GridHeight
        {
            get { return (int)this.GetValue(Building.GridHeightProperty); }
            set { this.SetValue(Building.GridHeightProperty, value); }
        }

        public int GridWidth
        {
            get { return (int)this.GetValue(Building.GridWidthProperty); }
            set { this.SetValue(Building.GridWidthProperty, value); }
        }

        public bool IsDragging
        {
            get { return (bool)this.GetValue(Building.IsDragggingProperty); }
            set { this.SetValue(Building.IsDragggingProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)this.GetValue(Building.IsSelectedProperty); }
            set { this.SetValue(Building.IsSelectedProperty, value); }
        }

        public string Label
        {
            get { return (string)this.GetValue(Building.LabelProperty); }
            set { this.SetValue(Building.LabelProperty, value); }
        }

        public bool ShowSpawnShieldLayer
        {
            get { return (bool)this.GetValue(Building.ShowSpawnShieldLayerProperty); }
            set { this.SetValue(Building.ShowSpawnShieldLayerProperty, value); }
        }

        void Building_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
            {
                this.IsSelected = !this.IsSelected;
            }
        }

        void Building_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtonState.Pressed == e.LeftButton)
            {
                if (!this.IsDragging)
                {
                    if (Building.UninitializeMousePosition == this.initialDragMousePosition)
                    {
                        this.initialDragMousePosition = e.GetPosition(null);
                    }
                    else
                    {
                        Point currentMousePosition = e.GetPosition(null);
                        if (SystemParameters.MinimumHorizontalDragDistance < Math.Abs(currentMousePosition.X - this.initialDragMousePosition.X) ||
                            SystemParameters.MinimumVerticalDragDistance < Math.Abs(currentMousePosition.Y - this.initialDragMousePosition.Y))
                        {
                            this.IsDragging = true;
                        }
                    }
                }
            }
        }

        void Building_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.IsDragging = false;
            this.initialDragMousePosition = Building.UninitializeMousePosition;
        }

        public Point GetInitialMouseLocationForDrag()
        {
            return this.initialDragMousePosition;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (Building.IsSelectedProperty == e.Property)
            {
                if ((bool)e.NewValue)
                {
                    this.buildingBorder.BorderBrush = new SolidColorBrush(Colors.Red);
                    this.buildingBorder.BorderThickness = new Thickness(2.5);
                }
                else
                {
                    this.buildingBorder.BorderBrush = new SolidColorBrush(Colors.Black);
                    this.buildingBorder.BorderThickness = new Thickness(1.0);
                }

                this.RaiseEvent(new RoutedEventArgs(Building.IsSelectedChangedEvent, this));
            }
            else if (Building.ShowSpawnShieldLayerProperty == e.Property)
            {
                this.InvalidateVisual();
            }
            else if (Building.IsDragggingProperty == e.Property)
            {
                this.RaiseEvent(new RoutedEventArgs(Building.IsDraggingChangedEvent, this));
            }
            else if (Grid.RowProperty == e.Property ||
                Grid.ColumnProperty == e.Property)
            {
                int left = Grid.GetColumn(this);
                int top = Grid.GetRow(this);

                if (Grid.RowProperty == e.Property)
                {
                    top = (int)e.NewValue;
                }
                else
                {
                    left = (int)e.NewValue;
                }

                int right = left + this.GridWidth;
                int bottom = top + this.GridHeight;
            }

            base.OnPropertyChanged(e);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.ShowSpawnShieldLayer)
            {
                this.VerifySpawnShieldAdornerInitialized();
            }
            else
            {
                this.VerifySpawnShieldAdornerCleared();
            }

            base.OnRender(drawingContext);
        }

        void VerifySpawnShieldAdornerCleared()
        {
            if (null != this.AdornerLayer &&
                null != this.spawnShieldAdorner)
            {
                this.AdornerLayer.Remove(this.spawnShieldAdorner);
                this.spawnShieldAdorner = null;
            }
        }

        void VerifySpawnShieldAdornerInitialized()
        {
            if (null != this.AdornerLayer &&
                null == this.spawnShieldAdorner)
            {
                this.spawnShieldAdorner = new BuildingSpawnShieldAdorner(this);

                this.AdornerLayer.Add(this.spawnShieldAdorner);
            }
        }
    }
}
