namespace CMA.core
{

    using System.Linq;

    using System.IO;
    using Autodesk.Revit.ApplicationServices;
    using Autodesk.Revit.Attributes;
    using Autodesk.Revit.DB;
    using Autodesk.Revit.UI;


    [Transaction(TransactionMode.Manual)]
    public class AddRandomSeedParameterCommand : IExternalCommand
    {


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Application app = uiApp.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            //var wallColl = new FilteredElementCollector(doc);
            //Wall placeholderWall = wallColl.OfClass(typeof(Wall)).Cast<Wall>().Where(w => w.WallType.Kind == WallKind.Basic).First();

            Parameter testParam = null;
            using (Transaction t = new Transaction(doc))
            {
                t.Start("Proxy Wand");
                var level = new FilteredElementCollector(doc).OfClass(typeof(Level)).Cast<Level>().First();
                var wallType = new FilteredElementCollector(doc).OfClass(typeof(WallType)).Cast<WallType>().Where(wt => wt.Kind == WallKind.Basic).First();

                Wall testWall = Wall.Create(doc, Line.CreateBound(XYZ.Zero, XYZ.BasisX), wallType.Id, level.Id, 10, 0, false, false);
                testParam = testWall.LookupParameter("_Random Seed");
                t.RollBack();
            }

            //if (placeholderWall.LookupParameter("_Random Seed") == null)
            if (testParam == null)
            {
                string sharedParameterFilename = @"C:\ProgramData\Autodesk\Revit\Shared Parameters through the API.txt";
                app.SharedParametersFilename = sharedParameterFilename;

                CreateExternalSharedParamFile(sharedParameterFilename);
                DefinitionFile definitionFile = app.OpenSharedParameterFile();

                using (Transaction t = new Transaction(doc))
                {
                    t.Start("_Random Seed Parameter");

                    SetNewParameterToInstanceWall(uiApp, definitionFile);

                    t.Commit();
                }

                File.Delete(sharedParameterFilename);
                app.SharedParametersFilename = string.Empty;
            }

            return Result.Succeeded;
        }

        private bool SetNewParameterToInstanceWall(UIApplication app, DefinitionFile definitionFile)
        {
            //Create a new group in the shared parameter file
            DefinitionGroups myDefGroups = definitionFile.Groups;
            DefinitionGroup myDefinitionGroup = myDefGroups.Create("Mein Parameter-Grüppchen");

            //Create a type Definition
            ExternalDefinitionCreationOptions options = new ExternalDefinitionCreationOptions("_Random Seed", ParameterType.Integer);
            options.UserModifiable = false;
            Definition myDef_RandomSeed = myDefinitionGroup.Definitions.Create(options);


            //Create a category set and insert category of wall to it
            CategorySet myCatergories = app.Application.Create.NewCategorySet();
            //Use built-in category to get category of wall
            Category myCategory = Category.GetCategory(app.ActiveUIDocument.Document, BuiltInCategory.OST_Walls);

            myCatergories.Insert(myCategory);

            //Create an object of instance binding according to the categories
            InstanceBinding instanceBinding = app.Application.Create.NewInstanceBinding(myCatergories);

            //Get the binding map of current document
            BindingMap bindingMap = app.ActiveUIDocument.Document.ParameterBindings;
            bool typeBindOk;

            typeBindOk = bindingMap.Insert(myDef_RandomSeed, instanceBinding, BuiltInParameterGroup.PG_GEOMETRY);

            return typeBindOk;
        }

        private void CreateExternalSharedParamFile(string sharedParameterFile)
        {
            FileStream fileStream = File.Create(sharedParameterFile);
            fileStream.Close();
        }

    }
}