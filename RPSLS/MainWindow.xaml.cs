// Author: David Numa 

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RPSLS
{
    /*
         RULES OF THE GAME: 
         
        (0.) Scissors cuts Paper (✌ > ✋)
        (1.) Paper covers Rock (✋ > ✊)
        (2.) Rock crushes Lizard (✊ > 🦎)
        (3.) Lizard poisons Spock (🦎 > 🖖)
        (4.) Spock smashes Scissors (🖖 > ✌)
        (5.) Scissors decapitates Lizard (✌ > 🦎)
        (6.) Lizard eats Paper (🦎 > ✋)
        (7.) Paper disproves Spock (✋ > 🖖)
        (8.) Spock vaproizes Rock (🖖 > ✊)
        (9.) Rock crushes Scissors (✊ > ✌)

    */
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void CalculateWinner_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Generate a new weapon for Player2
            // DONT'S PASS THE SENDER, pass the srring, El sender es de UI, sólo eso.
            int WEAPONPLAYER2 = 5;
            Random r = new Random();
            string weaponPlayer2 = r.Next(WEAPONPLAYER2).ToString();
            vm.Player2Weapon(weaponPlayer2);

            // Change the image of Player1's selected weapon
            string weaponPlayer1 = SelectedWeaponPlayer1(sender);
            vm.Player1Weapon(weaponPlayer1);

            // Who is the winner?
            vm.CalculateWinnerClickDown(weaponPlayer1, weaponPlayer2);
        }
        private void CalculateWinner_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Change margins to make it look like a click
            string identifier = SelectedWeaponPlayer1(sender);
            vm.GrowImage(identifier);
        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            // Change margins on mouse over
            string identifier = SelectedWeaponPlayer1(sender);
            vm.GrowImage(identifier);
        }
        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            // Change margins on mouse over
            string identifier = SelectedWeaponPlayer1(sender);
            vm.ReduceImage(identifier);
        }
        
        // Which weapon was selected?
        private string SelectedWeaponPlayer1(object sender)
        {
            Image weapon = sender as Image;
            if (weapon.Equals(rockPlayer))
                return "rock";
            else if (weapon.Equals(paperPlayer))
                return "paper";
            else if (weapon.Equals(scissorsPlayer))
                return "scissors";
            else if (weapon.Equals(lizardPlayer))
                return "lizard";
            else
                return "spock";
        }
    }
}
