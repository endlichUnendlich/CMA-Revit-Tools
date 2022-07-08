namespace CMA.core
{
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Selection;
    using Autodesk.Revit.DB;

    /// <summary>
    /// Zoom to the current selected Elements
    /// </summary>
    /// <seealso cref="Autodesk.Revit.UI.IExternalCommand" />
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class ZoomElementCommand : IExternalCommand
    {
        #region public methods

        /// <summary>
        /// Overload this method to implement and external command within Revit.
        /// </summary>
        /// <param name="commandData">An ExternalCommandData object which contains reference to Application and View
        /// needed by external command.</param>
        /// <param name="message">Error message can be returned by external command. This will be displayed only if the command status
        /// was "Failed".  There is a limit of 1023 characters for this message; strings longer than this will be truncated.</param>
        /// <param name="elements">Element set indicating problem elements to display in the failure dialog.  This will be used
        /// only if the command status was "Failed".</param>
        /// <returns>
        /// The result indicates if the execution fails, succeeds, or was canceled by user. If it does not
        /// succeed, Revit will undo any changes made by the external command.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            UIDocument uiDoc = commandData.Application.ActiveUIDocument;

            // Get the current selevtion.
            Selection sel = uiDoc.Selection;

            // If no Element is selcted, prompt the user to select one.
            if (sel.GetElementIds().Count == 0)
            {
                Reference reference = sel.PickObject(ObjectType.Element, "Wähle eine Element aus.");
                ElementId[] elementIds = new ElementId[] { reference.ElementId };
                sel.SetElementIds(elementIds);

            }

            // shows the elements by zoom to fit
            uiDoc.ShowElements(sel.GetElementIds());

            return Result.Succeeded;
        }

        /// <summary>
        /// Gets the full name of the class.
        /// </summary>
        /// <returns></returns>
        public static string GetFullClassName()
        {
            return typeof(ZoomElementCommand).Namespace + "." + nameof(ZoomElementCommand);
        }

        #endregion


    }
}
