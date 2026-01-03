# Tap Along With Beat - Unity Setup Checklist

This checklist guides you through setting up the Unity project after the boilerplate scripts have been created.

---

## ? QUICK START - Automation Tools

**NEW! Automated setup tools are now available to speed up your workflow:**

| Tool | Menu Path | What It Does |
|------|-----------|--------------|
| **Create Asset Folders** | `Tools > Tap Along With Beat > Create Asset Folders` | Creates all Audio, Sprites, Prefabs, Resources folders |
| **Setup Main Menu Scene** | `Tools > Tap Along With Beat > Setup Main Menu Scene` | Creates all manager GameObjects (GameManager, AudioManager, etc.) |
| **Setup Main Menu UI** | `Tools > Tap Along With Beat > Setup Main Menu UI` | Creates Canvas, EventSystem, MainMenu panel with buttons |
| **Create Note Prefab** | `Tools > Tap Along With Beat > Create Note Prefab` | Auto-generates configured Note prefab |
| **Setup Gameplay Scene** | `Tools > Tap Along With Beat > Setup Gameplay Scene` | Creates NoteSpawner, GameplayController, EffectsManager |
| **Setup Gameplay UI** | `Tools > Tap Along With Beat > Setup Gameplay UI` | Creates all gameplay UI panels (HUD, Pause, Game Over) |

**?? Recommended Workflow:**
1. Use automation tools for bulk setup
2. Complete manual configurations (see Step 3.5)
3. Test in Play mode
4. Fine-tune as needed

**?? What automation CANNOT do:**
- Assign object references in Inspector (you must drag & drop)
- Connect UI button OnClick events
- Configure specific values (you can adjust after creation)

---

## ?? Phase 1: Initial Unity Project Setup

### Step 1.1: Create Unity Project
- [ ] Open Unity Hub
- [ ] Click "New Project"
- [ ] Select **2D Core** template
- [ ] Project Name: `Tap-Along-With-Beat`
- [ ] Location: `D:\Jobs\workspace\Unity-Workspace\Tap-Along-With-Beat`
- [ ] Unity Version: **2021.3 LTS** or later (tested with **Unity 6.3 LTS**)
- [ ] Click "Create Project"

### Step 1.2: Verify Script Compilation
- [ ] Wait for Unity to open and import all assets
- [ ] Check Console window (Window > General > Console)
- [ ] Verify no compilation errors
- [ ] All 13 C# scripts should compile successfully

### Step 1.3: Configure Visual Studio
- [ ] In Unity: Edit > Preferences > External Tools
- [ ] Set External Script Editor to **Visual Studio 2022**
- [ ] Enable "Embedded packages", "Local packages", "Registry packages"
- [ ] Click "Regenerate project files"
- [ ] Open the `.sln` file in Visual Studio to verify

---

## ? Phase 2: Project Settings Configuration

### Step 2.1: Player Settings
- [ ] Edit > Project Settings > Player
- [ ] Company Name: `Your Company Name`
- [ ] Product Name: `Tap Along With Beat`
- [ ] Bundle Identifier: `com.yourcompany.tapalongwithbeat`
- [ ] Default Icon: (Add your app icon later)

### Step 2.2: Android Build Settings
- [ ] File > Build Profiles
- [ ] In the **Android** section, click "Switch Platform"
- [ ] Wait for platform switch to complete
- [ ] Click **"Player Settings"** button (top-right) or go to Edit > Project Settings > Player
- [ ] In Player Settings, select the **Android tab** (Android robot icon)
- [ ] Expand **"Other Settings"** section
- [ ] Configure Android settings:
  - [ ] **Minimum API Level**: **Android 7.0 'Nougat' (API level 24)**
  - [ ] **Target API Level**: **Automatic (highest installed)**
  - [ ] **Scripting Backend**: Change from Mono to **IL2CPP**
  - [ ] **Target Architectures**: Check **ARM64** (appears after IL2CPP is selected)

### Step 2.3: iOS Build Settings (if needed)
- [ ] File > Build Profiles
- [ ] In the **iOS** section, click "Switch Platform"
- [ ] Wait for platform switch to complete
- [ ] Click **"Player Settings"** button (top-right) or go to Edit > Project Settings > Player
- [ ] In Player Settings, select the **iOS tab**
- [ ] Expand **"Other Settings"** section
- [ ] Configure iOS settings:
  - [ ] **Target minimum iOS Version**: **12.0**
  - [ ] **Camera Usage Description**: (add description if using camera)

