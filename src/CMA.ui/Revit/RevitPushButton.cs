namespace CMA.ui
{
    using System;

    using Autodesk.Revit.UI;

    using core;
    using res;

    /// <summary>
    /// The Revit push button methods.
    /// </summary>
    public static class RevitPushButton
    {
        #region public methods

        /// <summary>
        /// Creates the push button based on data provided in <see cref="RevitPushButtonDataModel"/>.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static PushButton Create(RevitPushButtonDataModel data)
        {
            // The button name based on unique indentifier.
            string btnDataName = Guid.NewGuid().ToString();

            // Sets the button data.
            PushButtonData btnData = new PushButtonData(btnDataName, data.Label, CoreAssembly.GetAssemblyLocation(), data.CommandFullClassName)
            {
                ToolTip = data.Tooltip,
                LargeImage = ResourceImage.GetIcon(data.IconImageName),
                ToolTipImage = ResourceImage.GetIcon(data.TooltipImageName),
                LongDescription = data.LongDescription
            };

            // Returns created button and host it on panel in required data model.
            return data.Panel.AddItem(btnData) as PushButton;
        }

        #endregion
    }
}
