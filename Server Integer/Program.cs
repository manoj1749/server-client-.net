﻿using System;  
using System.Net;  
using System.Net.Sockets;  
using System.Text;  
  
public class SynchronousSocketListener {  
  
    // Incoming data from the client.  
    public static string data = null;  
  
    public static void StartListening() {  
        // Data buffer for incoming data.  
        byte[] bytes = new Byte[1024];  
  
        // Establish the local endpoint for the socket.  
        // Dns.GetHostName returns the name of the
        // host running the application.  
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
        IPAddress ipAddress = ipHostInfo.AddressList[0];  
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);  
  
        // Create a TCP/IP socket.  
        Socket listener = new Socket(ipAddress.AddressFamily,  
            SocketType.Stream, ProtocolType.Tcp );  
  
        // Bind the socket to the local endpoint and
        // listen for incoming connections.  
        try {  
            listener.Bind(localEndPoint);  
            listener.Listen(10);  
  
            // Start listening for connections.  
            while (true) {  
                Console.WriteLine("Waiting for a connection...");  
                // Program is suspended while waiting for an incoming connection.  
                Socket handler = listener.Accept();  
                data = null;  
  
                // An incoming connection needs to be processed.  
                while (true) {  
                    int bytesRec = handler.Receive(bytes);  
                    data += Encoding.ASCII.GetString(bytes,0,bytesRec);  
                    if (data.IndexOf("<EOF>") > -1) {  
                        break;  
                    }  
                }  
                //Console.WriteLine("{0}", data); 
                string dataStr = data.Substring(0,data.Length-5);
                bool isNumeric = int.TryParse(dataStr, out _);
                if (isNumeric)
                {
                int dataInt = Int32.Parse(dataStr);
                int dataf = dataInt*dataInt;
                // Show the data on the console.  
                Console.WriteLine( "The Square Of Received Number : {0}", dataf);  
                // Echo the data back to the client.  
                byte[] msg = Encoding.ASCII.GetBytes(dataf.ToString());  
                handler.Send(msg);  
                handler.Shutdown(SocketShutdown.Both);  
                handler.Close();  
                }
                else
                {
                    Console.WriteLine(dataStr);
                    byte[] msg = Encoding.ASCII.GetBytes(dataStr); 
                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();  
                }
            }  
  
        } catch (Exception e) {  
            Console.WriteLine(e.ToString());  
        }  
  
        Console.WriteLine("\nPress ENTER to continue...");  
        Console.Read();  
  
    }  
  
    public static int Main(String[] args) {  
        StartListening();  
        return 0;  
    }  
}