### Step 2.4: Quality & Audio Settings
- [ ] Edit > Project Settings > Quality
  - [ ] Set Default quality level
  - [ ] VSync Count: **Don't Sync** (for better control)
- [ ] Edit > Project Settings > Audio
  - [ ] DSP Buffer Size: **Best latency**
  - [ ] Sample Rate: **48000 Hz**

---

## ? Phase 3: Scene Setup

### Step 3.0: Quick Setup Using Automation Tools ? (RECOMMENDED)
**NEW! Use these automated tools to save time:**

- [ ] **Create Asset Folders**: 
  - Go to: `Tools > Tap Along With Beat > Create Asset Folders`
  - This creates all necessary folders (Audio, Sprites, Prefabs, etc.)
  
- [ ] **Setup Main Menu Scene**:
  - Create and open MainMenu scene: `File > New Scene` ? Save as `Assets/Scenes/MainMenu.unity`
  - Go to: `Tools > Tap Along With Beat > Setup Main Menu Scene`
  - This creates: GameManager, AudioManager, InputManager, ScoreManager, PlayerDataManager
  
- [ ] **Setup Main Menu UI**:
  - Go to: `Tools > Tap Along With Beat > Setup Main Menu UI`
  - This creates: Canvas (GameUI) with UIManager, EventSystem, MainMenuPanel with buttons
  
- [ ] **Create Note Prefab**:
  - Go to: `Tools > Tap Along With Beat > Create Note Prefab`
  - This creates and configures the Note prefab automatically
  
- [ ] **Setup Gameplay Scene**:
  - Create and open Gameplay scene: `File > New Scene` ? Save as `Assets/Scenes/Gameplay.unity`
  - Go to: `Tools > Tap Along With Beat > Setup Gameplay Scene`
  - This creates: NoteSpawner, GameplayController, EffectsManager
  - ?? **Important**: After this, manually assign Note Prefab to NoteSpawner Inspector
  
- [ ] **Setup Gameplay UI**:
  - Go to: `Tools > Tap Along With Beat > Setup Gameplay UI`
  - This creates: All gameplay panels (Gameplay, Pause, Game Over) with UI elements

**After using automation tools, skip to Step 3.5 below to complete manual configurations!**

---

### Step 3.1: Create Main Menu Scene (Manual Alternative)
- [ ] File > New Scene
- [ ] Save as: `Assets/Scenes/MainMenu.unity`
- [ ] Create folder if needed: `Assets/Scenes/`

### Step 3.2: Setup Main Menu Manager Objects
Create these GameObjects in MainMenu scene:

**GameManager:**
- [ ] GameObject > Create Empty
- [ ] Name: `GameManager`
- [ ] Add Component: `GameManager` script
- [ ] Note: DontDestroyOnLoad is handled by script

**AudioManager:**
- [ ] GameObject > Create Empty
- [ ] Name: `AudioManager`
- [ ] Add Component: `AudioManager` script
- [ ] Add Component: `Audio Source`
- [ ] Audio Source settings:
  - [ ] Play On Awake: **false**
  - [ ] Loop: **false**
  - [ ] Spatial Blend: **2D**

**InputManager:**
- [ ] GameObject > Create Empty
- [ ] Name: `InputManager`
- [ ] Add Component: `InputManager` script

**ScoreManager:**
- [ ] GameObject > Create Empty
- [ ] Name: `ScoreManager`
- [ ] Add Component: `ScoreManager` script

**PlayerDataManager:**
- [ ] GameObject > Create Empty
- [ ] Name: `PlayerDataManager`
- [ ] Add Component: `PlayerDataManager` script

**UIManager:**
- [ ] GameObject > UI > Canvas (if not exists)
- [ ] Rename Canvas to: `GameUI`
- [ ] Add Component: `UIManager` script to Canvas
- [ ] Canvas settings:
  - [ ] Render Mode: **Screen Space - Overlay**
  - [ ] Canvas Scaler: **Scale With Screen Size**
  - [ ] Reference Resolution: **1920 x 1080**

