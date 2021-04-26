using Okii.Protocol;
using System.Collections.Generic;
using Terminal.Gui;

namespace Okii.Gui
{
    public class RoomWindow : Window
    {
        private readonly OkiiClient client;
        private static readonly List<string> messages = new();

        // This is perhaps one of the most poorly designed programs I have ever made.
        // Its 12:36 AM. I want to cry
        private static ListView chatView;
        private static ListView onlineUsers;

        public RoomWindow(ref OkiiClient client) : base("Untitled Room")
        {
            this.client = client;
            InitializeControls();
        }

        public static void UpdateUserList()
        {
            onlineUsers.SetSource(OkiiClient.Users);
        }

        public static void OnMessageReceived(Message message)
        {
            messages.Add($"<{message.Author}> {message.Content}");
            // why?
            chatView.SetSource(messages); // my headphones ran out of battery. I want my music ;-;
        }

        private void InitializeControls()
        {
            chatView = new()
            {
                X = 0,
                Y = 0,
                Width = Dim.Percent(75),
                Height = Dim.Percent(90),
            };

            FrameView onlineUsersFrameView = new("Online Users")
            {
                X = Pos.Right(chatView),
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };

            onlineUsers = new()
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };

            FrameView actionBar = new(null)
            {
                X = 0,
                Y = Pos.Bottom(chatView),
                Width = chatView.Width,
                Height = Dim.Fill(),
            };

            TextField messageBox = new()
            {
                X = 0,
                Y = 0,
                Width = Dim.Percent(85),
                Height = Dim.Fill(),
            };

            Button sendButton = new("Send", true)
            {
                X = Pos.Right(messageBox),
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };

            sendButton.Clicked += () =>
            {
                // Added this on the server side but just in case??
                // Also why does this library use a different object for string???????????
                // so annoying
                if (string.IsNullOrWhiteSpace(messageBox.Text.ToString()))
                    return;

                // a lot of this is repetitive but im so tired and dont care
                client.SendMessagePacket(messageBox.Text.ToString());
                messageBox.Text = "";
            };

            onlineUsersFrameView.Add(onlineUsers);
            actionBar.Add(messageBox, sendButton);
            Add(chatView, actionBar, onlineUsersFrameView);
        }
    }
}
