// Author: David Numa
// "All hail Sam Kass!"

using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

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

namespace RPSLS
{
    class VM : INotifyPropertyChanged
    {
        #region Enums, variables and constants
        enum TBBTLeyend     
        {
            [Description("Scissors cuts Paper (✌ > ✋)")] case0,
            [Description("Paper covers Rock (✋ > ✊)")] case1,
            [Description("Rock crushes Lizard (✊ > 🦎)")] case2,
            [Description("Lizard poisons Spock (🦎 > 🖖)")] case3,
            [Description("Spock smashes Scissors (🖖 > ✌)")] case4,
            [Description("Scissors decapitates Lizard (✌ > 🦎)")] case5,
            [Description("Lizard eats Paper (🦎 > ✋)")] case6,
            [Description("Paper disproves Spock (✋ > 🖖)")] case7,
            [Description("Spock vaproizes Rock (🖖 > ✊)")] case8,
            // and, as it always has,
            [Description("Rock crushes Scissors (✊ > ✌)")] case9,
            [Description("All hail Sam Kass")] case10
        }
        // NOTE TU FUTURE ME: Make it Public so the Converter.cs can access to it
        public enum ColorMessageCodes
        {
            Black,
            IndianRed,
            ForestGreen
        }

        const int INITIAL_MARGIN = 10;
        const int END_MARGIN = 2;
        // crear un converter para pasar si soy rock,tijera, etc. y no usar el cambio de imagen así sino con enums
        // vm no debería saber la ruta de imágenes. Sólo saber qué es para clacular, para eso es el converter
        // 
        private string rockImage = "Images/rock.png";
        private string rockImage2 = "Images/rock.png";
        private string rockImageSelected = "Images/rock_selected.png";
        private string paperImage = "Images/paper.png";
        private string paperImage2 = "Images/paper.png";
        private string paperImageSelected = "Images/paper_selected.png";
        private string scissorsImage = "Images/scissors.png";
        private string scissorsImage2 = "Images/scissors.png";
        private string scissorsImageSelected = "Images/scissors_selected.png";
        private string lizardImage = "Images/lizard.png";
        private string lizardImage2 = "Images/lizard.png";
        private string lizardImageSelected = "Images/lizard_selected.png";
        private string spockImage = "Images/spock.png";
        private string spockImage2 = "Images/spock.png";
        private string spockImageSelected = "Images/spock_selected.png";
        private string winMessage = "You Win!";
        private string loseMessage = "You Lose!";
        private string tieMessage = "It's a Tie!";
        private string noMessage = "Select your Weapon";
        private string leyend = "All hail Sam Kass";

        private string player1 = "";
        private string player2 = "";
        private int winCounter = 0;
        private int loseCounter = 0;
        private int tieCounter = 0;

        private ColorMessageCodes colorCode;

        public Thickness marginRock = new Thickness(INITIAL_MARGIN);
        public Thickness marginPaper = new Thickness(INITIAL_MARGIN);
        public Thickness marginScissors = new Thickness(INITIAL_MARGIN);
        public Thickness marginLizard = new Thickness(INITIAL_MARGIN);
        public Thickness marginSpock = new Thickness(INITIAL_MARGIN);
        #endregion

        #region Methods to call the update function
        public string WeaponImagePlayer2
        {
            get { return rockImage; }
            set
            {
                rockImage = value;
                notifyChange();
                // return the variable to it's original value (it changes to _Selected after 1st click)
                rockImage = rockImage2;
            }
        }
        public string RockImagePlayer1
        {
            get { return rockImage; }
            set
            {
                rockImage = value;
                notifyChange();
                // return the variable to it's original value (it changes to _Selected after 1st click)
                rockImage = rockImage2;
            }
        }
        public string PaperImagePlayer1
        {
            get { return paperImage; }
            set
            {
                paperImage = value;
                notifyChange();
                // return the variable to it's original value (it changes to _Selected after 1st click)
                paperImage = paperImage2;
            }
        }
        public string ScissorsImagePlayer1
        {
            get { return scissorsImage; }
            set
            {
                scissorsImage = value;
                notifyChange();
                // return the variable to it's original value (it changes to _Selected after 1st click)
                scissorsImage = scissorsImage2;
            }
        }
        public string LizardImagePlayer1
        {
            get { return lizardImage; }
            set
            {
                lizardImage = value;
                notifyChange();
                // return the variable to it's original value (it changes to _Selected after 1st click)
                lizardImage = lizardImage2;
            }
        }
        public string SpockImagePlayer1
        {
            get { return spockImage; }
            set
            {
                spockImage = value;
                notifyChange();
                // return the variable to it's original value (it changes to _Selected after 1st click)
                spockImage = spockImage2;
            }
        }
        public string WinLoseMessage
        {
            get { return noMessage; }
            set
            {
                noMessage = value;
                notifyChange();
            }
        }
        public int WinCounter
        {
            get { return winCounter; }
            set
            {
                winCounter = value;
                notifyChange();
            }
        }
        public int LoseCounter
        {
            get { return loseCounter; }
            set
            {
                loseCounter = value;
                notifyChange();
            }
        }
        public int TieCounter
        {
            get { return tieCounter; }
            set
            {
                tieCounter = value;
                notifyChange();
            }
        }
        public string Leyend
        {
            get { return leyend; }
            set
            {
                leyend = value;
                notifyChange();
            }
        }
        public ColorMessageCodes ColorCode
        {
            get { return colorCode; }
            set
            {
                colorCode = value;
                notifyChange();
            }
        }
        #endregion

