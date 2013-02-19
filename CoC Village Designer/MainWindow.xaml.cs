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
    using JustADadSoftware.VillageDesigner.Data;
    using JustADadSoftware.VillageDesigner.Buildings;
    using System.Windows.Threading;

    public partial class MainWindow : Window
    {
        string currentFileName;
        StoreData storeData;

        public MainWindow()
        {
            InitializeComponent();
        }

        void AirAttackRangeLayerMenuItem_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (null != this.village)
            {
                if (this.airAttackRangeLayerMenuItem.IsChecked)
                {
                    this.village.ShowAttackRangeLayer |= AttackTargets.Air;
                }
                else
                {
                    this.village.ShowAttackRangeLayer ^= AttackTargets.Air;
                }
            }
        }

        void CommandBindingHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Owner = this;
            helpWindow.ShowDialog();
        }

        void CommandBindingNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.village.CreateNewVillage();
        }

        void CommandBindingOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DefaultExt = ".CoC";
            openFileDialog.Filter = "Clash of Clans Village Layout|*.CoC";
            openFileDialog.Multiselect = false;

            bool? dialogResult = openFileDialog.ShowDialog(this);
            if (dialogResult.GetValueOrDefault(false))
            {
                this.currentFileName = openFileDialog.FileName;
                this.village.OpenVillage(this.currentFileName);
            }
        }

        void CommandBindingSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.currentFileName))
            {
                this.SaveVillageAs();
            }
            else
            {
                this.village.SaveVillage(this.currentFileName);
            }
        }

        void CommandBindingSaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.SaveVillageAs();
        }

        void FileExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        void GroundAttackRangeLayerMenuItem_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (null != this.village)
            {
                if (this.groundAttackRangeLayerMenuItem.IsChecked)
                {
                    this.village.ShowAttackRangeLayer |= AttackTargets.Ground;
                }
                else
                {
                    this.village.ShowAttackRangeLayer ^= AttackTargets.Ground;
                }
            }
        }

        void HelpAboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            HelpAboutDialog helpAboutDialog = new HelpAboutDialog();
            helpAboutDialog.Owner = this;
            helpAboutDialog.ShowDialog();
        }

        void HelpFeedbackMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/JustADad/CoC-Village-Designer/issues");
        }

        void SaveVillageAs()
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.DefaultExt = ".CoC";
            saveFileDialog.FileName = this.currentFileName;
            saveFileDialog.Filter = "Clash of Clans Village Layout|*.CoC";

            bool? dialogResult = saveFileDialog.ShowDialog(this);
            if (dialogResult.GetValueOrDefault(false))
            {
                this.currentFileName = saveFileDialog.FileName;
                this.village.SaveVillage(this.currentFileName);
            }
        }

        void StoreItemButton_Click(object sender, RoutedEventArgs e)
        {
            ToolbarGroupItemButton storeItemButton = sender as ToolbarGroupItemButton;
            if (null != storeItemButton)
            {
                string buildingID = storeItemButton.Tag as string;
                if (!String.IsNullOrEmpty(buildingID))
                {
                    this.village.AddBuilding(buildingID);
                }
            }
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.storeData = StoreData.Initialize("JustADadSoftware.VillageDesigner.Data.CoCStoreData.xml");

            if (null != this.storeData &&
                null!=this.storeData.Sections)
            {
                string screenShotUriBase = String.Format(
                    "pack://application:,,,/{0};component/Images/{{0}}",
                    typeof(MainWindow).Assembly.FullName);

                foreach (StoreSection storeSection in this.storeData.Sections)
                {
                    BitmapImage storeSectionBitmapImage = new BitmapImage(new Uri(String.Format(
                        screenShotUriBase,
                        this.storeData.Screenshots[storeSection.StoreScreenIndex])));

                    ImageBrush storeSectionImageBrush = new ImageBrush(storeSectionBitmapImage);
                    storeSectionImageBrush.Stretch = Stretch.UniformToFill;
                    storeSectionImageBrush.Viewbox = storeSection.ScreenshotViewbox;
                    storeSectionImageBrush.ViewboxUnits = BrushMappingMode.Absolute;

                    ToolbarGroupButton storeSectionButton = new ToolbarGroupButton();
                    storeSectionButton.ImageBrush = storeSectionImageBrush;

                    if (null != storeSection.Items)
                    {
                        foreach (StoreItem storeItem in storeSection.Items)
                        {
                            BitmapImage storeItemBitmapImage = new BitmapImage(new Uri(String.Format(
                                screenShotUriBase,
                                storeSection.Screenshots[storeItem.SectionScreenIndex])));

                            ImageBrush storeItemImageBrush = new ImageBrush(storeItemBitmapImage);
                            storeItemImageBrush.Stretch = Stretch.UniformToFill;
                            storeItemImageBrush.Viewbox = storeItem.ScreenshotViewbox;
                            storeItemImageBrush.ViewboxUnits = BrushMappingMode.Absolute;

                            ToolbarGroupItemButton itemButton = new ToolbarGroupItemButton();
                            itemButton.ItemID = storeItem.ItemName;
                            itemButton.ImageBrush = storeItemImageBrush;
                            itemButton.Tag = storeItem.BuildingID;

                            itemButton.Click += new RoutedEventHandler(this.StoreItemButton_Click);

                            storeSectionButton.Items.Add(itemButton);
                        }
                    }

                    toolBoxPanel.Children.Add(storeSectionButton);
                }
            }
        }
    }
}
