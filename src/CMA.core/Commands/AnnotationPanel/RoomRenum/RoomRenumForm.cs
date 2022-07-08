namespace CMA.core
{
    using System;
    using System.Windows.Forms;

    using Autodesk.Revit.UI;

    public partial class RoomRenumForm : Form
    {
        #region private members

        private string roomPrefix;

        private int roomNumber = 1;

        private char roomIndex;

        private string roomSuffix;

        private string leadingZeros;

        #endregion

        #region constructor

        /// <summary>
        /// Default constructor.
        /// Initializes a new instance of the <see cref="RoomRenumForm"/> class.
        /// </summary>
        /// <param name="uiDocument">The UI document.</param>
        public RoomRenumForm()
        {
            InitializeComponent();
        }

        #endregion

        #region events

        private void txtPrefix_TextChanged(object sender, EventArgs e)
        {
            roomPrefix = txtPrefix.Text;
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {

            if (!Int32.TryParse(txtNumber.Text, out int rommInt) && txtNumber.Text != string.Empty)
            {
                Message.Display("Die Eingabe war keine Ganzzahl.", WindowType.Warning);
                txtNumber.Clear();
            }

            leadingZeros = "D" + txtNumber.Text.Length.ToString();

            if (txtNumber.Text == string.Empty)
                roomNumber = 1;
            else
                roomNumber = rommInt;
        }

        private void txtIndex_TextChanged(object sender, EventArgs e)
        {
            if (!char.TryParse(txtIndex.Text, out char index) && txtIndex.Text != string.Empty)
            {
                Message.Display("Die Eingabe erlaubt nur ein Zeichen.", WindowType.Warning);
                txtIndex.Clear();
            }
            roomIndex = index;
        }

        private void txtSuffix_TextChanged(object sender, EventArgs e)
        {
            roomSuffix = txtSuffix.Text;
        }



        #endregion

        #region public methods

        /// <summary>
        /// /// Gets the information provided from user.
        /// </summary>
        /// <returns></returns>
        public RoomRenumCommandData GetInformation()
        {

            // Information gatherd from window.
            RoomRenumCommandData information = new RoomRenumCommandData
            {
                RoomPrefix = roomPrefix,
                RoomNumber = roomNumber,
                RoomIndex = roomIndex,
                RoomSuffix = roomSuffix,
                LeadingZeros = leadingZeros
            };

            return information;
        }

        /// <summary>
        /// Update the room command data information and transmit it to the text boxes.
        /// </summary>
        /// <param name="userInfo">The user information.</param>
        public void UpdateRoomCommandData(RoomRenumCommandData userInfo)
        {
            // update the private members
            roomNumber = userInfo.RoomNumber;
            roomIndex = userInfo.RoomIndex;

            // update the content of the TextBoxes
            txtNumber.Text = roomNumber.ToString(leadingZeros);
            txtIndex.Text = roomIndex.ToString();
        }
        #endregion

    }
}
