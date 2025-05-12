using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;
using Autodesk.Windows;
using RevitAddinBootcamp.Common;

namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdSkills03 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your Module 03 Skills code goes here
            // 2. create instance of class
            Building building1 = new Building("Big Office Bulding", "10 Main Street",10,150000);
            //building.Name = "Big Office Bulding";
            //building.address = "10 Main Street";
            //building.NumberOffFloors = 10;
            //building.Area = 150000;
            Building building2 = new Building("Fancy Hotel", "15 Main Street", 15, 220000);

            // possiblilty to change values any time.
            building1.NumberOffFloors = 11;

            List<Building> buildings = new List<Building>();
            buildings.Add(building2);
            buildings.Add(building1);
            buildings.Add(new Building("Hospital", "25 Main Street", 25, 350000));
            buildings.Add(new Building("Giant Store", "30 Main Street", 5, 400000));
            buildings.Add(new Building("Wallmart", "50 Main Street", 1, 600000));

            // 5. create neighbourhood instance
            Neighbourhood downtown = new Neighbourhood("Downtown", "Middletown", "CT", buildings);

            TaskDialog.Show("Test", $"There are {downtown.GetBuildingCount()} buildings in the " + $"{downtown.Name} Neighbourhood.");

            // 6. Working with rooms
            FilteredElementCollector roomCollector = new FilteredElementCollector(doc);
            roomCollector.OfCategory(BuiltInCategory.OST_Rooms);

            Room currRoom = roomCollector.First() as Room;

            // 7. Get room name
            string roomName = currRoom.Name;

            // 7a. Check room name
            if(roomName.Contains("Master"))
            {
                TaskDialog.Show("ETST", "Found the room");
            }

            // 7b. Get room pint
            Location roomLocation = currRoom.Location;
            LocationPoint roomLocPnt = currRoom.Location as LocationPoint;
            XYZ roomPoint = roomLocPnt.Point;

            using Transaction t = new Transaction(doc);
            {
                t.Start("Insert Families into Rooms");
                // 8. insert families
                FamilySymbol currFamSymbol = GetFamilySymbolByName(doc, "Desk", "Large");
                currFamSymbol.Activate();

                foreach (Room CurrRoom2 in roomCollector)
                {
                    LocationPoint loc = CurrRoom2.Location as LocationPoint;

                    FamilyInstance CurrFamInstance = doc.Create.NewFamilyInstance(loc.Point, currFamSymbol, StructuralType.NonStructural);

                    string department = Utils.GetParameterValueAsString(CurrRoom2, "Department");
                    double area = Utils.GetParameterValueAsDouble(CurrRoom2, BuiltInParameter.ROOM_AREA);

                    Utils.SetParameterValue(CurrRoom2, "Department", "Architecture");
                }
                t.Commit();
            }

            return Result.Succeeded;
        }

        //private string GetParameterValueAsString(Element currElem, string paramName)
        //{
        //    Parameter currParam = currElem.LookupParameter(paramName);
        //    Parameter currParam2 = currElem.get_Parameter(BuiltInParameter.ROOM_AREA);

        //    if (currParam != null)
        //    {
        //        return currParam.AsString();
        //    }
        //    else
        //        return "";
        //}

        //private string GetParameterValueAsString(Element curElem, BuiltInParameter bip)
        //{
        //    Parameter curParam = curElem.get_Parameter(bip);

        //        if (curParam != null)
        //        {
        //            return curParam.AsString();
        //        }
        //        else
        //            return "";
        //}

        //private double GetParameterValueAsDouble(Element currElem, string paramName)
        //{
        //    Parameter currParam = currElem.LookupParameter(paramName);
        //    //Parameter currParam2 = currElem.get_Parameter(BuiltInParameter.ROOM_AREA);

        //    if (currParam != null)
        //    {
        //        return currParam.AsDouble();
        //    }
        //    else
        //        return 0;
        //}

        //private double GetParameterValueAsDouble(Element curElem, BuiltInParameter bip)
        //{
        //    Parameter curParam = curElem.get_Parameter(bip);

        //    if (curParam != null)
        //    {
        //        return curParam.AsDouble();
        //    }
        //    else
        //        return 0;
        //}

        //private void SetParameterValue(Element curElem, string ParaName, string value)
        //{
        //    Parameter curParam = curElem.LookupParameter(ParaName);

        //    if(curParam != null)
        //    {
        //        curParam.Set(value);
        //    }
        //}

        //private void SetParameterValue(Element curElem, string ParaName, int value)
        //{
        //    Parameter curParam = curElem.LookupParameter(ParaName);

        //    if (curParam != null)
        //    {
        //        curParam.Set(value);
        //    }
        //}

        private FamilySymbol GetFamilySymbolByName(Document doc, string familyName, string familySymbolName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(FamilySymbol));

            foreach (FamilySymbol currSymbol in collector)
            {
                if (currSymbol.FamilyName == familyName)
                {
                    if (currSymbol.Name == familySymbolName)
                    {
                        return currSymbol;
                    }
                }
            }
            return null;
        }
    }

    //// 1. create class
    //public class Building
    //{
    //    public string Name { get; set; }
    //    public string address { get; set; }
    //    public int NumberOffFloors { get; set; }
    //    public Double Area { get; set; }

    //    //3. Add constructor to class
    //    public Building(string _name, string _address, int _numberOffFloors, Double _area)
    //    {
    //        Name = _name;
    //        address = _address;
    //        NumberOffFloors = _numberOffFloors;
    //        Area = _area;
    //    }
    //}
}
