namespace JustADadSoftware.VillageDesigner.Buildings
{
    using System;
    using System.Windows.Media;
    using System.Windows.Data;
    using System.Windows;

    public abstract class DefensiveBuilding : Building
    {
        BuildingAttackRangeAdorner attackRangeAdorner;

        public static readonly DependencyProperty TargetsProperty = DependencyProperty.Register("Targets", typeof(AttackTargets), typeof(DefensiveBuilding));
        public static readonly DependencyProperty MaxAttackRangeProperty = DependencyProperty.Register("MaxAttackRange", typeof(int), typeof(DefensiveBuilding));
        public static readonly DependencyProperty MinAttackRangeProperty = DependencyProperty.Register("MinAttackRange", typeof(int), typeof(DefensiveBuilding));
        public static readonly DependencyProperty ShowAttackRangeLayerProperty = DependencyProperty.Register("ShowAttackRangeLayer", typeof(AttackTargets), typeof(DefensiveBuilding), new PropertyMetadata(AttackTargets.AirAndGround));

        public DefensiveBuilding(string id, string label, int gridWidth, int gridHeight)
            : this(id, label, gridWidth, gridHeight, -1, -1, AttackTargets.None)
        {
        }

        public DefensiveBuilding(string id, string label, int gridWidth, int gridHeight, int attackRange, AttackTargets targets)
            : this(id, label, gridWidth, gridHeight, attackRange, 0, targets)
        {
        }

        public DefensiveBuilding(string id, string label, int gridWidth, int gridHeight, int maxAttackRange, int minAttackRange, AttackTargets targets)
            : base(id, label, Colors.PaleGoldenrod, gridWidth, gridHeight)
        {
            this.MaxAttackRange = maxAttackRange;
            this.MinAttackRange = minAttackRange;
            this.Targets = targets;
        }

        public AttackTargets Targets
        {
            get { return (AttackTargets)this.GetValue(DefensiveBuilding.TargetsProperty); }
            set { this.SetValue(DefensiveBuilding.TargetsProperty, value); }
        }

        public int MaxAttackRange
        {
            get { return (int)this.GetValue(DefensiveBuilding.MaxAttackRangeProperty); }
            set { this.SetValue(DefensiveBuilding.MaxAttackRangeProperty, value); }
        }

        public int MinAttackRange
        {
            get { return (int)this.GetValue(DefensiveBuilding.MinAttackRangeProperty); }
            set { this.SetValue(DefensiveBuilding.MinAttackRangeProperty, value); }
        }

        public AttackTargets ShowAttackRangeLayer
        {
            get { return (AttackTargets)this.GetValue(DefensiveBuilding.ShowAttackRangeLayerProperty); }
            set { this.SetValue(DefensiveBuilding.ShowAttackRangeLayerProperty, value); }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (DefensiveBuilding.ShowAttackRangeLayerProperty == e.Property)
            {
                this.InvalidateVisual();
            } 

            base.OnPropertyChanged(e);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (AttackTargets.None == this.ShowAttackRangeLayer)
            {
                this.VerifyAttackRangeAdornerCleared();
            }
            else
            {
                this.VerifyAttackRangeAdornerInitialized();
            }

            base.OnRender(drawingContext);
        }

        void VerifyAttackRangeAdornerCleared()
        {
            if (null != this.AdornerLayer &&
                null != this.attackRangeAdorner)
            {
                this.AdornerLayer.Remove(this.attackRangeAdorner);
                this.attackRangeAdorner = null;
            }
        }

        void VerifyAttackRangeAdornerInitialized()
        {
            if (null != this.AdornerLayer &&
                null == this.attackRangeAdorner &&
                0 < this.MaxAttackRange)
            {
                this.attackRangeAdorner = new BuildingAttackRangeAdorner(this);

                Binding maxAttackRangeBinding = new Binding("MaxAttackRange");
                maxAttackRangeBinding.Source = this;

                this.attackRangeAdorner.SetBinding(BuildingAttackRangeAdorner.MaxRangeProperty, maxAttackRangeBinding);

                Binding minAttackRangeBinding = new Binding("MinAttackRange");
                minAttackRangeBinding.Source = this;

                this.attackRangeAdorner.SetBinding(BuildingAttackRangeAdorner.MinRangeProperty, minAttackRangeBinding);

                Binding showAttackRangeLayerBinding = new Binding("ShowAttackRangeLayer");
                showAttackRangeLayerBinding.Source = this;

                this.attackRangeAdorner.SetBinding(BuildingAttackRangeAdorner.ShowAttackRangeLayerProperty, showAttackRangeLayerBinding);

                this.AdornerLayer.Add(this.attackRangeAdorner);
            }
        }
    }
}
