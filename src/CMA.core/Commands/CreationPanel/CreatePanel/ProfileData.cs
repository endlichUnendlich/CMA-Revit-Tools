namespace CMA.core
{
    using System;
    using Autodesk.Revit.DB;

    public class ProfileData
    {
        public int Id { get; set; }

        public ElementId ProfileTypeId { get; set; }

        public string ProfileTypeName { get; set; }

        public double ProfileWidth { get; set; }

    }
}