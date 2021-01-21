using System;
using System.Collections.Generic;
using MicroserviceCommon.Clients;
using MicroserviceCommon.CommonModel.Order;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press ENTER key to start test...");
            Console.ReadLine();

            var publishers = new List<AbstractTestPublisher>();
            publishers.Add(new RandomOrderPublisher(2000, TimeSpan.FromMilliseconds(100)));
            publishers.Add(new CartServicePublisher(2000, TimeSpan.FromMilliseconds(100)));
            publishers.Add(new RandomPaymentPublisher(2000, TimeSpan.FromMilliseconds(100)));

            foreach (var publisher in publishers)
            {
                publisher.Start();
            }

            foreach (var publisher in publishers)
            {
                publisher.WaitForEnd();
            }

            Console.WriteLine("All publishers have ended, press ENTER key to continue...");
            Console.ReadLine();
        }
    }
}
