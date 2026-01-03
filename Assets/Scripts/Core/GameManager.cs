using UnityEngine;
using TapAlongWithBeat.Gameplay;
using TapAlongWithBeat.Data;

namespace TapAlongWithBeat.Core
{
    /// <summary>
    /// Main game manager that controls the overall game flow and state.
    /// Implements Singleton pattern for global access.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        _instance = go.AddComponent<GameManager>();
                    }
                }
                return _instance;
            }
        }

        [Header("Game Settings")]
        [SerializeField] private bool isPaused = false;
        [SerializeField] private float gameSpeed = 1f;

        private GameState _currentState = GameState.MainMenu;

        public GameState CurrentState => _currentState;
        public bool IsPaused => isPaused;
        public float GameSpeed => gameSpeed;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeGame();
        }

        private void Start()
        {
            // Initialize game systems
            Debug.Log("GameManager: Game initialized");
        }

        private void Update()
        {
            if (isPaused)
                return;

            // Update game logic based on current state
            UpdateGameState();
        }

        private void InitializeGame()
        {
            // Initialize core game systems
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void UpdateGameState()
        {
            switch (_currentState)
            {
                case GameState.MainMenu:
                    // Handle main menu logic
                    break;
                case GameState.Playing:
                    // Handle gameplay logic
                    break;
                case GameState.Paused:
                    // Handle pause logic
                    break;
                case GameState.GameOver:
                    // Handle game over logic
                    break;
            }
        }

        public void ChangeState(GameState newState)
        {
            _currentState = newState;
            Debug.Log($"GameManager: State changed to {newState}");

            switch (newState)
            {
                case GameState.Playing:
                    ResumeGame();
                    break;
                case GameState.Paused:
                    PauseGame();
                    break;
            }
        }

        public void StartGame()
        {
            ChangeState(GameState.Playing);
            AudioManager.Instance?.PlayMusic();
        }

        public void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0f;
            AudioManager.Instance?.PauseMusic();
        }

        public void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = gameSpeed;
            AudioManager.Instance?.ResumeMusic();
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            ScoreManager.Instance?.ResetScore();
            ChangeState(GameState.Playing);
        }

        public void GameOver()
        {
            ChangeState(GameState.GameOver);
            AudioManager.Instance?.StopMusic();
        }

        public void QuitGame()
        {
            Debug.Log("GameManager: Quitting game");
            Application.Quit();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus && _currentState == GameState.Playing)
            {
                PauseGame();
            }
        }

        private void OnApplicationQuit()
        {
            // Save game data before quitting
            PlayerDataManager.Instance?.SavePlayerData();
        }
    }

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver,
        Loading
    }
}