        #region Methods to change the UI
        // Change the Margin to makes it look like a button has been pressed.
        public Thickness MarginRock
        {
            get { return marginRock; }
            set { marginRock = value; notifyChange(); }
        }
        public Thickness MarginPaper
        {
            get { return marginPaper; }
            set { marginPaper = value; notifyChange(); }
        }
        public Thickness MarginScissors
        {
            get { return marginScissors; }
            set { marginScissors = value; notifyChange(); }
        }
        public Thickness MarginLizard
        {
            get { return marginLizard; }
            set { marginLizard = value; notifyChange(); }
        }
        public Thickness MarginSpock
        {
            get { return marginSpock; }
            set { marginSpock = value; notifyChange(); }
        }

        // Change the image of P2 after the user clicks on P1 image
        public void Player2Weapon(string weaponPlayer2)
        {
            Console.WriteLine("Player 2 weapon: " + weaponPlayer2);
            switch (weaponPlayer2)
            {
                case "0":
                    WeaponImagePlayer2 = rockImage;
                    player2 = "rock";
                    break;
                case "1":
                    WeaponImagePlayer2 = paperImage;
                    player2 = "paper";
                    break;
                case "2":
                    WeaponImagePlayer2 = scissorsImage;
                    player2 = "scissors";
                    break;
                case "3":
                    WeaponImagePlayer2 = lizardImage;
                    player2 = "lizard";
                    break;
                case "4":
                    WeaponImagePlayer2 = spockImage;
                    player2 = "spock";
                    break;
            }
        }
        // Change the image of P1 after the user clicks on it
        public void Player1Weapon(string weaponPlayer1)
        {
            ClearImages();
            switch (weaponPlayer1)
            {
                case "rock":
                    RockImagePlayer1 = rockImageSelected;
                    break;
                case "paper":
                    PaperImagePlayer1 = paperImageSelected;
                    break;
                case "scissors":
                    ScissorsImagePlayer1 = scissorsImageSelected;
                    break;
                case "lizard":
                    LizardImagePlayer1 = lizardImageSelected;
                    break;
                case "spock":
                    SpockImagePlayer1 = spockImageSelected;
                    break;
            }
            player1 = weaponPlayer1;
        }
        // Method to return all player 1 weapons to the unselected image
        public void ClearImages()
        {
            RockImagePlayer1 = rockImage;
            PaperImagePlayer1 = paperImage;
            ScissorsImagePlayer1 = scissorsImage;
            LizardImagePlayer1 = lizardImage;
            SpockImagePlayer1 = spockImage;
        }
        // Method to reduce the margin of the image
        public void GrowImage(string weaponPlayer1)
        {
            switch (weaponPlayer1)
            {
                case "rock":
                    MarginRock = new Thickness(END_MARGIN);
                    break;
                case "paper":
                    MarginPaper = new Thickness(END_MARGIN);
                    break;
                case "scissors":
                    MarginScissors = new Thickness(END_MARGIN);
                    break;
                case "lizard":
                    MarginLizard = new Thickness(END_MARGIN);
                    break;
                case "spock":
                    MarginSpock = new Thickness(END_MARGIN);
                    break;
            }
        }
        // Method to increase the margin of the image
        public void ReduceImage(string weaponPlayer1)
        {
            switch (weaponPlayer1)
            {
                case "rock":
                    MarginRock = new Thickness(INITIAL_MARGIN);
                    break;
                case "paper":
                    MarginPaper = new Thickness(INITIAL_MARGIN);
                    break;
                case "scissors":
                    MarginScissors = new Thickness(INITIAL_MARGIN);
                    break;
                case "lizard":
                    MarginLizard = new Thickness(INITIAL_MARGIN);
                    break;
                case "spock":
                    MarginSpock = new Thickness(INITIAL_MARGIN);
                    break;
            }
        }

        // Method to display the leyend saved in the enum
        public void DisplayLeyend(string caseNumber)
        {
            // FUTURE ME: See this: https://developerpublish.com/how-to-get-the-description-attribute-of-enum-in-c/

            var enumType = typeof(TBBTLeyend);
            var memberData = enumType.GetMember(caseNumber);
            var Description = (memberData[0].GetCustomAttributes(typeof(DescriptionAttribute),
                false).FirstOrDefault() as DescriptionAttribute).Description;
            Leyend = Description;
        }
        #endregion

