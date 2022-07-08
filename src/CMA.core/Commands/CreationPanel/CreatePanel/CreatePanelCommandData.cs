namespace CMA.core
{
    using System;
    using System.Collections.Generic;
    using Autodesk.Revit.DB;

    public class CreatePanelCommandData
    {
        public List<ProfileData> ProfileDataColl { get; set; } = new List<ProfileData>();

        public int AutoId { get; set; } = 0;

        public int RandomSeed { get; set; }
    }
}
