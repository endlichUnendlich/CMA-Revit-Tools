namespace CMA.core
{
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI.Selection;

    /// <summary>
    /// Selection filter based on the user provided category name.
    /// </summary>
    /// <seealso cref="Autodesk.Revit.UI.Selection.ISelectionFilter" />
    public class SelectionFilterByCategory : ISelectionFilter
    {
        #region private memebers

        // Private variable that holds category name.
        private string mCategoryName = "";

        #endregion

        #region constructor

        /// <summary>
        /// default constructor.
        /// Initializes a new instance of the <see cref="SelectionFilterByCategory" /> class.
        /// </summary>
        /// <param name="categoryName">the category name of the element, such as Walls, Floors, ....</param>
        public SelectionFilterByCategory(string categoryName)
        {
            mCategoryName = categoryName;
        }

        #endregion

        #region public methods    


        /// <summary>
        /// Allows the element selection if provided category is equal to selected one.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public bool AllowElement(Element element)
        {
            // Check if category matches.
            if (element.Category.Name == mCategoryName)
                return true;

            return false;
        }

        /// <summary>
        /// Override this post-filter method to specify if a reference to a piece of geometry is permitted to be selected.
        /// </summary>
        /// <param name="reference">A candidate reference in selection operation.</param>
        /// <param name="position">The 3D position of the mouse on the candidate reference.</param>
        /// <returns>
        /// Return true to allow the user to select this candidate reference. Return false to prevent selection of this candidate.
        /// </returns>
        /// <remarks>
        /// If an exception is thrown from this method, the element will not be permitted to be selected.
        /// </remarks>
        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }

        #endregion

    }
}
