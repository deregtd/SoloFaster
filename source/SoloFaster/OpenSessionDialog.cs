using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SoloFaster
{
    public partial class OpenSessionDialog : Form
    {
        private Dictionary<RecordedSession, string> sessionFilenameLookup;
        private List<RecordedSession> sessionList;
        public RecordedSession[] toOpen;

        public OpenSessionDialog(Dictionary<RecordedSession, string> sessionFilenameLookup, List<RecordedSession> sessionList)
        {
            InitializeComponent();

            this.sessionFilenameLookup = sessionFilenameLookup;
            this.sessionList = sessionList;
            this.toOpen = null;

            eventCombo.Items.Add("(All)");
            eventCombo.SelectedIndex = 0;

            foreach (RecordedSession session in sessionList)
            {
                if (!eventCombo.Items.Contains(session.GetFullEventName()))
                    eventCombo.Items.Add(session.GetFullEventName());
            }

            UpdateSessionList();
        }

        private void eventCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            driverCombo.Items.Clear();
            driverCombo.Items.Add("(All)");
            driverCombo.SelectedIndex = 0;

            foreach (RecordedSession session in sessionList)
            {
                if ((string)eventCombo.SelectedItem == "(All)" || session.GetFullEventName() == (string)eventCombo.SelectedItem)
                {
                    if (!driverCombo.Items.Contains(session.driverName))
                        driverCombo.Items.Add(session.driverName);
                }
            }

            UpdateSessionList();
        }

        private void driverCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSessionList();
        }

        private void UpdateSessionList()
        {
            //Update the displayed run list based on event/driver combo
            sessionListbox.Items.Clear();

            foreach (RecordedSession session in sessionList)
            {
                if ((string)eventCombo.SelectedItem == "(All)" || session.GetFullEventName() == (string)eventCombo.SelectedItem)
                {
                    if ((string)driverCombo.SelectedItem == "(All)" || session.driverName == (string)driverCombo.SelectedItem)
                        sessionListbox.Items.Add(session);
                }
            }
        }

        private void deleteSession_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this session? This can not be undone.", "Delete Recorded Session?", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                return;

            RecordedSession run = (RecordedSession)sessionListbox.SelectedItem;
            string fileName = sessionFilenameLookup[run];
            File.Delete(fileName);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
        }

        private void openSession_Click(object sender, EventArgs e)
        {
            List<RecordedSession> sessions = new List<RecordedSession>();
            foreach (RecordedSession session in sessionListbox.SelectedItems)
                sessions.Add(session);
            this.toOpen = sessions.ToArray();
        }

        private void sessionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            deleteSession.Enabled = openSession.Enabled = (sessionListbox.SelectedItems.Count > 0);
        }

        private void sessionListbox_DoubleClick(object sender, EventArgs e)
        {
            if (sessionListbox.SelectedItems.Count > 0)
            {
                openSession_Click(null, null);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
