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

  ![Unity Version 6000.2.2f1](https://img.shields.io/badge/Unity_Version-6000.2.2f1-FFFFFF.svg?style=flat-square&logo=unity) <br/>
  Game Build: ![Windows](https://img.shields.io/badge/Windows-004fe1.svg?style=flat-square&logo=windows) <br/>
  All art assets are made by our Game Artist
  All SFX can be found in [![Pixabay](https://img.shields.io/badge/Pixabay-191B26.svg?style=flat-square&logo=Pixabay)](https://pixabay.com) <br/> <br/>
  
  <b>Team:</b>
  - <a href="https://github.com/rchtr-chn">Richter Cheniago</a> (Game programmer)
  - <a href="https://github.com/wi1wil">Wilson Halim</a> (Assisting Game programmer)
  - <a href="https://jordytandiano.carrd.co">Jordy Tandiano</a> (Game Designer and Sound Artist)
  - <a href="https://kelvinkel.carrd.co">Kelvin</a> (Assisting Game programmer)
  - <a href="https://www.behance.net/epenaja">Melvern Sjah</a> (Game designer and artist)

<h2>💡 My Contributions</h2>

  as the main programmer of this project, I am tasked to make most of the mechanics that make the game function as intended, such as the main endless runner mechanic, code typing minigame, pop-up virus ads minigame, cellphone notification minigame, etc.

<h2>📜 Scripts</h2>

  | Script | Description |
  | ------ | ----------- |
  | `DeckManagerScript.cs` | Manages starting deck and saves any modification done to deck by player |
  | `HandManagerScript.cs` | Receives cards from `DeckManagerScript.cs` to be drawn on hand and returned to when needed|
  | `GameManagerScript.cs` | Organizes and centralized other minor managers and manages the turn-based system |
  | `ShopManagerScript.cs` | Manages the shop's cards to be displayed and sold to the player |
  | `Card.cs` | Blueprint for SOs that will carry a card's value and the potential card effect |
  | etc. |

<h2>📂 Folder Descriptions</h2>

  ```
  ├── Rat-Gambler                      # Root folder of this project
    ...
    ├── Assets                         # Assets folder of this project
      ...
      ├── Audio                        # Stores all BGM and audio clips used in this project
      ├── Fonts                        # Stores all fonts used in this project
      ├── Resources                    # Parent folder to organize blueprints (Scriptable Objects) and prefabs
        ├── CardData                   # Parent folder of all scriptable object types that are used in this project
          ...
        ├── Prefabs                    # Parent folder that stores prefabs that are instantiated during the project's runtime
          ...
      ├── Scenes                       # Stores all Unity Scenes used in this project
      ├── Scripts                      # Parent folder of all types of scripts that are used in this project
        ├── BackgroundManagers         # Stores scripts related to managers that function the game in the background
        ├── CardBehavior               # Stores scripts related to a card prefab
        ├── CardEffects                # Stores scripts consisting the logic behind every power cards
        ├── Cardshop                   # Stores scripts related to the card shop
        ├── CardSystem                 # Stores scripts related to card deck creation and usability during gameplay
        ├── Cookie                     # Stores scripts related to wagering cookies mechanic and cookie value modification
      ├── Sprites                      # Parent folder of all sprites that are used in this project
      ...
    ...
  ...
  ```
