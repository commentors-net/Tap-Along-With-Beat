using UnityEngine;
using System.Collections.Generic;

namespace TapAlongWithBeat.Gameplay
{
    /// <summary>
    /// Spawns and manages notes during gameplay based on beat map data.
    /// </summary>
    public class NoteSpawner : MonoBehaviour
    {
        [Header("Spawner Settings")]
        [SerializeField] private GameObject notePrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform targetPoint;
        [SerializeField] private float spawnOffsetTime = 2f; // Spawn notes X seconds before they should be hit

        [Header("Beat Map")]
        [SerializeField] private BeatMapData currentBeatMap;

        private List<NoteData> activeNotes = new List<NoteData>();
        private int currentNoteIndex = 0;
        private bool isSpawning = false;

        private void Start()
        {
            if (spawnPoint == null)
            {
                Debug.LogWarning("NoteSpawner: Spawn point not assigned!");
            }

            if (targetPoint == null)
            {
                Debug.LogWarning("NoteSpawner: Target point not assigned!");
            }
        }

        private void Update()
        {
            if (!isSpawning || currentBeatMap == null)
                return;

            SpawnNotes();
        }

        public void StartSpawning(BeatMapData beatMap)
        {
            currentBeatMap = beatMap;
            currentNoteIndex = 0;
            isSpawning = true;
            Debug.Log($"NoteSpawner: Started spawning with {beatMap.notes.Count} notes");
        }

        public void StopSpawning()
        {
            isSpawning = false;
            ClearAllNotes();
        }

        private void SpawnNotes()
        {
            if (Core.AudioManager.Instance == null || currentBeatMap == null)
                return;

            float currentTime = Core.AudioManager.Instance.MusicTime;

            while (currentNoteIndex < currentBeatMap.notes.Count)
            {
                NoteData noteData = currentBeatMap.notes[currentNoteIndex];
                float spawnTime = noteData.hitTime - spawnOffsetTime;

                if (currentTime >= spawnTime)
                {
                    SpawnNote(noteData);
                    currentNoteIndex++;
                }
                else
                {
                    break;
                }
            }
        }

        private void SpawnNote(NoteData noteData)
        {
            if (notePrefab == null || spawnPoint == null || targetPoint == null)
            {
                Debug.LogWarning("NoteSpawner: Missing required references!");
                return;
            }

            GameObject noteObject = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
            NoteController noteController = noteObject.GetComponent<NoteController>();

            if (noteController != null)
            {
                noteController.Initialize(spawnPoint.position, targetPoint.position, noteData.hitTime);
            }

            Debug.Log($"NoteSpawner: Spawned note at time {noteData.hitTime}");
        }

        private void ClearAllNotes()
        {
            NoteController[] notes = FindObjectsOfType<NoteController>();
            foreach (NoteController note in notes)
            {
                Destroy(note.gameObject);
            }
            activeNotes.Clear();
            currentNoteIndex = 0;
        }

        public void LoadBeatMap(BeatMapData beatMap)
        {
            currentBeatMap = beatMap;
            Debug.Log($"NoteSpawner: Loaded beat map with {beatMap.notes.Count} notes");
        }
    }

    [System.Serializable]
    public class BeatMapData
    {
        public string songName;
        public float bpm;
        public AudioClip audioClip;
        public List<NoteData> notes = new List<NoteData>();
    }

    [System.Serializable]
    public class NoteData
    {
        public float hitTime;
        public int lane;
        public NoteType noteType;
    }
}
