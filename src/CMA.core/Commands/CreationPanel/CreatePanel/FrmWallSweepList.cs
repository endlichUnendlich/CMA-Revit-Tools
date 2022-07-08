namespace CMA.core
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Linq;

    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;


    /// <summary>
    /// The form to get the input informations
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FrmWallSweepList : System.Windows.Forms.Form
    {
        // need tu populate Combobox
        private UIDocument uiDoc = null;

        #region constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FrmWallSweepList"/> class.
        /// </summary>
        /// <param name="uiDocument">The UI document.</param>
        public FrmWallSweepList(UIDocument uiDocument)
        {
            uiDoc = uiDocument;
            InitializeComponent();
        }
        #endregion

        #region events
        /// <summary>
        /// Handles the Load event of the FrmWallSweepList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FrmWallSweepList_Load(object sender, EventArgs e)
        {
            PopulateProfilTypeList();

            for (int i = 0; i < CreatePanelCommand.CurCommandData.ProfileDataColl.Count; i++)
            {
                ListViewItem row = new ListViewItem(CreatePanelCommand.CurCommandData.ProfileDataColl[i].Id.ToString());
                row.SubItems.Add(CreatePanelCommand.CurCommandData.ProfileDataColl[i].ProfileTypeName);
                row.SubItems.Add(CreatePanelCommand.CurCommandData.ProfileDataColl[i].ProfileWidth.ToString());

                lvwProfileTypes.Items.Add(row);
            }

            txtRandomSeed.Text = CreatePanelCommand.CurCommandData.RandomSeed.ToString();
        }
        private void txtRandomSeed_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtRandomSeed.Text, out int id) && !string.IsNullOrEmpty(txtRandomSeed.Text))
            {
                Message.Display("Das Eingabefeld erlaubt nur Ganzzahlen.", WindowType.Information);
                txtRandomSeed.Text = "0";
                return;
            }

            CreatePanelCommand.CurCommandData.RandomSeed = id;

        }
        //private void lvwProfileTypes_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        //{
        //    lvwProfileTypes.LostFocus += new EventHandler(lvwProfileTypes_LostFocus);

        //    lblIdValue.Text = lvwProfileTypes.FocusedItem.SubItems[0].Text;
        //    cmbProfileType.Text = lvwProfileTypes.FocusedItem.SubItems[1].Text;
        //    txtProfileWidth.Text = lvwProfileTypes.FocusedItem.SubItems[2].Text;
        //}

        private void lvwProfileTypes_LostFocus(object sender, EventArgs e)
        {
            if (lvwProfileTypes.FocusedItem == null)
                lblIdValue.Text = "< ID >";
        }

        private void lvwProfileTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvwProfileTypes.LostFocus += new EventHandler(lvwProfileTypes_LostFocus);

            lblIdValue.Text = lvwProfileTypes.FocusedItem.SubItems[0].Text;
            cmbProfileType.Text = lvwProfileTypes.FocusedItem.SubItems[1].Text;
            txtProfileWidth.Text = lvwProfileTypes.FocusedItem.SubItems[2].Text;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProfileWidth.Text))
            {
                Message.Display("Trage einen Wert in das Feld \"Profil Breite\" ein", WindowType.Information);
                return;
            }
            if (!double.TryParse(txtProfileWidth.Text, out double width) && !string.IsNullOrEmpty(txtProfileWidth.Text))
            {
                Message.Display("Das Eingabefeld erlaubt nur Zahlen.", WindowType.Information);
                txtProfileWidth.Clear();
                return;
            }

            ProfileData pData = new ProfileData();
            pData.Id = CreatePanelCommand.CurCommandData.AutoId;
            pData.ProfileTypeId = ((KeyValuePair<string, ElementId>)cmbProfileType.SelectedItem).Value;
            pData.ProfileTypeName = ((KeyValuePair<string, ElementId>)cmbProfileType.SelectedItem).Key;
            pData.ProfileWidth = width;
            CreatePanelCommand.CurCommandData.ProfileDataColl.Add(pData);

            form_Refresh(sender, e);

            ++CreatePanelCommand.CurCommandData.AutoId;

            //txtProfileWidth.Clear();
            cmbProfileType.Text = pData.ProfileTypeName;

            //ListViewItem item = new ListViewItem(autoId.ToString());
            //item.SubItems.Add(((KeyValuePair<string, ElementId>)cmbProfileType.SelectedItem).Key);
            //item.SubItems.Add(txtProfileWidth.Text);

            //listViewProfileTypes.Items.Add(item);

            //txtProfileWidth.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(lblIdValue.Text, out int id))
            {
                Message.Display("Wähle das Profil in der Liste aus, das aktualisiert werden soll.", WindowType.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtProfileWidth.Text))
            {
                Message.Display("Trage einen Wert in das Feld \"Profil Breite\" ein", WindowType.Information);
                return;
            }

            ProfileData pData = new ProfileData();
            for (int i = 0; i < CreatePanelCommand.CurCommandData.ProfileDataColl.Count; i++)
            {
                if (CreatePanelCommand.CurCommandData.ProfileDataColl[i].Id == id)
                {
                    pData = CreatePanelCommand.CurCommandData.ProfileDataColl[i];
                    pData.ProfileTypeName = ((KeyValuePair<string, ElementId>)cmbProfileType.SelectedItem).Key;
                    pData.ProfileWidth = double.Parse(txtProfileWidth.Text);
                }
            }

            form_Refresh(sender, e);

            cmbProfileType.Text = pData.ProfileTypeName;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(lblIdValue.Text, out int id))
            {
                Message.Display("Wähle ein Profil in der Liste aus, das entfernt werden soll.", WindowType.Information);
                return;
            }

            ProfileData pData = new ProfileData();
            //int curIndex = lvwProfileTypes.SelectedItems[0].Index;
            for (int i = 0; i < CreatePanelCommand.CurCommandData.ProfileDataColl.Count; i++)
            {
                if (CreatePanelCommand.CurCommandData.ProfileDataColl[i].Id == id)
                {
                    pData = CreatePanelCommand.CurCommandData.ProfileDataColl[i];
                    CreatePanelCommand.CurCommandData.ProfileDataColl.Remove(pData);
                    lblIdValue.Text = "";
                    txtProfileWidth.Clear();
                }
            }

            form_Refresh(sender, e);
            cmbProfileType.Text = pData.ProfileTypeName;


            //lvwProfileTypes.Focus();    
            //if ((curIndex + 1) < lvwProfileTypes.Items.Count)
            //{
            //    lvwProfileTypes.Items[curIndex].Selected = true;
            //}


            //if (listViewProfileTypes.Items.Count > 0)
            //    listViewProfileTypes.Items.Remove(listViewProfileTypes.SelectedItems[0]);
        }
        private void form_Refresh(object sender, EventArgs e)
        {
            lvwProfileTypes.Items.Clear();
            FrmWallSweepList_Load(sender, e);
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lvwProfileTypes.Items.Count == 0)
            {
                Message.Display("Es befindet sich kein Eintrag in der Profilliste.\nFüge ein Profil mit zugehöriger Breite der Liste hinzu, um den Befehl auszuführen.", WindowType.Information);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        #endregion

        #region private methods
        /// <summary>
        /// Gets the random seed.
        /// </summary>
        /// <returns></returns>
        public int getRandomSeed()
        {
            int rSeed;
            int.TryParse(txtRandomSeed.Text, out rSeed);
            return rSeed;
        }

        /// <summary>
        /// Populates the profil type list.
        /// </summary>
        private void PopulateProfilTypeList()
        {
            Document doc = uiDoc.Document;
            var allProfileTypesColl = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_ProfileFamilies).WhereElementIsElementType();
            //var profileTypes = allProfileTypesColl.
            //    Where(x => ((FamilySymbol)x).Family.get_Parameter(BuiltInParameter.FAM_PROFILE_USAGE).AsInteger() ==
            //    ((int)(ProfileFamilyUsage.Any | ProfileFamilyUsage.Reveal | ProfileFamilyUsage.WallSweep))).ToList();

            var list = new List<KeyValuePair<string, ElementId>>();
            foreach (var item in allProfileTypesColl)
                list.Add(new KeyValuePair<string, ElementId>(((FamilySymbol)item).FamilyName + " - " + item.Name, item.Id));

            cmbProfileType.DataSource = null;
            cmbProfileType.DataSource = new BindingSource(list, null);
            cmbProfileType.DisplayMember = nameof(KeyValuePair<string, ElementId>.Key);
            cmbProfileType.ValueMember = nameof(KeyValuePair<string, ElementId>.Value);
        }



        #endregion

    }
}
