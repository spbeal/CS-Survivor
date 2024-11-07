# Prefab: *LightManager*

## Components:

### Called Scripts

#### Lights:
>Contains the system to spawn lights in the game using a factory pattern. Spawns lights throughout the map in hardcoded locations.
- LightFactory
- SingleLight superclass with dynamic binding to the subclasses
- Two Subclasses 
  - WhiteLight contains a typical light with pulsing functionality that takes input to turn light and effect on and off
  - ColorLight contains a typical light with flashing blue/red functionality that takes input to turn light and effect on and off

https://github.com/user-attachments/assets/d135a695-a284-48d0-87ab-233198c31cec

