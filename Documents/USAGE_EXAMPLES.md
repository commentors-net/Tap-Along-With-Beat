# Code Examples and Usage

This document provides examples of how to use the various systems in the Tap Along With Beat game.

## Creating a Beat Map

### Using BeatMapCreator Script

```csharp
using TapAlongWithBeat.Gameplay;
using TapAlongWithBeat.Editor;

// In your Unity scene or test script
BeatMapCreator creator = GetComponent<BeatMapCreator>();

// Create a beat map programmatically
BeatMapData beatMap = new BeatMapData
{
    songName = "My Awesome Song",
    bpm = 120f,
    audioClip = myAudioClip
};

// Add notes manually
beatMap.notes.Add(new NoteData
{
    hitTime = 2.0f,  // 2 seconds into the song
    lane = 0,         // Lane 0 (leftmost)
    noteType = NoteType.Tap
});

beatMap.notes.Add(new NoteData
{
    hitTime = 2.5f,
    lane = 1,
    noteType = NoteType.Tap
});

// Save beat map to file
creator.SaveBeatMap(beatMap, "Assets/StreamingAssets/BeatMaps/mysong.json");
```

## Starting Gameplay

### From Main Menu Button

```csharp
using TapAlongWithBeat.Core;
using TapAlongWithBeat.UI;

public class MainMenuController : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        // Load gameplay scene
        UIManager.Instance?.ShowGameplay();
        GameManager.Instance?.StartGame();
    }
}
```

### Starting with Custom Beat Map

```csharp
using TapAlongWithBeat.Gameplay;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private BeatMapData selectedBeatMap;
    
    public void StartLevel()
    {
        GameplayController controller = FindObjectOfType<GameplayController>();
        if (controller != null)
        {
            controller.StartGameplay(selectedBeatMap);
        }
    }
}
```

## Handling Score Events

### Subscribing to Score Changes

```csharp
using TapAlongWithBeat.Gameplay;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private void Start()
    {
        // Subscribe to score events
        ScoreManager.Instance.OnScoreChanged += HandleScoreChanged;
        ScoreManager.Instance.OnComboChanged += HandleComboChanged;
        ScoreManager.Instance.OnHitRegistered += HandleHitRegistered;
    }
    
    private void OnDestroy()
    {
        // Always unsubscribe to prevent memory leaks
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= HandleScoreChanged;
            ScoreManager.Instance.OnComboChanged -= HandleComboChanged;
            ScoreManager.Instance.OnHitRegistered -= HandleHitRegistered;
        }
    }
    
    private void HandleScoreChanged(int newScore)
    {
        Debug.Log($"Score updated: {newScore}");
        // Update your UI here
    }
    
    private void HandleComboChanged(int combo)
    {
        Debug.Log($"Combo: {combo}");
        // Show combo text animation
    }
    
    private void HandleHitRegistered(HitQuality quality)
    {
        Debug.Log($"Hit quality: {quality}");
        // Show hit feedback
    }
}
```

## Custom Note Types

### Extending NoteController

```csharp
using TapAlongWithBeat.Gameplay;
using UnityEngine;

public class HoldNoteController : NoteController
{
    [SerializeField] private float holdDuration = 1f;
    private bool isHolding = false;
    private float holdStartTime;
    
    private void Update()
    {
        base.Update();
        
        if (isHolding)
        {
            CheckHoldDuration();
        }
    }
    
    public override void OnTap()
    {
        isHolding = true;
        holdStartTime = Time.time;
        base.OnTap();
    }
    
    private void CheckHoldDuration()
    {
        float holdTime = Time.time - holdStartTime;
        
        if (Input.touchCount == 0 && Input.GetMouseButton(0) == false)
        {
            // Released too early
            if (holdTime < holdDuration)
            {
                ScoreManager.Instance?.RegisterHit(HitQuality.Miss);
            }
            isHolding = false;
        }
        else if (holdTime >= holdDuration)
        {
            // Held long enough
            ScoreManager.Instance?.RegisterHit(HitQuality.Perfect);
            isHolding = false;
            Destroy(gameObject);
        }
    }
}
```

## Audio Synchronization

### Syncing Gameplay with Music

```csharp
using TapAlongWithBeat.Core;
using TapAlongWithBeat.Utilities;
using UnityEngine;

public class BeatIndicator : MonoBehaviour
{
    [SerializeField] private float bpm = 120f;
    [SerializeField] private GameObject beatVisualizer;
    
    private float lastBeatTime = 0f;
    
    private void Update()
    {
        if (AudioManager.Instance == null || !AudioManager.Instance.IsPlaying)
            return;
        
        float currentTime = AudioManager.Instance.MusicTime;
        float beatInterval = GameUtilities.BPMToSecondsPerBeat(bpm);
        
        if (currentTime - lastBeatTime >= beatInterval)
        {
            OnBeat();
            lastBeatTime = currentTime;
        }
    }
    
    private void OnBeat()
    {
        // Visualize the beat
        if (beatVisualizer != null)
        {
            beatVisualizer.transform.localScale = Vector3.one * 1.2f;
            // Scale back down with animation
        }
    }
}
```

