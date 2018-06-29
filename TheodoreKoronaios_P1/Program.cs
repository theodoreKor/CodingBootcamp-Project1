using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheodoreKoronaios_P1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.TreatControlCAsInput = true; // Disable Ctrl+C from exiting console

            MenuManager menus = new MenuManager(); // instantiate object with all the menus
            DataBaseManager dBManager = new DataBaseManager(); // instantiate object to access the database
            InputManager inputManager = new InputManager(); // instantiate object to handle user input from console

            MessageManager messageManager = new MessageManager(); // instantiate object to handle user messagess

            do
            {
                MainMenuOptions mainMenuChoice = menus.MainMenu(); // run the Main Menu. Returns Enum when Enter is pressed
                // Login, Signup, Info, Exit

                switch (mainMenuChoice)
                {
                    //==========================================================================================================//
                    //==========================================================================================================//
                    case MainMenuOptions.Login:
                        bool userExists = false;
                        //
                        bool userActive;
                        //

                        string usernameLogin;
                        menus.LoginMenu(); // Includes console clear and welcome message
                        usernameLogin = inputManager.InputUserName(); // Returns a string or null if ESC is pressed

                        if (usernameLogin != null) // if username is received
                        {
                            userExists = dBManager.DoesUsernameExist(usernameLogin); // checks if username exists in database
                            if (!userExists)
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("The username you entered does not exist in the database. Please SignUp.");
                                Console.WriteLine("Press any key to go back to the Main Menu.");
                                Console.ResetColor();
                                Console.ReadKey();
                                break;
                            }
                            else // username exists in database, continue to ask for password
                            {
                                userActive = dBManager.IsUserActive(usernameLogin); // check if user is active

                                if (!userActive) // user not active
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"The user with username '{usernameLogin}' is no longer active.");
                                    Console.WriteLine("Press any key to go back");
                                    Console.ReadKey();
                                    Console.ResetColor();
                                    break;
                                }

                                Console.WriteLine($"\nWelcome {usernameLogin}!");
                                Console.WriteLine("\nPlease enter your password.");
                                int numberOfAttemps = 0; // Holds the number of attemps for password input
                                int maxNumberOfAttemps = 3;
                                string passwordLogin;
                                bool isPasswordCorrect = false;
                                while (!isPasswordCorrect && numberOfAttemps < maxNumberOfAttemps)
                                {
                                    passwordLogin = inputManager.InputLoginPassword(); // only checks against zero length
                                    numberOfAttemps += 1;
                                    isPasswordCorrect = dBManager.IsPasswordCorrect(usernameLogin, passwordLogin);
                                    if (!isPasswordCorrect)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Wrong password. Try again.");
                                        Console.ResetColor();
                                    }
                                }
                                if (!isPasswordCorrect && (numberOfAttemps == maxNumberOfAttemps))
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine("Maximum number of attemps reached!");
                                    Console.ResetColor();
                                    Console.WriteLine("\nPress any key to go back.");
                                    Console.ReadKey();
                                    break; // exits the switch, back to main loop
                                }
                                if (isPasswordCorrect)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Correct Password.");
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("\nPress ESC to log out, Ctrl Q to exit or Enter to proceed to User Menu.");
                                    Console.ResetColor();
                                    //============================  ADMIN EDW PERA PREPEI NA MPEI SWITCH   ============================================================//
                                    ExitOptions userExitOption = inputManager.InputExitChoice();
                                    switch (userExitOption)
                                    {
                                        case ExitOptions.Esc:
                                            Console.Clear();
                                            Console.WriteLine("\nLogging out...");
                                            System.Threading.Thread.Sleep(700); // wait a bit...
                                            break;

                                        case ExitOptions.CtrlQ:
                                            Console.Clear();
                                            Console.WriteLine("\nClosing application...");
                                            Environment.Exit(0);
                                            break;

                                        case ExitOptions.Enter:
                                            //============================  ADMIN EDW PERA PREPEI NA MPEI SWITCH   ============================================================//
                                            // Log in user <-- MALON DEN XREIAZETAI 
                                            //dBManager.LogInUser(usernameLogin);
                                            //bool islogged = dBManager.IsLoggedIn(usernameLogin);
                                            bool isLogged = true;

                                            //
                                            UserTypes userType = dBManager.GetUserType(usernameLogin); //check user type

                                            switch (userType)
                                            {
                                                case UserTypes.SimpleUser:
                                                    do
                                                    {
                                                        UserMenuOptions userMenuOption = menus.UserMenu(usernameLogin);
                                                        switch (userMenuOption)
                                                        {
                                                            case UserMenuOptions.CreateNewMessage: // OK  
                                                                Console.Clear();
                                                                Console.WriteLine("======= Create new Message =======");
                                                                Console.WriteLine();
                                                                Console.WriteLine("Please type the username of the recipient of the message:\n");
                                                                string recipient = inputManager.InputUserName();

                                                                if (recipient is null) //if ESC is pressed
                                                                {
                                                                    break;
                                                                }

                                                                // check if recipient username exists in database
                                                                bool recipientExists = dBManager.DoesUsernameExist(recipient);
                                                                if (!recipientExists)
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                                                    Console.WriteLine();
                                                                    Console.WriteLine($"A user with username '{recipient}' does not exist.");
                                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                                    Console.WriteLine("\nPress any key to go back to the user menu");
                                                                    Console.ResetColor();
                                                                    Console.ReadKey();
                                                                }
                                                                else // the recipient exists. Go on to create and send message
                                                                {
                                                                    // check if recipient is active
                                                                    bool recipientActive = dBManager.IsUserActive(recipient);
                                                                    if (!recipientActive)
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                        Console.WriteLine("\nThe recipient you chose is no longer active.");
                                                                        Console.WriteLine("Try sending a message to another user.");
                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                    else
                                                                    {
                                                                        messageManager.CreateMessage(usernameLogin, recipient);
                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                    //
                                                                    //messageManager.CreateMessage(usernameLogin, recipient);
                                                                    //Console.ForegroundColor = ConsoleColor.Blue;
                                                                    //Console.WriteLine("\nPress any key to go back to the user menu");
                                                                    //Console.ResetColor();
                                                                    //Console.ReadKey();
                                                                }
                                                                break;

                                                            case UserMenuOptions.Inbox: // OK 
                                                                messageManager.ShowInbox(usernameLogin);
                                                                break;

                                                            case UserMenuOptions.SentMessages: // OK 
                                                                messageManager.ShowSentMessages(usernameLogin);
                                                                break;

                                                            case UserMenuOptions.Info:  // OK                                                   
                                                                dBManager.GetUserInfo(usernameLogin);
                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                Console.WriteLine("\nPress any key to go back");
                                                                Console.ResetColor();
                                                                Console.ReadKey();
                                                                break;

                                                            case UserMenuOptions.ExitToMain: // OK
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nLogging out...");
                                                                System.Threading.Thread.Sleep(700); // wait for 0.7 seconds
                                                                break;
                                                            case UserMenuOptions.Quit: // OK
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nClosing application...");
                                                                Environment.Exit(0);
                                                                break;
                                                        } // end of switch for user menu options
                                                    } while (isLogged);
                                                    break;
                                                //==========================================================================================================//
                                                case UserTypes.JuniorAdmin:

                                                    do
                                                    {
                                                        JuniorAdminMenuOptions juniorAdminMenuOption = menus.JuniorAdminMenu(usernameLogin);

                                                        switch (juniorAdminMenuOption)
                                                        {
                                                            case JuniorAdminMenuOptions.CreateNewMessage: // OK

                                                                Console.Clear();
                                                                Console.WriteLine("======= Create new Message =======");
                                                                Console.WriteLine();
                                                                Console.WriteLine("Please type the username of the recipient of the message:\n");
                                                                string recipient = inputManager.InputUserName();

                                                                if (recipient is null) //if ESC is pressed
                                                                {
                                                                    break;
                                                                }

                                                                // check if recipient username exists in database
                                                                bool recipientExists = dBManager.DoesUsernameExist(recipient);
                                                                if (!recipientExists)
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                                                    Console.WriteLine();
                                                                    Console.WriteLine($"A user with username '{recipient}' does not exist.");
                                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                                    Console.WriteLine("\nPress any key to go back to the user menu");
                                                                    Console.ResetColor();
                                                                    Console.ReadKey();
                                                                }
                                                                else // the recipient exists. Go on to create and send message
                                                                {
                                                                    // check if recipient is active
                                                                    bool recipientActive = dBManager.IsUserActive(recipient);
                                                                    if (!recipientActive)
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                        Console.WriteLine("\nThe recipient you chose is no longer active.");
                                                                        Console.WriteLine("Try sending a message to another user.");
                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                    else
                                                                    {
                                                                        messageManager.CreateMessage(usernameLogin, recipient);
                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                }
                                                                break;

                                                            case JuniorAdminMenuOptions.Inbox: // OK

                                                                messageManager.ShowInbox(usernameLogin);
                                                                break;

                                                            case JuniorAdminMenuOptions.SentMessages: // OK

                                                                messageManager.ShowSentMessages(usernameLogin);
                                                                break;

                                                            case JuniorAdminMenuOptions.Info: // OK

                                                                dBManager.GetUserInfo(usernameLogin);
                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                Console.WriteLine("\nPress any key to go back");
                                                                Console.ResetColor();
                                                                Console.ReadKey();
                                                                break;

                                                            case JuniorAdminMenuOptions.ViewUserInfo:  // OK

                                                                dBManager.ViewUserInfo();
                                                                break;

                                                            case JuniorAdminMenuOptions.ViewUserMessages:  // OK

                                                                messageManager.ViewUserMessages();
                                                                break;

                                                            case JuniorAdminMenuOptions.ViewAllMessages:  // OK

                                                                messageManager.ViewAllMessages();
                                                                break;

                                                            case JuniorAdminMenuOptions.EditMessages: // Ok

                                                                messageManager.EditMessage();
                                                                break;

                                                            case JuniorAdminMenuOptions.ExitToMain: // OK
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nGoodbye Junior...");
                                                                System.Threading.Thread.Sleep(700); // wait for 0.7 seconds
                                                                break;

                                                            case JuniorAdminMenuOptions.Quit: // OK
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nClosing application...");
                                                                Environment.Exit(0);
                                                                break;
                                                        }

                                                    } while (isLogged);

                                                    break;

                                                //==========================================================================================================//
                                                case UserTypes.MasterAdmin:

                                                    do
                                                    {
                                                        MasterAdminMenuOptions masterAdminMenuOption = menus.MasterAdminMenu(usernameLogin);

                                                        switch (masterAdminMenuOption)
                                                        {
                                                            case MasterAdminMenuOptions.CreateNewMessage: // OK

                                                                Console.Clear();
                                                                Console.WriteLine("======= Create new Message =======");
                                                                Console.WriteLine();
                                                                Console.WriteLine("Please type the username of the recipient of the message:\n");
                                                                string recipient = inputManager.InputUserName();

                                                                if (recipient is null) //if ESC is pressed
                                                                {
                                                                    break;
                                                                }

                                                                // check if recipient username exists in database
                                                                bool recipientExists = dBManager.DoesUsernameExist(recipient);
                                                                if (!recipientExists)
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                                                    Console.WriteLine();
                                                                    Console.WriteLine($"A user with username '{recipient}' does not exist.");
                                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                                    Console.WriteLine("\nPress any key to go back to the user menu");
                                                                    Console.ResetColor();
                                                                    Console.ReadKey();
                                                                }
                                                                else // the recipient exists. Go on to create and send message
                                                                {
                                                                    // check if recipient is active
                                                                    bool recipientActive = dBManager.IsUserActive(recipient);
                                                                    if (!recipientActive)
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                        Console.WriteLine("\nThe recipient you chose is no longer active.");
                                                                        Console.WriteLine("Try sending a message to another user.");
                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                    else
                                                                    {
                                                                        messageManager.CreateMessage(usernameLogin, recipient);
                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                }
                                                                break;

                                                            case MasterAdminMenuOptions.Inbox: // OK

                                                                messageManager.ShowInbox(usernameLogin);
                                                                break;

                                                            case MasterAdminMenuOptions.SentMessages: // OK

                                                                messageManager.ShowSentMessages(usernameLogin);
                                                                break;

                                                            case MasterAdminMenuOptions.Info: // OK

                                                                dBManager.GetUserInfo(usernameLogin);
                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                Console.WriteLine("\nPress any key to go back");
                                                                Console.ResetColor();
                                                                Console.ReadKey();
                                                                break;

                                                            case MasterAdminMenuOptions.ViewUserInfo:  // OK

                                                                dBManager.ViewUserInfo();
                                                                break;

                                                            case MasterAdminMenuOptions.ViewUserMessages:  // OK

                                                                messageManager.ViewUserMessages();
                                                                break;

                                                            case MasterAdminMenuOptions.ViewAllMessages:  // OK

                                                                messageManager.ViewAllMessages();
                                                                break;

                                                            case MasterAdminMenuOptions.EditMessages: // OK

                                                                messageManager.EditMessage();
                                                                break;

                                                            case MasterAdminMenuOptions.DeleteMessages: // Ok

                                                                messageManager.DeleteMessage();
                                                                break;

                                                            case MasterAdminMenuOptions.ExitToMain: // OK
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nGoodbye Admin...");
                                                                System.Threading.Thread.Sleep(700); // wait for 0.7 seconds
                                                                break;

                                                            case MasterAdminMenuOptions.Quit: // OK
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nClosing application...");
                                                                Environment.Exit(0);
                                                                break;
                                                        }

                                                    } while (isLogged);

                                                    break;

                                                case UserTypes.SuperAdmin:

                                                    do
                                                    {
                                                        SuperAdminMenuOptions superAdminMenuOption = menus.SuperAdminMenu(usernameLogin);

                                                        switch (superAdminMenuOption)
                                                        {
                                                            case SuperAdminMenuOptions.CreateNewMessage: // OK

                                                                Console.Clear();
                                                                Console.WriteLine("======= Create new Message =======");
                                                                Console.WriteLine();
                                                                Console.WriteLine("Please type the username of the recipient of the message:\n");
                                                                string recipient = inputManager.InputUserName();

                                                                if (recipient is null) //if ESC is pressed
                                                                {
                                                                    break;
                                                                }

                                                                // check if recipient username exists in database
                                                                bool recipientExists = dBManager.DoesUsernameExist(recipient);
                                                                if (!recipientExists)
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                                                    Console.WriteLine();
                                                                    Console.WriteLine($"A user with username '{recipient}' does not exist.");
                                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                                    Console.WriteLine("\nPress any key to go back to the user menu");
                                                                    Console.ResetColor();
                                                                    Console.ReadKey();
                                                                }
                                                                else // the recipient exists. Go on to create and send message
                                                                {
                                                                    // check if recipient is active
                                                                    bool recipientActive = dBManager.IsUserActive(recipient);
                                                                    if (!recipientActive)
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                        Console.WriteLine("\nThe recipient you chose is no longer active.");
                                                                        Console.WriteLine("Try sending a message to another user.");
                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                    else
                                                                    {
                                                                        messageManager.CreateMessage(usernameLogin, recipient);
                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                }
                                                                break;

                                                            case SuperAdminMenuOptions.Inbox: // OK

                                                                messageManager.ShowInbox(usernameLogin);
                                                                break;

                                                            case SuperAdminMenuOptions.SentMessages: // OK

                                                                messageManager.ShowSentMessages(usernameLogin);
                                                                break;

                                                            case SuperAdminMenuOptions.Info: // OK

                                                                dBManager.GetUserInfo(usernameLogin);
                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                Console.WriteLine("\nPress any key to go back");
                                                                Console.ResetColor();
                                                                Console.ReadKey();
                                                                break;
                                                            //==========================================================================================================================================//
                                                            case SuperAdminMenuOptions.CreateNewUser: // OK

                                                                dBManager.CreateNewUser();
                                                                break;

                                                            case SuperAdminMenuOptions.DeleteUser: // OK

                                                                dBManager.DeleteUser();
                                                                break;

                                                            case SuperAdminMenuOptions.ActivateUser:  // OK

                                                                dBManager.ActivateUser();
                                                                break;

                                                            case SuperAdminMenuOptions.EditUserType: // OK

                                                                dBManager.EditUserType();
                                                                break;

                                                            case SuperAdminMenuOptions.ViewUserInfo:  // OK

                                                                dBManager.ViewUserInfo();
                                                                break;

                                                            case SuperAdminMenuOptions.ViewUserMessages:  // OK

                                                                messageManager.ViewUserMessages();
                                                                break;

                                                            case SuperAdminMenuOptions.ViewAllMessages:  // OK

                                                                messageManager.ViewAllMessages();
                                                                break;

                                                            case SuperAdminMenuOptions.DeleteMessages: // OK

                                                                messageManager.DeleteMessage();
                                                                break;

                                                            case SuperAdminMenuOptions.EditMessages: // Ok

                                                                messageManager.EditMessage();
                                                                break;

                                                            case SuperAdminMenuOptions.ExitToMain: // OK
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nGoodbye Master...");
                                                                System.Threading.Thread.Sleep(700); // wait for 0.7 seconds
                                                                break;

                                                            case SuperAdminMenuOptions.Quit: // OK
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nClosing application...");
                                                                Environment.Exit(0);
                                                                break;
                                                        }

                                                    } while (isLogged);

                                                    break;
                                                    //==========================================================================================================//
                                            }
                                            //} while (isLogged);
                                            break;
                                    }
                                }
                            }
                        }
                        else // Break to main menu if username is null after ESC is pressed
                        {
                            break;
                        }
                        break;

                    case MainMenuOptions.SignUp: // OK
                        bool itExists = false;
                        string usernameSignup;
                        do
                        {
                            menus.SignUpMenu(); // includes console clear and welcome message
                            usernameSignup = inputManager.InputUserName(); // returns null if ESC is pressed
                            if (usernameSignup is null)
                            {
                                break;
                            }
                            itExists = dBManager.DoesUsernameExist(usernameSignup); // Check if username already exists in database
                            if (itExists)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("The username you entered already exists. Please choose another.");
                                Console.ResetColor();
                                Console.ReadKey();
                            }
                        } while (itExists);
                        if (usernameSignup != null) // username is null if ESC is pressed
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The username you entered is not taken. Nice choice!");
                            Console.ResetColor();

                            Console.WriteLine();
                            string password = inputManager.InputPassword();
                            dBManager.AddUser(usernameSignup, password); // add new user to database
                            Console.WriteLine("\nPress any key to go back to Main Menu.");
                            Console.ReadKey();
                        }
                        break;

                    case MainMenuOptions.Info: // OK
                        dBManager.GetInfo();
                        break;

                    case MainMenuOptions.Exit: // OK
                        Console.Clear();
                        Console.WriteLine("\nClosing application...");
                        Environment.Exit(0);
                        break;
                }
            } while (true);
        }
    }
}
