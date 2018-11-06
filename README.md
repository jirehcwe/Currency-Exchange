# Currency Exchange: An educational currency exchange training game

Currency Exchange is a math/currency game created for [Hope Training Centre](https://ohf.org.sg/tjsss-programme-cambodia/) located in Cambodia, Prey Veng. It was done in collaboration with [Operation Hope Foundation](https://ohf.org.sg) and [Renaissance Engineering Programme's HEAL Team](http://www.ntu.edu.sg/rep/reclub/Pages/Humanitarian-Engineers-and-Leaders.aspx)

![Screenshot of game](Assets/Art/screenshot.png)

![Screenshot of gameplay](Assets/Art/DSC08873.JPG)
    
One particular extension I'm proud of is the localisation system for the game. Localisation packages are wrapped in individual asset files, and changing languages or adding languages is as easy as making a new asset file in Unity and filling it up with the relevant unicode text for that language. For this game, there isn't a lot of text to localise, but this system can be extended very easily as the system uses a dictionary to store and retrieve the localised strings. The dictionary is also supported by the editor and can be easily updated within the inspector.
