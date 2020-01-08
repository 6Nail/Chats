using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            using(var timer = new Timer(UpdateMessages, socket, 50, 10))
            {
                socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
                socket.Listen(5);

                Console.WriteLine($"Приложение слушает порт");

                while (true)
                {
                    var incoming = await socket.AcceptAsync();
                    Console.WriteLine($"Получено входящее сообщение.");

                    while (incoming.Available > 0)
                    {
                        var buffer = new byte[incoming.Available];
                        await incoming.ReceiveAsync(buffer, SocketFlags.Partial);

                        var result = Encoding.UTF8.GetString(buffer).Split(":").ToList();

                        //using(var context = new ChatContext())
                        //{
                        //    context.Add(new Message { Who = result.First(), Text = result.Last() });
                        //    await context.SaveChangesAsync();
                        //}
                    }
                    incoming.Close();
                    Console.WriteLine("Входящее соединение закрыто");
                }
            }
        }

        public async static void UpdateMessages(object socket)
        {
            var socketUP = socket as Socket;
            using (var context = new ChatContext())
            {
                var messages = await context.Messages.ToListAsync();
                await socketUP.SendAsync(Encoding.UTF8.GetBytes($"{messages.First().Who} : {messages.First().Text}"), SocketFlags.Partial);
            }
        }
    }
}
