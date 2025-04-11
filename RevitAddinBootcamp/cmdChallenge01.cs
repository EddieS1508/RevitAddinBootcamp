using System.Xml.Serialization;

namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            int b1 = 1;
            int f1 = 1;
            int fb1 = 1;
            int num01 = 250;
            double Elev = 0;
            double FloorHeight = 15;

            // create a transaction to lock the model
            Transaction t = new Transaction(doc);
            t.Start("Challenge 01");

            for (int i = 0; i <= num01; i++)
            {
                //Increment floorheight value by floorheight
                Elev = Elev + FloorHeight;

                double Div3Check = i % 3; // Check divisble by 3
                double Div5Check = i % 5; // Check divisble by 5

                if (Div3Check == 0 && Div5Check == 0)
                {
                    // create a new level
                    Level newFBLevel = Level.Create(doc, Elev);
                    newFBLevel.Name = "FIZZBUZZ_" + i + " _" + fb1;
                    fb1 += 1;

                    // create filterd element collector for all view family types
                    FilteredElementCollector viewcollector = new FilteredElementCollector(doc);
                    viewcollector.OfClass(typeof(ViewFamilyType));

                    ViewFamilyType floorPlanVFT = null;
                    foreach (ViewFamilyType currVFT in viewcollector)
                    {
                        if (currVFT.ViewFamily == ViewFamily.FloorPlan)
                        {
                            floorPlanVFT = currVFT;
                        }
                    }

                    // create a floor plan view for each FIZZBUZZ
                    ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newFBLevel.Id);
                    newFloorPlan.Name = "FIZZBUZZ_" + i;

                    // create new sheets for FIZZBUZZ
                    FilteredElementCollector TblockCollector = new FilteredElementCollector(doc);
                    TblockCollector.OfCategory(BuiltInCategory.OST_TitleBlocks);
                    TblockCollector.WhereElementIsElementType();

                    ViewSheet newsheet = ViewSheet.Create(doc, TblockCollector.FirstElementId());
                    newsheet.Name = "FIZZBUZZ_" + i;
                    newsheet.SheetNumber = "FB_" + i;

                    // create a viewport
                    // first create a location point
                    XYZ insPoint = new XYZ(1, 0.75, 0);

                    Viewport newViewPort = Viewport.Create(doc, newsheet.Id, newFloorPlan.Id, insPoint);

                }
                else if(Div3Check == 0)
                {
                    // create a new level
                    Level newFLevel = Level.Create(doc, Elev);
                    newFLevel.Name = "FIZZ_" + i + " _" + f1;
                    f1 += 1;
                }
                else if(Div5Check == 0)
                {
                    // create a new level
                    Level newBLevel = Level.Create(doc, Elev);
                    newBLevel.Name = "BUZZ_" + i + " _" + b1;
                    b1 += 1;
                }
                else
                {
                    // create a new level
                    Level newLevel = Level.Create(doc, Elev);
                    newLevel.Name = "Level_" + i;
                }

                //    //Increment floorheight value by floorheight
                //    FloorHeight = StartElev + FloorHeight;
            }

            // Your Module 01 Challenge code goes here
            // Delete the TaskDialog below and add your code
            //TaskDialog.Show("Module 01 Challenge", "Coming Soon!");

            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge01";
            string buttonTitle = "Module\r01";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module01,
                Properties.Resources.Module01,
                "Module 01 Challenge");

            return myButtonData.Data;
        }
    }

}
