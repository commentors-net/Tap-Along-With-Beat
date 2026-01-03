using UnityEngine;

namespace TapAlongWithBeat.Gameplay
{
    /// <summary>
    /// Manages the game flow during gameplay including level progression and transitions.
    /// </summary>
    public class GameplayController : MonoBehaviour
    {
        [Header("Gameplay Components")]
        [SerializeField] private NoteSpawner noteSpawner;
        [SerializeField] private BeatMapData testBeatMap;

        [Header("Gameplay Settings")]
        [SerializeField] private float countdownDuration = 3f;
        [SerializeField] private bool autoStartMusic = true;

        private bool isPlaying = false;
        private float gameplayStartTime = 0f;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (isPlaying)
            {
                CheckGameplayEnd();
            }
        }

        private void Initialize()
        {
            if (noteSpawner == null)
            {
                noteSpawner = FindObjectOfType<NoteSpawner>();
            }

            // Subscribe to game manager events if needed
            Debug.Log("GameplayController: Initialized");
        }

        public void StartGameplay()
        {
            StartGameplay(testBeatMap);
        }

        public void StartGameplay(BeatMapData beatMap)
        {
            if (beatMap == null)
            {
                Debug.LogError("GameplayController: No beat map provided!");
                return;
            }

            Debug.Log($"GameplayController: Starting gameplay with {beatMap.songName}");

            // Reset score
            ScoreManager.Instance?.ResetScore();

            // Load beat map
            if (noteSpawner != null)
            {
                noteSpawner.LoadBeatMap(beatMap);
            }

            // Start countdown
            StartCoroutine(CountdownCoroutine(beatMap));
        }

        private System.Collections.IEnumerator CountdownCoroutine(BeatMapData beatMap)
        {
            // Show countdown UI
            for (int i = (int)countdownDuration; i > 0; i--)
            {
                Debug.Log($"Starting in {i}...");
                // TODO: Show countdown on UI
                yield return new WaitForSeconds(1f);
            }

            // Start music
            if (Core.AudioManager.Instance != null && beatMap.audioClip != null)
            {
                Core.AudioManager.Instance.PlayMusic(beatMap.audioClip);
            }

            // Start spawning notes
            if (noteSpawner != null)
            {
                noteSpawner.StartSpawning(beatMap);
            }

            isPlaying = true;
            gameplayStartTime = Time.time;

            Debug.Log("GameplayController: Gameplay started!");
        }

        private void CheckGameplayEnd()
        {
            // Check if music has ended
            if (Core.AudioManager.Instance != null && !Core.AudioManager.Instance.IsPlaying)
            {
                EndGameplay();
            }
        }

        public void EndGameplay()
        {
            if (!isPlaying)
                return;

            isPlaying = false;

            // Stop spawning
            if (noteSpawner != null)
            {
                noteSpawner.StopSpawning();
            }

            // Show game over screen
            Core.GameManager.Instance?.GameOver();
            UI.UIManager.Instance?.ShowGameOver();

            // Save high score
            if (ScoreManager.Instance != null && Data.PlayerDataManager.Instance != null)
            {
                var scoreData = ScoreManager.Instance.GetScoreData();
                Data.PlayerDataManager.Instance.UpdateHighScore(
                    testBeatMap?.songName ?? "Unknown",
                    scoreData.score,
                    scoreData.accuracy
                );
            }

            Debug.Log("GameplayController: Gameplay ended!");
        }

        public void PauseGameplay()
        {
            if (!isPlaying)
                return;

            Time.timeScale = 0f;
            Core.AudioManager.Instance?.PauseMusic();
            Debug.Log("GameplayController: Gameplay paused");
        }

        public void ResumeGameplay()
        {
            if (!isPlaying)
                return;

            Time.timeScale = 1f;
            Core.AudioManager.Instance?.ResumeMusic();
            Debug.Log("GameplayController: Gameplay resumed");
        }

        public void RestartGameplay()
        {
            StopAllCoroutines();
            
            if (noteSpawner != null)
            {
                noteSpawner.StopSpawning();
            }

            isPlaying = false;
            StartGameplay(testBeatMap);
        }
    }
}
