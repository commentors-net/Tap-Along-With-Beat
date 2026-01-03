using UnityEngine;
using System;
using TapAlongWithBeat.Input;

namespace TapAlongWithBeat.UI
{
    /// <summary>
    /// Manages all UI elements and updates in the game.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance => _instance;

        [Header("UI Panels")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject gameplayPanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject gameOverPanel;

        [Header("Gameplay UI")]
        [SerializeField] private UnityEngine.UI.Text scoreText;
        [SerializeField] private UnityEngine.UI.Text comboText;
        [SerializeField] private UnityEngine.UI.Text accuracyText;
        [SerializeField] private UnityEngine.UI.Slider progressBar;

        [Header("Game Over UI")]
        [SerializeField] private UnityEngine.UI.Text finalScoreText;
        [SerializeField] private UnityEngine.UI.Text finalAccuracyText;
        [SerializeField] private UnityEngine.UI.Text maxComboText;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Start()
        {
            ShowMainMenu();
            SubscribeToEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void Update()
        {
            UpdateProgressBar();
        }

        private void SubscribeToEvents()
        {
            if (Gameplay.ScoreManager.Instance != null)
            {
                Gameplay.ScoreManager.Instance.OnScoreChanged += UpdateScore;
                Gameplay.ScoreManager.Instance.OnComboChanged += UpdateCombo;
            }
        }

        private void UnsubscribeFromEvents()
        {
            if (Gameplay.ScoreManager.Instance != null)
            {
                Gameplay.ScoreManager.Instance.OnScoreChanged -= UpdateScore;
                Gameplay.ScoreManager.Instance.OnComboChanged -= UpdateCombo;
            }
        }

        #region Panel Management

        private void HideAllPanels()
        {
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (gameplayPanel != null) gameplayPanel.SetActive(false);
            if (pausePanel != null) pausePanel.SetActive(false);
            if (gameOverPanel != null) gameOverPanel.SetActive(false);
        }

        public void ShowMainMenu()
        {
            HideAllPanels();
            if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        }

        public void ShowGameplay()
        {
            HideAllPanels();
            if (gameplayPanel != null) gameplayPanel.SetActive(true);
        }

        public void ShowPause()
        {
            if (pausePanel != null) pausePanel.SetActive(true);
        }

        public void HidePause()
        {
            if (pausePanel != null) pausePanel.SetActive(false);
        }

        public void ShowGameOver()
        {
            HideAllPanels();
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
                DisplayGameOverStats();
            }
        }

        #endregion

        #region Gameplay UI Updates

        private void UpdateScore(int newScore)
        {
            if (scoreText != null)
            {
                scoreText.text = $"Score: {newScore:N0}";
            }
        }

        private void UpdateCombo(int combo)
        {
            if (comboText != null)
            {
                if (combo > 0)
                {
                    comboText.text = $"Combo: x{combo}";
                    comboText.gameObject.SetActive(true);
                }
                else
                {
                    comboText.gameObject.SetActive(false);
                }
            }
        }

        private void UpdateAccuracy(float accuracy)
        {
            if (accuracyText != null)
            {
                accuracyText.text = $"Accuracy: {accuracy:F1}%";
            }
        }

        private void UpdateProgressBar()
        {
            if (progressBar != null && Core.AudioManager.Instance != null)
            {
                float progress = Core.AudioManager.Instance.MusicTime / Core.AudioManager.Instance.MusicLength;
                progressBar.value = progress;
            }
        }

        #endregion

        #region Game Over Stats

        private void DisplayGameOverStats()
        {
            if (Gameplay.ScoreManager.Instance == null)
                return;

            var scoreData = Gameplay.ScoreManager.Instance.GetScoreData();

            if (finalScoreText != null)
            {
                finalScoreText.text = $"Final Score: {scoreData.score:N0}";
            }

            if (finalAccuracyText != null)
            {
                finalAccuracyText.text = $"Accuracy: {scoreData.accuracy:F1}%";
            }

            if (maxComboText != null)
            {
                maxComboText.text = $"Max Combo: x{scoreData.maxCombo}";
            }
        }

        #endregion

        #region Button Callbacks

        public void OnPlayButtonClicked()
        {
            ShowGameplay();
            Core.GameManager.Instance?.StartGame();
        }

        public void OnPauseButtonClicked()
        {
            ShowPause();
            Core.GameManager.Instance?.PauseGame();
        }

        public void OnResumeButtonClicked()
        {
            HidePause();
            Core.GameManager.Instance?.ResumeGame();
        }

        public void OnRestartButtonClicked()
        {
            ShowGameplay();
            Core.GameManager.Instance?.RestartGame();
        }

        public void OnMainMenuButtonClicked()
        {
            ShowMainMenu();
            Core.GameManager.Instance?.ChangeState(Core.GameState.MainMenu);
        }

        public void OnQuitButtonClicked()
        {
            Core.GameManager.Instance?.QuitGame();
        }

        #endregion
    }
}
