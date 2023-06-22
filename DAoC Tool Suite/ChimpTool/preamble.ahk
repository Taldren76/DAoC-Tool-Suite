; --------------------------------------------------------------------------------
; AHK preamble for DAoC  by Madgrim Laeknir (BelomarFleetfoot#0319).
; June 1, 2021 - Initial version.
; June 3, 2021 - Added key delay directive.
; --------------------------------------------------------------------------------

#SingleInstance, Force        ; Force a single instance of the script
#NoEnv                      ; For performance and compatibility with future AHK releases.
SendMode Event                ; To make this work with DAoC. ("Input" does not seem to work well.)
SetTitleMatchMode, 2        ; Match parts of the window title.
SetKeyDelay 30, 30            ; Control timing between consecutive key presses

; -- Variables
global HotKeysEnabled     := true
#MenuMaskKey vk00sc000
; -- Initialize the GUI
Gui,+Owner                            ; Prevent the window from having a taskbar icon
Gui,+AlwaysOnTop                    ; Make sure it is always on top (of DAoC)
Gui, Color, Lime                    ; Set the active color as window background
Gui, Font, S10, Helvetica            ; Set a nice font for any text
Gui,-Caption                        ; Remove the window title
Gui, Add, Button, x5 y5 gexit, X     ; Create the close button (calls exit)
Gui, Add, Text, x32 y12, % CharName    ; Set the character name
Gui, Show, x10 y10 w100 h36, DAoC    ; Now create the window
WinSet, Transparent, 200, DAoC        ; Make the window semi-transparent
Return 

; -- Close the script when window is closed
GuiClose:
exit:
{
    ExitApp
}

; -- Toggle hotkeys on and off
PrintScreen::
    Suspend
    if (HotKeysEnabled) {
        Gui, Color, Red
        HotKeysEnabled := false
    }
    else {
        Gui, Color, Lime
        HotKeysEnabled := true
    }
    return

; -- Reload the script using Ctrl+Alt+R
^!r::Reload