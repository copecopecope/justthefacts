### TODO

#### Crowd Control
* Play with simulation some more
	- Attractive forces?
* Control scheme not awesome
	- Tap to select, hold to drag, then fire

#### Paper headlines
* Add simple animation between headlines

#### Tasks
* Rare tasks
	- Save falling cat
	- Natural disasters (stretch goal)
 		- Indicate path
 		- Particle effect tornado

#### Scoring
* Penalties
	- Different initial scores for different task dificulties?

#### Optimizations
* Share materials to enable batching
	- http://dmayance.com/unity-paint-part-2/
	- http://docs.unity3d.com/Documentation/Manual/DrawCallBatching.html
* Instantiating so many people is a huge tax -- fix this!
	- Instantiate all N at beginning
	- Move people from right to left
* Still need to optimize crowd physics

#### Gameplay ideas
* If score is near 0, instantiate of the actor/target to help player

#### Peripheraff
* Tutorial for first time
* If we actually want to release this
	- Game Center integration (equiv Android?)
	- Resolution/screen size independence
		- UI elements with positions relative to viewport


#### Refactoring
* Change crowd/drag control to singleton (boo)
