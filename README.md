# ALPS ARFoundation-Unity Example Integration Project

<img src="https://img.shields.io/badge/unity-2020.3.38f1-blue"/> <img src="https://img.shields.io/badge/platform-iOS-green"/> <img src="https://img.shields.io/badge/license-MIT-red"/> 

The **Acoustic Location Processing System (ALPS)** by [Yodellabs](https://www.yodellabs.com/) is a markerless indoor positioning system for iOS smartphones and tablets (iPhones and iPads with ARkit compatibility). ALPS uses low-cost, energy harvesting ultrasound beacons to enable high accuracy localization applications such as multi-user augmented reality, fine grained retail analytics and next generation assistive technologies. 

https://user-images.githubusercontent.com/6497727/186274526-404aadf5-463e-43ac-8a1d-4362eee6932e.MOV

*Video of ALPS Running at the MuseumLab of Children's Museum of Pittsburgh, above.*


This repository integrates the ALPS iOS Framework with Unity's ARFoundation by providing an easy-to-use C# wrapper. 
This has been tested to work on Unity 2020.3.38f1, and will likely work on later versions as well. For more information about ARFoundation, please visit this link: https://github.com/Unity-Technologies/arfoundation-samples . ARFoundation has already been installed and configured in this repository, so you shouldn't need to change any settings for ARFoundation, unless you change the Unity version.

## Locations
This library has been configured to work at only **two** locations where the ALPS system has already been physically installed, calibrated and configured. These locations are as follows:

### <u>Carnegie Mellon University Entertainment Technology Center</u>
700 Technology Dr, Pittsburgh, PA 15219
*(1st Floor, Randy Pausch Auditorium)*
![CMU ETC](/ReadmeImages/cmuetc.jpg) <br/>
*Carnegie Mellon University's Entertainment Technology Center, main building.*
![CMU ETC](/ReadmeImages/fp_with_beacons_ETC.png) <br/>
1st Floor plan map, with beacon locations in meters.

|                   |        |       |        | 
|-------------------|--------|-------|--------| 
| ETC Beacons       | x      | y     | z      | 
| 1                 | 7.303  | 2.655 | 22.44  | 
| 2                 | 11.577 | 2.668 | 22.336 | 
| 3                 | 7.291  | 2.639 | 24.886 | 
| 4                 | 11.545 | 2.65  | 24.889 | 
| 5                 | 10.194 | 3.839 | 26.738 | 
| 6                 | 22.588 | 3.866 | 26.737 | 
| 7                 | 35.404 | 3.84  | 26.725 | 
| 8                 | 16.125 | 4.59  | 28.335 | 
| 9                 | 28.97  | 4.591 | 28.321 | 
| 10                | 23.727 | 4.039 | 19.181 | 
| 11                | 34.281 | 4.035 | 19.157 | 
| 12                | 30.022 | 4.036 | 21.644 | 
| 13                | 23.716 | 4.062 | 24.401 | 
| 14                | 34.273 | 3.84  | 26.725 | 

### <u>Children's Museum of Pittsburgh</u>
6 Allegheny Square E Suite 101, Pittsburgh, PA 15212 
*(1st Floor, MuseumLab TechLab)*
![Museum Lab](/ReadmeImages/museumlab.jpg) <br/>
*Children's Museum of Pittsburgh, MuseumLab building.*
![CMU ETC](/ReadmeImages/fp_with_beacons_MuseumLab.png) <br/>
1st Floor plan map, with beacon locations in meters.

|                   |        |       |        | 
|-------------------|--------|-------|--------| 
| MuseumLab Beacons | x      | y     | z      | 
| 1                 | 21.607 | 4.266 | 9.765  | 
| 2                 | 33.305 | 4.291 | 9.781  | 
| 3                 | 22.914 | 4.433 | 15.035 | 
| 4                 | 30.137 | 3.838 | 16.485 | 
| 5                 | 45.697 | 3.764 | 7.487  | 
| 6                 | 45.777 | 3.764 | 19.432 | 




## Running the ALPS app
To use the app, simply run the iOS app. Note that an active internet connection is required to connect to the ALPS positioning system! When the app is active, the installed ALPS beacons should automatically turn on and begin beaconing location data. When the ALPS system is not in use, they should automatically turn on and go to standby mode after 100 seconds of no activity.

![Museum Lab](/ReadmeImages/ALPS_sysadmin.png) <br/>

If for whatever reason the ALPS beacons do not automatically turn on (noted by a green blinking LED state on the ALPS base station), log in to the online ALPS system administration page at https://etc.alps.yodellabs.com/, go to the **Base Stations** tab and switch the Base Station at the correct location to *"Beaconing"* mode, as shown in the image above. The login username is `ETC` and the password is `**********` *(Note: this is not the actual password. If you need to know what the login credentials are, ask an appropriate administrator for support)*.

If the iOS App compiled and launched correctly, it should run now and connect to the ALPS system and update the location of the iOS device. To fully localize and converge on an accurate position/rotation, you may need to slowly walk around with the iOS device, facing the iOS device towards the ground at a 45 degree angle for around 30-60 seconds or until the ALPS positioning system successfully triangulates the iOS device's position.

## Unity Editor Notes
To prepare an ARKit scene for ALPS, you just need to plop in the following prefabs from the prefabs folder into your scene:

![ALPS Manager Info](/ReadmeImages/ALPS_Manager_Info.png) <br/>

* **GlobalFrame (ETC)** OR **GlobalFrame (MuseumLab)** <br/>
This is the global root node for ALPS, where everything should be parented under. This object must have a scale of `(-1,0,0)`. Note that you pick the GlobalFrame prefab based on which location you want to build your app for.
* **ALPS Manager** <br/>
This manages the ALPS connection. You will need to assign the `CamFrame` to the `AR Camera` (provided by ARFoundation), the `GlobalFrame` to an object representing the global root node, and `Info` to a text object where basic ALPS debugging info will be dumped.  
* **ALPS Canvas** <br/>
This contains a useful canvas with some basic text for debugging ALPS positioning.

![ALPS ETC Layout](/ReadmeImages/ALPS_ETC_Layout.jpg) <br/>
*This image above shows the beacon locations at CMU's ETC (marked by colored cubes).*

![Global Frame](/ReadmeImages/GlobalFrame.png) <br/>
*This image shows that the `GlobalFrame` object has a scale of `(-1,0,0)`.*

## Compilation

### Build Settings
![Build Settings](/ReadmeImages/BuildSettings.jpg) <br/>
There are three major scenes in the demo app: 

* **ALPS Scene Picker** *(This just allows the user to select the scene that they will be testing.)*
* **ALPS ETC** *This is a demo scene showing a floor map and beacon locations at the ETC.*
* **ALPS MuseumLab** *This is a demo scene showing a floor map and beacon locations at the MuseumLab.*

Note that the build target is for **iOS**.

### Player Settings
![Project Settings](/ReadmeImages/ProjectSettings.jpg) <br/>
In the Player Settings, make sure we have the following sections set correctly:

1. Set the **Bundle Identifier** to your preference.
2. Set the **Scripting Backend** to `IL2CPP`.
3. Set the **API Compatibility Level** to `.NET 4.0`.
4. Make sure **Camera Usage Description** has something filled in.
5. Make sure **Microphone Usage Description** has something filled in.
6. Make sure **Location Usage Description** has something filled in.

Then you can go to `File -> Build Settings -> Build` and Run to create an XCode project.

## XCode Settings
After Unity successfully builds an XCode project, we will need to do two things:

1. **Assigning a team** (this is required for all iOS apps, regardless of whether or not it was made in Unity).
2. **Assigning additional usage permissions** (needed by the ALPS system).

See below for more info:

### Adding Team and Signing
![XCode Team](/ReadmeImages/XCode_Team.jpg) <br/>
*Assign a team in the **Signing and Capabilities** tab. If you've never built for iOS before, you will need to create an Apple Developer account so you can fill this in here.*

### Adding Usage Permissions
![XCode Permissions](/ReadmeImages/XCode_Permissions.jpg) <br/>
For XCode to compile correctly and have the Unity iOS app not immediately crash upon successful compilation and push, we need the following permissions enabled in `XCode -> Unity-iPhone -> Info`:  

* `NSBluetoothPeripheralUsageDescription `
* `NSBluetoothAlwaysUsageDescription`

The following permissions should have already been added by Unity in the previous step, but if they have not been enabled, you will need to enable them in XCode: 

* `NSLocationAlwaysAndWhenInUseUsageDescription`
* `NSLocationUsageDescription`
* `NSMicrophoneUsageDescription`
* `NSCameraUsageDescription`

When all of this has been set correctly, you should be able to press the compile and run button in XCode, which will build, push, and then launch the app on your connected iOS device.

Have fun! :)

