### TODO

#### Crowd Control
* Enable multi-touch (not necessary?)
* Play with simulation some more
	- Attractive forces?
* Different people
	- With different odds, changes as game goes on

#### Paper headlines
* Text generation 
	- Increase in complexity as game progresses
* Add simple animation between headlines

#### Tasks
* Dragging one ped to another
	- Assassinations (implemented)
		- Fix collision detection
		- Actual bullet??
	- Proposals
	- Nabs
* Rare tasks
	- Save falling cat
	- Natural disasters (stretch goal)
 		- Indicate path
 		- Particle effect tornado

#### Scoring
* Penalties
	- Deduction for time/inaccuracy/being noticed (implemented)
	- End game if score reaches 0
	- Different initial scores for different task dificulties?

#### Optimizations
* Share materials to enable batching
	- http://dmayance.com/unity-paint-part-2/
	- http://docs.unity3d.com/Documentation/Manual/DrawCallBatching.html
* Instantiating so many people is a huge tax -- fix this!
	- Instantiate all N at beginning
	- Move people from right to left

#### Gameplay ideas
* Introduce new tasks over time
* Increase crowd number, variety, speed over time
* Increase scoring decrease rate over time

#### Peripheraff
* Pause 
* Title screen
* Tutorial for first time
* High scores
* If we actually want to release this
	- Game Center integration (equiv Android?)
	- Resolution/screen size independence
		- UI elements with positions relative to viewport
* Better graphics!!
* Audio/music?
