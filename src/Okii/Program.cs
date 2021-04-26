using Terminal.Gui;
using Okii.Gui;
using Okii.Protocol;

Application.Init();
var root = Application.Top;

OkiiClient client = new();
Application.MainLoop.Invoke(client.Connect);

RoomWindow roomWindow = new(ref client);
root.Add(roomWindow);

Application.Run();
client.Socket.Close();