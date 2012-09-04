

using System;
namespace XERP.Client.WPF.MainMenu
{
    class WindowLocator
    {
        private enum _executableProgramEnum {CompanyMaintenance, CompanyTypeMaintenance, MenuItemMaintenance,
        SystemUserMaintenance};

        private System.Windows.Window window;
        public WindowLocator()
        {
            
        }

        public void ShowWindow(string executableProgramID, out string errorMessage)
        {
            errorMessage = "";
            _executableProgramEnum executableProgramEnum;
            if (Enum.IsDefined(typeof(_executableProgramEnum), executableProgramID))
            {
                executableProgramEnum = (_executableProgramEnum)Enum.Parse(typeof(_executableProgramEnum), executableProgramID, true);
                
            }
            else
            {
                errorMessage = "Make sure the WindowLocator executableProgramEnum value matches to the ExecutableProgramID " + executableProgramID + " value";
                return;
            } 
            
            SelectWindowToShow(executableProgramEnum);  
        }

        private void SelectWindowToShow(_executableProgramEnum executableProgramEnum)
        {
            switch (executableProgramEnum)
            {
                case _executableProgramEnum.CompanyMaintenance:
                    window = new CompanyMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.CompanyTypeMaintenance:
                    window = new CompanyMaintenance.TypeMaintenancneWindow();
                    window.Show();
                    break;
            }
        }
    }
}
