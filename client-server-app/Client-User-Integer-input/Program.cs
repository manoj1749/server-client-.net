﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketClient
{

    public static void StartClient()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new byte[1024];

        // Connect to a remote device.  
        try
        {
            // Establish the remote endpoint for the socket.  
            // This example uses port 11000 on the local computer.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.  
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.
                Console.WriteLine("Enter The Number You Want To Send : ");

                // Create a string variable and get user input from the keyboard and store it in the variable
                string userInput = Console.ReadLine();
                //bool isNumeric = int.TryParse(userInput, out _);
                //Console.WriteLine(isNumeric);
                //if (isNumeric)
                //{
                //int dataInt = Int32.Parse(userInput);
                /*string[] userList = userInput.Split(" ");
                Console.WriteLine(userList[0]);
                if (userList[0] == "0")
                    Console.WriteLine("Only Factorial Is Needed");
                else if (userList[1] == "1")
                    Console.WriteLine("Only Fibonocci Is Needed");
                else if (userList[1] == "2")
                    Console.WriteLine("Both Factorial And Fibonacci Of Factorial Is Needed");*/
                Console.WriteLine("The Number You Entered Is : " + userInput);
                Console.WriteLine("=======================================");
                byte[] msg = Encoding.ASCII.GetBytes(userInput + "<EOF>");
                // Send the data through the socket.  
                //byte[] msg2 = Encoding.ASCII.GetBytes("<EOF>");
                int bytesSent1 = sender.Send(msg);
                //int bytesSent2 = sender.Send(msg2);
                // Receive the response from the remote device.
                int bytesRec = sender.Receive(bytes);
                //int bytesRec2 = sender.Receive(bytes);
                //onsole.WriteLine(bytesRec2);
                string serverErData1 = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                //string[] serverList = serverData.Split(",");
                Console.WriteLine(serverErData1);
                byte[] msg3;
                int bytesSent3;
                string x;
                //int t = 1;
                switch (serverErData1)
                {
                    case "0:0":
                        {
                            /*msg3 = Encoding.ASCII.GetBytes(errReply);
                            bytesSent3 = sender.Send(msg3);*/
                            Console.WriteLine("Code: " + serverErData1);
                            Console.WriteLine("Valid Input");
                            x = Operation();
                            msg3 = Encoding.ASCII.GetBytes(x);
                            bytesSent3 = sender.Send(msg3);
                            int bytesRec1 = sender.Receive(bytes);
                            string dataRec = Encoding.ASCII.GetString(bytes, 0, bytesRec1);
                            Console.WriteLine(dataRec);
                            Console.WriteLine("Do You Want To Do Some More Operations On This Number?");
                            Console.WriteLine("1:Yes");
                            Console.WriteLine("2:No");
                            int response = Console.Read();
                            if (response == 1)
                            {
                                x = Operation();
                            }
                            else if (response == 2)
                            {
                                Console.WriteLine("Sure!!!");
                                sender.Shutdown(SocketShutdown.Both);
                                sender.Close();
                            }
                            /*else
                            {
                                Console.WriteLine("Enter The Number Corresponding To The Operation Shown");
                            }*/
                            break;
                        }
                    case "0:1":
                        {
                            //Console.WriteLine("Input Is Out Of Range");
                            /*msg3 = Encoding.ASCII.GetBytes(errReply);
                            bytesSent3 = sender.Send(msg3);*/
                            Console.WriteLine("Code: " + serverErData1);
                            Console.WriteLine("Input Is Out Of Range");
                            break;
                        }
                    case "0:2":
                        {

                            /*msg3 = Encoding.ASCII.GetBytes(errReply);
                            bytesSent3 = sender.Send(msg3);*/
                            Console.WriteLine("Code: " + serverErData1);
                            Console.WriteLine("Invalid Input Datatype");
                            //StartClient();
                            break;
                        }
                }
                //byte[] msg3 = Encoding.ASCII.GetBytes(errReply);
                //int bytesSent3 = sender.Send(msg3);

                /*Console.WriteLine("The factorial of " + userInput + " : {0}",
                    serverList[1]);
                Console.WriteLine("The Sum Of Fibonacci Series less than " + serverList[1] + " : {0}",
                    serverList[2]);*/
                //Console.WriteLine("The sum of Fibonacci series less than : {0}",
                //    Encoding.ASCII.GetString(bytes, 0, bytesRec2));
                // Release the socket.  
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                //Console.WriteLine("Test");
                //}
                //else
                //{
                //Console.WriteLine("Only Numbers Are Accepted");
                //byte[] msg = Encoding.ASCII.GetBytes("Wrong Input Given<EOF>");
                //int bytesSent = sender.Send(msg);
                //int bytesRec = sender.Receive(bytes);
                //string serverData = Encoding.ASCII.GetString(bytes, 0, bytesRec);
                //Console.WriteLine(serverData);
                //sender.Shutdown(SocketShutdown.Both);
                //sender.Close();
                //}
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static string Operation()
    {
        Console.WriteLine("Enter the operation you want to do on the number");
        Console.WriteLine("1:Factorial");
        Console.WriteLine("2:Fibonocci");
        string operation = Console.ReadLine();
        if (operation == "1")
        {
            Console.WriteLine("Factorial Will Be Found");
            Console.WriteLine("=======================================");
            Console.Write("");
            //t = 0;
        }
        else if (operation == "2")
        {
            Console.WriteLine("Fibonocci Will Be Found");
            //t = 0;
        }
        return operation;
    }
    public static int Main(String[] args)
    {
        StartClient();
        return 0;
    }
}