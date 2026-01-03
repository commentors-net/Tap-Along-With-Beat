# Quick Reference Card

## ?? Essential Links

### Getting Started
? **[UNITY_SETUP_CHECKLIST.md](UNITY_SETUP_CHECKLIST.md)** - Start here!

### Need Help?
? **[INDEX.md](INDEX.md)** - Documentation navigation
? **[USAGE_EXAMPLES.md](USAGE_EXAMPLES.md)** - Code examples

### Reference
? **[PROJECT_STRUCTURE.md](PROJECT_STRUCTURE.md)** - Folder structure
? **[SETUP_GUIDE.md](SETUP_GUIDE.md)** - Detailed setup

---

## ?? Project Structure

```
Tap-Along-With-Beat/
??? Assets/Scripts/          (14 C# files ?)
??? Documents/               (7 documentation files ?)
??? README.md               (Project overview ?)
```

---

## ?? Core Systems

| System | Script | Purpose |
|--------|--------|---------|
| Game Flow | GameManager.cs | State management |
| Audio | AudioManager.cs | Music & SFX |
| Input | InputManager.cs | Touch/Mouse |
| Scoring | ScoreManager.cs | Score & Combos |
| Notes | NoteController.cs | Note behavior |
| Spawning | NoteSpawner.cs | Beat maps |
| Gameplay | GameplayController.cs | Level flow |
| UI | UIManager.cs | Panels & Display |
| Data | PlayerDataManager.cs | Save/Load |
| Effects | EffectsManager.cs | Visual feedback |

---

## ? Quick Commands

### Create Unity Project
```bash
# Open Unity Hub ? New Project ? 2D Core
# Name: Tap-Along-With-Beat
# Location: Current directory
```

### Open in Visual Studio
```bash
# Double-click .sln file after Unity creates it
# Or: Unity ? Assets ? Open C# Project
```

### Verify Scripts
```bash
# In Unity: Window ? Console
# Should be 0 errors after compilation
```

---

## ?? Common Tasks

### Create Beat Map
```csharp
// See: USAGE_EXAMPLES.md#creating-a-beat-map
BeatMapCreator creator = GetComponent<BeatMapCreator>();
BeatMapData beatMap = creator.CreateBeatMap();
```

### Subscribe to Score Events
```csharp
// See: USAGE_EXAMPLES.md#handling-score-events
ScoreManager.Instance.OnScoreChanged += HandleScoreChanged;
```

### Handle Input
```csharp
// See: USAGE_EXAMPLES.md#input-handling
InputManager.Instance.OnTapDetected += HandleTap;
```

---

## ?? Troubleshooting

| Issue | Solution |
|-------|----------|
| Scripts don't compile | Check Unity version (2021.3+) |
| UI not responding | Verify EventSystem exists |
| Audio not playing | Check AudioSource settings |
| Notes not spawning | Verify prefab assignment |

**Full Guide:** [SETUP_GUIDE.md](SETUP_GUIDE.md#troubleshooting)

---

## ?? Project Stats

- **C# Scripts:** 14 files (~2,000 lines)
- **Documentation:** 7 files (~3,000+ lines)
- **Code Examples:** 20+
- **Setup Steps:** 100+

---

## ? Checklist Progress

### Phase 1: Unity Setup
- [ ] Create Unity project
- [ ] Import scripts
- [ ] Configure settings

### Phase 2: Scenes
- [ ] Create MainMenu scene
- [ ] Create Gameplay scene
- [ ] Setup cameras

### Phase 3: Managers
- [ ] Create manager GameObjects
- [ ] Attach scripts
- [ ] Configure components

### Phase 4: UI
- [ ] Create Canvas
- [ ] Design UI layout
- [ ] Link references

### Phase 5: Gameplay
- [ ] Create Note prefab
- [ ] Setup NoteSpawner
- [ ] Configure timing

### Phase 6: Assets
- [ ] Import audio
- [ ] Import graphics
- [ ] Create effects

### Phase 7: Testing
- [ ] Test in Editor
- [ ] Build for device
- [ ] Verify functionality

**Full Checklist:** [UNITY_SETUP_CHECKLIST.md](UNITY_SETUP_CHECKLIST.md)

---

## ?? Platform Targets

- **Android:** API 24+ (Android 7.0+)
- **iOS:** iOS 12.0+
- **Editor:** Windows/Mac Unity

---

## ?? Important Files

| File | Location | Purpose |
|------|----------|---------|
| GameManager | Assets/Scripts/Core/ | Main controller |
| AudioManager | Assets/Scripts/Audio/ | Music system |
| NoteController | Assets/Scripts/Gameplay/ | Note behavior |
| UIManager | Assets/Scripts/UI/ | UI management |
| GameConfig | Assets/Scripts/Config/ | Settings |

---

## ?? Tips

1. **Start with the checklist** - Follow step-by-step
2. **Test early and often** - Verify each step
3. **Read the examples** - Learn the patterns
4. **Check documentation** - Everything is documented
5. **Ask for help** - Use GitHub Issues

---

## ?? Next Steps

1. ? Scripts created
2. ? Documentation ready
3. ? **Follow UNITY_SETUP_CHECKLIST.md**
4. ? Create scenes and UI
5. ? Add audio and graphics
6. ? Test and iterate
7. ? Publish to stores!

---

## ?? Support

- **Documentation:** [INDEX.md](INDEX.md)
- **Examples:** [USAGE_EXAMPLES.md](USAGE_EXAMPLES.md)
- **Setup Help:** [SETUP_GUIDE.md](SETUP_GUIDE.md)
- **Issues:** GitHub Issues
- **Contributions:** [CONTRIBUTING.md](CONTRIBUTING.md)

---

**?? Ready to build an amazing rhythm game! ??**

**Start here:** [UNITY_SETUP_CHECKLIST.md](UNITY_SETUP_CHECKLIST.md)
