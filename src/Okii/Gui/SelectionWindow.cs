using Terminal.Gui;

namespace Okii.Gui
{
    public class SelectionWindow : Window
    {
        private readonly View parent;

        public SelectionWindow(View parent) : base("Select a Room")
        {
            this.parent = parent;

            Width = Dim.Fill();
            Height = Dim.Fill();

            InitializeControls();
        }

        public void Close()
        {
            parent?.Remove(this);
        }

        private void InitializeControls()
        {
            Add(new Label(0, 0, "Welcome to Okii -- A simple chat app."));

            ListView roomList = new(new string[] { "Bruh", "Bruh 2" })
            {
                Width = Dim.Fill(),
                Height = Dim.Fill() - 6,
            };

            Add(roomList);

            Button refreshButton = new("Refresh List")
            {
                X = 0,
                Y = Pos.Top(roomList),
            };

            Button directConnectButton = new("Direct Connect")
            {
                X = Pos.Right(refreshButton) + 1,
                Y = Pos.Top(roomList),
            };

            //Add(refreshButton);
            //Add(directConnectButton);
        }
    }
}
