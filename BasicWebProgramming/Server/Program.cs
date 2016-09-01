using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BasicWebProgramming;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("WCF Simple Demo");

			// start server
			System.Threading.Thread thServer = new System.Threading.Thread(BasicWebProgramming.Program.Main);
			thServer.IsBackground = true;
			thServer.Start();
			System.Threading.Thread.Sleep(1000);  // wait for server to start up

			// run client
			ChannelFactory<IService> scf;
			scf = new ChannelFactory<IService>(
						new NetTcpBinding(),
						"net.tcp://localhost:8000");

			IService s;
			s = scf.CreateChannel();

			while (true)
			{
				Console.Write("CLIENT - Name: ");
				string name = Console.ReadLine();
				if (name == "")
					break;

				string response = s.Ping(name);
				Console.WriteLine("CLIENT - Response from service: " + response);
			}
			(s as ICommunicationObject).Close();
			// shutdown server
			BasicWebProgramming.Program.Stop();
			thServer.Join();
		}

	}
}
