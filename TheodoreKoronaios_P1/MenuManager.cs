using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheodoreKoronaios_P1
{
    public class MenuManager
    {

        // Scroll Menu 
        public int ScrollMenu(string header, string[] menuItems) // OK
        {
            int currentOption = 0; // Holds current option in the menu
            ConsoleKeyInfo keyPressed; // Struct ConsoleKeyInfo
            do
            {
                Console.Clear();
                Console.WriteLine(header); // Print the header in console
                Console.WriteLine(); // Leave an empty line
                for (int c = 0; c < menuItems.Length; c++)
                {
                    if (currentOption == c)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(">> ");
                        Console.WriteLine(menuItems[c]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"   {menuItems[c]}");
                    }
                }
                keyPressed = Console.ReadKey(true);

                if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    currentOption++;
                    if (currentOption > menuItems.Length - 1) //Go back to top
                    {
                        currentOption = 0;
                    }
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    currentOption--;
                    if (currentOption < 0) // Go back to bottom
                    {
                        currentOption = (menuItems.Length - 1);
                    }
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    return currentOption;
                }
            } while (true);

        }

        // Main Menu
        public MainMenuOptions MainMenu()
        {
            while (true) // Main Menu runs in while loop until user chooses to Exit 
            {
                string header = "Student: Theodore Koronaios\nC# BootCamp\n\nHello & welcome to this awesome application!\n";
                int option = ScrollMenu(header, Enum.GetNames(typeof(MainMenuOptions))); // Option returned by user
                switch (option)
                {
                    case 0:
                        return MainMenuOptions.Login;
                    case 1:
                        return MainMenuOptions.SignUp;
                    case 2:
                        return MainMenuOptions.Info;
                    case 3:
                        return MainMenuOptions.Exit;
                }
            }
        }

        // Menu User
        public UserMenuOptions UserMenu(string username)
        {
            while (true)
            {
                string header = $"======= Welcome to the User Menu {username}! =======\n\nPlease choose an option.\n";
                int option = ScrollMenu(header, Enum.GetNames(typeof(UserMenuOptions))); // Option returned by user
                switch (option)
                {
                    case 0:
                        return UserMenuOptions.CreateNewMessage;
                    case 1:
                        return UserMenuOptions.Inbox;
                    case 2:
                        return UserMenuOptions.SentMessages;
                    case 3:
                        return UserMenuOptions.Info;
                    case 4:
                        return UserMenuOptions.ExitToMain;
                    case 5:
                        return UserMenuOptions.Quit;
                }
            }

        }

        // Menu Super Admin
        public SuperAdminMenuOptions SuperAdminMenu(string username)
        {
            while (true)
            {
                string header = $"======= Welcome to the SUPER ADMIN Menu =======\n\nPlease choose an option.\n";
                int option = ScrollMenu(header, Enum.GetNames(typeof(SuperAdminMenuOptions))); // Option returned by super admin
                switch (option)
                {
                    case 0:
                        return SuperAdminMenuOptions.CreateNewMessage;
                    case 1:
                        return SuperAdminMenuOptions.Inbox;
                    case 2:
                        return SuperAdminMenuOptions.SentMessages;
                    case 3:
                        return SuperAdminMenuOptions.Info;
                    case 4:
                        return SuperAdminMenuOptions.CreateNewUser;
                    case 5:
                        return SuperAdminMenuOptions.DeleteUser;
                    case 6:
                        return SuperAdminMenuOptions.ActivateUser;
                    case 7:
                        return SuperAdminMenuOptions.EditUserType;
                    case 8:
                        return SuperAdminMenuOptions.ViewUserInfo;
                    case 9:
                        return SuperAdminMenuOptions.ViewUserMessages;
                    case 10:
                        return SuperAdminMenuOptions.ViewAllMessages;
                    case 11:
                        return SuperAdminMenuOptions.DeleteMessages;
                    case 12:
                        return SuperAdminMenuOptions.EditMessages;
                    case 13:
                        return SuperAdminMenuOptions.ExitToMain;
                    case 14:
                        return SuperAdminMenuOptions.Quit;
                }

            }
        }

        // Menu Junior Admin
        public JuniorAdminMenuOptions JuniorAdminMenu(string username)
        {
            while (true)
            {
                string header = $"======= Welcome to the Junior ADMIN Menu =======\n\nPlease choose an option.\n";
                int option = ScrollMenu(header, Enum.GetNames(typeof(JuniorAdminMenuOptions))); // Option returned by junior admin
                switch (option)
                {
                    case 0:
                        return JuniorAdminMenuOptions.CreateNewMessage;
                    case 1:
                        return JuniorAdminMenuOptions.Inbox;
                    case 2:
                        return JuniorAdminMenuOptions.SentMessages;
                    case 3:
                        return JuniorAdminMenuOptions.Info;
                    case 4:
                        return JuniorAdminMenuOptions.ViewUserInfo;
                    case 5:
                        return JuniorAdminMenuOptions.ViewUserMessages;
                    case 6:
                        return JuniorAdminMenuOptions.ViewAllMessages;
                    case 7:
                        return JuniorAdminMenuOptions.EditMessages;
                    case 8:
                        return JuniorAdminMenuOptions.ExitToMain;
                    case 9:
                        return JuniorAdminMenuOptions.Quit;
                }

            }
        }

        // Menu Master Admin
        public MasterAdminMenuOptions MasterAdminMenu(string username)
        {
            while (true)
            {
                string header = $"======= Welcome to the Master ADMIN Menu =======\n\nPlease choose an option.\n";
                int option = ScrollMenu(header, Enum.GetNames(typeof(MasterAdminMenuOptions))); // Option returned by master admin
                switch (option)
                {
                    case 0:
                        return MasterAdminMenuOptions.CreateNewMessage;
                    case 1:
                        return MasterAdminMenuOptions.Inbox;
                    case 2:
                        return MasterAdminMenuOptions.SentMessages;
                    case 3:
                        return MasterAdminMenuOptions.Info;
                    case 4:
                        return MasterAdminMenuOptions.ViewUserInfo;
                    case 5:
                        return MasterAdminMenuOptions.ViewUserMessages;
                    case 6:
                        return MasterAdminMenuOptions.ViewAllMessages;
                    case 7:
                        return MasterAdminMenuOptions.EditMessages;
                    case 8:
                        return MasterAdminMenuOptions.DeleteMessages;
                    case 9:
                        return MasterAdminMenuOptions.ExitToMain;
                    case 10:
                        return MasterAdminMenuOptions.Quit;
                }

            }
        }





        // Menu SignUp
        public void SignUpMenu()
        {
            Console.Clear();
            Console.WriteLine("======= Welcome to the Sign Up Menu! =======\n");
            Console.WriteLine("Seems like you are a new user!\n");
            Console.WriteLine("Please enter the username you would like to have or press ESC to go back:");

        }

        // Menu Login
        public void LoginMenu()
        {
            Console.Clear();
            Console.WriteLine("======= Welcome to the Login Menu! =======\n");
            Console.WriteLine("Enter your username or press ESC to go back:");
        }

        // Scroll Inbox Menu 
        public void ScrollInboxMenu(string username, List<Message> messages) // list of inbox messages
        {
            using (var db = new MyContext())
            {
                // arrays to hold messages information
                int[] messageIds = messages.Select(x => x.MessageId).ToArray(); // returns all items
                string[] subjects = messages.Select(x => x.Subject).ToArray();
                string[] contents = messages.Select(x => x.Content).ToArray();
                string[] senders = messages.Select(x => x.Sender.Username).ToArray();
                string[] date = messages.Select(x => x.DateCreated.ToString()).ToArray();

                int currentOption = 0; // Holds current option in the menu
                ConsoleKeyInfo keyPressed; // Struct ConsoleKeyInfo
                do
                {
                    Console.Clear();
                    Console.WriteLine(); // Leave an empty line
                    Console.WriteLine($"======= Inbox of {username} =======");
                    Console.WriteLine();
                    for (int c = 0; c < messageIds.Length; c++)
                    {
                        if (currentOption == c)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(">> ");
                            Console.WriteLine($"{messageIds[c]}: {subjects[c]}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"   {messageIds[c]}: {subjects[c]}");

                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\n\n\nPress ESC to go back.");
                    Console.ResetColor();
                    keyPressed = Console.ReadKey(true);

                    if (keyPressed.Key == ConsoleKey.DownArrow)
                    {
                        currentOption++;
                        if (currentOption > messageIds.Length - 1) //Go back to top
                        {
                            currentOption = 0;
                        }
                    }
                    else if (keyPressed.Key == ConsoleKey.UpArrow)
                    {
                        currentOption--;
                        if (currentOption < 0) // Go back to bottom
                        {
                            currentOption = (messageIds.Length - 1);
                        }
                    }
                    else if (keyPressed.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine($"Message ID   : {messageIds[currentOption]}");
                        Console.WriteLine($"From         : {senders[currentOption]}");
                        Console.WriteLine($"To           : {username}");
                        Console.WriteLine($"Received on  : {date[currentOption]}");

                        Console.WriteLine();
                        Console.WriteLine("\nBody of message:\n");
                        Console.WriteLine(contents[currentOption]);

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Press any key to go back.");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else if (keyPressed.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                } while (true);
            }
        }

        // Scroll Sent Menu 
        public void ScrollSentMenu(string username, List<Message> messages) // list of sent messages
        {
            using (var db = new MyContext())
            {
                // arrays to hold messages information
                int[] messageIds = messages.Select(x => x.MessageId).ToArray(); // returns all items
                string[] subjects = messages.Select(x => x.Subject).ToArray();
                string[] contents = messages.Select(x => x.Content).ToArray();
                string[] recipients = messages.Select(x => x.Recipient.Username).ToArray();
                string[] date = messages.Select(x => x.DateCreated.ToString()).ToArray();

                int currentOption = 0; // Holds current option in the menu
                ConsoleKeyInfo keyPressed; // Struct ConsoleKeyInfo
                do
                {
                    Console.Clear();
                    Console.WriteLine(); // Leave an empty line
                    Console.WriteLine($"======= Sent Messages from {username} =======");
                    Console.WriteLine();
                    for (int c = 0; c < messageIds.Length; c++)
                    {
                        if (currentOption == c)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(">> ");
                            Console.WriteLine($"{messageIds[c]}: {subjects[c]}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"   {messageIds[c]}: {subjects[c]}");

                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\n\n\nPress ESC to go back.");
                    Console.ResetColor();
                    keyPressed = Console.ReadKey(true);

                    if (keyPressed.Key == ConsoleKey.DownArrow)
                    {
                        currentOption++;
                        if (currentOption > messageIds.Length - 1) //Go back to top
                        {
                            currentOption = 0;
                        }
                    }
                    else if (keyPressed.Key == ConsoleKey.UpArrow)
                    {
                        currentOption--;
                        if (currentOption < 0) // Go back to bottom
                        {
                            currentOption = (messageIds.Length - 1);
                        }
                    }
                    else if (keyPressed.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine($"Message ID   : {messageIds[currentOption]}");
                        Console.WriteLine($"From         : {username}");
                        Console.WriteLine($"To           : {recipients[currentOption]}");
                        Console.WriteLine($"Sent on      : {date[currentOption]}");

                        Console.WriteLine();
                        Console.WriteLine("\nBody of message:\n");
                        Console.WriteLine(contents[currentOption]);

                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Press any key to go back.");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else if (keyPressed.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                } while (true);
            }
        }
    }
}
