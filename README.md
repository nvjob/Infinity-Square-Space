# Infinity Square/Space 1.0.1
#### [nvjob.github.io/unity/infinity-square-space](https://nvjob.github.io/unity/infinity-square-space)

![GitHub Logo](https://raw.githubusercontent.com/nvjob/nvjob.github.io/master/repo/unity%20assets/infinity%20square%20space/101/pic/10.jpg)

**The prototype of the game is open source. Unity Asset.**<br/>
Features: infinite procedurally generated world, almost complete destructible of everything, a very large number of NPCs (up to 1000 in one star system), battles involving hundreds of NPC, gravity is an important game element.

This prototype of the game is completely playable, but nevertheless, this is not a complete game.<br/>
The source contains all the tools for the development of the game, but you need the above-average programming skill. There are no comments in the code, but the code itself is well structured, all the scripts and shaders, functions and variables are named so that it is clear what they are responsible for.

**This game prototype consists of five main parts:**
- Pools of static game objects.
- Procedural generation based on the coordinate system.
- The system of destruction, based on the substitution of objects, objects from the pool of static objects.
- Artificial intelligence, with a primitive simulation of life and interaction with each other, the world and the player.
- Game controller, with a set of weapons and skills.

**Download standalone version on Itch.io** - https://nvjob.itch.io/infinity-squarespace-standalone <br/>
**WEB version on Itch.io** - https://nvjob.itch.io/infinity-square-space-web

-------------------------------------------------------------------

### Prerequisites
To work on the project, you will need a Unity version of at least 2019.1.8 or higher (64-bit).<br/>
Scripting Runtime Version - .net 4.x Equivalent 

### Installation:
https://www.youtube.com/watch?v=1DalkV98lyI<br/>
- Create a new project in Unity.
- Download Assets and ProjectSettings and place them in the folder of your new project.
- Open the desired scene in the Scenes directory.

![GitHub Logo](https://raw.githubusercontent.com/nvjob/nvjob.github.io/master/repo/unity%20assets/infinity%20square%20space/101/pic/3.png)

### Basic information
The “Main” directory contains all the files and scripts associated with the procedural generation of the planetary system. The “AI“ directory contains all the files and scripts associated with artificial intelligence. The “Player” directory contains all the files and scripts associated with the game controller, inventory and interface. The “Menu” directory contains all the files and scripts associated with the initial menu for selecting a planet.

The “Universe” script in the “Menu” scene is responsible for generating the star field.

![GitHub Logo](https://raw.githubusercontent.com/nvjob/nvjob.github.io/master/repo/unity%20assets/infinity%20square%20space/101/pic/1.png)
![GitHub Logo](https://raw.githubusercontent.com/nvjob/nvjob.github.io/master/repo/unity%20assets/infinity%20square%20space/101/pic/22.jpg)

The “StarSystem” script in the “Main” scene is responsible for generating the star system.

![GitHub Logo](https://raw.githubusercontent.com/nvjob/nvjob.github.io/master/repo/unity%20assets/infinity%20square%20space/101/pic/2.png)
![GitHub Logo](https://raw.githubusercontent.com/nvjob/nvjob.github.io/master/repo/unity%20assets/infinity%20square%20space/101/pic/7.jpg)

When you first start in the editor, first start the “Menu” scene to apply the settings of the “Main” scene, which are stored in “PlayerPrefs”. To test the main “Main” scene, you can generate the star systems you need using test seed.**

![GitHub Logo](https://raw.githubusercontent.com/nvjob/nvjob.github.io/master/repo/unity%20assets/infinity%20square%20space/101/pic/2a.png)

-------------------------------------------------------------------

**Patrons:** [nvjob.github.io/patrons](https://nvjob.github.io/patrons)<br>
*You can become one of the patrons, or make a sponsorship donation.*

-------------------------------------------------------------------

**Authors:** [#NVJOB. Developer Nicholas Veselov. Разработчик Николай Веселов. Санкт-Петербург.](https://nvjob.github.io)

**License:** MIT License. [Clarification of licenses](https://nvjob.github.io/mit-license).

**Support:** [Report a Problem](https://nvjob.github.io/reportaproblem/).
