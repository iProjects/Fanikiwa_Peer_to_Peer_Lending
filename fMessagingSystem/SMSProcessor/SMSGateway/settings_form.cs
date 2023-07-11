using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using fCommon.Utility;
using System.IO;

namespace SMSGateway
{
    public partial class settings_form : Form
    {
        public settings_form()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            string image_file = "signal-yellow.gif";
            set_status_image(image_file);

            pictureBoxStatus.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void set_status_image(string image_name)
        {
            string app_dir = Utils.get_application_path();
            string resources_dir = Path.Combine(app_dir, "resources");
            string image_file_path = Path.Combine(resources_dir, image_name);

            if (File.Exists(image_file_path))
            {
                pictureBoxStatus.ImageLocation = image_file_path;
            }
        }
        private void btnConnected_Click(object sender, EventArgs e)
        {
            bool is_connected = false;

            if (is_connected)
            {
                string image_file = "signal-green.gif";
                set_status_image(image_file);
            }
            else
            {
                string image_file = "signal-red.gif";
                set_status_image(image_file);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }






    }
}
