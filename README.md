# HW4
## Devlog
[MG4 Commit](https://github.com/random0624/HW4/commit/e2c5452bc953a682354639c3384cdd04a068144a)

Added Ground Tile, Bird Sprite, player can now jump in place.

### Project Devlog
I mostly used MVC for the Control + View responsibilities within GameController. 

Control in GameController class houses the “game rule” entry points such as AddScore() (increases _score and calls OnScoreChanged), and GameOver() (sets _isGameActive = false and calls OnGameOver event). 

View is primarily the score UI as GameController references the UI element _scoreText and controls what the player sees with the call to UpdateScoreUI(int score) (by setting _scoreText.text) within GameController.cs.

Player remains decoupled from both the UI, Pipes, and Spawner as it does not communicate with them at all but communicates upward by calling GameController.Instance.AddScore() when entering the "ScoreZone" Trigger and GameController.Instance.GameOver() when colliding with a "Pipe" within Player.cs.

Decoupling is enforced via a Singleton instance of GameController (created in Awake()) providing a single “control access point”, and C# events; OnScoreChanged allows the UI/Audio to be updated based upon _score without Player being aware of _scoreText or Audio, while OnGameOver sends out a broadcast that other independent systems can subscribe to so they know when to stop (for example PipeSpawner.StopSpawning() in PipeSpawner.cs, and Pipe.StopPipe() in Pipe.cs) – so without Player having references to these systems, the pipes will stop generating/moving.

## Open-Source Assets
If you added any other assets, list them here!
- [Brackey's Platformer Bundle](https://brackeysgames.itch.io/brackeys-platformer-bundle) - sound effects
- [2D pixel art seagull sprites](https://elthen.itch.io/2d-pixel-art-seagull-sprites) - seagull sprites