## Input Handling

### Custom Touch Handler

```csharp
using TapAlongWithBeat.Input;
using UnityEngine;

public class CustomTouchHandler : MonoBehaviour
{
    private void Start()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnTapDetected += HandleTap;
            InputManager.Instance.OnTapHold += HandleHold;
            InputManager.Instance.OnTapReleased += HandleRelease;
        }
    }
    
    private void OnDestroy()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnTapDetected -= HandleTap;
            InputManager.Instance.OnTapHold -= HandleHold;
            InputManager.Instance.OnTapReleased -= HandleRelease;
        }
    }
    
    private void HandleTap(Vector2 position)
    {
        Debug.Log($"Tapped at: {position}");
        // Spawn visual effect at tap position
    }
    
    private void HandleHold(Vector2 position, float duration)
    {
        Debug.Log($"Holding for {duration} seconds");
    }
    
    private void HandleRelease(Vector2 position)
    {
        Debug.Log($"Released at: {position}");
    }
}
```

## Saving and Loading Player Data

### Updating High Scores

```csharp
using TapAlongWithBeat.Data;
using TapAlongWithBeat.Gameplay;

public class GameOverHandler : MonoBehaviour
{
    public void OnGameEnd(string songName)
    {
        if (ScoreManager.Instance != null && PlayerDataManager.Instance != null)
        {
            ScoreData finalScore = ScoreManager.Instance.GetScoreData();
            
            // Update high score
            PlayerDataManager.Instance.UpdateHighScore(
                songName,
                finalScore.score,
                finalScore.accuracy
            );
            
            // Get high score for display
            HighScoreData highScore = PlayerDataManager.Instance.GetHighScore(songName);
            Debug.Log($"High Score: {highScore.score}");
        }
    }
}
```

## Using Game Config

### Loading Configuration

```csharp
using TapAlongWithBeat.Config;
using UnityEngine;

public class ConfigLoader : MonoBehaviour
{
    private void Start()
    {
        // Load from Resources folder
        GameConfig config = Resources.Load<GameConfig>("Config/GameConfig");
        
        if (config != null)
        {
            // Apply configuration
            Application.targetFrameRate = config.targetFrameRate;
            QualitySettings.vSyncCount = config.enableVSync ? 1 : 0;
            
            Debug.Log($"Loaded config: Perfect Window = {config.perfectWindow}s");
        }
    }
}
```

## Creating Visual Effects

### Custom Hit Effect

```csharp
using TapAlongWithBeat.Effects;
using TapAlongWithBeat.Gameplay;
using UnityEngine;

public class CustomHitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private AnimationCurve scaleCurve;
    
    public void PlayEffect(HitQuality quality, Vector3 position)
    {
        // Spawn at position
        transform.position = position;
        
        // Set color based on quality
        Color color = TapAlongWithBeat.Utilities.GameUtilities.GetHitQualityColor(quality);
        var main = particleSystem.main;
        main.startColor = color;
        
        // Play particle
        particleSystem.Play();
        
        // Animate scale
        StartCoroutine(AnimateScale());
    }
    
    private System.Collections.IEnumerator AnimateScale()
    {
        float duration = 0.5f;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float scale = scaleCurve.Evaluate(elapsed / duration);
            transform.localScale = Vector3.one * scale;
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
```

## Testing in Editor

### Quick Test Script

```csharp
using TapAlongWithBeat.Core;
using TapAlongWithBeat.Gameplay;
using UnityEngine;

public class QuickTest : MonoBehaviour
{
    [SerializeField] private AudioClip testSong;
    
    private void Start()
    {
        // Quick test of all systems
        Debug.Log("=== Quick System Test ===");
        
        // Test GameManager
        if (GameManager.Instance != null)
        {
            Debug.Log("? GameManager initialized");
        }
        
        // Test AudioManager
        if (AudioManager.Instance != null)
        {
            Debug.Log("? AudioManager initialized");
            if (testSong != null)
            {
                AudioManager.Instance.PlayMusic(testSong);
            }
        }
        
        // Test ScoreManager
        if (ScoreManager.Instance != null)
        {
            Debug.Log("? ScoreManager initialized");
            ScoreManager.Instance.RegisterHit(HitQuality.Perfect);
            Debug.Log($"  Test score: {ScoreManager.Instance.CurrentScore}");
        }
        
        Debug.Log("=== Test Complete ===");
    }
}
```

These examples demonstrate the core functionality of the game systems. Use them as templates for extending and customizing your rhythm game!
