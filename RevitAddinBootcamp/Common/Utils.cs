namespace RevitAddinBootcamp.Common
{
    internal static class Utils
    {
        // cmdSkill03 methods
        public static string GetParameterValueAsString(Element currElem, string paramName)
        {
            Parameter currParam = currElem.LookupParameter(paramName);
            Parameter currParam2 = currElem.get_Parameter(BuiltInParameter.ROOM_AREA);

            if (currParam != null)
            {
                return currParam.AsString();
            }
            else
                return "";
        }

        public static string GetParameterValueAsString(Element curElem, BuiltInParameter bip)
        {
            Parameter curParam = curElem.get_Parameter(bip);

            if (curParam != null)
            {
                return curParam.AsString();
            }
            else
                return "";
        }

        public static double GetParameterValueAsDouble(Element currElem, string paramName)
        {
            Parameter currParam = currElem.LookupParameter(paramName);
            //Parameter currParam2 = currElem.get_Parameter(BuiltInParameter.ROOM_AREA);

            if (currParam != null)
            {
                return currParam.AsDouble();
            }
            else
                return 0;
        }

        public static double GetParameterValueAsDouble(Element curElem, BuiltInParameter bip)
        {
            Parameter curParam = curElem.get_Parameter(bip);

            if (curParam != null)
            {
                return curParam.AsDouble();
            }
            else
                return 0;
        }

        public static void SetParameterValue(Element curElem, string ParaName, string value)
        {
            Parameter curParam = curElem.LookupParameter(ParaName);

            if (curParam != null)
            {
                curParam.Set(value);
            }
        }

        private static void SetParameterValue(Element curElem, string ParaName, int value)
        {
            Parameter curParam = curElem.LookupParameter(ParaName);

            if (curParam != null)
            {
                curParam.Set(value);
            }
        }

        // Ribbon methods
        internal static RibbonPanel CreateRibbonPanel(UIControlledApplication app, string tabName, string panelName)
        {
            RibbonPanel curPanel;
            if (GetRibbonPanelByName(app, tabName, panelName) == null)
                curPanel = app.CreateRibbonPanel(tabName, panelName);
            else
                curPanel = GetRibbonPanelByName(app, tabName, panelName);
            return curPanel;
        }
        internal static RibbonPanel GetRibbonPanelByName(UIControlledApplication app, string tabName, string panelName)
        {
            foreach (RibbonPanel tmpPanel in app.GetRibbonPanels(tabName))
            {
                if (tmpPanel.Name == panelName)
                    return tmpPanel;
            }
            return null;
        }
    }
}
