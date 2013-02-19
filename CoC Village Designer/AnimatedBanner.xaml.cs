namespace JustADadSoftware.VillageDesigner
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public partial class AnimatedBanner : UserControl
    {
        public static readonly RoutedEvent AnimationCompletedEvent = EventManager.RegisterRoutedEvent("AnimationCompleted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AnimatedBanner));
        public static readonly DependencyProperty RepeatBehaviorProperty = DependencyProperty.Register("RepeatBehavior", typeof(RepeatBehavior), typeof(AnimatedBanner));

        public AnimatedBanner()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public event RoutedEventHandler AnimationCompleted
        {
            add { this.AddHandler(AnimatedBanner.AnimationCompletedEvent, value); }
            remove { this.RemoveHandler(AnimatedBanner.AnimationCompletedEvent, value); }
        }

        public RepeatBehavior RepeatBehavior
        {
            get { return (RepeatBehavior)this.GetValue(AnimatedBanner.RepeatBehaviorProperty); }
            set { this.SetValue(AnimatedBanner.RepeatBehaviorProperty, value); }
        }

        void Image_AnimationCompleted(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs routedEventArgs = new RoutedEventArgs(
                AnimatedBanner.AnimationCompletedEvent,
                this);

            this.RaiseEvent(routedEventArgs);
        }

        public void Play()
        {
            WpfAnimatedGif.ImageAnimationController imageAnimationController = WpfAnimatedGif.ImageBehavior.GetAnimationController(this.animatedImage);
            if (null != imageAnimationController)
            {
                imageAnimationController.Play();
            }
        }
    }
}
