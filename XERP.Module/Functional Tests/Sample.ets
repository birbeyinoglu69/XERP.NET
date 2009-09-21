;#DropDB XERPEasyTest

#Application XERPWin
#Application XERPWeb

*Action Navigation(User)

*ProcessRecord
 Full Name = ''

#IfDef XERPWeb
*Action Edit
#EndIf
 
*FillForm
 Full Name = Test User
 
*Action Cancel

#IfDef XERPWin
*HandleDialog
 Respond = Yes
#EndIf

*CheckFieldValues
 Full Name = ''
