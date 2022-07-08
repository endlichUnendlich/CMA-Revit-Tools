namespace CMA.core
{
    using System.Text;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Selection;
    using Autodesk.Revit.DB;

    /// <summary>
    /// Command code to be executed when button is clicked.
    /// </summary>
    /// <seealso cref="Autodesk.Revit.UI.IExternalCommand" />
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class TagWallLayersCommand : IExternalCommand
    {

        #region public methods


        /// <summary>
        /// Tag wall layers by creating text note element on user specified point an populate it with layer information.
        /// </summary>
        /// <param name="commandData">The command data.</param>
        /// <param name="message">The message.</param>
        /// <param name="elements">The elements.</param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Application Context
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Check if we are in the Revit project, not in the family one.
            if (doc.IsFamilyDocument)
            {
                Message.Display("Can't use command in family document.", WindowType.Warning);
                return Result.Cancelled;
            }

            // Get access to current view.
            Autodesk.Revit.DB.View activeView = uidoc.ActiveView;

            // Check if the TextNote can be created in the currently active project view.
            bool canCreateTextNoteInView = false;
            switch (activeView.ViewType)
            {
                case ViewType.FloorPlan:
                    canCreateTextNoteInView = true;
                    break;
                case ViewType.CeilingPlan:
                    canCreateTextNoteInView = true;
                    break;
                case ViewType.Detail:
                    canCreateTextNoteInView = true;
                    break;
                case ViewType.Elevation:
                    canCreateTextNoteInView = true;
                    break;
                case ViewType.Section:
                    canCreateTextNoteInView = true;
                    break;
            }

            // Collector for data provided in window.
            TagWallLayersCommandData userInfo = new TagWallLayersCommandData();

            if (!canCreateTextNoteInView)
            {
                Message.Display("Text Note element can't be created in the current view.", WindowType.Error);
                return Result.Cancelled;
            }

            // Get user provided information from window and show dialog.
            using (TagWallLayersForm window = new TagWallLayersForm(uidoc))
            {
                window.ShowDialog();

                if (window.DialogResult == DialogResult.Cancel)
                    return Result.Cancelled;

                userInfo = window.GetInformation();
            }

            // Ask user to select one basic wall.
            Reference selectedReference = uidoc.Selection.PickObject(ObjectType.Element, new SelectionFilterByCategory(Category.GetCategory(doc, BuiltInCategory.OST_Walls).Name), "Select one basic wall.");
            Element selctedElement = doc.GetElement(selectedReference);

            // Cast generic element type to more specific Wall type.
            Wall wall = selctedElement as Wall;

            // Check if wall is type of basic wall
            if (wall.IsStackedWall)
            {
                Message.Display("Wall you selected is category of the Stacked Wall.\nIt's not supported by this command.", WindowType.Warning);
                return Result.Cancelled;
            }

            // Access list of wall layers.
            IList<CompoundStructureLayer> layers = wall.WallType.GetCompoundStructure().GetLayers();

            // Get layer information in structured string format for Text Note.
            StringBuilder msg = new StringBuilder();

            foreach (CompoundStructureLayer layer in layers)
            {
                Material material = doc.GetElement(layer.MaterialId) as Material;

                msg.AppendLine();

                if (userInfo.Function)
                    msg.Append(layer.Function.ToString());

                // Check if material is attached to the layer in wall compound structure.
                if (userInfo.Name)
                {
                    if (material != null)
                        msg.Append(" " + material.Name);
                    else
                        msg.Append(" <Nach Kategorie>");           
                }

                // Convert units to metric.
                // Revit by default uses imperial units.
                if (userInfo.Thickness)
                    msg.Append(" " + LengthUnitConverter.ConvertToMetric(layer.Width, userInfo.UnitType, userInfo.DecimalPlaces).ToString());

            }

            // Create Text Note options
            TextNoteOptions textNoteOptions = new TextNoteOptions
            {
                VerticalAlignment = VerticalTextAlignment.Top,
                HorizontalAlignment = HorizontalTextAlignment.Left,
                TypeId = userInfo.TextTypeId
            };

            // Open Revit document transaction to create new Text Note element.
            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("Tag Wall Layers Command");


                XYZ pt = new XYZ();

                // Construct a sketchplane for user to pick point if we are in elevation or section view.
                if (activeView.ViewType == ViewType.Elevation || activeView.ViewType == ViewType.Section)
                {
                    Plane plane = Plane.CreateByNormalAndOrigin(activeView.ViewDirection, activeView.Origin);
                    SketchPlane sketchPlane = SketchPlane.Create(doc, plane);
                    activeView.SketchPlane = sketchPlane;

                    // Ask user to pick location point for the TextNote element.
                    pt = uidoc.Selection.PickPoint("Pick Text Note location point.");
                }
                else
                {
                    // Ask user to pick location point for the TextNote element.
                    pt = uidoc.Selection.PickPoint("Pick Text Note location point.");
                }

                // Create Text Note with wall layers information on user specified point in the current active view.
                TextNote.Create(doc, activeView.Id, pt, msg.ToString(), textNoteOptions);

                transaction.Commit();
            }

            return Result.Succeeded;
        }


        /// <summary>
        /// Gets the full name of the class.
        /// </summary>
        /// <returns></returns>
        public static string GetFullClassName()
        {
            return typeof(TagWallLayersCommand).Namespace + "." + nameof(TagWallLayersCommand);
        }

        #endregion
    }
}
