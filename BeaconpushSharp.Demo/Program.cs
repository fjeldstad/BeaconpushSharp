using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeaconpushSharp.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // On-site version
            //var beacon = new Beacon("my-web-site", "http://localhost:6053/1.0.0");

            // Cloud version
            var beacon = new Beacon("my-api-key", "my-secret-key-or-null-if-auth-disabled", "http://api.beaconpush.com/1.0.0");
            
            Console.WriteLine("Number of online users: {0}", beacon.OnlineUserCount());

            Console.Write("Sending message to channel...");
            beacon.Channel("my-channel").Send(new { message = "Test message sent to channel" });
            Console.WriteLine("done.");

            foreach (var user in beacon.Channel("my-channel").Users())
            {
                Console.Write("Sending message to user '{0}'...", user.Username);
                user.Send(new { message = "Test message sent directly to user." });
                Console.WriteLine("done.");
            }


            Console.ReadKey();
        }
    }
}
