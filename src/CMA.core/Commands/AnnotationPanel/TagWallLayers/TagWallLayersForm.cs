namespace CMA.core
{
    using System;
    using System.Windows.Forms;
    using System.Collections.Generic;

    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;

    /// <summary>
    /// Tag wall layer aquisition form.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class TagWallLayersForm : System.Windows.Forms.Form
    {
        #region private members

        /// <summary>
        /// The private reference to the <see cref="UIDocument"/>
        /// </summary>
        private UIDocument uiDoc = null;

        /// <summary>
        /// The private text type identifier.
        /// </summary>
        private ElementId textTypeID = null;

        /// <summary>
        /// The unit type to convert to.
        /// </summary>
        private LengthUnitType unitType = LengthUnitType.milimeter;

        /// <summary>
        /// The decimal places precision.
        /// </summary>
        private int decimalPlaces = 1;

        #endregion

        #region constructor

        /// <summary>
        /// Default constructor.
        /// Initializes a new instance of the <see cref="TagWallLayersForm" /> class.
        /// </summary>
        /// <param name="uiDocument">The UI document.</param>
        public TagWallLayersForm(UIDocument uiDocument)
        {
            InitializeComponent();
            uiDoc = uiDocument;
        }

        #endregion

        #region events

        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnOk_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Handles the Load event of the TagWallLayersForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TagWallLayersForm_Load(object sender, System.EventArgs e)
        {
            // Populate items with data on form load event.
            PopulateTextNoteTypeList();
            PopulateUnitTypeList();
            PopulateDecimalPlacesList();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbTextNoteElementType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmbTextNoteElementType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            textTypeID = ((KeyValuePair<string, ElementId>)cmbTextNoteElementType.SelectedItem).Value;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbUnitType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmbUnitType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            unitType = (LengthUnitType)cmbUnitType.SelectedValue;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbDecimalPlaces control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmbDecimalPlaces_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            decimalPlaces = (int)cmbDecimalPlaces.SelectedValue;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Gets the information provided from user.
        /// </summary>
        /// <returns></returns>
        public TagWallLayersCommandData GetInformation()
        {
            // Information gatherd from window.
            TagWallLayersCommandData information = new TagWallLayersCommandData
            {
                Function = chkFunction.Checked,
                Name = chkName.Checked,
                Thickness = chkThickness.Checked,
                TextTypeId = textTypeID,
                UnitType = unitType,
                DecimalPlaces = decimalPlaces
            };

            return information;
        }

        #endregion

        #region private methods

        /// <summary>
        /// Populates the text note type list.
        /// </summary>
        private void PopulateTextNoteTypeList()
        {
            Document doc = uiDoc.Document;
            FilteredElementCollector allTaxtElementTypes = new FilteredElementCollector(doc).OfClass(typeof(TextElementType));

            List<KeyValuePair<string, ElementId>> list = new List<KeyValuePair<string, ElementId>>();

            foreach (Element item in allTaxtElementTypes)
                list.Add(new KeyValuePair<string, ElementId>(item.Name, item.Id));

            cmbTextNoteElementType.DataSource = null;
            cmbTextNoteElementType.DataSource = new BindingSource(list, null);
            cmbTextNoteElementType.DisplayMember = "Key";
            cmbTextNoteElementType.ValueMember = "Value";
        }

        /// <summary>
        /// Populates the unit type list.
        /// </summary>
        private void PopulateUnitTypeList()
        {
            // Populate list from enum types.
            cmbUnitType.DataSource = Enum.GetValues(typeof(LengthUnitType));
        }

        /// <summary>
        /// Populates the decimal places list.
        /// </summary>
        private void PopulateDecimalPlacesList()
        {
            // List of prcisions.
            List<int> values = new List<int>() { 0, 1, 2, 3 };

            // Define list as binding source for ui control.
            var source = new BindingSource
            {
                DataSource = values
            };

            // Bind data to ui control to populate list.
            cmbDecimalPlaces.DataSource = source;
            cmbDecimalPlaces.SelectedItem = values[2];
        }


        #endregion

    }
}
