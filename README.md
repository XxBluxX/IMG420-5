# IMG420-5
## How shader works:
In the .gdshader there is code included to cause a wave effect as well as set a changing color effect from top to bottom.
<br>
The shader is linked under the gpuparticle2d node in the process material as a custom shader.
<br>
The particle control script sets up how the particles will act, adjusting speed, displacement, etc.
## Physics properties:
I struggled with the chain physics, as shown by the chains not being properly connected. What I was able to successfully implement however was creating the chains with proper initial placement and anchoring from the intial root chain. The issue came with linking the chains to the previous chain rather than only reacting in connection to the root chain. However I was able to get the chains into the game and have the player able to collide and affect them in a manner that chains should react if they were only connected and reacted together.
## Raycast detection:
The fully successful implementation was with the raycast detection. This script creates a laser on screen and is able to detect when the player interrupts the laser. When this is triggered the laser will trigger the system which changes the laser color, stops the laser at the point of collision (so it is not as if it is going through the player but instead hitting them), triggers the "alarm system" which is a red tint that covers the screen and finally, starts the timer so that the alarm will reset after a bit.
