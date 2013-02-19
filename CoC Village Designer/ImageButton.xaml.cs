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

    public partial class ImageButton : Button
    {
        Rect imageBrushViewbox;

        public static readonly DependencyProperty ImageBrushProperty = DependencyProperty.Register("ImageBrush", typeof(ImageBrush), typeof(ImageButton));

        public ImageButton()
        {
            InitializeComponent();

            this.DataContext = this;
        }

        public ImageBrush ImageBrush
        {
            get { return (ImageBrush)this.GetValue(ImageButton.ImageBrushProperty); }
            set { this.SetValue(ImageButton.ImageBrushProperty, value); }
        }

        void Button_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (null != this.ImageBrush &&
                default(Rect) != this.imageBrushViewbox)
            {
                if (e.WidthChanged)
                {
                    this.Height = e.NewSize.Width * (this.imageBrushViewbox.Height / this.imageBrushViewbox.Width);
                }
                else
                {
                    if (this.ActualHeight != (this.ActualWidth * (this.imageBrushViewbox.Height / this.imageBrushViewbox.Width)))
                    {
                        this.Height = e.NewSize.Width * (this.imageBrushViewbox.Height / this.imageBrushViewbox.Width);
                    }
                }
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (ImageButton.ImageBrushProperty == e.Property)
            {
                ImageBrush newImageBrush = e.NewValue as ImageBrush;
                if (null != newImageBrush)
                {
                    this.imageBrushViewbox = newImageBrush.Viewbox;
                }
            }

            base.OnPropertyChanged(e);
        }
    }
}
