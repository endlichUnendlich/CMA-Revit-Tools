namespace CMA.core
{
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Selection;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.DB.Architecture;

    /// <summary>
    /// Command code to be executed when button is clicked.
    /// </summary>
    /// <seealso cref="Autodesk.Revit.UI.IExternalCommand" />
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class RoomRenumCommand : IExternalCommand
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
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            // List that will contain references to the selected elements
            SelectionFilterByCategory roomFilter = new SelectionFilterByCategory(Category.GetCategory(doc, BuiltInCategory.OST_Rooms).Name);

            // Create an CommandData object and show a window form to collect information provided by the user.
            RoomRenumCommandData userInfo = new RoomRenumCommandData();
            RoomRenumForm window = new RoomRenumForm();
            window.Show();

            // Repeat the command until OperationCancelException is thrown and breaks the loop
            while (true)
            {
                // Ask user to select a room
                Reference selRoom = null;
                try
                {
                    selRoom = uidoc.Selection.PickObject(ObjectType.Element, roomFilter, "Wähle einen Räume aus die neu nummeriert werden sollen. Beende den Befehl mit ESC.");
                    if (window.IsDisposed) break;
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    break;
                }

                Room room = doc.GetElement(selRoom) as Room;
                if (room == null) continue;

                // Number parameter of the selected room
                Parameter roomNum = room.get_Parameter(BuiltInParameter.ROOM_NUMBER);

                // Get the information provided by the user from the form
                userInfo = window.GetInformation();

                string paramValue;
                if (userInfo.RoomIndex != 0)
                    paramValue = userInfo.RoomPrefix + userInfo.RoomNumber.ToString(userInfo.LeadingZeros) + userInfo.RoomIndex.ToString() + userInfo.RoomSuffix;
                else
                    paramValue = userInfo.RoomPrefix + userInfo.RoomNumber.ToString(userInfo.LeadingZeros) + userInfo.RoomSuffix;

                // Set the new room number
                using (Transaction t = new Transaction(doc, "Raum nummerieren"))
                {
                    t.Start();
                    roomNum.Set(paramValue);
                    t.Commit();
                }

                // Increase the room number and update the CommandData information
                if (userInfo.RoomIndex != 0)
                    userInfo.RoomIndex++;
                else
                    userInfo.RoomNumber++;
                window.UpdateRoomCommandData(userInfo);
            }

            // Close the window form when the command is canceled.
            window.Close();

            return Result.Succeeded;
        }

        /// <summary>
        /// Gets the full name of the class.
        /// </summary>
        /// <returns></returns>
        public static string GetFullClassName()
        {
            return typeof(RoomRenumCommand).Namespace + "." + nameof(RoomRenumCommand);
        }

        #endregion
    }
}
