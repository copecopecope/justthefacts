### TODO

#### Crowd Control
* Enable multi-touch (not necessary?)
	- Fix how multi touch works with buttons!!!
* Play with simulation some more
	- Attractive forces?
* Control scheme not awesome
	- Tap to select, hold to drag, then fire
* Different people
	- With different odds, changes as game goes on

#### Paper headlines
* Text generation 
	- Increase in complexity as game progresses
* Add simple animation between headlines

#### Tasks
* Dragging one ped to another
	- Assassinations 
		- Fix collision detection (actual bullet?)
	- Proposals
	- Nabs
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
* Introduce new tasks over time
* Increase crowd number, variety, speed over time
* Increase scoring decrease rate over time

#### Peripheraff
* Title screen
* Tutorial for first time
* If we actually want to release this
	- Game Center integration (equiv Android?)
	- Resolution/screen size independence
		- UI elements with positions relative to viewport
* Better graphics!!
* Audio/music?

#### Refactoring
* Change headline/crowd/drag control to singleton