*For more information about how to build an iOS app for Unity, refer to the following links:*

* https://www.juegostudio.com/blog/ios-game-development-via-unity-guide-for-beginners
* https://docs.unity3d.com/Manual/iphone.html

## Special Thanks
This project was made possible with the support of the following organizations:

### <u>Yodel Labs</u>
![ALPS Logo](/ReadmeImages/alps-logo-dark.png) <br/>

[**Yodel Labs**](https://www.yodellabs.com/
) is a Carnegie Mellon University spinout founded in 2017. Our mission is to provide the most accurate location services in the mobile device industry, enabling your mobile app to bridge the gap between the virtual and the physical world.

### <u>Carnegie Mellon University Entertainment Technology Center</u>
![ETC Logo](/ReadmeImages/etc_logo.jpg) <br/>

Carnegie Mellon University's [**Entertainment Technology Center (ETC)**](https://www.etc.cmu.edu/) was founded in 1998 with Randy Pausch and Don Marinelli as the co-directors. The faculty and staff worked together to articulate our academic mission which focuses on educational goals and creative development. And we also created an R&D Agenda exploring transformational games, innovation by design and interactive storytelling. Throughout, we work to prepare students to graduate as creative professionals The ETC's unique, two-year, Master of Entertainment Technology (MET) degree was founded as a joint venture between Carnegie Mellon University’s School of Computer Science and the College of Fine Arts.  The MET is currently considered a terminal degree.

