using Okii.Protocol;
using System;
using System.Collections.Generic;
using WebSocketSharp;
using Newtonsoft.Json;
using Terminal.Gui;
using Okii.Gui;

namespace Okii.Protocol
{
    public class OkiiClient
    {
        public WebSocket Socket { get; }
        public static string[] Users { get; private set; }

        public OkiiClient()
        {
            Socket = new("wss://pro-chat-server.herokuapp.com/");
            Socket.OnOpen += OnConnectionOpened;
            Socket.OnMessage += OnMessage;
            Socket.OnClose += OnConnectionClosed;
        }

        /// <summary>
        /// Attempts a connection to the server.
        /// </summary>
        public void Connect()
        {
            // btw future me
            // Socket is public for some reason. might want to fix it
            Socket.Connect();
        }

        public void SendMessagePacket(string message)
        {
            Socket.Send($"SEND\n{message}");
        }

        private void OnConnectionOpened(object sender, EventArgs e)
        {
            Socket.Send("CONNECT\n");
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsText)
            {
                string[] split = e.Data.Split('\n');

                switch (split[0])
                {
                    case "OK":
                        break;
                    case "MESSAGE":
                        Console.WriteLine(split[1]);
                        RoomWindow.OnMessageReceived(JsonConvert.DeserializeObject<Message>(split[1]));
                        break;
                    case "LIST":
                        // update: this has been fixed.
                        dynamic list = JsonConvert.DeserializeObject(split[1]);
                        Users = list.ToObject<string[]>();
                        RoomWindow.UpdateUserList();
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnConnectionClosed(object sender, CloseEventArgs e)
        {
            switch (e.Code)
            {
                // I feel like there is a better way of handling this. No clue ima tired
                case 1006:
                    if (MessageBox.ErrorQuery("Server Error", "The server is not responding.\nExit and try again later.", "Exit") == 0)
                    {
                        Application.RequestStop();
                    }
                    break;
            }
        }
    }
}
