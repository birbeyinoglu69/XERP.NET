﻿using System;
namespace XERP.Client.WPF.MainMenu
{
    class WindowLocator
    {
        private enum _executableProgramEnum {CompanyMaintenance, CompanyTypeMaintenance, CompanyCodeMaintenance, 
            MenuItemMaintenance,
            SystemUserMaintenance, SystemUserTypeMaintenance, SystemUserCodeMaintenance,
            SecurityGroupMaintenance, SecurityGroupTypeMaintenance, SecurityGroupCodeMaintenance
        };

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
                errorMessage = "Make sure the WindowLocator.cs Class executableProgramEnum value matches to the ExecutableProgramID " + executableProgramID + " value";
                return;
            } 
            
            SelectWindowToShow(executableProgramEnum, out errorMessage);  
        }

        private void SelectWindowToShow(_executableProgramEnum executableProgramEnum, out string errorMessage)
        {
            errorMessage = "";
            switch (executableProgramEnum)
            {
                case _executableProgramEnum.CompanyMaintenance:
                    window = new CompanyMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.CompanyTypeMaintenance:
                    window = new CompanyMaintenance.TypeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.CompanyCodeMaintenance:
                    window = new CompanyMaintenance.CodeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.SystemUserMaintenance:
                    window = new SystemUserMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.SystemUserTypeMaintenance:
                    window = new SystemUserMaintenance.TypeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.SystemUserCodeMaintenance:
                    window = new SystemUserMaintenance.CodeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.SecurityGroupMaintenance:
                    window = new SecurityGroupMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.SecurityGroupTypeMaintenance:
                    window = new SecurityGroupMaintenance.TypeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.SecurityGroupCodeMaintenance:
                    window = new SecurityGroupMaintenance.CodeMaintenanceWindow();
                    window.Show();
                    break;
                default:
                    errorMessage = "No Executable Program Was Assigned To Be Shown In The WindowLocator.cs class";
                    break;
            }
        }
    }
}