using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RevitAddinBootcamp
{
    public class AppSkills : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            //1. Create New Tab
            string tabName = "My Tab";
            app.CreateRibbonTab(tabName);
            //Safer method for tab creation
            try
            {
                app.CreateRibbonTab(tabName);
            }
            catch (Exception error)
            {
                Debug.Print("Tab Already Exists");
                Debug.Print(error.Message);
            }

            //2. Create Panel
            string panelName1 = "Panel 1";
            string panelName2 = "Panel 2";
            string panelName3 = "Panel 3";

            RibbonPanel panel = app.CreateRibbonPanel(tabName, panelName1);
            RibbonPanel panel2 = app.CreateRibbonPanel(panelName2); // no tabname will place it in the ADDINS tab
            //RibbonPanel panel3 = app.CreateRibbonPanel("Architecture", panelName3);

            //2a. Get Existing Panel
            List<RibbonPanel> panelList = app.GetRibbonPanels();
            List<RibbonPanel> panelList2 = app.GetRibbonPanels(tabName);

            //2b. Create/Get panel method - Safe Method
            RibbonPanel panel4 = CreateGetPanel(app, tabName, panelName1);

            // 3. Create Button Data
            PushButtonData buttonData1 = new PushButtonData(
                "button1",
                "Command 1",
                Assembly.GetExecutingAssembly().Location,
                "RevitAddinBootcamp.Command1"
                );

            PushButtonData buttonData2 = new PushButtonData(
                "button2",
                "Button\rCommand 2",
                Assembly.GetExecutingAssembly().Location,
                "RevitAddinBootcamp.Command2"
                );

            //4. Add tooltips
            buttonData1.ToolTip = "This is Command 1";
            buttonData2.ToolTip = "This is Command 2";

            //5. Add Images to buttons
            buttonData1.LargeImage = ConvertToImageSource(Properties.Resources.Green_32);
            buttonData1.Image = ConvertToImageSource(Properties.Resources.Green_16);
            buttonData2.LargeImage = ConvertToImageSource(Properties.Resources.Blue_32);
            buttonData2.Image = ConvertToImageSource(Properties.Resources.Blue_16);

            //6. Creat Push Botton
            PushButton button1 = panel.AddItem(buttonData1) as PushButton;
            PushButton button2 = panel.AddItem(buttonData2) as PushButton;

            return Result.Succeeded;
        }

        private RibbonPanel CreateGetPanel(UIControlledApplication app, string tabName, string panelName1)
        {
            // Look for panel in tab
            foreach (RibbonPanel panel in app.GetRibbonPanels(tabName))
            {
                if (panel.Name == panelName1)
                {
                    return panel;
                }
            }

            // If panel not found create it
            //RibbonPanel returnPanel = app.CreateRibbonPanel(tabName, panelName1);
            //return returnPanel;

            // Line saver
            return app.CreateRibbonPanel(tabName, panelName1);
        }

        public Result OnShutdown(UIControlledApplication app)
        {
            return Result.Succeeded;
        }

        public BitmapImage ConvertToImageSource(byte[] imageData)
        {
            using (MemoryStream mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.StreamSource = mem;
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.EndInit();
            }

            return null;
        }

    }
}
