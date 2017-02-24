# game-object-destruction
Unity 3-D script to make destructible game objects that gets destroyed on collision.

# Features:
This script can be used to make destructable game objects consiting of more than one rigidbodies. For example any game object you want to make destructable such as street lights, chairs etc. <br>
Attach this script to any game object and this script will first initially set all rigidbodies as kinematic so they do not interact with physics untill and unless a collision with them occurs. On collision, rigidbodies are un-set as kinematic and they start interacting with physics. <br>
A minimum velocity can be set for destruction to happen eg. a game object such as chair or light pole should only destruct if collision velocity magnitude of other colliding collider is greater than say, 5.
<br>
Sound effects can also be added. Different audio sources can be added and they will play randomly on collision.
