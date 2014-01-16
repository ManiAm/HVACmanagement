using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irisys.BlackfinAPI;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Reflection;

using System.Threading;

namespace BlackfinAPIConsole
{
    /// <summary>
    /// Console based application demonstrating use of the .Net Irisys Blackfin API
    /// Uses Command pattern for simplicity
    /// Two nested loops for program logic (connection loop + command loop)
    /// </summary>
    public class BlackfinConsole
    {
        private readonly Blackfin blackfin;
        private bool isEnding;

        /// <summary>
        /// Creates a new BlackfinConsole instance
        /// </summary>
        /// <param name="bf"></param>
        BlackfinConsole(Blackfin bf)
        {
            blackfin = bf;
        }

        /// <summary>
        /// Stops the console application from working with the current blackfin
        /// </summary>
        void Disconnect()
        {
            isEnding = true;
        }

        /// <summary>
        /// Prompts the user to input a command
        /// and then executes it with appropriate arguments
        /// </summary>
        private void DoConsoleCommandLoop(Socket clientLabVIEW)
        {
            List<ConsoleCommand> Commands = BlackfinConsoleCommands.GetAllConsoleCommands();

            // sort commands by command name (don't take get/set prefix into account)
            Commands.Sort((o1, o2) =>
            {
                return o1.getName().Substring(3).CompareTo(o2.getName().Substring(3));
            });

            Console.WriteLine("########################");
            Console.WriteLine("Start sending commands.");
            Console.WriteLine("########################");
            Console.WriteLine();

            while (!isEnding)
            {
                // Console.WriteLine("Please enter a command to execute (or 'help'):");
                // String userString = Console.ReadLine().ToLower();
                String userString = "getcurrentcount";
                Console.WriteLine("command: ");
                Console.WriteLine(userString + "\n");

                if (userString.Equals("help"))
                {
                    Console.WriteLine("Help - Prints this list of available commands");
                    Console.WriteLine("Disconnect - Disconnects from the current device");
                    foreach (ConsoleCommand c in Commands)
                    {
                        Console.WriteLine(c.getName() + " - " + c.getHelp());
                    }
                }
                else if (userString.Equals("disconnect"))
                {
                    break;
                }
                else
                {
                    // try and execute a command
                    bool executed = false;
                    bool bDisconnect = false;
                    foreach (ConsoleCommand c in Commands)
                    {
                        try
                        {
                            if (c.getName().ToLower().Equals(userString))
                            {
                                executed = true;

                                // assemble any required arguments
                                String[] args = new String[c.getArguments().Length];
                                int i = 0;

                                foreach (String s in c.getArguments())
                                {
                                    Console.WriteLine("Please enter a value for " + s + ":");
                                    args[i++] = Console.ReadLine();
                                }

                                // execute command
                                string result = c.execute(blackfin, args);

                                if (result == null)
                                    Console.WriteLine("There was a problem executing the command");
                                else
                                {
                                    Console.Write("response:\n" + result + "\n");
                                    Console.Write("sending to LabVIEW: ");

                                    try
                                    {                                        
                                        string size = result.Length.ToString().PadLeft(10, '0');

                                        // first send the size of result  
                                        clientLabVIEW.Send(Encoding.ASCII.GetBytes(size));

                                        // then send the result itself  
                                        clientLabVIEW.Send(Encoding.ASCII.GetBytes(result));

                                        Console.WriteLine("done!");
                                        Console.WriteLine();
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine("Error!");
                                        Console.WriteLine(e.Message);
                                        Console.WriteLine("Program now exits ...");
                                        Environment.Exit(0);
                                    }
                                }

                                break;
                            }
                        }
                        catch (CommandArgumentException e)
                        {
                            Console.WriteLine("Problem with command argument: " + e.Message);
                        }
                        catch (ConnectionResetException e)
                        {
                          Console.WriteLine(e.Message);
                          Console.WriteLine("");
                          Console.WriteLine("This command has caused the connection to be reset");
                          bDisconnect = true;
                        }
                    }
                    if (!executed)
                    {
                        Console.WriteLine("No command was found matching: " + userString);
                    }

                    if (bDisconnect)
                      break;
                }
                
                // wait for a specific time
                Console.WriteLine("#################################################");
                Console.WriteLine("Waiting for 4 seconds to get the next sample ...");
                Console.WriteLine("#################################################");
                Console.WriteLine();

                Thread.Sleep(4000);
            }
        }

        /// <summary>
        /// Entry point for application.
        /// Starts the user CLI loop
        /// </summary>
        /// <param name="args"></param>
        public static void Main(String[] args)
        {
            bool ConnectedToLabVIEW = false;

            BlackfinEngine engine = new BlackfinEngine();
            engine.StartEngine();

            Socket clientLabVIEW = null;

            while (true)
            {
                if(!ConnectedToLabVIEW)
                {
                    try
                    {
                        // connect to labview TCP server
                        IPAddress ipinet2 = IPAddress.Parse("127.0.0.1");
                        IPEndPoint endPoint2 = new IPEndPoint(ipinet2, 4619);

                        Console.WriteLine("Connecting to the LabVIEW at " + ipinet2.ToString());

                        clientLabVIEW = new Socket(endPoint2.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                        clientLabVIEW.Connect(endPoint2);
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("Can not connect to the labVIEW! Re-trying in 10 seconds ...\n");
                        Thread.Sleep(10000);
                        continue;
                    }
                    
                    Console.WriteLine("LabVIEW Connected.\n");
                    ConnectedToLabVIEW = true;
                }

                try
                {
                    // connect to the people counter dvice
                    IPAddress ipinet = IPAddress.Parse("192.168.50.60");
                    IPEndPoint endPoint = new IPEndPoint(ipinet, 4505);

                    Console.WriteLine("Connecting to the device at " + ipinet.ToString());

                    Socket skt = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    skt.Connect(endPoint);

                    Blackfin blackfin = new Blackfin();
                    BlackfinConsole console = new BlackfinConsole(blackfin);

                    engine.AddNewCounterEndPoint(blackfin, skt, (bf, error, something, type) =>
                    {
                        Console.WriteLine("Comms Error!");
                        if (type == BlackfinEngine.ConnectionErrorType.FATAL)
                        {
                            console.Disconnect();
                        }
                    });

                    Console.WriteLine("Device Connected.\n");
                    console.DoConsoleCommandLoop(clientLabVIEW);
                    engine.RemoveCounterEndPoint(blackfin);
                    Console.WriteLine("Disconnected from device");
                }
                catch (Exception)
                {
                    Console.WriteLine("Can not connect to the Device! Re-trying in 10 seconds ...\n");
                    Thread.Sleep(10000);
                }
            }

            engine.ShutdownEngine();
        }

    }

    /// <summary>
    /// This will be thrown if incorrect arguments are passed to a command
    /// </summary>
    public class CommandArgumentException : Exception
    {
        public CommandArgumentException(String ss)
            : base(ss)
        {
        }
    }

    /// <summary>
    /// This will be thrown if any of the SetClientConfig* or SetDNS* commands are used
    /// as these will cause the device to be disconnected
    /// </summary>
    public class ConnectionResetException : Exception
    {
      public ConnectionResetException(String ss)
        : base(ss)
      {
      }
    }


}
