namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdSkills01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            //string myVar = "This is my first variable";
            //TaskDialog.Show("Test", "This is new message from me!!");

            //TaskDialog.Show("Test", "This is another from me!!");
            //TaskDialog.Show("Test", "Add via GitHub!!");
            //TaskDialog.Show("Test", "Push to GitHub!!");

            // Your Module 01 Skills code goes here
            // let's create some variables
            // DataType VariableName = Value; always end with semicolon

            // Create string variable
            string text1 = "This is my test";
            string text2 = "this is my next text";

            // combine text
            string text3 = text1 + text2;
            string text4 = text1 + " " + text2;

            // create number variables
            int number1 = 10;
            double number2 = 20.5;

            // do some math
            double number3 = number1 + number2;
            double number4 = number1 - number2;
            double number5 = number4 / number3;
            double number6 = number5 * number4;

            // convert meters to feet
            double meters = 4;
            double meterstofeet = meters * 3.28084;

            // convert mm to feet
            double mm = 3500;
            double mm2feet = mm / 304.8;
            double mm2feet2 = (mm / 1000) * 3.28084;

            // find the remainder when deviding (ie, the modulo of mod)
            double remainder1 = 100 % 10; // equals 0 (100 / 10 = 10)
            double remainder2 = 100 % 9; // equal = 1 (100 / 9 = 11 with remainder)

            // increment by 1
            number6++;
            number6--;

            // conditional logic == != > < >= <=

            // check a value with IF
            if (number6 > 10)
            {
                // do smething if true
            }

            // check multi[;t conditions and peform action
            if (number5 == 100)
            {
                // do something
            }
            else
            {
                //dp something else
            }

            // compound conditional statement
            if (number6 > 01 && number5 == 100)
            {
                // doo something if both conditions met
            }

            // either condition met
            if (number5 == 100 || number6 > 10)
            {
                //do something
            }

            // create a list
            List<string> list1 = new List<string>();

            // add items to the list
            list1.Add(text1);
            list1.Add(text2);
            list1.Add("This new text");

            List<int> list2 = new List<int> { 1, 2, 3, 4, 5 };
            List<string> list3 = new List<string> { "one", "two" };

            // loop through list items
            foreach (string currentstring in list1)
            {
                // process list item
            }

            int letterCount = 0;
            foreach (string currentString in list1)
            {
                letterCount = letterCount + currentString.Length;
            }

            // loop through range of numbers
            for (int i = 0; i <= 10; i++)
            {
                // do something

            }

            // loop through variables
            int numberCount = 0;
            int counter = 100;
            for (int i = 0; i <= counter; i++)
            {
                numberCount += i;
            }

            // create a transaction to lock the model
            Transaction t = new Transaction(doc);
            t.Start("Doing something in Revit");
            // make revit changes
            // Delete the TaskDialog below and add your code
            TaskDialog.Show("Module 01 Skills", "Got Here!");
            // create a floor level
            Level newLevel = Level.Create(doc, 10);
            newLevel.Name = "NewLevel";


            // create filterd element collector to all view family type
            FilteredElementCollector collector1 = new FilteredElementCollector(doc);
            collector1.OfClass(typeof(ViewFamilyType));

            ViewFamilyType floorPlanVFT = null;
            foreach(ViewFamilyType cuurVFT in collector1)
            {
                if (cuurVFT.ViewFamily == ViewFamily.FloorPlan)
                    {
                        floorPlanVFT = cuurVFT;
                }
            }

            // create a floor pan view
            ViewPlan newFloorPlan = ViewPlan.Create(doc, floorPlanVFT.Id, newLevel.Id);
            newFloorPlan.Name = "My New Floorplan";

            // create a ceiling plan
            ViewFamilyType ceilingPlanVFT = null;
            foreach(ViewFamilyType currVFT in collector1)
            {
                if(currVFT.ViewFamily == ViewFamily.CeilingPlan)
                {
                    ceilingPlanVFT=currVFT;
                }
            }

            // create ceiling plan view
            ViewPlan newceilingplan = ViewPlan.Create(doc,ceilingPlanVFT.Id, newLevel.Id);
            newceilingplan.Name = "New CeilingPlan";

            // create new sheet
            FilteredElementCollector collector2 = new FilteredElementCollector(doc);
            collector2.OfCategory(BuiltInCategory.OST_TitleBlocks);
            collector2.WhereElementIsElementType();

            ViewSheet newsheet = ViewSheet.Create(doc, collector2.FirstElementId());
            newsheet.Name = "My new Sheet";
            newsheet.SheetNumber = "Z101";


            // create a viewport
            // first create a location point
            XYZ insPoint = new XYZ();
            XYZ insPoint2 = new XYZ(1, 0.5, 0);

            Viewport newViewPort = Viewport.Create(doc, newsheet.Id, newFloorPlan.Id, insPoint2);

            t.Commit();
            t.Dispose();

            return Result.Succeeded;
        }
    }

}
