# MonoDino - Chrome Dinosaur Game Clone

A Chrome Dinosaur Game Clone that has been created using C# and Monogame Framework.

## Gameplay

**Might take some time to load**

<img src="https://github.com/user-attachments/assets/8acc7ce0-d29b-4413-9041-5ecfec6bd1f8" alt="Loading..." width="500">

## Installation

The following are the instructions for running the code:

- Download the project as a ZIP file or clone it using GitHub Desktop.
- Open the project in Visual Studio or VS Code.
- Make sure you have C# installed.
  - If you are using Visual Studio, Install .NET desktop development.
  - If you are using VS Code, run the following command:
    
  ```
  code --install-extension ms-dotnettools.csharp
  ```
  
  - To verify installation run the following command:
    
  ```
  dotnet --version
  ```
  
- Install Monogame
  - If you are using Visual Studio you can install Monogame through Extensions window.
  - If you are using VS Code, you can run the following command to install Monogame Templates:
    
  ```
  dotnet new --install MonoGame.Templates.CSharp
  ```

- Run the code

## How to Use

To start the game, press space or up. For jumping, press space or up. Press down for crouching. While in air, press down to come back down faster. After game is over, press jump or up to restart the game. The highscore appears after your first game over.

## Code

Here is the brief explanation for what the classes are responsible for.  

Further details can be found as comment lines in the files themselves.

- **Main.cs:** The code starts up from here. It responsible for loading data, updating, and drawing.

- **GameState.cs:** Abstract class for each game state. This project has only one game state, so it is not necessary (read line 22 in Main.cs).

- **Input.cs:** Responsible for mouse and keyboard detection. The project does not have any mouse inputs, but they are implemented just in case.

- **Sounds.cs:** Class for loading and playing the sound effects.

- **Sprite.cs:** Class that stores the data for rendering sprites, including texture, position, rotation, scale, and effects.

- **Level.cs:** The Game State in which we play the game. It's base class is GameState.cs, therefore Level is a type of GameState.

- **Score.cs:** Class for managing and drawing score, high score, and blinking animations. 

- **Background.cs:** Class for drawing the ground and clouds. 

- **Dino.cs:** Class for manages dino's physics, collisions, controls, and movements. You can change elements like velocity, jumping strength, gravity, and hitbox size.

- **Cactus.cs:** Class that for drawing and managing cactuses, their hitbox, and creation logic. You change their min and max spawn time.

- **Bird.cs:** Class that manages the movement speeds, heights and boxes of the created birds. You can change their speed, height, min and max spawn time, and hitbox.

- **Animation.cs:** Animation manager class.
 
- **AnimationBird.cs:** Class that manages bird animations.
   
- **AnimationDino.cs:** Class that manages dino animations. 
