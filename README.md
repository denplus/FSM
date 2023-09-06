# FSM
Solution for test assigment

The project contains 3 scenes:
* Root - this scene responsible for initialization of all services (EntryPoint script). This scene always loaded even when you are on another scene (with help of SceneAutoLoader)
* HuntingMode - this is game play scene that have main script HuntingModeView
* HuntingGroup - this is group scene that have main script HuntingGroupView

Terms:
In code I use the term “unit” to specify the figures that player will control (Fox, …) and “huntUnit” describe the figure that player will attack.

All classes are written with SOLID principles (At least I hope so :) )  

Also, I have divided some code layers with help of assembly definitions: Data, Utils, Services, ViewPresentation. This division helps to maintain code and keep good understanding of abstract logic for each layer.

The configuration are all in GameSettings scriptable object.

Services can be use in different project, they are separed from main project logic and can be transferred. 
