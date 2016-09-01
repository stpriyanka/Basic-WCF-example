using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebProgramming
{


	[ServiceContract]
	public interface IService
	{
		[OperationContract]
		string Ping(string name);
	}



	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	internal class ServiceImplementation : IService
	{
		#region IService Members

		public string Ping(string name)
		{
			Console.WriteLine("SERVER - Processing Ping('{0}')", name);
			return "Hello, " + name;
		}

		#endregion
	}

	public class Program
	{
		private static System.Threading.AutoResetEvent stopFlag = new System.Threading.AutoResetEvent(false);

		public static void Main()
		{
			ServiceHost svh = new ServiceHost(typeof(ServiceImplementation));
			svh.AddServiceEndpoint(
				typeof(IService),
				new NetTcpBinding(),
				"net.tcp://localhost:8000");
			svh.Open();

			Console.WriteLine("SERVER - Running...");
			stopFlag.WaitOne();

			Console.WriteLine("SERVER - Shutting down...");
			svh.Close();

			Console.WriteLine("SERVER - Shut down!");
		}

		public static void Stop()
		{
			stopFlag.Set();
		}
	


	}
}

