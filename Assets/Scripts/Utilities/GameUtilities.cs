using UnityEngine;

namespace TapAlongWithBeat.Utilities
{
    /// <summary>
    /// Utility class for common helper functions.
    /// </summary>
    public static class GameUtilities
    {
        /// <summary>
        /// Converts BPM to seconds per beat
        /// </summary>
        public static float BPMToSecondsPerBeat(float bpm)
        {
            return 60f / bpm;
        }

        /// <summary>
        /// Converts seconds to beats based on BPM
        /// </summary>
        public static float SecondToBeats(float seconds, float bpm)
        {
            return seconds / BPMToSecondsPerBeat(bpm);
        }

        /// <summary>
        /// Converts beats to seconds based on BPM
        /// </summary>
        public static float BeatsToSeconds(float beats, float bpm)
        {
            return beats * BPMToSecondsPerBeat(bpm);
        }

        /// <summary>
        /// Remaps a value from one range to another
        /// </summary>
        public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
        }

        /// <summary>
        /// Formats score with commas for better readability
        /// </summary>
        public static string FormatScore(int score)
        {
            return score.ToString("N0");
        }

        /// <summary>
        /// Formats accuracy percentage
        /// </summary>
        public static string FormatAccuracy(float accuracy)
        {
            return $"{accuracy:F1}%";
        }

        /// <summary>
        /// Calculates letter grade based on accuracy
        /// </summary>
        public static string GetLetterGrade(float accuracy)
        {
            if (accuracy >= 95f) return "S";
            if (accuracy >= 90f) return "A";
            if (accuracy >= 80f) return "B";
            if (accuracy >= 70f) return "C";
            if (accuracy >= 60f) return "D";
            return "F";
        }

        /// <summary>
        /// Safe destroy that works in both play mode and edit mode
        /// </summary>
        public static void SafeDestroy(Object obj)
        {
            if (obj == null) return;

            if (Application.isPlaying)
            {
                Object.Destroy(obj);
            }
            else
            {
                Object.DestroyImmediate(obj);
            }
        }

        /// <summary>
        /// Checks if the device is a mobile device
        /// </summary>
        public static bool IsMobileDevice()
        {
            return Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        }

        /// <summary>
        /// Gets a color based on hit quality
        /// </summary>
        public static Color GetHitQualityColor(Gameplay.HitQuality quality)
        {
            switch (quality)
            {
                case Gameplay.HitQuality.Perfect:
                    return new Color(1f, 0.84f, 0f); // Gold
                case Gameplay.HitQuality.Good:
                    return new Color(0f, 1f, 0f); // Green
                case Gameplay.HitQuality.Ok:
                    return new Color(1f, 0.5f, 0f); // Orange
                case Gameplay.HitQuality.Miss:
                    return new Color(1f, 0f, 0f); // Red
                default:
                    return Color.white;
            }
        }
    }
}
