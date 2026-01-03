using UnityEngine;

namespace TapAlongWithBeat.Config
{
    /// <summary>
    /// Central configuration for game constants and settings.
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Tap Along With Beat/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [Header("Timing Windows (seconds)")]
        [Tooltip("Time window for perfect hit detection")]
        public float perfectWindow = 0.05f;

        [Tooltip("Time window for good hit detection")]
        public float goodWindow = 0.1f;

        [Tooltip("Time window for ok hit detection")]
        public float okWindow = 0.15f;

        [Header("Scoring")]
        [Tooltip("Points awarded for a perfect hit")]
        public int perfectHitScore = 100;

        [Tooltip("Points awarded for a good hit")]
        public int goodHitScore = 50;

        [Tooltip("Points awarded for an ok hit")]
        public int okHitScore = 25;

        [Tooltip("Maximum combo multiplier")]
        public int maxComboMultiplier = 4;

        [Tooltip("Combo count required for each multiplier level")]
        public int comboPerMultiplier = 10;

        [Header("Note Settings")]
        [Tooltip("Speed at which notes move")]
        public float noteSpeed = 5f;

        [Tooltip("How far ahead to spawn notes (seconds)")]
        public float noteSpawnOffset = 2f;

        [Header("Audio Settings")]
        [Tooltip("Default music volume")]
        [Range(0f, 1f)]
        public float defaultMusicVolume = 0.7f;

        [Tooltip("Default SFX volume")]
        [Range(0f, 1f)]
        public float defaultSFXVolume = 0.8f;

        [Header("Visual Settings")]
        [Tooltip("Enable particle effects")]
        public bool enableParticles = true;

        [Tooltip("Enable screen shake effects")]
        public bool enableScreenShake = true;

        [Tooltip("Enable combo text")]
        public bool enableComboText = true;

        [Header("Performance")]
        [Tooltip("Target frame rate")]
        public int targetFrameRate = 60;

        [Tooltip("Enable VSync")]
        public bool enableVSync = false;
    }
}
