using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using System.Windows.Media;

namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge02 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your Module 02 Challenge code goes here
            // Delete the TaskDialog below and add your code
            //TaskDialog.Show("Module 02 Challenge", "Coming Soon!");

            //IList<Element> PickElementsByRectangle(string statusPrompt);
            //IList<Element> picklist = uidoc.Selection.PickElementsByRectangle("Select Element by Rectangle Selection");

            // Filter Selected Element for Model curves
            //List<CurveElement> modelCurves = new List<CurveElement>();
            //foreach (Element elem in picklist)
            //{
            //    if (elem is CurveElement)
            //    {
            //        CurveElement curveElem = (CurveElement)elem;

            //        if (curveElem.CurveElementType == CurveElementType.ModelCurve)
            //        {
            //            modelCurves.Add(curveElem);
            //        }
            //    }
            //}

            //View currView = doc.ActiveView;
            //Level currLevel = currView.GenLevel;

            //foreach (CurveElement currentCurve in modelCurves)
            //{
            //    Curve curve = currentCurve.GeometryCurve;
            //    XYZ startPoint = curve.GetEndPoint(0);
            //    XYZ endPoint = curve.GetEndPoint(1);

            //    GraphicsStyle curStyle = currentCurve.LineStyle as GraphicsStyle;

            //}

            //using (Transaction t = new Transaction(doc))
            //{

            //    //Transaction t = new Transaction(doc);
            //    t.Start("Create Walls");

            //    // Create wall
            //    Curve curCurve1 = modelCurves[0].GeometryCurve;
            //    Curve curCurve2 = modelCurves[1].GeometryCurve;
            //    Curve curCurve3 = modelCurves[2].GeometryCurve;
            //    Curve curCurve4 = modelCurves[3].GeometryCurve;

            //    //Wall newWall = Wall.Create(doc, mcurves, false);

            //    // Create with wall types
            //    FilteredElementCollector pipeCollector = new FilteredElementCollector(doc);
            //    pipeCollector.OfClass(typeof(PipeType));

            //foreach (WallType wallType in walltypes)
            //{
            //    if (wallType.Name == "A - GLAZ - Storefront wall")
            //    {
            //        Wall newWall = Wall.Create(doc, curCurve1, currLevel.Id, false);
            //    }
            //    if (wallType.Name == "A-WALL - Generic 8\" wall")
            //    {
            //        Wall newWall2 = Wall.Create(doc, curCurve2, currLevel.Id, false);
            //    }
            //if (wallType.Name == "M-DUCT - Default duct")
            //{
            //    Wall newWall3 = Wall.Create(doc, curCurve3, currLevel.Id, false);
            //}
            //if (wallType.Name == "P-PIPE - Default pipe")
            //{
            //    Wall newWall4 = Wall.Create(doc, curCurve4, currLevel.Id, false);
            //}
            //}

            //t.Commit();
            return Result.Succeeded;
        }


        internal string PipeMethod()
        {
            return "Pipping Complete";
        }

            //return Result.Succeeded;
        //}
        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge02";
            string buttonTitle = "Module\r02";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module02,
                Properties.Resources.Module02,
                "Module 02 Challenge");

            return myButtonData.Data;
        }
    }

}
