using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheodoreKoronaios_P1
{
    public class InputManager
    {
        // Receives Username from console, returns string Username
        // If ESC is pressed returns null
        public string InputUserName() // OK. Returns null if ESC is pressed while typing
        {
            string username;
            //WriteLine("Please enter the username you would like to have or press ESC to go back:");
            ConsoleKeyInfo keyPressed;
            username = "";
            do
            {
                keyPressed = Console.ReadKey(true); // Using false the pressed key is displayed in the console window
                while (keyPressed.Key == ConsoleKey.Enter && username.Length == 0) // Prevent from typing zero length username
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nUsername cannot be of zero length! Please try again.");
                    Console.ResetColor();
                    keyPressed = Console.ReadKey(true);
                }
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    return null;
                }
                if ((!char.IsControl(keyPressed.KeyChar))) // To remove control characters
                {
                    username += keyPressed.KeyChar;
                    Console.Write(keyPressed.KeyChar);
                }
                else
                {
                    if (keyPressed.Key == ConsoleKey.Backspace && username.Length > 0)
                    {
                        username = username.Substring(0, (username.Length - 1));
                        Console.Write("\b \b"); // "\b" is ASCII backspace
                                                //first moves the caret back, then writes a whitespace character that overwrites
                                                //the last char and moves the caret forward again. So we write a second \b to move
                                                //the caret back again. Now we have done what the backspace button normally does.
                    }
                }
            }
            while (keyPressed.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed
            Console.WriteLine();

            return username;
        }

        // Method to receive password from user. Checks for minimum length
        public string InputPassword() // OK
        {
            string password = "";
            int MinPassLength = 4; // Minimum length of password
            Console.WriteLine($"Please enter your preferred password [minimum {MinPassLength} characters long] : ");
            ConsoleKeyInfo keyPressed;
            while (password.Length < MinPassLength)
            {
                do
                {
                    keyPressed = Console.ReadKey(true); // Using false the pressed key is displayed in the console window

                    while (keyPressed.Key == ConsoleKey.Enter && password.Length == 0) // Prevent from typing zero length password
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nPassword cannot be of zero length! Please try again.");
                        Console.ResetColor();
                        keyPressed = Console.ReadKey(true);
                    }

                    if ((!char.IsControl(keyPressed.KeyChar))) // Remove control characters
                    {
                        password += keyPressed.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (keyPressed.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b"); // "\b" is ASCII backspace
                            //first moves the caret back, then writes a whitespace character that overwrites
                            //the last char and moves the caret forward again. So we write a second \b to move
                            //the caret back again. Now we have done what the backspace button normally does.
                        }
                    }
                }
                while (keyPressed.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed
                if (password.Length < MinPassLength)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nPassword must be at least {MinPassLength} characters long.\nPlease try again.");
                    Console.ResetColor();
                    password = "";
                }
            }
            Console.WriteLine();
            return password;
        }

        // Method to receive password during Login. Checks ONLY for zero length and not minimum length. Returns string.
        public string InputLoginPassword()
        {
            string password;
            ConsoleKeyInfo keyPressed;
            password = "";
            do
            {
                keyPressed = Console.ReadKey(true); // using false the pressed key is displayed in the console window

                while (keyPressed.Key == ConsoleKey.Enter && password.Length == 0) // prevent for typing zero length password
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPassword cannot be of zero length! Please try again.");
                    Console.ResetColor();
                    keyPressed = Console.ReadKey(true);
                }
                // Backspace Should Not Work
                //if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                if ((!char.IsControl(keyPressed.KeyChar))) // to remove control characters like ESC or F5
                {
                    password += keyPressed.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (keyPressed.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b"); // "\b" is ASCII backspace
                                                //first moves the caret back, then writes a whitespace character that overwrites
                                                //the last char and moves the caret forward again. So we write a second \b to move
                                                //the caret back again. Now we have done what the backspace button normally does.
                    }
                }
            }
            while (keyPressed.Key != ConsoleKey.Enter); // Stops Receving Keys Once Enter is Pressed
            Console.WriteLine();
            return password; // returns string password with length >0
        }

        // Method to receive ESC, CTRL Q or Enter
        public ExitOptions InputExitChoice()
        {
            do
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    return ExitOptions.Esc;
                }
                else if (keyPressed.Modifiers == ConsoleModifiers.Control & keyPressed.Key == ConsoleKey.Q)
                {
                    return ExitOptions.CtrlQ;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    return ExitOptions.Enter;
                }
            } while (true);
        }

    }
}