### Step 3.3: Create Main Menu UI
Under GameUI Canvas, create:

**Main Menu Panel:**
- [ ] Right-click Canvas > UI > Panel
- [ ] Name: `MainMenuPanel`
- [ ] Add UI elements:
  - [ ] Text: "Tap Along With Beat" (Title)
  - [ ] Button: "Play"
  - [ ] Button: "Settings"
  - [ ] Button: "Quit"

### Step 3.4: Create Gameplay Scene
- [ ] File > New Scene
- [ ] Save as: `Assets/Scenes/Gameplay.unity`
- [ ] Add Main Camera (should exist by default)
- [ ] Camera settings:
  - [ ] Projection: **Orthographic**
  - [ ] Size: **5**
  - [ ] Background: **Black** or your choice

### Step 3.5: Complete Manual Configurations (After Automation)

**If you used the automation tools, complete these steps:**

**For NoteSpawner:**
- [ ] Select NoteSpawner in Hierarchy
- [ ] In Inspector, assign these references:
  - [ ] **Note Prefab**: Drag `Assets/Prefabs/Gameplay/Note` from Project window
  - [ ] **Spawn Point**: Drag child object `SpawnPoint` from Hierarchy
  - [ ] **Target Point**: Drag child object `TargetPoint` from Hierarchy

**For GameplayController:**
- [ ] Select GameplayController in Hierarchy
- [ ] In Inspector, assign:
  - [ ] **Note Spawner**: Drag NoteSpawner from Hierarchy

**For UIManager (Both Scenes):**
- [ ] Select Canvas (GameUI) in Hierarchy
- [ ] In Inspector, assign all UI references:
  - [ ] Drag MainMenuPanel (if in MainMenu scene)
  - [ ] Drag GameplayPanel, PausePanel, GameOverPanel (if in Gameplay scene)
  - [ ] Drag all Text elements (ScoreText, ComboText, etc.)
  - [ ] Drag ProgressBar slider

**Setup Button Events:**
- [ ] For each button, click "+" in OnClick() section:
  - [ ] Drag Canvas (GameUI) to the object field
  - [ ] Select function: `UIManager > [appropriate method]`
  - Examples:
    - Play Button ? `UIManager.OnPlayButtonClicked`
    - Pause Button ? `UIManager.OnPauseButtonClicked`
    - Resume Button ? `UIManager.OnResumeButtonClicked`
    - Restart/Retry Button ? `UIManager.OnRestartButtonClicked`
    - Main Menu Button ? `UIManager.OnMainMenuButtonClicked`
    - Quit Button ? `UIManager.OnQuitButtonClicked`

**Configure Camera (Gameplay Scene):**
- [ ] Select Main Camera
- [ ] Set Projection: **Orthographic**
- [ ] Set Size: **5**
- [ ] Set Background: **Black** (or your preference)

---

## ? Phase 4: Gameplay Setup

### ? Quick Note: Use Automation!
**If you haven't already, use the automated tools from Phase 3, Step 3.0 to quickly set up:**
- Note Prefab creation
- Gameplay controllers
- Gameplay UI

**Then proceed to manual configurations in Step 3.5**

---

### Step 4.1: Create Note Prefab (Manual Alternative - Skip if using automation)
- [ ] Create folder: `Assets/Prefabs/Gameplay/`
- [ ] In Hierarchy: GameObject > 2D Object > Sprite > Circle
- [ ] Name: `Note`
- [ ] Add Components:
  - [ ] Circle Collider 2D
  - [ ] Rigidbody 2D (Is Kinematic: **true**)
  - [ ] `NoteController` script
- [ ] Configure NoteController:
  - [ ] Note Speed: **5**
  - [ ] Perfect Window: **0.05**
  - [ ] Good Window: **0.1**
  - [ ] OK Window: **0.15**
- [ ] Drag to Prefabs folder to create prefab
- [ ] Delete from Hierarchy

### Step 4.2: Setup Gameplay Controllers
In Gameplay scene:

