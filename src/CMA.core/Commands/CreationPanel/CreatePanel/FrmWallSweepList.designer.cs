
namespace CMA.core
{
    partial class FrmWallSweepList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWallSweepList));
            this.lvwProfileTypes = new System.Windows.Forms.ListView();
            this.clmID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmWidth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtProfileWidth = new System.Windows.Forms.TextBox();
            this.cmbProfileType = new System.Windows.Forms.ComboBox();
            this.lblIdValue = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.txtRandomSeed = new System.Windows.Forms.TextBox();
            this.lblRandomSeed = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblHelp = new System.Windows.Forms.Label();
            this.gbSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwProfileTypes
            // 
            this.lvwProfileTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwProfileTypes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmID,
            this.clmName,
            this.clmWidth});
            this.lvwProfileTypes.FullRowSelect = true;
            this.lvwProfileTypes.GridLines = true;
            this.lvwProfileTypes.HideSelection = false;
            this.lvwProfileTypes.Location = new System.Drawing.Point(6, 19);
            this.lvwProfileTypes.Name = "lvwProfileTypes";
            this.lvwProfileTypes.Size = new System.Drawing.Size(450, 140);
            this.lvwProfileTypes.TabIndex = 8;
            this.lvwProfileTypes.UseCompatibleStateImageBehavior = false;
            this.lvwProfileTypes.View = System.Windows.Forms.View.Details;
            this.lvwProfileTypes.SelectedIndexChanged += new System.EventHandler(this.lvwProfileTypes_SelectedIndexChanged);
            // 
            // clmID
            // 
            this.clmID.Text = "ID";
            this.clmID.Width = 25;
            // 
            // clmName
            // 
            this.clmName.Text = "Profil Typ (Name)";
            this.clmName.Width = 280;
            // 
            // clmWidth
            // 
            this.clmWidth.Text = "Profil Breite (Meter)";
            this.clmWidth.Width = 120;
            // 
            // txtProfileWidth
            // 
            this.txtProfileWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProfileWidth.Location = new System.Drawing.Point(540, 95);
            this.txtProfileWidth.Name = "txtProfileWidth";
            this.txtProfileWidth.Size = new System.Drawing.Size(257, 20);
            this.txtProfileWidth.TabIndex = 2;
            this.txtProfileWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmbProfileType
            // 
            this.cmbProfileType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbProfileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProfileType.DropDownWidth = 400;
            this.cmbProfileType.FormattingEnabled = true;
            this.cmbProfileType.IntegralHeight = false;
            this.cmbProfileType.Location = new System.Drawing.Point(540, 59);
            this.cmbProfileType.Name = "cmbProfileType";
            this.cmbProfileType.Size = new System.Drawing.Size(257, 21);
            this.cmbProfileType.TabIndex = 1;
            // 
            // lblIdValue
            // 
            this.lblIdValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIdValue.AutoSize = true;
            this.lblIdValue.Location = new System.Drawing.Point(557, 26);
            this.lblIdValue.Name = "lblIdValue";
            this.lblIdValue.Size = new System.Drawing.Size(36, 13);
            this.lblIdValue.TabIndex = 11;
            this.lblIdValue.Text = "< ID >";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(540, 130);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Hinzufügen";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(621, 130);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(95, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Aktualisieren";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(722, 130);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Entfernen";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(464, 62);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(54, 13);
            this.lblName.TabIndex = 13;
            this.lblName.Text = "Profil Typ:";
            // 
            // lblWidth
            // 
            this.lblWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(464, 98);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(63, 13);
            this.lblWidth.TabIndex = 14;
            this.lblWidth.Text = "Profil Breite:";
            // 
            // lblId
            // 
            this.lblId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(464, 26);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(21, 13);
            this.lblId.TabIndex = 12;
            this.lblId.Text = "ID:";
            // 
            // gbSettings
            // 
            this.gbSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSettings.Controls.Add(this.lblId);
            this.gbSettings.Controls.Add(this.txtProfileWidth);
            this.gbSettings.Controls.Add(this.lblWidth);
            this.gbSettings.Controls.Add(this.cmbProfileType);
            this.gbSettings.Controls.Add(this.lblName);
            this.gbSettings.Controls.Add(this.lvwProfileTypes);
            this.gbSettings.Controls.Add(this.lblIdValue);
            this.gbSettings.Controls.Add(this.btnRemove);
            this.gbSettings.Controls.Add(this.btnAdd);
            this.gbSettings.Controls.Add(this.btnUpdate);
            this.gbSettings.Location = new System.Drawing.Point(12, 9);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(810, 175);
            this.gbSettings.TabIndex = 9;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Profil Typ Einstellungen";
            // 
            // txtRandomSeed
            // 
            this.txtRandomSeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRandomSeed.Location = new System.Drawing.Point(552, 196);
            this.txtRandomSeed.Name = "txtRandomSeed";
            this.txtRandomSeed.Size = new System.Drawing.Size(257, 20);
            this.txtRandomSeed.TabIndex = 6;
            this.txtRandomSeed.Text = "0";
            this.txtRandomSeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtRandomSeed.TextChanged += new System.EventHandler(this.txtRandomSeed_TextChanged);
            // 
            // lblRandomSeed
            // 
            this.lblRandomSeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRandomSeed.AutoSize = true;
            this.lblRandomSeed.Location = new System.Drawing.Point(476, 199);
            this.lblRandomSeed.Name = "lblRandomSeed";
            this.lblRandomSeed.Size = new System.Drawing.Size(78, 13);
            this.lblRandomSeed.TabIndex = 15;
            this.lblRandomSeed.Text = "Random Seed:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(734, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(653, 226);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lblHelp
            // 
            this.lblHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHelp.AutoSize = true;
            this.lblHelp.Location = new System.Drawing.Point(15, 199);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(330, 39);
            this.lblHelp.TabIndex = 10;
            this.lblHelp.Text = "Hier können mehrere Zeile Text stehen, \r\nmit Hinweise zur Benutzung der einzelnen" +
    " Köpfe bzw. Eingabefelder.\r\nJe nach Eingabefeld kann sich die Eingabe informatio" +
    "n anpassen";
            // 
            // FrmWallSweepList
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(834, 261);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtRandomSeed);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.lblRandomSeed);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1200, 600);
            this.MinimumSize = new System.Drawing.Size(850, 300);
            this.Name = "FrmWallSweepList";
            this.Text = "Fugen Profile";
            this.Load += new System.EventHandler(this.FrmWallSweepList_Load);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwProfileTypes;
        private System.Windows.Forms.TextBox txtProfileWidth;
        private System.Windows.Forms.ComboBox cmbProfileType;
        private System.Windows.Forms.Label lblIdValue;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.TextBox txtRandomSeed;
        private System.Windows.Forms.Label lblRandomSeed;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ColumnHeader clmID;
        private System.Windows.Forms.ColumnHeader clmName;
        private System.Windows.Forms.ColumnHeader clmWidth;
        private System.Windows.Forms.Label lblHelp;
    }
}