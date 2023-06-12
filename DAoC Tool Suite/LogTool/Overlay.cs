namespace DAoCToolSuite.LogTool
{
    public partial class Overlay : Form
    {
        public Overlay()
        {
            InitializeComponent();
            MouseDown += new MouseEventHandler(Overlay_MouseDown);
            MoveLabel.MouseDown += new MouseEventHandler(MoveLabel_MouseDown);
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Overlay_MouseDown(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _ = ReleaseCapture();
                _ = SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void MoveLabel_MouseDown(object? sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _ = ReleaseCapture();
                _ = SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void MoveLabel_Click(object sender, EventArgs e)
        {

        }

        private void Overlay_Load(object sender, EventArgs e)
        {

        }
    }
}
