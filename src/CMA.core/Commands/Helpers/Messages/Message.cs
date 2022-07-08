namespace CMA.core
{
    using Autodesk.Revit.UI;

    /// <summary>
    /// Display messages helper method
    /// </summary>
    public static class Message
    {
        #region public methods

        /// <summary>
        /// Displays the specified message.
        /// </summary>
        /// <param name="message">The message to display in the window.</param>
        /// <param name="windowType">Type of the message <see cref="WindowType"/>.</param>
        public static void Display(string message, WindowType windowType)
        {
            string title = "";
            TaskDialogIcon icon = TaskDialogIcon.TaskDialogIconNone;

            // Customize window based on type of message.
            switch (windowType)
            {
                case WindowType.Information:
                    title = "~ INFORMATION ~";
                    icon = TaskDialogIcon.TaskDialogIconInformation;
                    break;
                case WindowType.Warning:
                    title = "~ WARNING ~";
                    icon = TaskDialogIcon.TaskDialogIconWarning;
                    break;
                case WindowType.Error:
                    title = "~ ERROR ~";
                    icon = TaskDialogIcon.TaskDialogIconError;
                    break;
                default:
                    break;
            }

            // Construct window to display specified message.
            var window = new TaskDialog(title)
            {
                MainContent = message,
                MainIcon = icon,
                CommonButtons = TaskDialogCommonButtons.Close
            };
            window.Show();
        }

        #endregion

    }
}