**NoteSpawner:**
- [ ] GameObject > Create Empty
- [ ] Name: `NoteSpawner`
- [ ] Add Component: `NoteSpawner` script
- [ ] Create child GameObject: `SpawnPoint` (top of screen, Y = 6)
- [ ] Create child GameObject: `TargetPoint` (bottom, Y = -4)
- [ ] Assign references:
  - [ ] Note Prefab: Drag the Note prefab
  - [ ] Spawn Point: Drag SpawnPoint transform
  - [ ] Target Point: Drag TargetPoint transform

**GameplayController:**
- [ ] GameObject > Create Empty
- [ ] Name: `GameplayController`
- [ ] Add Component: `GameplayController` script
- [ ] Assign reference:
  - [ ] Note Spawner: Drag NoteSpawner GameObject

**EffectsManager:**
- [ ] GameObject > Create Empty
- [ ] Name: `EffectsManager`
- [ ] Add Component: `EffectsManager` script

### Step 4.3: Create Gameplay UI
- [ ] Copy GameUI Canvas from MainMenu scene OR create new Canvas
- [ ] Under Canvas, create panels:

**Gameplay Panel:**
- [ ] Panel: `GameplayPanel`
- [ ] Text: `ScoreText` (e.g., "Score: 0")
- [ ] Text: `ComboText` (e.g., "Combo: x0")
- [ ] Text: `AccuracyText` (e.g., "Accuracy: 100%")
- [ ] Slider: `ProgressBar`
- [ ] Button: `PauseButton`

**Pause Panel:**
- [ ] Panel: `PausePanel` (initially disabled)
- [ ] Text: "PAUSED"
- [ ] Button: "Resume"
- [ ] Button: "Restart"
- [ ] Button: "Main Menu"

**Game Over Panel:**
- [ ] Panel: `GameOverPanel` (initially disabled)
- [ ] Text: "GAME OVER"
- [ ] Text: `FinalScoreText`
- [ ] Text: `FinalAccuracyText`
- [ ] Text: `MaxComboText`
- [ ] Button: "Retry"
- [ ] Button: "Main Menu"

### Step 4.4: Link UI References
- [ ] Select Canvas with UIManager
- [ ] Drag UI elements to inspector:
  - [ ] Main Menu Panel
  - [ ] Gameplay Panel
  - [ ] Pause Panel
  - [ ] Game Over Panel
  - [ ] Score Text
  - [ ] Combo Text
  - [ ] Accuracy Text
  - [ ] Progress Bar
  - [ ] Final Score Text
  - [ ] Final Accuracy Text
  - [ ] Max Combo Text

### Step 4.5: Setup UI Button Callbacks
For each button, set OnClick() events:
- [ ] Play Button ? `UIManager.OnPlayButtonClicked`
- [ ] Pause Button ? `UIManager.OnPauseButtonClicked`
- [ ] Resume Button ? `UIManager.OnResumeButtonClicked`
- [ ] Restart Button ? `UIManager.OnRestartButtonClicked`
- [ ] Main Menu Buttons ? `UIManager.OnMainMenuButtonClicked`
- [ ] Quit Button ? `UIManager.OnQuitButtonClicked`

---

## ?? Phase 5: Configuration & Resources

### Step 5.0: Quick Folder Creation ?
**Use automation to create all asset folders at once:**
- [ ] Go to: `Tools > Tap Along With Beat > Create Asset Folders`
- [ ] This creates all folders listed in Step 5.2 automatically
- [ ] Skip to Step 5.1 for GameConfig creation

---

### Step 5.1: Create GameConfig Asset
- [ ] Create folder: `Assets/Resources/Config/`
- [ ] Right-click in folder
- [ ] Create > Tap Along With Beat > Game Config
- [ ] Name: `GameConfig`
- [ ] Configure values:
  - [ ] Perfect Window: **0.05**
  - [ ] Good Window: **0.1**
  - [ ] OK Window: **0.15**
  - [ ] Perfect Hit Score: **100**
  - [ ] Good Hit Score: **50**
  - [ ] OK Hit Score: **25**
  - [ ] Note Speed: **5**
  - [ ] Target Frame Rate: **60**