### <u>Children's Museum of Pittsburgh MuseumLab</u>
![MuseumLab Logo](/ReadmeImages/museumlab_Logo.png) <br/>

[**Children’s Museum of Pittsburgh**](https://pittsburghkids.org/) has grown along with your family, creating [**MuseumLab**](https://museumlab.org/) as a place where kids 10+ can have cutting-edge experiences in art, tech and making. It’s just the thing for their inquiring minds, increasing skills and independent inclinations. It all takes place in a “beautiful ruin” located right next door to the Children’s Museum, the former Carnegie Free Library of Allegheny. In creating MuseumLab, we renovated the building to reveal much of its original 1890 archways, columns and mosaic floors, and designed a unique space that honors the past while welcoming the future. And with the creation of MuseumLab, we’ve created the largest cultural campus for children in the country. Right here in Pittsburgh.

## Third Party Assets Used (FREE)
* azixMcAze | Unity-UIGradient (FREE) <br/>
https://github.com/azixMcAze/Unity-UIGradient

* doomlaser | DepthMask-Unity-Shader (FREE) <br/>
https://github.com/doomlaser/DepthMask-Unity-Shader

* SpaceMadness | Lunar Mobile Console - FREE (FREE) <br/> 
https://assetstore.unity.com/packages/tools/gui/lunar-mobile-console-free-82881

* Ciconia Studio | Free Double Sided Shaders (FREE) <br/>
https://assetstore.unity.com/packages/vfx/shaders/free-double-sided-shaders-23087

* OmniSAR Technologies | Lite FPS Counter - Probably the world's fastest FPS counter (FREE) <br/>
https://assetstore.unity.com/packages/tools/integration/lite-fps-counter-probably-the-world-s-fastest-fps-counter-132638
