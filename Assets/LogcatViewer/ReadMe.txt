============================================================
 		LogcatViewer  ver1.0
============================================================

Summary:
	LogcatViewer can be seen  the log of Android devices within UnityEditor .
	
	
	
*----*----*----*----*----*----*----*
Features: 
	Only Unity app logs.
	Specify the number of lines to output (100 ~5000 lines)
	Switch between the items to be displayed.
	Change the font size.
	Filter the log type and text.


*----*----*----*----*----*----*----*
Usage:
	- Open the window by choosing the menu Window > LogcatViewer

	- Press 'Once logcat' Button
		 run a once logcat command. This is the same command as 'adb logcat -d'.

	- Press 'Start logcat' Button
		Start logging.
		Press the Stop logcat button to stop.

	- Press 'Connect devices List' Button
		Show  a list of Android devices that are connected to the PC.
		This is the same command as 'adb devices'.
		offline state  can not displayed log.


	- Press 'adb Restart' Button
		This is the same command as 
		 'adb kill-server' and 'adb start-server'.




*----*----*----*----*----*----*----*
Environment:
	Works on non-Pro. Works on Unity 3 and 4.
	You need to set the Android SDK Location of External Tools of Unity Preferences.
	
	
