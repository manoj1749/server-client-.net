using System;  
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
                
                string errorCode="";
                int dataInt = Int32.Parse(dataStr);
                Console.WriteLine(dataInt);
                if (dataInt>0 & dataInt<13)
                {
                    errorCode="{Code:0 Code Compiled Successfully}";
                }
                else
                {
                    errorCode="{Code:1 Input Is Out Of Range}";
                }
                int dataFact = 1,i;
                for(i =1;i<=dataInt;i++)
                {
                    dataFact=dataFact*i;
                    Console.WriteLine(dataFact);
                    //dataInt=dataInt-1;
                }
                int n1=0,n2=1,n3,j,dataf;
                dataf=n1+n2;
                //Console.WriteLine(0);
                //Console.Write(n1+" "+n2+" ");
                for(j=2;n2<dataFact;j++)
                {
                    n3=n1+n2;
                    //Console.WriteLine(n1+" "+n2+" ");
                    //Console.WriteLine(n3+" ");
                    dataf+=n3;
                    n1=n2;
                    n2=n3;
                }
                // Show the data on the console.  
                Console.WriteLine( "The Factorial Of Received Number : {0}", dataFact);  
                Console.WriteLine("The Sum Of Fibonacci Series less than "+dataFact+" Is: {0}", dataf);
                // Echo the data back to the client.  

                byte[] msg = Encoding.ASCII.GetBytes(errorCode.ToString()+","+dataFact.ToString()+","+dataf.ToString());
                Console.WriteLine(msg);
                handler.Send(msg);                  
                handler.Shutdown(SocketShutdown.Both);  
                handler.Close();  
                }
                else
                {
                    string errorCode="{Code:2 Only Numbers Are Accepted}";
                    Console.WriteLine(errorCode);
                    byte[] msg = Encoding.ASCII.GetBytes(errorCode);
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