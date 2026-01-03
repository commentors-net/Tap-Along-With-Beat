using UnityEngine;
using System.IO;

namespace TapAlongWithBeat.Data
{
    /// <summary>
    /// Manages player data including high scores, settings, and progression.
    /// Handles save/load operations.
    /// </summary>
    public class PlayerDataManager : MonoBehaviour
    {
        private static PlayerDataManager _instance;
        public static PlayerDataManager Instance => _instance;

        private const string SAVE_FILE_NAME = "playerdata.json";
        private PlayerData playerData;
        private string savePath;

        public PlayerData CurrentPlayerData => playerData;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            LoadPlayerData();
        }

        public void LoadPlayerData()
        {
            if (File.Exists(savePath))
            {
                try
                {
                    string json = File.ReadAllText(savePath);
                    playerData = JsonUtility.FromJson<PlayerData>(json);
                    Debug.Log("PlayerDataManager: Player data loaded successfully");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"PlayerDataManager: Error loading player data: {e.Message}");
                    CreateNewPlayerData();
                }
            }
            else
            {
                CreateNewPlayerData();
            }
        }

        public void SavePlayerData()
        {
            try
            {
                string json = JsonUtility.ToJson(playerData, true);
                File.WriteAllText(savePath, json);
                Debug.Log("PlayerDataManager: Player data saved successfully");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"PlayerDataManager: Error saving player data: {e.Message}");
            }
        }

        private void CreateNewPlayerData()
        {
            playerData = new PlayerData
            {
                playerName = "Player",
                totalScore = 0,
                gamesPlayed = 0,
                musicVolume = 0.7f,
                sfxVolume = 0.8f,
                lastPlayedDate = System.DateTime.Now.ToString()
            };

            SavePlayerData();
            Debug.Log("PlayerDataManager: New player data created");
        }

        public void UpdateHighScore(string songName, int score, float accuracy)
        {
            if (!playerData.highScores.ContainsKey(songName))
            {
                playerData.highScores.Add(songName, new HighScoreData
                {
                    score = score,
                    accuracy = accuracy,
                    dateAchieved = System.DateTime.Now.ToString()
                });
            }
            else if (score > playerData.highScores[songName].score)
            {
                playerData.highScores[songName].score = score;
                playerData.highScores[songName].accuracy = accuracy;
                playerData.highScores[songName].dateAchieved = System.DateTime.Now.ToString();
            }

            playerData.totalScore += score;
            playerData.gamesPlayed++;
            SavePlayerData();
        }

        public HighScoreData GetHighScore(string songName)
        {
            if (playerData.highScores.ContainsKey(songName))
            {
                return playerData.highScores[songName];
            }
            return new HighScoreData { score = 0, accuracy = 0f };
        }

        public void UpdateSettings(float musicVolume, float sfxVolume)
        {
            playerData.musicVolume = musicVolume;
            playerData.sfxVolume = sfxVolume;
            SavePlayerData();

            // Apply settings
            Core.AudioManager.Instance?.SetMusicVolume(musicVolume);
            Core.AudioManager.Instance?.SetSFXVolume(sfxVolume);
        }

        public void ResetPlayerData()
        {
            CreateNewPlayerData();
            Debug.Log("PlayerDataManager: Player data reset");
        }

        private void OnApplicationQuit()
        {
            SavePlayerData();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SavePlayerData();
            }
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int totalScore;
        public int gamesPlayed;
        public float musicVolume;
        public float sfxVolume;
        public string lastPlayedDate;
        public System.Collections.Generic.Dictionary<string, HighScoreData> highScores = 
            new System.Collections.Generic.Dictionary<string, HighScoreData>();
    }

    [System.Serializable]
    public class HighScoreData
    {
        public int score;
        public float accuracy;
        public string dateAchieved;
    }
}
