# Dungeon-Shooter

This repo contains the source code for a Unity game project I made called Dugeon Shooter for my college game development class.

I had a lot of fun making this project and learned a lot about the game design and development process.

The game is heavily optimized to be a standalone desktop application, however I did build for WebGL as well and it certainly is pushing the limits there.

WebGL build of the game is currently live here: https://simmer.io/@austino/dungeon-shooter

A Zipped Windows build of the game is here: https://drive.google.com/file/d/1rL_dwXVv9NRKwU7FNoxye9Fxq2s7Zz8e/view?usp=sharing

Youtube video demo - https://youtu.be/49oymqJse2s

## Game Design

This is a simple fps game where you run around and shoot zombies that are trying to eat you!
You have an assault rifle and you have to see how long you can make it in the dungeon & how may zombies you can bring down with you, before falling to the zombies.

Another feature of the game is that the world dynamically builds as the player moves around, periodically new enemies will spawn as well keeping the players on their toes.

## Game Developmet Learning

This project was really born out of trying a boat load of free assets from the Unity Asset Store, and stitching different prefabs, models, animations, artwork, & music together to create the game I was trying to make.
I will credit all assets used at a later section of this README.

Essentially the main things I contributed of my own to make everything work include:
* Several scripts that manage the main systems of the game, as well as editing some of the scripts I imported from the asset store to suit my needs.
* The Zombie asset I used had the individual animations but I had to write a behavior controller as well as wire a new animation controller.
* I also implemented a music management system for the game that plays 9 differet tracks I paid $1 to HumbleBundle for (Tracks and Licenses seen in `Assets/Audio`)

Overall I had a lot of fun with this and think it is a neat prototype, just imagine what we could do with some premium assets!

## Play Testing & Feedback

Throughout the development process I regularly consulted with classmates about my ideas and had people I lived with play the game as I worked on it.

Several suggestions helped make the game what it ended up being:
* Add in game music and now playing feature
* Spawn enemies from different positions
* "Secret Portals" - A bug I turned into a feature where sometimes a player would end up out of bounds and could walk through a portal and respawn in the dungeon

## References
### Assets Used

Cursors and Crosshairs Ultimate Pack -> https://assetstore.unity.com/packages/tools/gui/cursors-and-crosshairs-ultimate-pack-109530

Simple Healthbars -> https://assetstore.unity.com/packages/tools/gui/simple-healthbars-132547

Blood Gush -> https://assetstore.unity.com/packages/vfx/particles/blood-gush-73426

Zombie -> https://assetstore.unity.com/packages/3d/characters/humanoids/zombie-30232

Polygonal's Low-Poly Particle Pack -> https://assetstore.unity.com/packages/vfx/particles/polygonal-s-low-poly-particle-pack-118355

Low Poly FPS Pack - Free (Sample) -> https://assetstore.unity.com/packages/3d/props/weapons/low-poly-fps-pack-free-sample-144839

Low Poly Dungeons Lite -> https://assetstore.unity.com/packages/3d/environments/dungeons/low-poly-dungeons-lite-177937

### Music

See the Audio directory within Assets, all in game music has an appropriate license associated with it.

Minecraft Damage sound downloaded from youtube -> https://www.youtube.com/watch?v=nbONxjyiVj4&ab_channel=SoundEffectsCrazy

### Images

Main Menu Splash -> https://mynoise.net/Data/DUNGEON/fb.jpg

Death Splash -> https://i.ytimg.com/vi/-ZGlaAxB7nI/maxresdefault.jpg

Secret Portal Splash -> https://www.artstation.com/artwork/BmPKlm
