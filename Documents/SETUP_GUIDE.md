# Setup Guide - Tap Along With Beat

## Prerequisites

### Required Software
1. **Unity Hub** (Latest version)
2. **Unity Editor** (2021.3 LTS or later recommended)
3. **Visual Studio 2022** with "Game Development with Unity" workload
4. **Git** (for version control)

### Unity Modules Required
- Android Build Support
- iOS Build Support
- Visual Studio Editor package

## Step-by-Step Setup

### 1. Create Unity Project

```bash
# Navigate to your workspace
cd D:\Jobs\workspace\Unity-Workspace\Tap-Along-With-Beat
```

**In Unity Hub:**
1. Click "New Project"
2. Select "2D Core" template
3. Project Name: "Tap-Along-With-Beat"
4. Location: Your current directory
5. Click "Create Project"

### 2. Configure Project Settings

**File > Build Settings**
- Add scenes: MainMenu, Gameplay
- Switch to Android platform (or iOS)

**Edit > Project Settings**

**Player Settings:**
- Company Name: Your company name
- Product Name: Tap Along With Beat
- Default Icon: (Add your icon)
- Bundle Identifier: com.yourcompany.tapalongwithbeat

**Android Settings:**
- Minimum API Level: Android 7.0 (API level 24)
- Target API Level: Latest
- Scripting Backend: IL2CPP
- Target Architectures: ARM64

**iOS Settings:**
- Target minimum iOS Version: 12.0
- Camera Usage Description: (if using camera)
- Microphone Usage Description: (if recording)

**Quality Settings:**
- Set target frame rate to 60 FPS
- Disable VSync for better performance

**Audio Settings:**
- DSP Buffer Size: Best latency
- Sample Rate: 48000 Hz

### 3. Import Scripts

All C# scripts are already created in the `Assets/Scripts/` directory. Unity will automatically compile them.

**Verify Compilation:**
1. Open Unity
2. Wait for script compilation
3. Check Console for any errors
4. All scripts should compile successfully

### 4. Setup Visual Studio Integration

**Unity > Preferences > External Tools**
1. External Script Editor: Visual Studio 2022
2. Enable "Embedded packages"
3. Enable "Local packages"
4. Enable "Registry packages"
5. Click "Regenerate project files"

**In Visual Studio:**
1. Open the `.sln` file in your project directory
2. Enable GitHub Copilot (if installed)
3. Verify IntelliSense is working

### 5. Create Essential GameObjects

**Create Managers (in MainMenu scene):**

1. **GameManager**
   - Create Empty GameObject: "GameManager"
   - Add Component: GameManager script
   - Set DontDestroyOnLoad (script handles this)

2. **AudioManager**
   - Create Empty GameObject: "AudioManager"
   - Add Component: AudioManager script
   - Add Component: Audio Source (for music)
   - Configure Audio Source:
     - Play On Awake: false
     - Loop: false
     - Priority: 0

3. **InputManager**
   - Create Empty GameObject: "InputManager"
   - Add Component: InputManager script
   - Set Interactable Layer to "Notes"

4. **ScoreManager**
   - Create Empty GameObject: "ScoreManager"
   - Add Component: ScoreManager script

5. **UIManager**
   - Create Canvas: "GameUI"
   - Add Component: UIManager script
   - Create child panels for menus

6. **PlayerDataManager**
   - Create Empty GameObject: "PlayerDataManager"
   - Add Component: PlayerDataManager script

### 6. Create Note Prefab

1. Create 2D Sprite GameObject: "Note"
2. Add Components:
   - Sprite Renderer (use circle or custom sprite)
   - Circle Collider 2D
   - Rigidbody2D (set to Kinematic)
   - NoteController script
3. Configure NoteController:
   - Note Speed: 5
   - Perfect Window: 0.05
   - Good Window: 0.1
   - OK Window: 0.15
4. Save as Prefab: `Assets/Prefabs/Gameplay/Note.prefab`

### 7. Create UI Layout

**Main Menu Panel:**
- Title Text
- Play Button
- Settings Button
- Quit Button

**Gameplay Panel:**
- Score Text
- Combo Text
- Accuracy Text
- Progress Bar
- Pause Button

**Pause Panel:**
- Resume Button
- Restart Button
- Main Menu Button

**Game Over Panel:**
- Final Score Text
- Accuracy Text
- Max Combo Text
- Retry Button
- Main Menu Button

### 8. Setup Gameplay Scene

1. Create new scene: "Gameplay"
2. Add Camera (Orthographic)
3. Add Background sprite
4. Create GameObject: "NoteSpawner"
   - Add NoteSpawner script
   - Assign Note Prefab
   - Create Spawn Point (top of screen)
   - Create Target Point (bottom hit zone)
5. Create GameObject: "GameplayController"
   - Add GameplayController script
   - Link NoteSpawner reference

### 9. Create GameConfig Asset

1. Right-click in `Assets/Resources/Config/`
2. Create > Tap Along With Beat > Game Config
3. Configure default values:
   - Perfect Window: 0.05
   - Good Window: 0.1
   - OK Window: 0.15
   - Perfect Score: 100
   - Note Speed: 5

### 10. Test the Game

1. Create a simple beat map using BeatMapCreator
2. Assign to GameplayController
3. Press Play in Unity Editor
4. Test touch/click input
5. Verify score updates
6. Check audio synchronization

## Troubleshooting

### Scripts Don't Compile
- Check Unity version (2021.3 LTS or later)
- Verify all scripts are in correct folders
- Check for missing namespace references

### Input Not Working
- Verify Input System settings
- Check camera setup (should be main camera)
- Ensure EventSystem exists for UI

### Audio Not Playing
- Check Audio Source components
- Verify audio clips are assigned
- Check audio mixer settings
- Ensure device volume is up

### Build Errors
- Update Android SDK/NDK
- Check signing keys (Android)
- Verify provisioning profile (iOS)
- Clear build cache and rebuild

## Next Steps

1. Add custom graphics and animations
2. Create beat maps for your music tracks
3. Implement more note types (hold, slide)
4. Add particle effects
5. Integrate Firebase for leaderboards
6. Test on physical devices
7. Optimize performance
8. Publish to stores

## Development Best Practices

- Use version control (Git)
- Test on real devices frequently
- Profile performance regularly
- Keep beat map files organized
- Document custom additions
- Follow Unity naming conventions
- Comment complex logic
- Create backups before major changes

## Resources

- [Unity Documentation](https://docs.unity3d.com/)
- [C# Programming Guide](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Unity Best Practices](https://unity.com/how-to/programming-unity)
- [Mobile Optimization](https://docs.unity3d.com/Manual/MobileOptimization.html)

---

**For questions or issues, refer to the code comments or Unity community forums.**
