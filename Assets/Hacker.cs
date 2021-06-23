using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Transactions;
using UnityEditor;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Game configuration data
    string[] level1Password = { "elsa", "anna", "olaf", "kristoff", "sven", "hans", "marshmallow" };
    string[] level2Password = { "jareth", "sarah", "hoggle", "sir didymus", "ambrosius", "toby" };
    string[] level3Password = { "bob", "doug", "elon", "dragon", "space", "astronaut"};
    const string menuHint = ("(Type menu to return)");  // a constant is more efficient if you're not going to change the variable
    public GameObject audioObject;


    enum Screen { MainMenu, WaitingForPassword, WinScreen, AdminLogin };
    Screen currentScreen;
    int level;
    string password;
     
    // Start is called before the first frame update
    void Start()
    {
        showMainMenu();
    }

    

    void showMainMenu()
    {
        currentScreen = Screen.MainMenu;

        PrepareScreenForText();
        Terminal.WriteLine("0: Login as another user");
        Terminal.WriteLine("1: Frozen 1 movie quiz");
        Terminal.WriteLine("2: Labyrinth movie quiz");
        Terminal.WriteLine("3: More coming soon!");
        Terminal.WriteLine("");
    }

    void OnUserInput(string input)  // WHY?  whenever user inputs something, check these functions (based on the currentScreen) to see what they're trying to do
    {
        if (currentScreen == Screen.WaitingForPassword)  // I find this function quite confusing.  I think it is not launching another function, but it then watches that function for user input
        {
            CheckPassword(input);
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.AdminLogin)
        {
            RunAdminLogin(input);
        }
        if (input == "menu" || input == "Menu")  // WHY?  to allow user to always return to main menu
        {
            showMainMenu();
            password = "";  // reset password
        }
        if (input == "quit" || input == "exit")
        {
            Application.Quit();
            Terminal.WriteLine("Please close the browser tab to exit.");
        }                
    }

    void RunMainMenu(string input)  // WHY?  So we can check if user trying to access a menu option
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");  // here we are setting which level numbers are valid
        if (isValidLevelNumber)  // if this is true
        {
            level = int.Parse(input);  // parse converts the level number which is a string, into an integer
            AskForPassword();            
        }
        else if (input == "0")
        {
            currentScreen = Screen.AdminLogin;
            RunAdminLogin(input);  // this is what actually launches this function
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level.");            
        }
    }

    void PrepareScreenForText()  // created this function instead of typing this a million times
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("  (C) IMEL SYSTEMS 1976                   ** AUTHORISED USERS ONLY **             (Type menu to return to the menu at any time)");
        Terminal.WriteLine("");
        Terminal.WriteLine("");
    }

    void RunAdminLogin(string input)  
    {
        PrepareScreenForText();
        Terminal.WriteLine("Enter login name: ");
        
        if (input == "imogen" || input == "Imogen" || input == "Immy" || input == "immy")
        {
            PrepareScreenForText();
            Terminal.WriteLine("Hi Immy x");
        }
        else if (input == "Eliza" || input == "eliza")
        {
            PrepareScreenForText();
            Terminal.WriteLine("Hi Lizy x");
        }
        else if (input == "Carly" || input == "carly")
        {
            PrepareScreenForText();
            Terminal.WriteLine("Hello Little Love x");
        }
        else if (input == "poo" || input == "bum" || input == "fart" || input == "wee")
        {
            PrepareScreenForText();
            Terminal.WriteLine("Oh dear, that's a bit rude!");
        }
        else if (input == "Owen" || input == "owen")
        {
            PrepareScreenForText();
            Terminal.WriteLine("YOU ARE NOT OWEN!");
        }
        else if (input == "Daddy" || input == "daddy")
        {
            PrepareScreenForText();
            Terminal.WriteLine("YOU ARE NOT DADDY!");
        }        
    }

    void AskForPassword()
    {
        PrepareScreenForText();
        Terminal.WriteLine("Circumventing firewalls...");
        Terminal.WriteLine("...");
        Terminal.WriteLine("Accesing Mainframe");
        Terminal.WriteLine("...");
        Terminal.WriteLine("...");
        currentScreen = Screen.WaitingForPassword;
        SetRandomPassword();
        Terminal.WriteLine("Enter your password. Hint: " + password.Anagram());
    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                password = level1Password[Random.Range(0, level1Password.Length)];
                break;
            case 2:
                password = level2Password[Random.Range(0, level2Password.Length)];
                break;
            case 3:
                password = level3Password[Random.Range(0, level3Password.Length)];
                break;
            default:
                Debug.LogError("Invalid level number selected.");  // this shouldn't be possible but is good practice
                break;

        }        
    }

    void CheckPassword(string input)  // WHY?  So we can see if password user entered was correct or not
    {
        if (input == password)
        {
            PrepareScreenForText();
            Terminal.WriteLine("Correct password!");
            DisplayWinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void DisplayWinScreen()
    {
        currentScreen = Screen.WinScreen;
        PrepareScreenForText();
        ShowLevelReward();       
    }


    void ShowLevelReward()
    {
        switch (level)
        {
            case 1:
                PrepareScreenForText();
                Terminal.WriteLine("Welcome Fox Mulder.");                
                break;
            case 2:
                PrepareScreenForText();
                Terminal.WriteLine("It's further than you think.");
                GameObject audioWin = Instantiate(audioObject, transform.position, transform.rotation);
                break;
            case 3:
                PrepareScreenForText();
                Terminal.WriteLine("Welcome home Bob and Doug!");
                break;


        }
    }
}