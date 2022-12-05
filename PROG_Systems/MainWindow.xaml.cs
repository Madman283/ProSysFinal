using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG_Systems
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        Enviroment enviroment = new Enviroment();
        Player player = new Player();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            enviroment.entities = Utility.LoadEntities("../../data/data.xml");
            TextNotes.Text = enviroment.GetAllEntityINFO();
        }

        private void ChangeTime_Click(object sender, RoutedEventArgs e)
        {
            enviroment.TimePasses();
            UpdateInfo();
        }
        private void UpdateInfo()
        {
            TextNotes.Text = enviroment.day.ToString() + "\n";
            TextNotes.Text += enviroment.GetAllEntityINFO();
            Updates.Text = enviroment.hawkDecoy.timeLeft.ToString();
        }
        private void HawkD_Click(object sender, RoutedEventArgs e)
        {
            if (enviroment.hawkDecoy.timeLeft <= 0)
            {
                if (player.currency >= enviroment.hawkDecoy.cost)
                {
                    enviroment.hawkDecoy.timeLeft = 3;
                    player.currency -= enviroment.hawkDecoy.cost;
                    Updates.Text = "Hawk Decoy Installed";
                }
                else
                {
                    Updates.Text = "Not enough money for that";
                }
            }
            else
            {
                Updates.Text = "You cannot have more than one deployed at a time";
            }
        }

        private void AddB_Click(object sender, RoutedEventArgs e)
        {
            if (player.currency >= 3)
            {
                enviroment.ChangeEntityAmount("Bat", 3);
                UpdateInfo();
                player.currency -= 3;
            }
            else
            {
                Updates.Text = "Not enough money for that";
            }
            
        }

      

        private void AddHH_Click(object sender, RoutedEventArgs e)
        {
            if (player.currency >= 5)
            {
                enviroment.ChangeEntityAmount("Hawk", -1);
                UpdateInfo();
                player.currency += 1;
            }
            else
            {
                Updates.Text = "Not enough money for that";
            }
        }
    }
}
