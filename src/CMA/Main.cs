namespace CMA
{
    using Autodesk.Revit.UI;
    using core;

    /// <summary>
    /// Plugin's main entry point.
    /// </summary>
    /// <seealso cref="Autodesk.Revit.UI.IExternalApplication" />
    public class Main : IExternalApplication
    {
        public static CreatePanelCommandData CurCommandData { get; set; }

        #region external application public methods

        /// <summary>
        /// Called when Revit shots down.
        /// </summary>
        /// <param name="application">The Application</param>
        /// <returns></returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }


        /// <summary>
        /// Called when Revit starts up.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <returns></returns>
        public Result OnStartup(UIControlledApplication application)
        {
            CurCommandData = new CreatePanelCommandData();

            // Initialize whole Plugin's user interface.
            SetupInterface ui = new SetupInterface();
            ui.Initialize(application);

            return Result.Succeeded;
        }
        #endregion

    }
}
