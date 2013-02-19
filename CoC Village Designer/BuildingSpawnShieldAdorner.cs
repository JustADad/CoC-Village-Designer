namespace JustADadSoftware.VillageDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Documents;
    using System.Windows.Media;
    using System.Windows.Media.Effects;
    using System.Windows;

    public class BuildingSpawnShieldAdorner : Adorner
    {
        Building adornedBuilding;
        SolidColorBrush spawnShieldBrush;

        public BuildingSpawnShieldAdorner(Building adornedBuilding)
            : base(adornedBuilding)
        {
            this.adornedBuilding = adornedBuilding;
            this.InitializeSpawnShieldVisual();
            this.InitializeSpawnShieldVisualEffects();
        }

        void InitializeSpawnShieldVisual()
        {
            this.spawnShieldBrush = new SolidColorBrush(Colors.Purple);
            this.Opacity = 0.2;
            this.IsHitTestVisible = false;
        }

        void InitializeSpawnShieldVisualEffects()
        {
            //DropShadowEffect dropShadowEffect = new DropShadowEffect();
            //dropShadowEffect.BlurRadius = 25.0;
            //dropShadowEffect.ShadowDepth = 2;
            //dropShadowEffect.Opacity = 0.5;

            //this.Effect = dropShadowEffect;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.adornedBuilding.ShowSpawnShieldLayer)
            {
                Rect adornedBuildingRect = new Rect(this.AdornedElement.RenderSize);

                double gridCellSize = (adornedBuildingRect.Width / (double)this.adornedBuilding.GridWidth);

                Rect spawnShieldRect = new Rect(
                    adornedBuildingRect.X - gridCellSize,
                    adornedBuildingRect.Y - gridCellSize,
                    adornedBuildingRect.Width + (2 * gridCellSize),
                    adornedBuildingRect.Height + (2 * gridCellSize));

                Geometry spawnShieldGeometry = Geometry.Combine(
                    new RectangleGeometry(spawnShieldRect),
                    new RectangleGeometry(adornedBuildingRect),
                    GeometryCombineMode.Exclude,
                    null);

                drawingContext.DrawGeometry(
                    this.spawnShieldBrush,
                    null,
                    spawnShieldGeometry);
            }

            base.OnRender(drawingContext);
        }
    }
}
