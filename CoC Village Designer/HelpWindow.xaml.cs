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

    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();

            this.instructionsTextBox.Text += "Here are some simple instructions for using the designer.\n";
            this.instructionsTextBox.Text += "\n";
            this.instructionsTextBox.Text += "Use the context menu (right-click) on the Town Hall to set the desired Town Hall level.  The designer will cap you to the maximum number of buildings for that level.\n";
            this.instructionsTextBox.Text += "\n";
            this.instructionsTextBox.Text += "WARNING: If you add buildings and then reduce your Town Hall level it is possible to have more buildings than allowed for that Town Hall level.  Simply delete the extra buildings by selecting the buildign (left-click) and press DEL.\n";
            this.instructionsTextBox.Text += "\n";
            this.instructionsTextBox.Text += "Use the familiar looking shop buttons on the far left to add buildings to your village.  You can also use the details pane on the left to keep track of how many of each building type you are allowed and how many you have already placed.\n";
            this.instructionsTextBox.Text += "\n";
            this.instructionsTextBox.Text += "[Yes, I realize some of the buttons appear to be disabled.  This was a quick side project where I used screen shots from my iPad to capture button images.  In some cases I have already placed my maximum number of buildings.  In other cases I have not gotten to a level to unlock said buildings.  Feel free to post screenshots on the official forum thread (see below) to help me out.]\n";
            this.instructionsTextBox.Text += "\n";
            this.instructionsTextBox.Text += "You can select a building and use the '+' key to add another building of the same type, if you have not placed the maximum number of that type of building already.\n";
            this.instructionsTextBox.Text += "\n";
            this.instructionsTextBox.Text += "You can move buildings with either drag and drop or by selecting them (left-click) and using the arrow keys or the gamer \"wasd\" keys.\n";
            this.instructionsTextBox.Text += "\n";
            this.instructionsTextBox.Text += "I hope you enjoy this little side project and put it to good use.  If you have any feedback (suggestions, bug reports, complaints) use the Help->Provide Feedback menu item to open the official thread on the Alatreon Clan Forums.  Thank you.\n";
        }

        void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
