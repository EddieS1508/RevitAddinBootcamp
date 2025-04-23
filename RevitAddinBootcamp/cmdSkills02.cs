namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdSkills02 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your Module 02 Skills code goes here
            //IList<Element> PickElementsByRectangle(string statusPrompt);
            IList<Element> picklist = uidoc.Selection.PickElementsByRectangle("Select Element by Rectangle Selection");

            TaskDialog.Show("Test", "I Selected " + picklist.Count.ToString() + " Elements");

            // Filter selected elements
            List<CurveElement> allCurves = new List<CurveElement>();
            foreach (Element elem in picklist)
            {
                if (elem is CurveElement)
                {
                    allCurves.Add(elem as CurveElement);   //(CurveElement) elem);
                }
            }

            // Filter Selected Element for Model curves
            List<CurveElement> modelCurves = new List<CurveElement>();
            foreach (Element elem2 in picklist)
            {
                if (elem2 is CurveElement)
                {
                    CurveElement curveElem = (CurveElement) elem2;

                    if(curveElem.CurveElementType == CurveElementType.ModelCurve)
                    {
                        modelCurves.Add(curveElem);
                    }
                }
            }
            return Result.Succeeded;
        }
    }
}
