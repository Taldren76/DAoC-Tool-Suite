
namespace DAoCToolSuite.ChimpTool
{
    public partial class ShutDown : Form
    {
        public ShutDown()
        {
            InitializeComponent();
        }

        public void SetLocation()
        {
            if (Owner != null && StartPosition == FormStartPosition.Manual)
            {
                int offset = Owner.OwnedForms.Length * 38;  // approx. 10mm
                Point p = new(Owner.Left + (Owner.Width / 2) - (Width / 2) + offset, Owner.Top + (Owner.Height / 2) - (Height / 2) + offset);
                Location = p;
            }
        }


    }
}
