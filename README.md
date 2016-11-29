# gifsVR
Gifs streaming app for the GearVR/Oculus

<h1>Setup</h1>
1. Get android SDK (Android studio)
2. Enable dev mode on your android device & USB debugging
3. Get oculus signature file (you need device id: use cmd on windows "adb devices") from https://dashboard.oculus.com/tools/osig-generator
and put it in Assets/Plugins/Android/assets
4. Create key sore and key for the app in the Unity player preferences
5. Build and Run app.

<h1>Quick code guide</h2>
The main logic is within AnimatedGifDrawer.cs attached to the plane.
<code>public string url = "http://192.168.0.14/";</code>
