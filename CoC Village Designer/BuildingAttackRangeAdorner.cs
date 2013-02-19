namespace JustADadSoftware.VillageDesigner
{
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;
    using JustADadSoftware.VillageDesigner.Buildings;

    public class BuildingAttackRangeAdorner : Adorner
    {
        DefensiveBuilding adornedBuilding;
        SolidColorBrush maxRangeBrush;
        Pen maxRangePen;

        public static readonly DependencyProperty MaxRangeProperty = DependencyProperty.Register(
            "MaxRange",
            typeof(int),
            typeof(BuildingAttackRangeAdorner),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty MinRangeProperty = DependencyProperty.Register(
            "MinRange",
            typeof(int),
            typeof(BuildingAttackRangeAdorner),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ShowAttackRangeLayerProperty = DependencyProperty.Register(
            "ShowAttackRangeLayer", 
            typeof(AttackTargets), 
            typeof(BuildingAttackRangeAdorner),
            new FrameworkPropertyMetadata(AttackTargets.None, FrameworkPropertyMetadataOptions.AffectsRender));


        public BuildingAttackRangeAdorner(DefensiveBuilding adornedBuilding)
            : base(adornedBuilding)
        {
            this.adornedBuilding = adornedBuilding;

            this.InitializeAttackRangeVisual();
            this.InitializeAttackRangeVisualEffects();
        }

        public int MaxRange
        {
            get { return (int)this.GetValue(BuildingAttackRangeAdorner.MaxRangeProperty); }
            set { this.SetValue(BuildingAttackRangeAdorner.MaxRangeProperty, value); }
        }

        public int MinRange
        {
            get { return (int)this.GetValue(BuildingAttackRangeAdorner.MinRangeProperty); }
            set { this.SetValue(BuildingAttackRangeAdorner.MinRangeProperty, value); }
        }

        public AttackTargets ShowAttackRangeLayer
        {
            get { return (AttackTargets)this.GetValue(BuildingAttackRangeAdorner.ShowAttackRangeLayerProperty); }
            set { this.SetValue(BuildingAttackRangeAdorner.ShowAttackRangeLayerProperty, value); }
        }

        void InitializeAttackRangeVisual()
        {
            this.maxRangeBrush = new SolidColorBrush(Colors.Red);
            this.maxRangeBrush.Opacity = 0.1;

            this.maxRangePen = new Pen(
                new SolidColorBrush(Colors.White),
                1.0);

            this.IsHitTestVisible = false;
        }

        void InitializeAttackRangeVisualEffects()
        {
            //DropShadowEffect dropShadowEffect = new DropShadowEffect();
            //dropShadowEffect.BlurRadius = 25.0;
            //dropShadowEffect.ShadowDepth = 2;
            //dropShadowEffect.Opacity = 0.5;

            //this.Effect = dropShadowEffect;
        }

        protected override void ParentLayoutInvalidated(UIElement child)
        {
            base.ParentLayoutInvalidated(child);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            bool showAttackRangeLayer = false;

            switch (this.ShowAttackRangeLayer)
            {
                case(AttackTargets.Ground):
                case(AttackTargets.Air):
                    showAttackRangeLayer = this.adornedBuilding.Targets.HasFlag(this.ShowAttackRangeLayer);
                    break;

                case(AttackTargets.AirAndGround):
                    showAttackRangeLayer = (AttackTargets.None != this.adornedBuilding.Targets);
                    break;
            }

            if (showAttackRangeLayer)
            {
                Rect adornedBuildingRect = new Rect(this.AdornedElement.RenderSize);

                int buildingGridWidth = this.adornedBuilding.GridWidth;
                int buildingGridHeight = this.adornedBuilding.GridHeight;

                double gridCellSize = (adornedBuildingRect.Width / (double)buildingGridWidth);

                Point adornedBuildingCenter = Point.Subtract(
                    adornedBuildingRect.BottomRight,
                    new Vector(adornedBuildingRect.Width / 2.0, adornedBuildingRect.Height / 2.0));

                double maxRangeRadiusX = gridCellSize * (this.MaxRange);
                double maxRangeRadiusY = gridCellSize * (this.MaxRange);

                Geometry attackRangeGeometry = new EllipseGeometry(
                    adornedBuildingCenter,
                    maxRangeRadiusX,
                    maxRangeRadiusY);

                if (0 < this.MinRange)
                {

                    double minRangeRadiusX = gridCellSize * (this.MinRange);
                    double minRangeRadiusY = gridCellSize * (this.MinRange);

                    EllipseGeometry minRangeEllipse = new EllipseGeometry(
                        adornedBuildingCenter,
                        minRangeRadiusX,
                        minRangeRadiusY);

                    attackRangeGeometry = Geometry.Combine(
                        attackRangeGeometry,
                        minRangeEllipse,
                        GeometryCombineMode.Exclude,
                        null);
                }

                drawingContext.DrawGeometry(
                    this.maxRangeBrush,
                    this.maxRangePen,
                    attackRangeGeometry);
            }

            base.OnRender(drawingContext);
        }
    }
}
