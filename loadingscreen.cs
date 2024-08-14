using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace automugshot
{
    public partial class loadingscreen : Form
    {
        string mesh;
        public loadingscreen(string ms)
        {
            InitializeComponent();
            mesh = ms;
        }

        private void loadingscreen_Load(object sender, EventArgs e)
        {
            messageshown.Text = mesh;
            this.ControlBox = false;
        }

        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static loadingscreen splashForm;

        static public void ShowSplashScreen(string ms)
        {
            // Make sure it is only launched once.    
            if (splashForm != null) return;
            splashForm = new loadingscreen(ms);
            Thread thread = new Thread(new ThreadStart(loadingscreen.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static private void ShowForm()
        {
            if (splashForm != null) Application.Run(splashForm);
        }

        static public void CloseForm()
        {
            splashForm?.Invoke(new CloseDelegate(loadingscreen.CloseFormInternal));
        }

        static private void CloseFormInternal()
        {
            if (splashForm != null)
            {
                splashForm.Close();
                splashForm = null;
            };
        }
    }
}