        #region Methods that calculates the winner
        // Method executed after user clicks on an image
        public void CalculateWinnerClickDown(string weaponPlayer1, string weaponPlayer2)
        {
            ReduceImage(weaponPlayer1);
            int winner = CalcWinner();
            // Display the message: you win, you lose, it's a tie
            switch(winner)
            {
                case 0:
                    WinLoseMessage = tieMessage;
                    ColorCode = ColorMessageCodes.Black;
                    break;
                case 1:
                    WinLoseMessage = winMessage;
                    ColorCode = ColorMessageCodes.ForestGreen;
                    break;
                case 2:
                    WinLoseMessage = loseMessage;
                    ColorCode = ColorMessageCodes.IndianRed;
                    break;
            }
        }

        // Method to validate the winner of the game
        public int CalcWinner()
        {
            // Hacer un enum para evitar estas variables
            // return 0 = tie
            // return 1 = player1 wins
            // return 2 = player2 wins

            // NOTE FOR FUTURE ME: there's gotta be a simpler way... 

            /*    \ P1      Rock    Paper   Sciss.  Lizard  Spock
             * P2  \
             * Rock         TIE     WIN     LOSE    LOSE    WIN    
             * Paper        LOSE    TIE     WIN     WIN     LOSE
             * Scissors     WIN     LOSE    TIE     LOSE    WIN
             * Lizard       WIN     LOSE    WIN     TIE     LOSE
             * Spock        LOSE    WIN     LOSE    WIN     TIE
             */

            if (player1 == player2)
            {
                WinLoseMessage = tieMessage;
                TieCounter++;
                DisplayLeyend(TBBTLeyend.case10.ToString());
                return 0;
            }
            else if (player1 == "rock")
            {
                if (player2 == "paper")
                {
                    LoseCounter++;
                    DisplayLeyend(TBBTLeyend.case1.ToString());
                    return 2;
                }
                else if (player2 == "scissors")
                {
                    WinCounter++;
                    DisplayLeyend(TBBTLeyend.case9.ToString());
                    return 1;
                }
                else if (player2 == "lizard")
                {
                    WinCounter++;
                    DisplayLeyend(TBBTLeyend.case2.ToString());
                    return 1;
                }
                else if (player2 == "spock")
                {
                    LoseCounter++;
                    DisplayLeyend(TBBTLeyend.case8.ToString());
                    return 2;
                }
            }
            else if (player1 == "paper")
            {
                if (player2 == "rock")
                {
                    WinCounter++;
                    DisplayLeyend(TBBTLeyend.case2.ToString());
                    return 1;
                }
                else if (player2 == "scissors")
                {
                    LoseCounter++;
                    DisplayLeyend(TBBTLeyend.case0.ToString());
                    return 2;
                }
                else if (player2 == "lizard")
                {
                    LoseCounter++;
                    DisplayLeyend(TBBTLeyend.case6.ToString());
                    return 2;
                }
                else if (player2 == "spock")
                {
                    WinCounter++;
                    DisplayLeyend(TBBTLeyend.case7.ToString());
                    return 1;
                }
            }
            else if (player1 == "scissors")
            {
                if (player2 == "rock")
                {
                    LoseCounter++;
                    DisplayLeyend(TBBTLeyend.case9.ToString());
                    return 2;
                }
                else if (player2 == "paper")
                {
                    WinCounter++;
                    DisplayLeyend(TBBTLeyend.case0.ToString());
                    return 1;
                }
                else if (player2 == "lizard")
                {
                    WinCounter++;
                    DisplayLeyend(TBBTLeyend.case5.ToString());
                    return 1;
                }
                else if (player2 == "spock")
                {
                    LoseCounter++;
                    DisplayLeyend(TBBTLeyend.case4.ToString());
                    return 2;
                }
            }
            else if (player1 == "lizard")
            {
                if (player2 == "rock")
                {
                    LoseCounter++;
                    DisplayLeyend(TBBTLeyend.case2.ToString());
                    return 2;
                }
                else if (player2 == "paper")
                {
                    WinCounter++;
                    DisplayLeyend(TBBTLeyend.case6.ToString());
                    return 1;
                }
                else if (player2 == "scissors")
                {
                    LoseCounter++;
                    DisplayLeyend(TBBTLeyend.case5.ToString());
                    return 2;
                }
                else if (player2 == "spock")
                {
                    WinCounter++;
                    DisplayLeyend(TBBTLeyend.case3.ToString());
                    return 1;
                }
            }
            else if (player1 == "spock")
            {
                if (player2 == "rock")
                {
                    WinCounter++;
                    DisplayLeyend(TBBTLeyend.case8.ToString());
                    return 1;
                }
                else if (player2 == "paper")
                {
                    LoseCounter++;
                    DisplayLeyend(TBBTLeyend.case7.ToString());
                    return 2;
                }
                else if (player2 == "scissors")
                {
                    WinCounter++;
                    DisplayLeyend(TBBTLeyend.case4.ToString());
                    return 1;
                }
                else if (player2 == "lizard")
                {
                    LoseCounter++;
                    DisplayLeyend(TBBTLeyend.case3.ToString());
                    return 2;
                }
            }
            return 0;
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void notifyChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
