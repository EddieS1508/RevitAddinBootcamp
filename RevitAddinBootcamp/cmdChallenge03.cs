using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.DB.Structure;
using RevitAddinBootcamp.Common;

namespace RevitAddinBootcamp
{
    [Transaction(TransactionMode.Manual)]
    public class cmdChallenge03 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Revit application and document variables
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Your Module 03 Challenge code goes here
            // create instance of classes for each room
            Rooms classroom1 = new Rooms("Classroom", "Desk", "Teacher", 1);
            Rooms classroom2 = new Rooms("Classroom", "Desk", "Student", 6);
            Rooms classroom3 = new Rooms("Classroom", "Chair-Desk", "Default", 7);
            Rooms classroom4 = new Rooms("Classroom", "Shelf", "Large", 1);

            Rooms office1 = new Rooms("Office", "Desk", "Teacher", 1);
            Rooms office2 = new Rooms("Office", "Desk", "Student", 1);
            Rooms office3 = new Rooms("Office", "Chair-Desk", "Default", 1);
            Rooms office4 = new Rooms("Office", "Shelf", "Small", 1);

            Rooms vrlab1 = new Rooms("VR Lab", "Table-Rectangular", "Small", 1);
            Rooms vrlab2 = new Rooms("VR Lab", "Table-Rectangular", "Large", 8);
            Rooms vrlab3 = new Rooms("VR Lab", "Chair-Desk", "Default", 9);

            Rooms computerlab1 = new Rooms("Computer Lab", "Desk", "Teacher", 1);
            Rooms computerlab2 = new Rooms("Computer Lab", "Desk", "Student", 10);
            Rooms computerlab3 = new Rooms("Computer Lab", "Chair-Desk", "Default", 11);
            Rooms computerlab4 = new Rooms("Computer Lab", "Shelf", "Large", 2);

            Rooms studentlounge1 = new Rooms("Student Lounge", "Sofa", "Large", 2);
            Rooms studentlounge2 = new Rooms("Student Lounge", "Table-Coffee", "Square", 2);
            Rooms studentlounge3 = new Rooms("Student Lounge", "Sofa", "Small", 2);
            Rooms studentlounge4 = new Rooms("Student Lounge", "Table-Coffee", "Large", 1);

            Rooms waiting1 = new Rooms("Waiting", "Chair-Waiting", "Default", 2);
            Rooms waiting2 = new Rooms("Waiting", "Table-Coffee", "Large", 1);

            List<Rooms> rooms = new List<Rooms>();
            rooms.Add(classroom1);
            rooms.Add(classroom2);
            rooms.Add(classroom3);
            rooms.Add(classroom4);
            rooms.Add(office1);
            rooms.Add(office2);
            rooms.Add(office3);
            rooms.Add(office4);
            rooms.Add(vrlab1);
            rooms.Add(vrlab2);
            rooms.Add(vrlab3);
            rooms.Add(computerlab1);
            rooms.Add(computerlab2);
            rooms.Add(computerlab3);
            rooms.Add(computerlab4);
            rooms.Add(studentlounge1);
            rooms.Add(studentlounge2);
            rooms.Add(studentlounge3);
            rooms.Add(studentlounge4);
            rooms.Add(waiting2);
            rooms.Add(waiting1);

            int famCount = 0;
            // Collect all rooms
            FilteredElementCollector roomCollector = new FilteredElementCollector(doc);
            roomCollector.OfCategory(BuiltInCategory.OST_Rooms);

            using Transaction t = new Transaction(doc);
            {
                t.Start("Insert Families into Rooms");
                // cycle through rooms, match and insert families

                foreach (Room CurrRoom in roomCollector)
                {
                    foreach (Rooms room in rooms)
                    {
                        string roomName = room.Roomname;
                        if (CurrRoom.Name.Contains(roomName))
                        {
                            FamilySymbol currFamSymbol = GetFamilySymbolByName(doc, room.familyName, room.familyType);
                            currFamSymbol.Activate();

                            LocationPoint loc = CurrRoom.Location as LocationPoint;

                            // loop through quantity req'd
                            for (int i = 1; i <= room.familyQuantity; i++)
                            {
                                FamilyInstance CurrFamInstance = doc.Create.NewFamilyInstance(loc.Point, currFamSymbol, StructuralType.NonStructural);
                            }
                            

                            famCount = famCount + 1;
                        }
                    }
                    Utils.SetParameterValue(CurrRoom, "Furniture Count", famCount.ToString());
                }
                t.Commit();
            }



            return Result.Succeeded;
        }

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

        internal static PushButtonData GetButtonData()
        {
            // use this method to define the properties for this command in the Revit ribbon
            string buttonInternalName = "btnChallenge03";
            string buttonTitle = "Module\r03";

            Common.ButtonDataClass myButtonData = new Common.ButtonDataClass(
                buttonInternalName,
                buttonTitle,
                MethodBase.GetCurrentMethod().DeclaringType?.FullName,
                Properties.Resources.Module03,
                Properties.Resources.Module03,
                "Module 03 Challenge");

            return myButtonData.Data;
        }
    }


    // create classes
    public class Classroom
    {
        public string Roomname { get; set; }
        public string familyName { get; set; }
        public string familyType { get; set; }
        public int familyQuantity { get; set; }

        //3. Add constructor to class
        public Classroom(string _roomname, string _famname, string _famtype, int _famQty)
        {
            Roomname = _roomname;
            familyName = _famname;
            familyType = _famtype;
            familyQuantity = _famQty;
        }
    }

    public class Rooms
    {
        public string Roomname { get; set; }
        public string familyName { get; set; }
        public string familyType { get; set; }
        public int familyQuantity { get; set; }

        //3. Add constructor to class
        public Rooms(string _roomname, string _famname, string _famtype, int _famQty)
        {
            Roomname = _roomname;
            familyName = _famname;
            familyType = _famtype;
            familyQuantity = _famQty;
        }
    }

}
