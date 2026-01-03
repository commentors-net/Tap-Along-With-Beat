using UnityEngine;
using TapAlongWithBeat.Gameplay;

namespace TapAlongWithBeat.Editor
{
    /// <summary>
    /// Helper class to create beat maps from editor or runtime.
    /// </summary>
    public class BeatMapCreator : MonoBehaviour
    {
        [Header("Beat Map Creation")]
        [SerializeField] private string songName = "New Song";
        [SerializeField] private float bpm = 120f;
        [SerializeField] private AudioClip audioClip;

        [Header("Auto Generate Settings")]
        [SerializeField] private bool autoGenerateBeats = false;
        [SerializeField] private int numberOfBeats = 4;
        [SerializeField] private float startTime = 0f;

        public BeatMapData CreateBeatMap()
        {
            BeatMapData beatMap = new BeatMapData
            {
                songName = songName,
                bpm = bpm,
                audioClip = audioClip
            };

            if (autoGenerateBeats)
            {
                GenerateBeats(beatMap);
            }

            return beatMap;
        }

        private void GenerateBeats(BeatMapData beatMap)
        {
            float beatInterval = 60f / bpm;

            for (int i = 0; i < numberOfBeats; i++)
            {
                float hitTime = startTime + (i * beatInterval);
                
                NoteData note = new NoteData
                {
                    hitTime = hitTime,
                    lane = Random.Range(0, 4),
                    noteType = NoteType.Tap
                };

                beatMap.notes.Add(note);
            }

            Debug.Log($"BeatMapCreator: Generated {numberOfBeats} beats for {songName}");
        }

        [ContextMenu("Create Sample Beat Map")]
        public void CreateSampleBeatMap()
        {
            BeatMapData sampleBeatMap = CreateBeatMap();
            Debug.Log($"Created beat map: {sampleBeatMap.songName} with {sampleBeatMap.notes.Count} notes");
        }

        public void SaveBeatMap(BeatMapData beatMap, string path)
        {
            string json = JsonUtility.ToJson(beatMap, true);
            System.IO.File.WriteAllText(path, json);
            Debug.Log($"BeatMapCreator: Beat map saved to {path}");
        }

        public BeatMapData LoadBeatMap(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                Debug.LogError($"BeatMapCreator: File not found at {path}");
                return null;
            }

            string json = System.IO.File.ReadAllText(path);
            BeatMapData beatMap = JsonUtility.FromJson<BeatMapData>(json);
            Debug.Log($"BeatMapCreator: Beat map loaded from {path}");
            return beatMap;
        }
    }
}
