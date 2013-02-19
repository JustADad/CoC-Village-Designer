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
    using System.Windows.Shapes;

    public partial class AnimatedSplashScreen : Window
    {
        MainWindow mainWindow;

        public AnimatedSplashScreen()
        {
            InitializeComponent();
        }

        void Image_AnimationCompleted(object sender, RoutedEventArgs e)
        {
            if (null == this.mainWindow)
            {
                this.mainWindow = new MainWindow();
                this.mainWindow.Show();

                this.animatedBanner.Play();
            }
            else
            {
                this.Close();
            }
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2; 
            this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2;
        }
    }
}
