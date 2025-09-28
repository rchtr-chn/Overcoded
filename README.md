<h2>💻 OVERCODED</h2>
  <img width="250"alt="1759051950833525040143945092613" src="https://github.com/user-attachments/assets/188808d8-3bbc-4738-b37b-7cab910a8e4e" />

  You play as a stressed out and sleep deprived game developer trying to bugfix your game, eyes fixated on the screen, fingers locked in position to execute every single line of code to ensure the game runs smoothly. However, problems start to appear.. Haunting the player the longer they try to survive.


<h2>⬇️ Game Pages</h2>
  itch.io: https://rchtr-chn.itch.io/overcoded
  
<h2>🎮 Controls</h2>

  | Input | Function |
  | -------------------- | --------------------- |
  | Left Mouse Button | Interact with in-game elements |
  | Up Arrow Key or W Key| Jump |
  | Down Arrow Key or S Key| Duck/Slide |
  | Keyboard | Type in code |
  
<h2>📋 Project Information</h2>

  ![Unity Version 2021.3.45f1](https://img.shields.io/badge/Unity_Version-2021.3.45f1-FFFFFF.svg?style=flat-square&logo=unity) <br/>
  Game Build: ![Windows](https://img.shields.io/badge/Windows-004fe1.svg?style=flat-square&logo=windows) <br/>
  All art assets are made by our Game Artist
  All SFX can be found in [![Pixabay](https://img.shields.io/badge/Pixabay-191B26.svg?style=flat-square&logo=Pixabay)](https://pixabay.com) <br/> <br/>
  
  <b>Team:</b>
  - <a href="https://github.com/rchtr-chn">Richter Cheniago</a> (Game programmer)
  - <a href="https://github.com/wi1wil">Wilson Halim</a> (Assisting Game programmer)
  - <a href="https://jordytandiano.my.canva.site">Jordy Tandiano</a> (Game Designer and Sound Artist)
  - <a href="https://kelvinkel.carrd.co">Kelvin</a> (Assisting Game programmer)
  - <a href="https://www.behance.net/epenaja">Melvern Sjah</a> (Game designer and artist)

<h2>💡 My Contributions</h2>

  as the main programmer of this project, I am tasked to make most of the mechanics that make the game function as intended, such as the main endless runner mechanic, code typing minigame, pop-up virus ads minigame, cellphone notification minigame, etc.

<h2>📜 Scripts</h2>

  | Script | Description |
  | ------ | ----------- |
  | `PlayerTypingScript.cs` | Manages player's ability to type in lines of code in the game |
  | `PopUpSpawnerScript.cs` | Manages the pop up virus ads minigame functionality appearing on the player's screen |
  | `FlyScript.cs` | Manages the fly minigame roaming around player screen in the game |
  | `CoffeeScript.cs` | Manages the caffeine crashout minigame functionality in the game |
  | `ChatBubbleScript.cs` | Manages the chat bubble minigame functionality appearing above the player's phone |
  | etc. |

<h2>📂 Folder Descriptions</h2>

  ```
  ├── Overcoded                     # Root folder of this project
    ...
    ├── Assets                         # Assets folder of this project
      ...
      ├── Animation                        # Stores all animation clip and controllers
      ├── Audio Mixers                      # Stores all audio mixer assets
      ├── Dialogue                   # Stores all dialogue used in the game via .ink format
      ├── Fonts                   # Stores all font assets used
      ├── Prefabs                   # Stores all prefabs used
      ├── Scripts                      # Stores all scripts used in this project
        ├── GameOver                   # Stores all scripts related to the game over scene
        ├── Managers                   # Stores all scripts that function the background of the game
        ├── Minigames                   # Parent folder to organize all scripts related to gameplay loop
          ├── Chat Bubble                   # Stores all scripts related to the chat bubble minigame
          ├── Coffee                   # Stores all scripts related to the coffee drink minigame
          ├── Fly                   # Stores all scripts related to the flies minigame
          ├── Main                   # Stores all scripts related to the core gameplay loop (endless runner, and code typing minigame)
          ├── PopUpVirus                   # Parent folder to organize blueprints (Scriptable Objects) and prefabs
        ├── Other                   # Stores other scripts outside previous categories
      ├── SFX                   # Stores all SFX used in this project
      ├── Sprites                   # Stores all placeholder/temporary and final art assets used in this project
      ...
    ...
  ...
  ```
