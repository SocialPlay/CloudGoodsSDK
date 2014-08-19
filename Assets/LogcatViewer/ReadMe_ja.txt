============================================================
 		LogcatViewer  ver1.0
============================================================

概要:
LogcatViewerはAndroid端末のログをUnityEditorで確認できます。


*----*----*----*----*----*----*----*
特徴:
	Unityアプリのログのみに限定できます
	出力する行数を指定出来ます(100行～5000行)
	表示する項目を切り替えられます。
	フォントサイズを変更できます。
	文字とログタイプでフィルタリング出来ます。


*----*----*----*----*----*----*----*
使い方:
	メニューの　Window > LogcatViewer　からLogcatViewerWIndowを開く

	●Once logcatボタン
		logcatを1度だけ実行します。 adb logcat -d と同じコマンドです。

	●Start logcatボタン
		継続的にlogを出力します。停止はStop logcatボタンを押して下さい。

	●Connect devices Listボタン
		PCに接続されたAndroid端末の一覧を表示します。adb devicesコマンドと同じです。
		offlineだとログが表示されません。

	●adb Restartボタン
		 'adb kill-server' and 'adb start-server'コマンドと同じです。
		adb Restartはデバイスが認識されない、offlineなどの時に使うと認識される場合があります。




*----*----*----*----*----*----*----*
動作環境:
	非Proでも動きます。
	Unity3，4でも動作します。
	
	事前にAndroid SDKのインストールと
	Unity Preferences > External Tools > Android SDK Locationを設定しておく必要があります。
