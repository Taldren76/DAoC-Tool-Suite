DAoC Tool Suite 0.9.9.0 Release Candidate 3
---
Changes:
1. Renamed Account to ChimpPages

Fixes:
1. Will no longer crash if you attempt to associate an ahk with no character selected.
2. Lock Overlay will actually lock the overlay.
3. Add ChimpPage will be greyed out unless the name entered in the ChimpPage combobox is unique.
4. Export Json will no longer leave BackupDB greyed out.
5. Import Json will no longer leave the chimptool table hidden from view when complete.

DAoC Tool Suite 0.9.8 Release Candidate 2
---
Important Note: It seems it is best practice to uninstall the previous version rather than installing the new one ovetop the old. Make a DB Backup (or Export) in ChimpTool and CharacterTool and uninstall it completely. Then install the new version and perform a DB Restore (or Import) in ChimpTool and CharacterTool.

Changes:
1. Added new contect menu option to move a character between accounts in ChimpTool.

Fixes:
1. None - none reported. :)

DAoC Tool Suite 0.9.7 Release Candidate 1
---
Changes:
1. Added new contect and menu options to restore settings for selected character.

Fixes:
1. Fixed issue where if you change the text line for the Game.DLL folder location in the text box and not actually browse to the location using the browse button, it doesn't actually change and will try with the wrong game location.

DAoC Tool Suite BETA 0.9.6
---
Changes:
1. ChimpTool - Added RightClick context menu.
2. ChimpTool - Added Account Management under Account menu. You can now rename your accounts.

Bug Fixes:
1. The ChimpTool table is now readonly.

DAoC Tool Suite BETA 0.9.5
---
Changes:
1. Moved all data into User's AppData folder to avoid the evil that is the VirtualStore folder.
2. Removed the bespoke Settings Manager in favor of Winform's built in Settings manager.
3. Added the ability to Launch 2 characters at one time from two different accounts. Only one instance of AHK can be active at one time, so the first character launched with an associated AHK script will have that script active for both characters.
4. Added a 2 minute clock when the game.dll closes. This will not block the Launch button , but may be used after a crash as an indication when you can log back in.

Bug Fixes:
1. Fixed refresh issue.
2. Fixed some edge conditions around characters with 0 RPs and no realm rank.

DAoC Tool Suite BETA 0.9.2
---
Changes:
1. ChimpTool - Fixed the Launch issue. 
2. ChimpTool - Added the ability to associate an AHK script with a character. You may add one script per character per account. You do not need AHK installed to use the scripts as it is fully integrated into ChimpTool. Just besure the that your script does not contain #Warn and is version 1.1 compatible. Once associated, AHK will be running the script until you quit the game. This means you can't swap scripts without completely leaving the game as logging out to the character select screen will still have the last character selected's AHK script still active.
3. ChimpTool - Added a menu. Still fleshing out the options so not everything is functional.

DAoC Tool Suite 0.9.1 BETA
---
Changes:
1. Users can now launch characters directly into game from ChimpTool via the launch button. Will prompt you for the game.dll directory and your login credentials. These are stored in your local database file and are not backed up to the json file in your documents folder. If you restore the DB you will have to re-enter the credentials. If you mess up your credentials, you can change them by Shift-Clicking the Launch button.
2. Each account in the ChimpTool has its own DAOC credentials. So you can setup multiple accounts for each of your DAoC accounts.
3. Vastly improved the clarity of the text in the LogTool Overlay.
4. Added the ability to customize what statistics are shown in the LogTool Overlay.

Bug Fixes:
1. Many localization fixes (Thanks Koxi)
2. Code cleanup pass.

I recommend uninstalling the previous version before installing the new one. Be sure to create backups of your DBs in ChimpTool and CharacterTool.

Two files are available:
1. DAoC_Tool_Suite_Beta_0.9.1.zip - Can be unzipped anywhere are run.
2. DAoC_Tool_Suite_Beta_0.9.1_WinInstaller.zip - Full windows installer.


DAoC Tool Suite Beta v0.7.1
---
CharacterTool Changes:
1. Added the ability to backup all INI files. Please pay attention to the warning next to the Save All button for details.
2. Added the ability to Edit backup records in the database.
3. Added the ability to Restore or Delete multiple records at a time.

Bug Fixes:
Yes.


DAoC Tool Suite Beta v0.7.0
---
Changes:
1. Completed refactor of the logparser statistics and overlay.
2. You may now close the launcher and it won't kill the other tools.

Bug Fixes:
Too many to count.

As always, Backup your databases in Chimp/Character tool BEFORE upgrading.


DAoC Tool Suite BETA 0.6.2.0
---
Bug Fixes:
1. Fixed issue where LogParser main display will be all white on first launch. (FOR REAL THIS TIME)
2. Performance improvement during active parsing.


DAoC Tool Suite BETA 0.6.1.0
---
New:
1. Added a ProgressBar to the Log Parser
2. Last parsed file is now saved upon closing Log Parser.

Bug Fixes:
1. Fixed issue where LogParser main display will be all white on first launch.


DAoC Tool Suite v0.4.0 BETA
---
Fixes a race condition that could crash the app when closing the ChimpTool or CharacterTool forms.
Stability improvements.
Perforamnce improvements.


DAoC Tool Suite v0.3.0 BETA Release
---
This suite includes a new version of ChimpTool that can deal with the Heralds SSL issues and still function. 
Also included is a newer version of my DAoC Tools application for managing character settings.
Still in active development is the DAoC Log Parser Tool which did not make the cut for this realse.

If you are a ChimpTool user and wish to use this applicaiton, Backup your DB in the ChimpTool application and uninstall the program. Install DAoC Tool Suite, launch ChimpTool from the app, and Restore the DB.
