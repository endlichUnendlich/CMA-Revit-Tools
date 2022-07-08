namespace CMA
{
    using Autodesk.Revit.UI;

    using ui;
    using core;

    public class SetupInterface
    {
        #region constructor        

        /// <summary>
        /// Default constructor.
        /// Initializes a new instance of the <see cref="SetupInterface"/> class.
        /// </summary>
        public SetupInterface()
        {

        }

        #endregion

        #region public methods

        /// <summary>
        /// Initializes all interface elements on custom created Revit tab.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Initialize(UIControlledApplication app)
        {
            // Create ribbon tab.
            string tabName = "CMA ToolBox";
            app.CreateRibbonTab(tabName);

            #region AnnotationPanel

            // Create the annotation panel
            RibbonPanel annotationPanel = app.CreateRibbonPanel(tabName, "Beschriftungs-Tools");

            #region TagWallLayerButton

            //// Populate button data model.
            //RevitPushButtonDataModel tagWallButtonData = new RevitPushButtonDataModel
            //{
            //    Label = "Tag Wall\nLayers",
            //    Panel = annotationPanel,
            //    Tooltip = "This is some sample tooltip text,\nreplace it with real one later, ...",
            //    CommandFullClassName = TagWallLayersCommand.GetFullClassName(),
            //    IconImageName = "TagWallLayersIcon_32.PNG",
            //    TooltipImageName = "TagWallLayersIcon_32.PNG"
            //};

            //// Create button from provided data.
            //PushButton tagWallButton = RevitPushButton.Create(tagWallButtonData);

            #endregion TagWallLayerButton

            #region RoomRenumButton

            // Populate button data model.
            RevitPushButtonDataModel roomRenumButtonData = new RevitPushButtonDataModel
            {
                Label = "Raum Nummer\nsetzen",
                Panel = annotationPanel,
                Tooltip = "Setzt die Raum Nummer nach benutzerdefinierter Maske.",
                CommandFullClassName = RoomRenumCommand.GetFullClassName(),
                IconImageName = "RaumNummerierung_32.PNG",
                TooltipImageName = "RaumNummerierung_32.PNG"
            };

            // Create button from provided data.
            PushButton roomRenumButton = RevitPushButton.Create(roomRenumButtonData);

            #endregion

            #endregion

            #region CreationPanel

            // Create the annotation panel
            RibbonPanel creationPanel = app.CreateRibbonPanel(tabName, "Erstellungs-Tools");

            #region CreatePanelButton

            // Populate button data model.
            RevitPushButtonDataModel createPanelButtonData = new RevitPushButtonDataModel
            {
                Label = "Panele zufällig\nerstellen",
                Panel = creationPanel,
                Tooltip = "Erstellt Panele nach zufälligem Muster.",
                CommandFullClassName = CreatePanelCommand.GetFullClassName(),
                IconImageName = "CreatePanel_32.png",
                TooltipImageName = "CreatePanel_32.png"
            };

            // Create button from provided data.
            PushButton createPanelButton = RevitPushButton.Create(createPanelButtonData);

            #endregion

            #endregion

            #region UtilityPanel

            // Create the utility panel
            RibbonPanel utilityPanel = app.CreateRibbonPanel(tabName, "Utility Tools");

            #region ZoomElementButton

            // Populate button data model.
            RevitPushButtonDataModel zoomElementButtonData = new RevitPushButtonDataModel
            {
                Label = "Zoom\nElement",
                Panel = utilityPanel,
                Tooltip = "Zoomt auf die ausgewählten Elemente",
                CommandFullClassName = ZoomElementCommand.GetFullClassName(),
                IconImageName = "ZoomToFit_32.PNG",
                TooltipImageName = "ZoomToFit_32.PNG"
            };

            // Create button from provided data.
            PushButton zoomElementButton = RevitPushButton.Create(zoomElementButtonData);

            #endregion

            #region ProxyButton

            //// Populate button data model.
            //RevitPushButtonDataModel proxyButtonData = new RevitPushButtonDataModel
            //{
            //    Label = "...    ???    ...",
            //    Panel = utilityPanel,
            //    Tooltip = "?",
            //    CommandFullClassName = ZoomElementCommand.GetFullClassName(),
            //    IconImageName = "Proxy_32.png",
            //    TooltipImageName = "Proxy_32.png"
            //};

            //// Create button from provided data.
            //PushButton proxyButton = RevitPushButton.Create(proxyButtonData);

            #endregion

            #endregion

        }

        #endregion

    }
}
