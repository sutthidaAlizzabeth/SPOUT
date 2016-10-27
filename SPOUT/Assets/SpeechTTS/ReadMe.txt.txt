Thank you for Purchasing Android Speech Recognizer and Text To Speech Plugin

Here's something you need to know before you get started, this instructions are seems a lot but if you are in hurry just export the plugin in your project and follow the sample code in each demo inside the Android Ultimate plugin folder just dont forget to put permissions on your Android manifest and you are good to go. you can always come back here and read this guides if you are having
some trouble.

Requirements:

this plugin needs the following inside the Assets/Plugins/Android folder

1. SpeechTTS jar file
2. Android Manifest File- including permissions


How to test:

1. Look for SampleAndroidManifest folder, inside that there's an Android Manifest if you don't have a Android manifest file just copy it and paste it inside Assets/Plugins/Android

2. change android bundle identifier to your own package example: com.gigadrillgames.speechtts

3. Browse Assets/SpeechTTS/scenes inside this folder you will see a demo scene, try to load it and compile on android device to see the Speech Recognizer and Text To Speech Plugin in action.

4. in this demo scene you will noticed many canvas each canvas demonstrate on how to used specific task  or features.

5. inside Assets/SpeechTTS/Scripts you will find an example folder inside that you will see example codes this scripts shows on how to use Speech Recognizer and Text To Speech Plugin.

Important:
you will noticed when testing the plugin on actual android device you will see Toast Message showing every time when you do some task to remove this toast messages just set the debug to 0
or when you are done testing you should turn the debug to 0 to remove irrelevant toast messages from appearing.

Note:
some of the task that you will do require some permissions without this permission on your manifest there will be 2 scenarios.

1. it will not work.
2. your app might crashed.

The Following Permissions are needed to make Speech Recognizer and Text To Speech Plugin work

<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.READ_INTERNAL_STORAGE" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />    	

<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.RECORD_AUDIO" />


For questions,clarifications,concerns,comments and suggestions 
just email us at gigadrillgames@gmail.com

For more informations:
http://www.gigadrillgames.com/android-ultimate-plugin/

For more Tutorials:
http://www.gigadrillgames.com/2015/07/26/list-of-tutorials-for-android-ultimate-plugin/

Frequently asked questions:
http://www.gigadrillgames.com/2015/07/29/faq-android-ultimate-plugin/
