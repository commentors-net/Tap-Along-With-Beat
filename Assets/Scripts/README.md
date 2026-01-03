# Tap Along With Beat - Unity Scripts

This folder contains all the C# scripts for the Tap Along With Beat rhythm game.

## Folder Structure

### Core/
- **GameManager.cs**: Main game manager with singleton pattern. Controls overall game flow and state management.

### Audio/
- **AudioManager.cs**: Manages all audio including music synchronization, sound effects, and beat timing.

### Input/
- **InputManager.cs**: Handles touch and mouse input for tap detection. Works on both mobile and desktop.

### Gameplay/
- **ScoreManager.cs**: Manages scoring system, combos, multipliers, and accuracy tracking.
- **NoteController.cs**: Controls individual note behavior, timing, and hit detection.
- **NoteSpawner.cs**: Spawns notes based on beat map data.
- **GameplayController.cs**: Manages gameplay flow, level progression, and transitions.

### UI/
- **UIManager.cs**: Manages all UI panels, updates score displays, and handles menu navigation.

### Data/
- **PlayerDataManager.cs**: Handles save/load operations for player data, high scores, and settings.

### Effects/
- **EffectsManager.cs**: Manages visual effects like particles, screen shake, and animations.

### Utilities/
- **GameUtilities.cs**: Helper functions for common operations like BPM conversion, formatting, etc.
- **Singleton.cs**: Generic singleton base class for MonoBehaviours.

### Config/
- **GameConfig.cs**: ScriptableObject for centralized game configuration and settings.

### Editor/
- **BeatMapCreator.cs**: Tool for creating and editing beat maps.

## Key Features

- **Singleton Pattern**: Core managers use singleton pattern for global access
- **Event System**: Score and combo changes use events for loose coupling
- **Mobile & Desktop Support**: Input system supports both touch and mouse
- **Timing System**: Precise timing windows for hit detection (perfect, good, ok, miss)
- **Data Persistence**: JSON-based save system for player data
- **Modular Architecture**: Easy to extend and modify individual components

## Getting Started

1. Attach `GameManager` to a GameObject in your main scene
2. Attach `AudioManager` with AudioSource components
3. Set up `UIManager` with required UI panel references
4. Create a beat map using `BeatMapCreator`
5. Assign the beat map to `GameplayController`

## Unity Lifecycle Methods Used

- **Awake()**: Singleton initialization and component setup
- **Start()**: Initial configuration and event subscriptions
- **Update()**: Core game loop, input handling, and state updates
- **OnDestroy()**: Cleanup and event unsubscription
- **OnApplicationQuit/Pause()**: Save data and handle app lifecycle

## Notes

- All scripts use namespaces to avoid conflicts
- Manager classes follow singleton pattern
- Scripts are documented with XML comments
- Mobile-optimized with platform-specific code paths
