﻿using System;
using System.IO;
using WebSocketSharp;

namespace TestListener
{
    /// <summary>
    /// This test listener writes incoming bytes as jpg file.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Directory.CreateDirectory("files");
            using (var ws = new WebSocket("ws://localhost:8888/main"))
            {
                ws.OnMessage += OnWebSocketMessage;
                ws.Connect();

                CommandLoop();
            }
        }

        private static void OnWebSocketMessage(object sender, MessageEventArgs e)
        {
            if (!e.IsBinary) return;

            using (FileStream file = new FileStream(
                string.Format("files/{0}.jpg", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")),
                FileMode.Create))
            {
                file.Write(e.RawData, 0, e.RawData.Length);
            }
        }

        static void CommandLoop()
        {
            string command;
            while (true)
            {
                command = Console.ReadLine();
                if (command.CompareTo("exit") == 0)
                {
                    break;
                }
            }
        }
    }
}
