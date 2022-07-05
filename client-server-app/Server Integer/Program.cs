using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketListener
{
    // Incoming data from the client.  
    public static string data = null;
    public static int dataInt,x;

    public static void StartListening()
    {
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
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and
        // listen for incoming connections.  
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            // Start listening for connections.  
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                // Program is suspended while waiting for an incoming connection.  
                Socket handler = listener.Accept();
                data = null;

                // An incoming connection needs to be processed.  
                while (true)
                {
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        break;
                    }
                }
                //Console.WriteLine("{0}", data); 
                string dataStr = data.Substring(0, data.Length - 5);
                //string[] dataList = dataStr.Split(" ");
                //string operation = dataList[0];
                //Console.WriteLine(operation);
                bool isNumeric = int.TryParse(dataStr, out _);
            
                if (isNumeric)
                {

                    string errorCode = "";
                    dataInt = Int32.Parse(dataStr);
                    Console.WriteLine(dataInt);
                    if (dataInt >= 0 & dataInt < 13)
                    {
                        errorCode = "0:0";
                        Console.WriteLine("Code: " + errorCode + " Valid Input");
                        byte[] msg = Encoding.ASCII.GetBytes(errorCode);

                        handler.Send(msg);
                        //handler.Shutdown(SocketShutdown.Both);
                        //handler.Close();
                    }
                    else
                    {
                        errorCode = "0:1";
                        Console.WriteLine("Code: " + errorCode + " Input Is Out Of Range");
                        byte[] msg = Encoding.ASCII.GetBytes(errorCode);
                        handler.Send(msg);
                        //handler.Shutdown(SocketShutdown.Both);
                        //handler.Close();
                    }
                }
                else
                {
                    string errorCode = "0:2";
                    Console.WriteLine("Code: " + errorCode + " Invalid Input");
                    byte[] msg = Encoding.ASCII.GetBytes(errorCode);
                    handler.Send(msg);
                    //handler.Shutdown(SocketShutdown.Both);
                    //handler.Close();
                }
                int bytesRec1 = handler.Receive(bytes);
                string dataStr1 = Encoding.ASCII.GetString(bytes, 0, bytesRec1);
                Console.WriteLine(dataStr1);
                int trueF = 1;
                x = dataInt;
                while (trueF==1)
                {
                    switch (dataStr1)
                    {
                        /*case "1":
                        {
                           x = factorial(dataInt);
                           Console.WriteLine(x);
                           dataInt=x;
                           break;
                        }*/
                        case "2":
                        {
                           x = fibonacci(dataInt);
                           Console.WriteLine(x);
                           dataInt=x;
                           trueF=0;
                           break;
                        }
                        
                    }
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static int fibonacci(int num)
    {

        if (num == 0)
        {
            Console.Write(0);
            return 0;
        }

        // Initialize the first & second
        // terms of the Fibonacci series
        int first = 0, second = 1;

        // Store the third term
        int third = first + second;

        // Iterate until the third term
        // is less than or equal to num
        while (third <= num)
        {

            // Update the first
            first = second;

            // Update the second
            second = third;

            // Update the third
            third = first + second;
        }

        // Store the Fibonacci number
        // having smaller difference with N
        int ans = (Math.Abs(third - num) >=
                Math.Abs(second - num)) ?
                        second : third;

        // Print the result
        Console.WriteLine(ans);
        return ans;
    }

    public static int factorial(int num)
    {

    
        if (num == 0)
            return 1;
 
        return num * factorial(num - 1);
    }

    public static int Main(String[] args)
    {
        StartListening();
        return 0;
    }
}
