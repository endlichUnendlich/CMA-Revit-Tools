
namespace CMA.core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Autodesk.Revit.ApplicationServices;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.UI.Selection;

    [Transaction(TransactionMode.Manual)]
    public class CreatePanelCommand : IExternalCommand
    {
        public static CreatePanelCommandData CurCommandData { get; set; } = new CreatePanelCommandData();

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            Selection sel = uiDoc.Selection;

            // save the length units of the Document
            ForgeTypeId docUnit = doc.GetUnits().GetFormatOptions(SpecTypeId.Length).GetUnitTypeId(); // Revit 2021
            //DisplayUnitType docUnit = doc.GetUnits().GetFormatOptions(UnitType.UT_Length).DisplayUnits; // Revit 2020

            //pick one wall and get the length parameter
            Reference reference = null;
            try
            {
                //select one wall to operate on
                reference = sel.PickObject(ObjectType.Element, "Wähle eine Wand für die Panele aus");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
            }
            Wall wall = doc.GetElement(reference) as Wall;

            SetJoinToMiter(doc, wall);

            Parameter p = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
            double length = p.AsDouble();

            //convert it in meters
            double wallLength = UnitUtils.ConvertFromInternalUnits(length, docUnit);
            wallLength = Math.Round(wallLength, 4);


            //possible profile type names, profile type ids an profile widths in the same order
            List<string> posProfilTypNames = new List<string>();
            List<ElementId> posProfilTypIds = new List<ElementId>();
            List<double> posProfileWidth = new List<double>();

            //prompt the user to define a seed integer
            int mySeed;


            // using a Winows Form for the custom integer input
            using (FrmWallSweepList myForm = new FrmWallSweepList(uiDoc))
            {
                //Windows Form: Sead input
                myForm.ShowDialog();
                if (myForm.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    return Result.Cancelled;
                }

                foreach (var item in CurCommandData.ProfileDataColl)
                {
                    posProfilTypNames.Add(item.ProfileTypeName);
                    posProfilTypIds.Add(item.ProfileTypeId);
                    posProfileWidth.Add(item.ProfileWidth);
                }

                mySeed = CurCommandData.RandomSeed;
            }

            AddRandomSeedParameterCommand a = new AddRandomSeedParameterCommand();
            a.Execute(commandData,ref message, elements);

            using (Transaction t = new Transaction(doc, "Panele erstellen"))
            {
                t.Start();

                ElementType oldWallSweepType = getWallSweepTypeByName(doc, "Fuge");

                //create the wall sweep/ reveal types
                foreach (var item in CurCommandData.ProfileDataColl)
                {
                    ElementType newWallSweepType = getWallSweepTypeByName(doc, item.ProfileTypeName);
                    if (newWallSweepType == null)
                    {
                        Parameter oldProfileParam = oldWallSweepType.get_Parameter(BuiltInParameter.REVEAL_PROFILE_PARAM);

                        newWallSweepType = oldWallSweepType.Duplicate(item.ProfileTypeName);
                        Parameter newProfileParam = newWallSweepType.get_Parameter(BuiltInParameter.REVEAL_PROFILE_PARAM);
                        newProfileParam.Set(item.ProfileTypeId);
                    }
                    
                }

                //save the value of the "Random Sead" parameter of the wall
                Parameter rSeadParam = wall.LookupParameter("_Random Seed");
                rSeadParam.Set(mySeed);

                Random rnd = new Random(mySeed);

                //populate the lists for Reveal creation

                //List of wall sweep types name, ids and widths
                List<string> listProfileTypeNames = new List<string>();
                List<ElementId> listProfileTypeIds = new List<ElementId>();
                List<double> listProfileWidths = new List<double>();

                //generate x random numbers from 0 to 1 but not the same consecutive (fortlaufend)
                while (Math.Round(listProfileWidths.Sum(), 4) <= wallLength)
                {
                    //random integer between 0 and 3
                    int wSTindex = rnd.Next(posProfilTypNames.Count);

                    //after the conditions are met add an item of the posWallSweepTypes array with the coresponding random index to the list.
                    listProfileTypeNames.Add(posProfilTypNames[wSTindex]);
                    listProfileTypeIds.Add(posProfilTypIds[wSTindex]);
                    //add the corresponding double which represents the width of the profile to the list
                    listProfileWidths.Add(posProfileWidth[wSTindex]);
                }


                //			//Create the wall sweeps
                //the start distance of the wall sweep
                //double distance = (wallLength - (listProfileWidths.Sum() - 1)) / 2;
                double distance = 0.0;
                //iterator for the width
                int iterator = 0;


                //    if (wallLength - (listProfileWidths.Sum() - 1) != 0)
                //    {
                //        // create the first Wall Sweep Type
                //        string firstWallSweepName = listProfileTypeNames[0].Replace("rechts", "links");
                //    ElementType firstWallSweepType = getWallSweepTypeByName(doc, firstWallSweepName);
                //    WallSweepInfo firstWallSweepInfo = new WallSweepInfo(WallSweepType.Reveal, true);
                //    firstWallSweepInfo.Distance = Math.Round(UnitUtils.ConvertToInternalUnits(distance, docUnit), 4);
                //    WallSweep firstWallSweep = WallSweep.Create(wall, firstWallSweepType.Id, firstWallSweepInfo);
                //}
                //    else
                //{
                //    listProfileTypeNames.RemoveAt(listProfileTypeNames.Count - 1);
                //    listProfileWidths.RemoveAt(listProfileWidths.Count - 1);
                //}


                //crate for every item in the list listProfileTypeNames a wall sweep with the coresponding name
                foreach (string wallSweepName in listProfileTypeNames)
                {
                    //use the this method to get the wall sweep type by name
                    ElementType wallSweepType = getWallSweepTypeByName(doc, wallSweepName);

                    //if getting the type by name was succsefull
                    if (wallSweepType != null)
                    {
                        //choose between Sweep and Reveal and between vertical or horizontal
                        WallSweepInfo wallSweepInfo = new WallSweepInfo(WallSweepType.Reveal, true);

                        //define the insert distance from the start of the wall
                        wallSweepInfo.Distance = Math.Round(UnitUtils.ConvertToInternalUnits(distance, docUnit), 4);
                        distance += (listProfileWidths[iterator]);
                        iterator++;

                        //create the wall sweep
                        WallSweep wallSweep = WallSweep.Create(wall, wallSweepType.Id, wallSweepInfo);
                    }
                    //error handling
                    else
                    {
                        TaskDialog.Show("ERROR", "Es wurde kein Fugen-Typ gefunden");
                    }
                }
                t.Commit();
            }

            return Result.Succeeded;
        }

        private void SetJoinToMiter(Document doc, Wall wall)
        {
            LocationCurve lc = wall.Location as LocationCurve;

            using (Transaction t = new Transaction(doc, "Wandverbindung auf Gehrung"))
            {
                t.Start();
                lc.set_JoinType(0, JoinType.Miter);
                lc.set_JoinType(1, JoinType.Miter);
                t.Commit();
            }
        }

        //the method to get the Element Type of a wall sweep
        private static ElementType getWallSweepTypeByName(Document curDoc, string wallSweepName)
        {
            //get all Wall Sweep Types as Element Type
            FilteredElementCollector wallSweepTypeColl = new FilteredElementCollector(curDoc);
            wallSweepTypeColl.OfCategory(BuiltInCategory.OST_Reveals).WhereElementIsElementType();

            //loop through all Wall Sweep Types and look for match
            foreach (ElementType curWallSweepType in wallSweepTypeColl)
            {
                //if the
                if (curWallSweepType.Name == wallSweepName)
                {
                    return curWallSweepType;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the full name of the class.
        /// </summary>
        /// <returns></returns>
        public static string GetFullClassName()
        {
            return typeof(CreatePanelCommand).Namespace + "." + nameof(CreatePanelCommand);
        }
    }
}
