using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace UltraStikTest
{
    public partial class Form1 : Form
    {
        private UltraStik m_ultraStik = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
			m_ultraStik = new UltraStik(this);
			m_ultraStik.OnUsbDeviceAttached += new UltraStik.UsbDeviceAttachedDelegate(OnUsbDeviceAttached);
			m_ultraStik.OnUsbDeviceRemoved += new UltraStik.UsbDeviceRemovedDelegate(OnUsbDeviceRemoved);
			m_ultraStik.Initialize();

			UpdateDeviceList();

			cboMaps.Items.AddRange(UltraStik.FriendlyMapNameArray);
            cboMaps.SelectedIndex = 0;

            cboId.Items.AddRange(new string[] { "1", "2", "3", "4", "5" });
            cboId.SelectedIndex = 0;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
			m_ultraStik.Shutdown();
        }

        private void butSetMap_Click(object sender, EventArgs e)
        {
            if (lvwDevices.SelectedIndices.Count == 0)
                return;

			int selectedJoy = lvwDevices.SelectedIndices[0];

			m_ultraStik.SetRestrictor(selectedJoy, chkRestrictor.Checked);
			m_ultraStik.SetFlash(selectedJoy, rdoFlash.Checked);

			m_ultraStik.LoadMap(selectedJoy, UltraStik.MapNameArray[cboMaps.SelectedIndex]);
        }

        private void butBrowseMapFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            fd.Filter = "UltraStik Map (*.um,*.ugc)|*.um;*.ugc|All files (*.*)|*.*";
            fd.InitialDirectory = Path.Combine(Application.StartupPath, "UltraStikMaps");

            if (fd.ShowDialog() == DialogResult.OK)
                txtMapFile.Text = fd.FileName;
        }

        private void butSetMapFile_Click(object sender, EventArgs e)
        {
            if (lvwDevices.SelectedIndices.Count == 0)
                return;

			int selectedJoy = lvwDevices.SelectedIndices[0];

			m_ultraStik.SetRestrictor(selectedJoy, chkRestrictor.Checked);
			m_ultraStik.SetFlash(selectedJoy, rdoFlash.Checked);

            if(Path.GetExtension(txtMapFile.Text) == ".ugc")
				m_ultraStik.LoadConfigFile(Path.Combine(Application.StartupPath, "UltraStikMaps"), txtMapFile.Text);
            else
				m_ultraStik.LoadMapFile(selectedJoy, txtMapFile.Text);
        }

        private void buSetId_Click(object sender, EventArgs e)
        {
            if (lvwDevices.SelectedIndices.Count == 0)
                return;

            int selectedJoy = lvwDevices.SelectedIndices[0];

            switch (cboId.Text)
            {
                case "1":
					m_ultraStik.SetUltraStikId(selectedJoy, UltraStik.UltraStikId.Id1);
                    break;
                case "2":
					m_ultraStik.SetUltraStikId(selectedJoy, UltraStik.UltraStikId.Id2);
                    break;
                case "3":
					m_ultraStik.SetUltraStikId(selectedJoy, UltraStik.UltraStikId.Id3);
                    break;
                case "4":
					m_ultraStik.SetUltraStikId(selectedJoy, UltraStik.UltraStikId.Id4);
                    break;
            }
        }

		private void OnUsbDeviceAttached(int id)
		{
			UpdateDeviceList();
		}

		private void OnUsbDeviceRemoved(int id)
		{
			UpdateDeviceList();
		}

		private void UpdateDeviceList()
		{
			lvwDevices.Items.Clear();

			for (int i = 0; i < m_ultraStik.NumDevices; i++)
				lvwDevices.Items.Add(new ListViewItem(new string[] { m_ultraStik.GetVendorId(i).ToString("X"), m_ultraStik.GetProductId(i).ToString("X"), m_ultraStik.GetVersionNumber(i).ToString("X"), m_ultraStik.GetVendorName(i), m_ultraStik.GetProductName(i), m_ultraStik.GetSerialNumber(i), m_ultraStik.GetDevicePath(i) }));

			for (int i = 0; i < lvwDevices.Columns.Count; i++)
				lvwDevices.Columns[i].Width = -2;

			if (lvwDevices.Items.Count > 0)
			{
				lvwDevices.SelectedIndices.Clear();
				lvwDevices.SelectedIndices.Add(0);
			}
		}
    }
}