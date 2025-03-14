#SingleInstance Force

; Gotta be admin to change adapter settings. Snippet from the docs (in Variables)
; or shajul's, I don't know anymore: http://www.autohotkey.com/board/topic/46526-run-as-administrator-xpvista7-a-isadmin-params-lib/
; TODO: not working if compiled?
if not A_IsAdmin
{
	if A_IsCompiled
		DllCall("shell32\ShellExecuteA", uint, 0, str, "RunAs", str, A_ScriptFullPath, str, "", str, A_WorkingDir, int, 1)
			; note the A, no dice without it, don't know of side effects
	else
		DllCall("shell32\ShellExecute", uint, 0, str, "RunAs", str, A_AhkPath, str, """" . A_ScriptFullPath . """", str, A_WorkingDir, int, 1)
		
	ExitApp
}


presets_ini_file := A_ScriptDir "\presets.ini"
interfaces_tmpfile := A_ScriptDir "\tmp.txt"


Gui, Add, Text, x12 y9 w120 h20 , Interfaces
Gui, Add, ListBox, x12 y29 w120 h120 vinterface gupdate_cmd, % get_interfaces_list(interfaces_tmpfile)

Gui, Add, Text, x12 y149 w120 h20 , Presets
Gui, Add, ListBox, x12 y169 w120 h120 vpreset gpreset_select Hwndpresets_hwnd, % ini_get_sections(presets_ini_file)

Gui, Add, GroupBox, x152 y9 w260 h120 , IP
Gui, Add, CheckBox, x162 y29 w70 h20 vip_ignore gip_toggle, ignore
Gui, Add, CheckBox, x242 y29 w70 h20 vip_auto gip_toggle, dhcp
Gui, Add, Text, x162 y59 w80 h20 , gateway
Gui, Add, Edit, x242 y59 w120 h20 vgateway gupdate_cmd, 192.168.0.1
Gui, Add, Text, x162 y79 w80 h20 , computer
Gui, Add, Edit, x242 y79 w120 h20 vcomp_ip gupdate_cmd, 192.168.0.2
Gui, Add, Text, x162 y99 w80 h20 , netmask
Gui, Add, Edit, x242 y99 w120 h20 vnetmask gupdate_cmd, 255.255.255.0

Gui, Add, GroupBox, x152 y139 w260 h100 , DNS
Gui, Add, CheckBox, x160 y160 w70 h20 vdns_ignore gdns_toggle, ignore
Gui, Add, CheckBox, x242 y159 w70 h20 checked vdns_auto gdns_toggle, auto
Gui, Add, Button, x312 y159 w90 h20 gset_google_dns, Google DNS
Gui, Add, Text, x162 y189 w80 h20 , server 1
Gui, Add, Edit, x242 y189 w120 h20 vdns_1 gupdate_cmd, 8.8.8.8
Gui, Add, Text, x162 y209 w80 h20 , server 2
Gui, Add, Edit, x242 y209 w120 h20 vdns_2 gupdate_cmd, 8.8.4.4

;Gui, Add, Text, x12 y279 w120 h20 , Cmd
Gui, Add, Edit, x12 y299 w400 h50 vcmd, Edit
Gui, Add, Button, x420 y299 w80 h48 grun_cmd, Run

Gui, Add, Button, x432 y19 w60 h30 gsave, Save to Presets
Gui, Add, Button, x432 y50 w60 h30 gdel, Delete this Preset

; Generated using SmartGUI Creator 4.0

gosub, ip_toggle
gosub, dns_toggle

Gui, +Hwndgui_hwnd

Menu, Tray, NoStandard
Menu, Tray, Tip, IP vahetaja
Menu, Tray, Add, Open, open
Menu, Tray, Add, Exit, #^!+Esc

Return

; end of autoexec ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

open:
{
    Gui, Show
}

#^!+Esc::ExitApp

get_interfaces_list(tmp_file) {
	filedelete, % tmp_file
	runwait, %comspec% /c "For /f "skip=3 tokens=3*" `%G In ('netsh interface show interface') Do echo `%H>> %tmp_file%", % A_ScriptDir, hide
	fileread, interfaces, % tmp_file
	;filedelete, % tmp_file  ; don't leave nothing in the dir
	stringreplace, interfaces, interfaces, `r`n, |, all
	stringreplace, interfaces, interfaces, |, ||  ; preselect first
	return interfaces
}



; ip + dns 

ip_toggle:
	gui, submit, nohide
	if ip_ignore
		guicontrol, disable, ip_auto
	else
		guicontrol, enable, ip_auto
		
	if (ip_ignore or ip_auto)
		action := "disable"
	else
		action := "enable"

	guicontrol, %action%, gateway
	guicontrol, %action%, comp_ip
	guicontrol, %action%, netmask
	gosub, update_cmd
return

dns_toggle:
	gui, submit, nohide
	if dns_ignore
		guicontrol, disable, dns_auto
	else
		guicontrol, enable, dns_auto
		
	if (dns_ignore or dns_auto)
		action := "disable"
	else
		action := "enable"

	guicontrol, %action%, dns_1
	guicontrol, %action%, dns_2
	gosub, update_cmd
return

set_google_dns:
	dns_1 := "8.8.8.8"
	dns_2 := "8.8.4.4"
	guicontrol,, dns_1, % dns_1
	guicontrol,, dns_2, % dns_2
	gosub, update_cmd
return



; gui-initiated stuff

update_cmd:
	gui, submit, nohide
	cmd := ""
	if not ip_ignore
	{
		if ip_auto
			cmd .= "netsh interface ip set address """ interface """ dhcp & "
		else
			cmd .= "netsh interface ipv4 set address name=""" interface """ source=static address=" comp_ip " mask=" netmask " gateway=" gateway " & "
	}

	if not dns_ignore
	{
		if dns_auto
			cmd .= "netsh interface ip set dns """ interface """ dhcp & "
		else
		{
			if dns_1
				cmd .= "netsh interface ip set dns name=""" interface """ static " dns_1 " & "
			if dns_2
				cmd .= "netsh interface ip add dns name=""" interface """ addr=" dns_2 " index=2 & "
		}	
	}
	
	cmd := regexreplace(cmd, "& $", "")
	guicontrol,, cmd, % cmd
return


update_gui:
	controls = 
	(
		ip_ignore
		ip_auto
		gateway
		comp_ip
		netmask
		dns_ignore
		dns_auto
		dns_1
		dns_2
	)

	loop, parse, controls, `n, `r%A_Tab%%A_Space%
		guicontrol,, %A_LoopField%, % %A_LoopField%
	
	gosub, ip_toggle
	gosub, dns_toggle
	gosub, update_cmd
return


preset_select:
	gui, submit, nohide
	
	iniread, ip_ignore, % presets_ini_file, % preset, ip_ignore, 0
	if not ip_ignore
	{
		iniread, ip_auto, % presets_ini_file, % preset, ip_auto, 0
		if not ip_auto
		{
			iniread, gateway, % presets_ini_file, % preset, gateway, 
			iniread, comp_ip, % presets_ini_file, % preset, comp_ip, 
			iniread, netmask, % presets_ini_file, % preset, netmask, 
		}
	}
	
	iniread, dns_ignore, % presets_ini_file, % preset, dns_ignore, 0
	if not dns_ignore
	{
		iniread, dns_auto, % presets_ini_file, % preset, dns_auto, 0
		if not dns_auto
		{
			iniread, dns_1, % presets_ini_file, % preset, dns_2, 
			iniread, dns_2, % presets_ini_file, % preset, dns_1, 
		}
	}
	
	gosub, update_gui
	
	if (A_GuiEvent == "DoubleClick")
		gosub, run_cmd

return

NotifyTrayClick_201: ; Left click (Button down)
{
    Gui, Show
    Return
}

run_cmd:
	gui, submit, nohide
	RunWait, %comspec% /c %cmd%
return


save:
	gui, submit, nohide
	inputbox, name, name of entry, name of entry,,,,,,,, % gateway
	if ErrorLevel
	{
		return
	}

	; check if already exists
	current_sections := ini_get_sections(presets_ini_file)
	loop, parse, current_sections, |
	{
		if (name == A_LoopField)
		{
			msgbox There is an entry called %name% already.`nChoose another name.
			return
		}
	}
	
	iniwrite, % ip_ignore, % presets_ini_file, % name, ip_ignore
	if not ip_ignore
	{
		iniwrite, % ip_auto, % presets_ini_file, % name, ip_auto
		if not ip_auto
		{
			iniwrite, % gateway, % presets_ini_file, % name, gateway
			iniwrite, % comp_ip, % presets_ini_file, % name, comp_ip
			iniwrite, % netmask, % presets_ini_file, % name, netmask
		}
	}
	
	iniwrite, % dns_ignore, % presets_ini_file, % name, dns_ignore
	if not dns_ignore
	{
		iniwrite, % dns_auto, % presets_ini_file, % name, dns_auto
		if not dns_auto
		{
			iniwrite, % dns_1, % presets_ini_file, % name, dns_1
			iniwrite, % dns_2, % presets_ini_file, % name, dns_2
		}
	}
	
	guicontrol,, preset, % name  ; TODO: select new entry?
	GuiControl, ChooseString, preset, % name
return

del:
	gui, submit, nohide
	ini_delete_section(presets_ini_file, preset)
	guicontrol,, preset, % "|" ini_get_sections(presets_ini_file)
return


; Left Click funktisoon
NotifyTrayClick(P*) {              ;  v0.41 by SKAN on D39E/D39N @ tiny.cc/notifytrayclick
    Static Msg, Fun:="NotifyTrayClick", NM:=OnMessage(0x404,Func(Fun),-1),  Chk,T:=-250,Clk:=1
      If ( (NM := Format(Fun . "_{:03X}", Msg := P[2])) && P.Count()<4 )
         Return ( T := Max(-5000, 0-(P[1] ? Abs(P[1]) : 250)) )
      Critical
      If ( ( Msg<0x201 || Msg>0x209 ) || ( IsFunc(NM) || Islabel(NM) )=0 )
         Return
      Chk := (Fun . "_" . (Msg<=0x203 ? "203" : Msg<=0x206 ? "206" : Msg<=0x209 ? "209" : ""))
      SetTimer, %NM%,  %  (Msg==0x203        || Msg==0x206        || Msg==0x209)
        ? (-1, Clk:=2) : ( Clk=2 ? ("Off", Clk:=1) : ( IsFunc(Chk) || IsLabel(Chk) ? T : -1) )
    Return True
}

#include %a_scriptdir%\ini.ahk
#include %a_scriptdir%\shellrun.ahk