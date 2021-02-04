<h1> CSC2033: Custom Team Project - Game </h1>

<h3> Synopsis </h3>

This custom project is a 3d game developed in Unity. The player must select a level. Upon doing so the player is placed
in a 3d environment. Enemies will appear and walk towards a point in the map, in order to hit an object. If enough
enemies hit the object (point to defend) the player fails. If the player gets too close to the enemies, the enemy will
hit the player, the player will lose health. The player can also fail by taking too much damage. The player has a laser
weapon to shoot enemies. The player must shoot all of the enemies in order to win. The player may also place turrets
in between waves of enemies with money earned from killing enemies.

<h3> Testing </h3>

See the final product evaluation test document for more details on testing. As a team we took an agile approach
hence our testing and implementation phases were interconnected. Our main testing method was white box testing. The 
document mentioned provides details about a test of our final product. 

<h3> Requirements </h3>

* Developed and executable in: Unity 2020.1.16f1
* Unity Plugin/Package: Cinemachine (Install in Unity: Window > Package Manager)
* Microsoft Visual Studio (to view C# scripts)

<h3> How To Use - Entry Point </h3>

Head over to the project tab, under assets, open scenes, double click the scene named 'Menu', enable the option to
maximize on play. Click the play button at the top of the screen in Unity.  
At this point you should be on the main menu for the game. Click the options button to adjust the volume settings I recommend turning the volume down below half.  
Exit out of the options menu and click on the play game button. Select any level you'd like to play then click to shoot on 
enemies that appear. Defend the point near where the player starts, as failing to do so fails the level.  
Earn money 
by killing the enemies. When enough enemies have been killed that will signify the end of a wave. A new phase will commence, 
the turret building phase.  
This is where the player may place turrets with the money they've earned, when you are done 
placing turrets, be sure to press 'G' to start the next wave. Good luck!

<h3> License </h3>

See the LICENSE.md file for license details.

<h3> Built With </h3>

* [Audio](https://www.youtube.com/watch?v=yQgVKR6PMqo&t=604s) - Audio settings menu & code used in AudioSettings.cs
* [Main Menu](https://www.youtube.com/watch?v=zc8ac_qUXQY&t) - Inspired by this project/tutorial
* [Pause Menu](https://www.youtube.com/watch?v=JivuXdrIHK0) - Code used in PauseMenu.cs
* [Game Over Screen](https://www.youtube.com/watch?v=VbZ9_C4-Qbo&t) - Code used in GameManager.cs
* [Sound Files](freesound.org) - In game sound effects
* [3rd Person Camera](https://www.youtube.com/watch?v=hb9FoFEFR3M&ab_channel=TheKiwiCoder) - Used to create third person camera    
* [Enemy Model & Animation](https://www.mixamo.com/#/) - Imported enemy models and their animations from this site
* [Turret Rotation](https://www.youtube.com/watch?v=QKhn2kl9_8I&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0&index=4&ab_channel=Brackeys) - Used code from here to rotate turrets

<h3> Team Members </h3>

* Name: <b> Lee Taylor </b> 
* Name: <b> Mark Nicholson </b> 
* Name: <b> Jack Collins </b> 
* Name: <b> Adomas Anskaitis </b>
* Name: <b> Ayrton Magras </b>
