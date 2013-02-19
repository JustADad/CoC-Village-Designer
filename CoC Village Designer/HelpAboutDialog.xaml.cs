namespace JustADadSoftware.VillageDesigner
{
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Media.Imaging;
    using System.Windows.Controls;
    using System.Windows.Media;

    public partial class HelpAboutDialog : Window
    {
        static string AssemblyTitle;
        static string CompanyName;
        static string Copyright;
        static string Description;
        static string ProductName;
        static string VersionString;

        static HelpAboutDialog()
        {
            Assembly productAssembly = Assembly.GetAssembly(typeof(HelpAboutDialog));

            object[] attributes = productAssembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (0 < attributes.Length)
            {
                HelpAboutDialog.AssemblyTitle = ((AssemblyTitleAttribute)attributes[0]).Title;
            }

            attributes = productAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (0 < attributes.Length)
            {
                HelpAboutDialog.CompanyName = ((AssemblyCompanyAttribute)attributes[0]).Company;
            }

            attributes = productAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (0 < attributes.Length)
            {
                HelpAboutDialog.Copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }

            attributes = productAssembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (0 < attributes.Length)
            {
                HelpAboutDialog.Description = ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }

            attributes = productAssembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (0 < attributes.Length)
            {
                HelpAboutDialog.ProductName = ((AssemblyProductAttribute)attributes[0]).Product;
            }

            HelpAboutDialog.VersionString = productAssembly.GetName().Version.ToString();
        }

        public HelpAboutDialog()
        {
            InitializeComponent();

            this.Title = String.Format("About {0}...", HelpAboutDialog.AssemblyTitle);
            
            this.companyNameTextBlock.Text = HelpAboutDialog.CompanyName;
            this.copyrightTextBlock.Text = HelpAboutDialog.Copyright;
            this.descriptionTextBox.Text = String.Format("Description: {0}", HelpAboutDialog.Description);
            this.productNameTextBlock.Text = HelpAboutDialog.ProductName;
            this.versionTextBlock.Text = HelpAboutDialog.VersionString;
        }

        void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
