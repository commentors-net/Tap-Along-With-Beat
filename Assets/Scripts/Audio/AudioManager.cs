using UnityEngine;
using System.Collections;

namespace TapAlongWithBeat.Core
{
    /// <summary>
    /// Manages all audio in the game including music synchronization and sound effects.
    /// Critical for rhythm game mechanics.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        public static AudioManager Instance => _instance;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip[] musicTracks;
        [SerializeField] private AudioClip tapSound;
        [SerializeField] private AudioClip perfectHitSound;
        [SerializeField] private AudioClip missSound;

        [Header("Settings")]
        [SerializeField] private float musicVolume = 0.7f;
        [SerializeField] private float sfxVolume = 0.8f;

        private int currentTrackIndex = 0;
        private float musicStartTime = 0f;

        public float MusicTime => musicSource.time;
        public float MusicLength => musicSource.clip != null ? musicSource.clip.length : 0f;
        public bool IsPlaying => musicSource.isPlaying;
        public AudioClip CurrentTrack => musicSource.clip;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
        }

        private void Start()
        {
            ApplyAudioSettings();
        }

        private void Update()
        {
            // Monitor music playback for synchronization
            if (musicSource.isPlaying)
            {
                CheckMusicEvents();
            }
        }

        private void InitializeAudioSources()
        {
            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
            }

            if (sfxSource == null)
            {
                GameObject sfxObject = new GameObject("SFX Source");
                sfxObject.transform.SetParent(transform);
                sfxSource = sfxObject.AddComponent<AudioSource>();
            }

            musicSource.loop = false;
            musicSource.playOnAwake = false;
            sfxSource.playOnAwake = false;
        }

        private void ApplyAudioSettings()
        {
            musicSource.volume = musicVolume;
            sfxSource.volume = sfxVolume;
        }

        public void PlayMusic(int trackIndex = 0)
        {
            if (musicTracks == null || musicTracks.Length == 0)
            {
                Debug.LogWarning("AudioManager: No music tracks assigned");
                return;
            }

            currentTrackIndex = Mathf.Clamp(trackIndex, 0, musicTracks.Length - 1);
            musicSource.clip = musicTracks[currentTrackIndex];
            musicSource.Play();
            musicStartTime = Time.time;
            Debug.Log($"AudioManager: Playing track {currentTrackIndex}");
        }

        public void PlayMusic(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.LogWarning("AudioManager: Null audio clip provided");
                return;
            }

            musicSource.clip = clip;
            musicSource.Play();
            musicStartTime = Time.time;
        }

        public void PauseMusic()
        {
            if (musicSource.isPlaying)
            {
                musicSource.Pause();
            }
        }

        public void ResumeMusic()
        {
            if (!musicSource.isPlaying)
            {
                musicSource.UnPause();
            }
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public void PlaySFX(AudioClip clip, float volumeScale = 1f)
        {
            if (clip != null)
            {
                sfxSource.PlayOneShot(clip, volumeScale);
            }
        }

        public void PlayTapSound()
        {
            PlaySFX(tapSound);
        }

        public void PlayPerfectHitSound()
        {
            PlaySFX(perfectHitSound, 1.2f);
        }

        public void PlayMissSound()
        {
            PlaySFX(missSound, 0.8f);
        }

        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);
            musicSource.volume = musicVolume;
        }

        public void SetSFXVolume(float volume)
        {
            sfxVolume = Mathf.Clamp01(volume);
            sfxSource.volume = sfxVolume;
        }

        private void CheckMusicEvents()
        {
            // Check for music end
            if (!musicSource.isPlaying && musicSource.time == 0)
            {
                OnMusicEnded();
            }
        }

        private void OnMusicEnded()
        {
            Debug.Log("AudioManager: Music ended");
            GameManager.Instance?.GameOver();
        }

        public float GetBeatTime(float bpm)
        {
            return 60f / bpm;
        }

        public int GetCurrentBeat(float bpm)
        {
            float beatTime = GetBeatTime(bpm);
            return Mathf.FloorToInt(MusicTime / beatTime);
        }
    }
}
