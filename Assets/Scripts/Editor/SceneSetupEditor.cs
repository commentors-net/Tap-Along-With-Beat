using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace TapAlongWithBeat.Editor
{
    /// <summary>
    /// Editor utility to automatically set up MainMenu scene with all required managers.
    /// Access via: Tools > Tap Along With Beat > Setup Main Menu Scene
    /// </summary>
    public class SceneSetupEditor : EditorWindow
    {
        [MenuItem("Tools/Tap Along With Beat/Setup Main Menu Scene")]
        public static void SetupMainMenuScene()
        {
            if (!EditorUtility.DisplayDialog("Setup Main Menu Scene",
                "This will create all manager objects in the current scene. Continue?",
                "Yes", "Cancel"))
            {
                return;
            }

            Scene currentScene = SceneManager.GetActiveScene();
            
            // Create GameManager
            CreateManager("GameManager", "TapAlongWithBeat.Core.GameManager");
            
            // Create AudioManager with AudioSource
            CreateAudioManager();
            
            // Create InputManager
            CreateManager("InputManager", "TapAlongWithBeat.Input.InputManager");
            
            // Create ScoreManager
            CreateManager("ScoreManager", "TapAlongWithBeat.Gameplay.ScoreManager");
            
            // Create PlayerDataManager
            CreateManager("PlayerDataManager", "TapAlongWithBeat.Data.PlayerDataManager");
            
            // Mark scene as dirty
            EditorSceneManager.MarkSceneDirty(currentScene);
            
            Debug.Log("? Main Menu Scene setup complete! All managers have been created.");
            EditorUtility.DisplayDialog("Setup Complete",
                "Main Menu scene has been set up successfully!\n\n" +
                "Created:\n" +
                "- GameManager\n" +
                "- AudioManager\n" +
                "- InputManager\n" +
                "- ScoreManager\n" +
                "- PlayerDataManager\n\n" +
                "Next: Use 'Setup Main Menu UI' to create the UI elements.",
                "OK");
        }

        private static void CreateManager(string objectName, string typeName)
        {
            GameObject existing = GameObject.Find(objectName);
            if (existing != null)
            {
                Debug.LogWarning($"{objectName} already exists. Skipping...");
                return;
            }

            GameObject manager = new GameObject(objectName);
            System.Type type = System.Type.GetType(typeName + ", Assembly-CSharp");
            if (type != null)
            {
                manager.AddComponent(type);
                Debug.Log($"? Created {objectName}");
            }
            else
            {
                Debug.LogError($"? Could not find type: {typeName}");
            }
        }

        private static void CreateAudioManager()
        {
            GameObject existing = GameObject.Find("AudioManager");
            if (existing != null)
            {
                Debug.LogWarning("AudioManager already exists. Skipping...");
                return;
            }

            GameObject audioManager = new GameObject("AudioManager");
            System.Type type = System.Type.GetType("TapAlongWithBeat.Core.AudioManager, Assembly-CSharp");
            if (type != null)
            {
                audioManager.AddComponent(type);
                
                // Add and configure AudioSource
                AudioSource musicSource = audioManager.AddComponent<AudioSource>();
                musicSource.playOnAwake = false;
                musicSource.loop = false;
                musicSource.spatialBlend = 0f; // 2D
                
                Debug.Log("? Created AudioManager with AudioSource");
            }
            else
            {
                Debug.LogError("? Could not find AudioManager type");
            }
        }
    }
}