### Step 5.2: Prepare Asset Folders
Create these folders in Assets:
- [ ] `Assets/Audio/Music/`
- [ ] `Assets/Audio/SFX/`
- [ ] `Assets/Sprites/UI/`
- [ ] `Assets/Sprites/Notes/`
- [ ] `Assets/Sprites/Backgrounds/`
- [ ] `Assets/Materials/`
- [ ] `Assets/Prefabs/Managers/`
- [ ] `Assets/Prefabs/Effects/`
- [ ] `Assets/StreamingAssets/BeatMaps/`
- [ ] `Assets/Fonts/`

---

## ? Phase 6: Create Test Beat Map

### Step 6.1: Create BeatMap GameObject
- [ ] In Gameplay scene: GameObject > Create Empty
- [ ] Name: `BeatMapCreator`
- [ ] Add Component: `BeatMapCreator` script
- [ ] Configure:
  - [ ] Song Name: "Test Song"
  - [ ] BPM: **120**
  - [ ] Auto Generate Beats: **true**
  - [ ] Number Of Beats: **16**
  - [ ] Start Time: **2.0**

### Step 6.2: Generate Test Beat Map
- [ ] In Inspector, click context menu (?)
- [ ] Select "Create Sample Beat Map"
- [ ] Check Console for confirmation

---

## ? Phase 7: Testing

### Step 7.1: Basic Test in Editor
- [ ] Open MainMenu scene
- [ ] Press Play
- [ ] Check Console for manager initialization messages
- [ ] Click Play button (should transition to Gameplay)

### Step 7.2: Gameplay Test
- [ ] Open Gameplay scene
- [ ] Assign test audio clip to AudioManager (optional)
- [ ] Press Play
- [ ] Click/tap on screen to test input
- [ ] Verify score updates
- [ ] Check note spawning (after adding audio)

### Step 7.3: Build Settings
- [ ] File > Build Profiles
- [ ] Add Scenes to your active build profile (Android or PC):
  - [ ] Click "+" or drag MainMenu scene (index 0)
  - [ ] Click "+" or drag Gameplay scene (index 1)
- [ ] Verify scene order is correct
- [ ] Click "Build" or "Build and Run" when ready to test

---

## ? Phase 8: Next Steps

### Add Audio Assets
- [ ] Import music tracks to `Assets/Audio/Music/`
- [ ] Import sound effects to `Assets/Audio/SFX/`
- [ ] Assign to AudioManager arrays

### Create Custom Graphics
- [ ] Design note sprites
- [ ] Create background images
- [ ] Design UI elements
- [ ] Import to appropriate folders

### Implement Particle Effects
- [ ] Create particle systems for:
  - [ ] Perfect hit effect
  - [ ] Good hit effect
  - [ ] Miss effect
- [ ] Save as prefabs in `Assets/Prefabs/Effects/`
- [ ] Assign to EffectsManager

### Create Real Beat Maps
- [ ] Use BeatMapCreator for each song
- [ ] Save as JSON in `Assets/StreamingAssets/BeatMaps/`
- [ ] Test timing with actual music

### Testing on Device
- [ ] Build APK (File > Build Profiles > Build)
- [ ] Select Android platform if not already active
- [ ] Click "Build" and choose output location
- [ ] Install on Android device
- [ ] Test touch input
- [ ] Check performance
- [ ] Adjust settings as needed

---

## ?? Documentation Reference

All documentation is in the `Documents/` folder:
- **PROJECT_STRUCTURE.md** - Complete folder structure
- **SETUP_GUIDE.md** - Detailed setup instructions
- **USAGE_EXAMPLES.md** - Code examples and patterns

## ?? Troubleshooting

**Scripts don't compile:**
- Check Unity version (2021.3 LTS+)
- Verify all scripts are in correct folders
- Check Console for specific errors

**UI not responding:**
- Ensure EventSystem exists in scene
- Check Canvas settings
- Verify button OnClick events are assigned

**Audio not playing:**
- Check Audio Source settings
- Verify audio clips are assigned
- Check device/editor volume

**Notes not spawning:**
- Verify Note prefab is assigned
- Check spawn/target points are positioned
- Ensure beat map has notes

---

## ? Completion

Once all checkboxes are complete, your project is ready for:
- Adding custom content (graphics, audio, beat maps)
- Advanced features (more note types, effects, animations)
- Firebase integration for leaderboards
- Publishing to app stores

**Happy coding! ????**
