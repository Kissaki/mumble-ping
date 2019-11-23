using KCode.MumblePing.Packets;
using System;
using System.Globalization;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;

namespace KCode.MumblePing
{
    static class Program
    {
        static void Main(string[] args)
        {
            var (isValid, hostname, port) = ParseArgs(args);
            if (!isValid) return;

            DoPing(hostname, port);
        }

        private static (bool isValid, string hostname, int port) ParseArgs(string[] args)
        {
            string hostname;
            int? port;
            switch (args.Length)
            {
                case 1:
                    var parts = args[0].Split(":");
                    hostname = parts[0];
                    port = parts.Length > 1 ? int.Parse(parts[1], CultureInfo.InvariantCulture) : (int?)null;
                    break;
                case 2:
                    hostname = args[0];
                    port = int.Parse(args[1], CultureInfo.InvariantCulture);
                    break;
                default:
                    Console.WriteLine($"Usage: {Assembly.GetExecutingAssembly().GetName().Name} ( <hostname> | <hostname>:<port> | <hostname> <port> )");
                    return (isValid: false, hostname: "", port: default);
            }
            return (isValid: true, hostname, port ?? 64738);
        }

        private static void DoPing(string hostname, int port)
        {
            using var client = new UdpClient();
            var tSend = SendPing(client, hostname, port);
            var tReceive = ReceivePing(client);

            Task.WhenAll(tSend, tReceive).Wait();
            HandlePingResponse(tReceive.Result.Buffer);
        }

        private static Task<int> SendPing(UdpClient client, string hostname, int port)
        {
            client.Connect(hostname, port);

            Console.WriteLine($"Sending ping to {hostname}:{port}…");
            var sendBuf = PingRequest.Create().ToBytes();
            return client.SendAsync(sendBuf, sendBuf.Length);
        }

        private static Task<UdpReceiveResult> ReceivePing(UdpClient client) => client.ReceiveAsync();

        private static void HandlePingResponse(byte[] buffer)
        {
            var res = PingResponse.Parse(buffer);
            Console.WriteLine($"Ping response received: {res.ToString()}");
        }
    }
}
