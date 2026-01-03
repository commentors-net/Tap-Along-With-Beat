using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TapAlongWithBeat.Editor
{
    /// <summary>
    /// Editor utility to automatically set up UI elements and Canvas.
    /// Access via: Tools > Tap Along With Beat > Setup UI
    /// </summary>
    public class UISetupEditor : EditorWindow
    {
        [MenuItem("Tools/Tap Along With Beat/Setup Main Menu UI")]
        public static void SetupMainMenuUI()
        {
            if (!EditorUtility.DisplayDialog("Setup Main Menu UI",
                "This will create:\n" +
                "- Canvas (GameUI) with UIManager\n" +
                "- EventSystem\n" +
                "- Main Menu Panel with buttons\n\n" +
                "Continue?",
                "Yes", "Cancel"))
            {
                return;
            }

            Scene currentScene = SceneManager.GetActiveScene();
            
            // Create or get Canvas
            Canvas canvas = CreateCanvas();
            
            // Create EventSystem if doesn't exist
            CreateEventSystem();
            
            // Create Main Menu Panel
            CreateMainMenuPanel(canvas.transform);
            
            EditorSceneManager.MarkSceneDirty(currentScene);
            
            Debug.Log("? Main Menu UI setup complete!");
            EditorUtility.DisplayDialog("Setup Complete",
                "Main Menu UI has been created!\n\n" +
                "Created:\n" +
                "- GameUI Canvas with UIManager\n" +
                "- EventSystem\n" +
                "- MainMenuPanel with Play, Settings, Quit buttons\n\n" +
                "?? Remember to:\n" +
                "- Assign UI references in UIManager Inspector\n" +
                "- Connect button OnClick events",
                "OK");
        }

        [MenuItem("Tools/Tap Along With Beat/Setup Gameplay UI")]
        public static void SetupGameplayUI()
        {
            if (!EditorUtility.DisplayDialog("Setup Gameplay UI",
                "This will create:\n" +
                "- Canvas (GameUI) with UIManager\n" +
                "- EventSystem\n" +
                "- Gameplay Panel\n" +
                "- Pause Panel\n" +
                "- Game Over Panel\n\n" +
                "Continue?",
                "Yes", "Cancel"))
            {
                return;
            }

            Scene currentScene = SceneManager.GetActiveScene();
            
            // Create or get Canvas
            Canvas canvas = CreateCanvas();
            
            // Create EventSystem if doesn't exist
            CreateEventSystem();
            
            // Create Gameplay UI Panels
            CreateGameplayPanel(canvas.transform);
            CreatePausePanel(canvas.transform);
            CreateGameOverPanel(canvas.transform);
            
            EditorSceneManager.MarkSceneDirty(currentScene);
            
            Debug.Log("? Gameplay UI setup complete!");
            EditorUtility.DisplayDialog("Setup Complete",
                "Gameplay UI has been created!\n\n" +
                "Created:\n" +
                "- GameUI Canvas with UIManager\n" +
                "- EventSystem\n" +
                "- GameplayPanel (Score, Combo, Accuracy, Progress)\n" +
                "- PausePanel (Resume, Restart, Main Menu)\n" +
                "- GameOverPanel (Final stats, Retry, Main Menu)\n\n" +
                "?? Remember to:\n" +
                "- Assign UI references in UIManager Inspector\n" +
                "- Connect all button OnClick events",
                "OK");
        }

        private static Canvas CreateCanvas()
        {
            // Check if Canvas already exists
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            if (canvas != null)
            {
                canvas.gameObject.name = "GameUI";
                Debug.Log("? Using existing Canvas, renamed to GameUI");
            }
            else
            {
                GameObject canvasObj = new GameObject("GameUI");
                canvas = canvasObj.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                
                CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080);
                scaler.matchWidthOrHeight = 0.5f;
                
                canvasObj.AddComponent<GraphicRaycaster>();
                
                Debug.Log("? Created GameUI Canvas");
            }
            
            // Add UIManager if it doesn't exist using reflection
            System.Type uiManagerType = System.Type.GetType("TapAlongWithBeat.UI.UIManager, Assembly-CSharp");
            if (uiManagerType != null && canvas.GetComponent(uiManagerType) == null)
            {
                canvas.gameObject.AddComponent(uiManagerType);
                Debug.Log("? Added UIManager to Canvas");
            }
            
            return canvas;
        }

        private static void CreateEventSystem()
        {
            if (GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
                Debug.Log("? Created EventSystem");
            }
        }

        private static void CreateMainMenuPanel(Transform parent)
        {
            GameObject panel = CreatePanel("MainMenuPanel", parent);
            
            // Create Title Text
            GameObject title = CreateText("TitleText", panel.transform, "Tap Along With Beat", 72);
            RectTransform titleRect = title.GetComponent<RectTransform>();
            titleRect.anchoredPosition = new Vector2(0, 200);
            
            // Create Play Button
            GameObject playBtn = CreateButton("PlayButton", panel.transform, "Play", new Vector2(0, 50));
            
            // Create Settings Button
            GameObject settingsBtn = CreateButton("SettingsButton", panel.transform, "Settings", new Vector2(0, -50));
            
            // Create Quit Button
            GameObject quitBtn = CreateButton("QuitButton", panel.transform, "Quit", new Vector2(0, -150));
            
            Debug.Log("? Created Main Menu Panel with UI elements");
        }

        private static void CreateGameplayPanel(Transform parent)
        {
            GameObject panel = CreatePanel("GameplayPanel", parent);
            
            // Make it transparent background
            Image panelImage = panel.GetComponent<Image>();
            panelImage.color = new Color(0, 0, 0, 0);
            
            // Create Score Text (Top Left)
            GameObject scoreText = CreateText("ScoreText", panel.transform, "Score: 0", 36);
            RectTransform scoreRect = scoreText.GetComponent<RectTransform>();
            scoreRect.anchorMin = new Vector2(0, 1);
            scoreRect.anchorMax = new Vector2(0, 1);
            scoreRect.pivot = new Vector2(0, 1);
            scoreRect.anchoredPosition = new Vector2(20, -20);
            scoreRect.sizeDelta = new Vector2(300, 50);
            
            // Create Combo Text (Top Center)
            GameObject comboText = CreateText("ComboText", panel.transform, "Combo: x0", 48);
            RectTransform comboRect = comboText.GetComponent<RectTransform>();
            comboRect.anchoredPosition = new Vector2(0, 400);
            
            // Create Accuracy Text (Top Right)
            GameObject accuracyText = CreateText("AccuracyText", panel.transform, "100%", 36);
            RectTransform accuracyRect = accuracyText.GetComponent<RectTransform>();
            accuracyRect.anchorMin = new Vector2(1, 1);
            accuracyRect.anchorMax = new Vector2(1, 1);
            accuracyRect.pivot = new Vector2(1, 1);
            accuracyRect.anchoredPosition = new Vector2(-20, -20);
            accuracyRect.sizeDelta = new Vector2(200, 50);
            
            // Create Progress Bar (Bottom)
            GameObject progressBar = CreateSlider("ProgressBar", panel.transform, new Vector2(0, -450));
            
            // Create Pause Button (Top Right, below accuracy)
            GameObject pauseBtn = CreateButton("PauseButton", panel.transform, "||", new Vector2(0, 0));
            RectTransform pauseRect = pauseBtn.GetComponent<RectTransform>();
            pauseRect.anchorMin = new Vector2(1, 1);
            pauseRect.anchorMax = new Vector2(1, 1);
            pauseRect.pivot = new Vector2(1, 1);
            pauseRect.anchoredPosition = new Vector2(-20, -80);
            pauseRect.sizeDelta = new Vector2(80, 80);
            
            Debug.Log("? Created Gameplay Panel with HUD elements");
        }

        private static void CreatePausePanel(Transform parent)
        {
            GameObject panel = CreatePanel("PausePanel", parent);
            panel.SetActive(false); // Initially disabled
            
            // Make background semi-transparent
            Image panelImage = panel.GetComponent<Image>();
            panelImage.color = new Color(0, 0, 0, 0.8f);
            
            // Create Paused Text
            GameObject pausedText = CreateText("PausedText", panel.transform, "PAUSED", 72);
            RectTransform pausedRect = pausedText.GetComponent<RectTransform>();
            pausedRect.anchoredPosition = new Vector2(0, 200);
            
            // Create Resume Button
            GameObject resumeBtn = CreateButton("ResumeButton", panel.transform, "Resume", new Vector2(0, 50));
            
            // Create Restart Button
            GameObject restartBtn = CreateButton("RestartButton", panel.transform, "Restart", new Vector2(0, -50));
            
            // Create Main Menu Button
            GameObject menuBtn = CreateButton("MainMenuButton", panel.transform, "Main Menu", new Vector2(0, -150));
            
            Debug.Log("? Created Pause Panel (initially disabled)");
        }

        private static void CreateGameOverPanel(Transform parent)
        {
            GameObject panel = CreatePanel("GameOverPanel", parent);
            panel.SetActive(false); // Initially disabled
            
            // Make background semi-transparent
            Image panelImage = panel.GetComponent<Image>();
            panelImage.color = new Color(0, 0, 0, 0.9f);
            
            // Create Game Over Text
            GameObject gameOverText = CreateText("GameOverText", panel.transform, "GAME OVER", 72);
            RectTransform gameOverRect = gameOverText.GetComponent<RectTransform>();
            gameOverRect.anchoredPosition = new Vector2(0, 300);
            
            // Create Final Score Text
            GameObject finalScoreText = CreateText("FinalScoreText", panel.transform, "Score: 0", 48);
            RectTransform finalScoreRect = finalScoreText.GetComponent<RectTransform>();
            finalScoreRect.anchoredPosition = new Vector2(0, 150);
            
            // Create Final Accuracy Text
            GameObject finalAccuracyText = CreateText("FinalAccuracyText", panel.transform, "Accuracy: 100%", 36);
            RectTransform finalAccuracyRect = finalAccuracyText.GetComponent<RectTransform>();
            finalAccuracyRect.anchoredPosition = new Vector2(0, 80);
            
            // Create Max Combo Text
            GameObject maxComboText = CreateText("MaxComboText", panel.transform, "Max Combo: x0", 36);
            RectTransform maxComboRect = maxComboText.GetComponent<RectTransform>();
            maxComboRect.anchoredPosition = new Vector2(0, 10);
            
            // Create Retry Button
            GameObject retryBtn = CreateButton("RetryButton", panel.transform, "Retry", new Vector2(0, -100));
            
            // Create Main Menu Button
            GameObject menuBtn = CreateButton("MainMenuButton", panel.transform, "Main Menu", new Vector2(0, -200));
            
            Debug.Log("? Created Game Over Panel (initially disabled)");
        }

        private static GameObject CreatePanel(string name, Transform parent)
        {
            GameObject panel = new GameObject(name);
            panel.transform.SetParent(parent, false);
            
            RectTransform rect = panel.AddComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.sizeDelta = Vector2.zero;
            rect.anchoredPosition = Vector2.zero;
            
            Image image = panel.AddComponent<Image>();
            image.color = new Color(0.1f, 0.1f, 0.1f, 0.95f);
            
            return panel;
        }

        private static GameObject CreateButton(string name, Transform parent, string text, Vector2 position)
        {
            GameObject buttonObj = new GameObject(name);
            buttonObj.transform.SetParent(parent, false);
            
            RectTransform rect = buttonObj.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(300, 80);
            rect.anchoredPosition = position;
            
            Image image = buttonObj.AddComponent<Image>();
            image.color = new Color(0.2f, 0.6f, 1f, 1f);
            
            Button button = buttonObj.AddComponent<Button>();
            
            // Create Text child
            GameObject textObj = new GameObject("Text");
            textObj.transform.SetParent(buttonObj.transform, false);
            
            RectTransform textRect = textObj.AddComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.sizeDelta = Vector2.zero;
            
            // Use Unity's built-in Text component instead of TextMeshPro
            Text txt = textObj.AddComponent<Text>();
            txt.text = text;
            txt.fontSize = 36;
            txt.color = Color.white;
            txt.alignment = TextAnchor.MiddleCenter;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            
            return buttonObj;
        }

        private static GameObject CreateText(string name, Transform parent, string text, int fontSize)
        {
            GameObject textObj = new GameObject(name);
            textObj.transform.SetParent(parent, false);
            
            RectTransform rect = textObj.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(800, 100);
            
            // Use Unity's built-in Text component instead of TextMeshPro
            Text txt = textObj.AddComponent<Text>();
            txt.text = text;
            txt.fontSize = fontSize;
            txt.color = Color.white;
            txt.alignment = TextAnchor.MiddleCenter;
            txt.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            
            return textObj;
        }

        private static GameObject CreateSlider(string name, Transform parent, Vector2 position)
        {
            GameObject sliderObj = new GameObject(name);
            sliderObj.transform.SetParent(parent, false);
            
            RectTransform rect = sliderObj.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(800, 30);
            rect.anchoredPosition = position;
            
            Slider slider = sliderObj.AddComponent<Slider>();
            slider.minValue = 0;
            slider.maxValue = 1;
            slider.value = 0;
            
            // Create Background
            GameObject bg = new GameObject("Background");
            bg.transform.SetParent(sliderObj.transform, false);
            RectTransform bgRect = bg.AddComponent<RectTransform>();
            bgRect.anchorMin = Vector2.zero;
            bgRect.anchorMax = Vector2.one;
            bgRect.sizeDelta = Vector2.zero;
            Image bgImage = bg.AddComponent<Image>();
            bgImage.color = new Color(0.2f, 0.2f, 0.2f, 1f);
            
            // Create Fill Area
            GameObject fillArea = new GameObject("Fill Area");
            fillArea.transform.SetParent(sliderObj.transform, false);
            RectTransform fillAreaRect = fillArea.AddComponent<RectTransform>();
            fillAreaRect.anchorMin = Vector2.zero;
            fillAreaRect.anchorMax = Vector2.one;
            fillAreaRect.sizeDelta = Vector2.zero;
            
            // Create Fill
            GameObject fill = new GameObject("Fill");
            fill.transform.SetParent(fillArea.transform, false);
            RectTransform fillRect = fill.AddComponent<RectTransform>();
            fillRect.anchorMin = Vector2.zero;
            fillRect.anchorMax = Vector2.one;
            fillRect.sizeDelta = Vector2.zero;
            Image fillImage = fill.AddComponent<Image>();
            fillImage.color = new Color(0.2f, 0.8f, 0.3f, 1f);
            
            slider.fillRect = fillRect;
            
            return sliderObj;
        }
    }
}
