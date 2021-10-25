using SharedLib;
using System.Net.Sockets;
using System.Text.Json;

namespace Server
{
	internal class Functions
	{
		public static void ProcessClientRequests(object argument)
		{
			TcpClient client = (TcpClient)argument;
			try
			{
				StreamReader reader = new StreamReader(client.GetStream());
				StreamWriter writer = new StreamWriter(client.GetStream());
				string s = String.Empty;
				while (!(s = reader.ReadLine()).Equals("Exit") || (s == null))
				{
					Console.WriteLine("From client -> " + s);
                    //writer.WriteLine(s);	
					writer.WriteLine(JsonSerializer.Serialize<Response>(ProcessMessage(s)));
					writer.Flush();
				}
				reader.Close();
				writer.Close();
				client.Close();
				Console.WriteLine("Closing client connection!");
			}
			catch (IOException)
			{
				Console.WriteLine("Problem with client communication. Exiting thread.");
			}
			finally
			{
				if (client != null)
				{
					client.Close();
				}
			}
		}
		private static Response ProcessMessage(string message)
		{
            try
            {
				var messageAsJson = JsonSerializer.Deserialize<dynamic>(message);
				return ValidateMessage(messageAsJson);
            }
            catch (Exception)
            {
				Response response = new Response();
				response.Status = "6 error cannot convert request to json";
				return response;
            }
		}

		private static Response ValidateMessage(Request jsonMsg)
		{
			string[] methodOptions = {"create", "read", "update", "delete", "echo"};
			string[] methodAcceptsBody = { "create", "update", "echo" };

			Response response = new Response();
			response.Status = "1 success (temp)";

			if (jsonMsg.Method is null || jsonMsg.Path is null || jsonMsg.Date is 0)
			{
				response.Status = "4 missing resource,";
				if (jsonMsg.Method is null) response.Status += " missing method,";
				if (jsonMsg.Path is null) response.Status += " missing date,";
				if (jsonMsg.Date is 0) response.Status += " missing date";
				return response;
			}

            if (jsonMsg.Body is not null && jsonMsg.Body is "{}")
            {
				response.Status = "4 illegal method";
				return response;

			}

			if (!methodOptions.Contains(jsonMsg.Method) ) 
			{
				response.Status = "4 illegal method";
				return response;
			}

            if (methodAcceptsBody.Contains(jsonMsg.Method) && jsonMsg.Body is null)
            {
				response.Status = "4 missing body";
				return response;
            }

			return response;
		}

		private static void SendHTTPRequest() { } // TODO
	}
}
