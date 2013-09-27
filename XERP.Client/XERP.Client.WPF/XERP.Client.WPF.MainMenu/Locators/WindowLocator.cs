using System;
namespace XERP.Client.WPF.MainMenu
{
    class WindowLocator
    {
        private enum _executableProgramEnum {CompanyMaintenance, CompanyTypeMaintenance, CompanyCodeMaintenance, 
            SystemUserMaintenance, SystemUserTypeMaintenance, SystemUserCodeMaintenance,
            SecurityGroupMaintenance, SecurityGroupTypeMaintenance, SecurityGroupCodeMaintenance,
            AddressMaintenance,
            MenuItemMaintenance, MenuItemTypeMaintenance, MenuItemCodeMaintenance,
            ExecutableProgramMaintenance, ExecutableProgramTypeMaintenance, ExecutableProgramCodeMaintenance,
            UdListMaintenance,
            PlantMaintenance, PlantTypeMaintenance, PlantCodeMaintenance,
            WarehouseMaintenance, WarehouseTypeMaintenance, WarehouseCodeMaintenance,
            WarehouseLocationMaintenance, WarehouseLocationTypeMaintenance, WarehouseLocationCodeMaintenance,
            WarehouseLocationBinMaintenance, WarehouseLocationBinTypeMaintenance, WarehouseLocationBinCodeMaintenance,
            MenuIconMaintenance
        };

        private System.Windows.Window window;
        public WindowLocator()
        {
            
        }

        public void ShowWindow(string executableProgramID, out string errorMessage)
        {
            errorMessage = "";
            if (String.IsNullOrEmpty(executableProgramID))
            {
                errorMessage = "Menu Item Selected Is Executable But Does Not Have An Executable Program Defined.  Check The Menu Maintenance Program For The Selected Menu Item To Ensure One Is Provided.";
                return;
            }

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
                case _executableProgramEnum.AddressMaintenance:
                    window = new AddressMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.MenuItemMaintenance:
                    window = new MenuItemMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.MenuItemTypeMaintenance:
                    window = new MenuItemMaintenance.TypeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.MenuItemCodeMaintenance:
                    window = new MenuItemMaintenance.CodeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.ExecutableProgramMaintenance:
                    window = new ExecutableProgramMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.ExecutableProgramTypeMaintenance:
                    window = new ExecutableProgramMaintenance.TypeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.ExecutableProgramCodeMaintenance:
                    window = new ExecutableProgramMaintenance.CodeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.UdListMaintenance:
                    window = new UdListMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.PlantMaintenance:
                    window = new PlantMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.PlantTypeMaintenance:
                    window = new PlantMaintenance.TypeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.PlantCodeMaintenance:
                    window = new PlantMaintenance.CodeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.MenuIconMaintenance:
                    window = new DBStoredImageMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.WarehouseMaintenance:
                    window = new WarehouseMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.WarehouseTypeMaintenance:
                    window = new WarehouseMaintenance.TypeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.WarehouseCodeMaintenance:
                    window = new WarehouseMaintenance.CodeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.WarehouseLocationMaintenance:
                    window = new WarehouseLocationMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.WarehouseLocationTypeMaintenance:
                    window = new WarehouseLocationMaintenance.TypeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.WarehouseLocationCodeMaintenance:
                    window = new WarehouseLocationMaintenance.CodeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.WarehouseLocationBinMaintenance:
                    window = new WarehouseLocationBinMaintenance.MainWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.WarehouseLocationBinTypeMaintenance:
                    window = new WarehouseLocationBinMaintenance.TypeMaintenanceWindow();
                    window.Show();
                    break;
                case _executableProgramEnum.WarehouseLocationBinCodeMaintenance:
                    window = new WarehouseLocationBinMaintenance.CodeMaintenanceWindow();
                    window.Show();
                    break;
                default:
                    errorMessage = "No Executable Program Was Assigned To Be Shown In The WindowLocator.cs class";
                    break;
            }
        }
    }
}
