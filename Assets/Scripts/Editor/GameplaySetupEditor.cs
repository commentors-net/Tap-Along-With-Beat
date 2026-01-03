using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace TapAlongWithBeat.Editor
{
    /// <summary>
    /// Editor utility to automatically set up Gameplay scene with controllers and prefabs.
    /// Access via: Tools > Tap Along With Beat > Setup Gameplay Scene
    /// </summary>
    public class GameplaySetupEditor : EditorWindow
    {
        [MenuItem("Tools/Tap Along With Beat/Setup Gameplay Scene")]
        public static void SetupGameplayScene()
        {
            if (!EditorUtility.DisplayDialog("Setup Gameplay Scene",
                "This will create gameplay controllers in the current scene.\n\n" +
                "Note: Make sure you have created the Note prefab first!\n" +
                "(Use 'Create Note Prefab' tool first if you haven't)",
                "Continue", "Cancel"))
            {
                return;
            }

            Scene currentScene = SceneManager.GetActiveScene();
            
            // Create NoteSpawner
            CreateNoteSpawner();
            
            // Create GameplayController
            CreateGameplayController();
            
            // Create EffectsManager
            CreateEffectsManager();
            
            // Mark scene as dirty
            EditorSceneManager.MarkSceneDirty(currentScene);
            
            Debug.Log("? Gameplay Scene setup complete!");
            EditorUtility.DisplayDialog("Setup Complete",
                "Gameplay scene has been set up successfully!\n\n" +
                "Created:\n" +
                "- NoteSpawner (with SpawnPoint and TargetPoint)\n" +
                "- GameplayController\n" +
                "- EffectsManager\n\n" +
                "?? Remember to:\n" +
                "1. Assign Note Prefab to NoteSpawner\n" +
                "2. Assign NoteSpawner to GameplayController\n" +
                "3. Configure Camera settings (Orthographic, Size: 5)",
                "OK");
        }

        [MenuItem("Tools/Tap Along With Beat/Create Note Prefab")]
        public static void CreateNotePrefab()
        {
            if (!EditorUtility.DisplayDialog("Create Note Prefab",
                "This will create a Note prefab in Assets/Prefabs/Gameplay/.\n\n" +
                "A temporary GameObject will be created in the scene, configured, " +
                "saved as prefab, then removed.",
                "Create", "Cancel"))
            {
                return;
            }

            // Ensure folder exists
            if (!AssetDatabase.IsValidFolder("Assets/Prefabs"))
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            if (!AssetDatabase.IsValidFolder("Assets/Prefabs/Gameplay"))
                AssetDatabase.CreateFolder("Assets", "Gameplay");

            // Create temporary GameObject
            GameObject note = new GameObject("Note");
            
            // Add Sprite Renderer
            SpriteRenderer sr = note.AddComponent<SpriteRenderer>();
            sr.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Knob.psd");
            sr.color = Color.white;
            
            // Scale it appropriately
            note.transform.localScale = Vector3.one * 0.5f;
            
            // Add Circle Collider 2D
            CircleCollider2D collider = note.AddComponent<CircleCollider2D>();
            collider.radius = 0.5f;
            
            // Add Rigidbody 2D
            Rigidbody2D rb = note.AddComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.gravityScale = 0f;
            
            // Add NoteController script using reflection
            System.Type noteControllerType = System.Type.GetType("TapAlongWithBeat.Gameplay.NoteController, Assembly-CSharp");
            if (noteControllerType != null)
            {
                Component nc = note.AddComponent(noteControllerType);
                
                // Set serialized fields using SerializedObject
                SerializedObject serializedNote = new SerializedObject(nc);
                serializedNote.FindProperty("noteSpeed").floatValue = 5f;
                serializedNote.FindProperty("perfectWindow").floatValue = 0.05f;
                serializedNote.FindProperty("goodWindow").floatValue = 0.1f;
                serializedNote.FindProperty("okWindow").floatValue = 0.15f;
                serializedNote.FindProperty("spriteRenderer").objectReferenceValue = sr;
                serializedNote.FindProperty("normalColor").colorValue = Color.white;
                serializedNote.FindProperty("hitColor").colorValue = Color.green;
                serializedNote.ApplyModifiedProperties();
            }
            else
            {
                Debug.LogError("? Could not find NoteController type");
            }
            
            // Create prefab
            string prefabPath = "Assets/Prefabs/Gameplay/Note.prefab";
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(note, prefabPath);
            
            // Destroy temporary GameObject
            DestroyImmediate(note);
            
            // Select the prefab
            Selection.activeObject = prefab;
            EditorGUIUtility.PingObject(prefab);
            
            Debug.Log($"? Note prefab created at: {prefabPath}");
            EditorUtility.DisplayDialog("Prefab Created",
                $"Note prefab has been created successfully!\n\nLocation: {prefabPath}\n\n" +
                "The prefab is now selected in the Project window.",
                "OK");
        }

        private static void CreateNoteSpawner()
        {
            GameObject existing = GameObject.Find("NoteSpawner");
            if (existing != null)
            {
                Debug.LogWarning("NoteSpawner already exists. Skipping...");
                return;
            }

            GameObject noteSpawner = new GameObject("NoteSpawner");
            System.Type type = System.Type.GetType("TapAlongWithBeat.Gameplay.NoteSpawner, Assembly-CSharp");
            if (type != null)
            {
                noteSpawner.AddComponent(type);
            }
            
            // Create SpawnPoint child
            GameObject spawnPoint = new GameObject("SpawnPoint");
            spawnPoint.transform.SetParent(noteSpawner.transform);
            spawnPoint.transform.position = new Vector3(0, 6, 0);
            
            // Create TargetPoint child
            GameObject targetPoint = new GameObject("TargetPoint");
            targetPoint.transform.SetParent(noteSpawner.transform);
            targetPoint.transform.position = new Vector3(0, -4, 0);
            
            Debug.Log("? Created NoteSpawner with SpawnPoint (Y=6) and TargetPoint (Y=-4)");
            Debug.LogWarning("?? Remember to assign Note Prefab and transform references in NoteSpawner!");
        }

        private static void CreateGameplayController()
        {
            GameObject existing = GameObject.Find("GameplayController");
            if (existing != null)
            {
                Debug.LogWarning("GameplayController already exists. Skipping...");
                return;
            }

            GameObject gameplayController = new GameObject("GameplayController");
            System.Type type = System.Type.GetType("TapAlongWithBeat.Gameplay.GameplayController, Assembly-CSharp");
            if (type != null)
            {
                gameplayController.AddComponent(type);
                Debug.Log("? Created GameplayController");
                Debug.LogWarning("?? Remember to assign NoteSpawner reference in GameplayController!");
            }
        }

        private static void CreateEffectsManager()
        {
            GameObject existing = GameObject.Find("EffectsManager");
            if (existing != null)
            {
                Debug.LogWarning("EffectsManager already exists. Skipping...");
                return;
            }

            GameObject effectsManager = new GameObject("EffectsManager");
            System.Type type = System.Type.GetType("TapAlongWithBeat.Effects.EffectsManager, Assembly-CSharp");
            if (type != null)
            {
                effectsManager.AddComponent(type);
                Debug.Log("? Created EffectsManager");
            }
        }

        [MenuItem("Tools/Tap Along With Beat/Create Asset Folders")]
        public static void CreateAssetFolders()
        {
            string[] folders = new string[]
            {
                "Assets/Audio",
                "Assets/Audio/Music",
                "Assets/Audio/SFX",
                "Assets/Sprites",
                "Assets/Sprites/UI",
                "Assets/Sprites/Notes",
                "Assets/Sprites/Backgrounds",
                "Assets/Materials",
                "Assets/Prefabs",
                "Assets/Prefabs/Managers",
                "Assets/Prefabs/Gameplay",
                "Assets/Prefabs/Effects",
                "Assets/StreamingAssets",
                "Assets/StreamingAssets/BeatMaps",
                "Assets/Resources",
                "Assets/Resources/Config",
                "Assets/Fonts"
            };

            int created = 0;
            foreach (string folder in folders)
            {
                if (!AssetDatabase.IsValidFolder(folder))
                {
                    string[] parts = folder.Split('/');
                    string parent = parts[0];
                    
                    for (int i = 1; i < parts.Length; i++)
                    {
                        string fullPath = parent + "/" + parts[i];
                        if (!AssetDatabase.IsValidFolder(fullPath))
                        {
                            AssetDatabase.CreateFolder(parent, parts[i]);
                            created++;
                        }
                        parent = fullPath;
                    }
                }
            }

            AssetDatabase.Refresh();
            Debug.Log($"? Created {created} folders for project assets!");
            EditorUtility.DisplayDialog("Folders Created",
                $"Created {created} asset folders!\n\n" +
                "All necessary folders for audio, sprites, prefabs, and more have been set up.",
                "OK");
        }
    }
}
