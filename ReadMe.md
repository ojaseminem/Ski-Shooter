# Ski Shooter
***

This ReadMe contains the basic documentation for the Game.
It will help you understand the code architecture, naming conventions, etc.

---

<details>
<summary>Folder Structure & Description</summary>

* Animations
  *  Contains Animation files and Animation Controllers for the Character and UI Elements.
* Assets
  *  Contains all the necessary assets for each level. Please do not change any import settings, especially for the models.
* Data
  *  Contains an "UserData" scriptable object file which you can use to get the collectables data.
* Imported
  *  Contains the source files for the character textures and the bullet FX.
* Prefab
  *  Contains prefabs used in the game.
* Scenes
  *  Contains 3 scenes : RegistrationLoginScene (not included in build settings yet), MenuScene (included in the build settings), GameScene (included in the build settings).
* Scripts
  *  Contains the Gameplay scripts. You can add a new folder / namespace for Ad Integrations or other purposes.
* TextMesh Pro
  *  Contains Text Mesh Pro package being used for the UI.

</details>

---

<details>
<summary>Script Name & Usage Description</summary>
  <details>
  <summary>Managers</summary>

* AudioManager
  * Manager for creating and handling the audio sources responsible for the complete game audio sfx.
* GameEvents
  * Manager being used for UI related events.
* MenuManager
  * Manager responsible for MenuScene UI related events.
* PlayerManager
  * Handles secondary player events such as collectable calculation, game started, paused and over events.
* SwipeManager
  * Generates Swipe events for mobile touch and for mouse click and drag events.
* TutorialManager
  * Responsible for tutorial related events.
  
  </details>

<details>
<summary>Misc</summary>

* DestroyableObstacleHandler
  * Handles snow boulder behaviour.
* PickUp
  * Being used for collectables like coin or diamond.
* ProjectileMoveScript
  * Is activated after the projectile spawns and is used for moving the projectile straight.
* Sound
  * Serializable script being used for adding sounds on the AudioManager GameObject in the inspector.
  * It contains the variables used to input sounds and other values to play it in game.
* TimeCalculator
  * Is created only for data collection purposes.
  * Call public variable "elapsedTime" by calling "TimeCalculator.instance.elapsedTime". This will return a float.
  * You can get a string value already formatted in the manner ("mm : ss . ff"). 
  You can call this by using "TimeCalculator.userPlayTime".

</details>

<details>
<summary>Player</summary>

* PlayerController
  * Handles movement and shooting events in the player controller.
  * Generates OnTriggerEnter events.
* PlayerShooting
  * Secondary script being used from an Animation Event to shoot the projectile.

</details>

<details>
<summary>Scriptable Objects</summary>

* UserData
  * Consists variables to use as data holder during the gameplay.

</details>

<details>
<summary>Tiles</summary>

* Tile
  * Enables all the boulders inside the tile, if there's any.
  * Being used to fix a bug.
* TileManager
  * Handles generating tiles based on player position.

</details>

</details>

---

<details>
<summary>Need Help / Support</summary>

If you face any bug, issue or need any help understanding the code to implement your own,
you can contact me on +91 84839 73734 / sharmaojas3@gmail.com

</details>