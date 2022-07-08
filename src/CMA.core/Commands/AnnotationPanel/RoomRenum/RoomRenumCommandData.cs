namespace CMA.core
{

    /// <summary>
    /// Information an data model for command <see cref="RoomRenumCommand"/> to execute.
    /// </summary>
    public class RoomRenumCommandData
    {

        #region public properties

        /// <summary>
        /// Gets or sets the room prefix.
        /// </summary>
        /// <value>
        /// The room prefix.
        /// </value>
        public string RoomPrefix { get; set; }

        /// <summary>
        /// Gets or sets the room number.
        /// </summary>
        /// <value>
        /// The room number.
        /// </value>
        public int RoomNumber { get; set; }

        /// <summary>
        /// Gets or sets the index of the room.
        /// </summary>
        /// <value>
        /// The index of the room.
        /// </value>
        public char RoomIndex { get; set; }

        /// <summary>
        /// Gets or sets the room suffix.
        /// </summary>
        /// <value>
        /// The room suffix.
        /// </value>
        public string RoomSuffix { get; set; }

        /// <summary>
        /// Gets or sets the leading zeros.
        /// </summary>
        /// <value>
        /// The leading zeros.
        /// </value>
        public string LeadingZeros { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomRenumCommandData"/> class.
        /// </summary>
        public RoomRenumCommandData()
        {

        }

        #endregion

    }
}
