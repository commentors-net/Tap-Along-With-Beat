using UnityEngine;
using System;

namespace TapAlongWithBeat.Gameplay
{
    /// <summary>
    /// Manages scoring system including combo, multipliers, and accuracy tracking.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        private static ScoreManager _instance;
        public static ScoreManager Instance => _instance;

        [Header("Score Settings")]
        [SerializeField] private int perfectHitScore = 100;
        [SerializeField] private int goodHitScore = 50;
        [SerializeField] private int okHitScore = 25;
        [SerializeField] private int maxComboMultiplier = 4;
        [SerializeField] private int comboForMultiplier = 10;

        private int currentScore = 0;
        private int currentCombo = 0;
        private int maxCombo = 0;
        private int perfectHits = 0;
        private int goodHits = 0;
        private int okHits = 0;
        private int missedHits = 0;
        private int totalNotes = 0;
        private float accuracy = 100f;

        public int CurrentScore => currentScore;
        public int CurrentCombo => currentCombo;
        public int MaxCombo => maxCombo;
        public float Accuracy => accuracy;

        public event Action<int> OnScoreChanged;
        public event Action<int> OnComboChanged;
        public event Action<HitQuality> OnHitRegistered;

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
            ResetScore();
        }

        public void RegisterHit(HitQuality quality)
        {
            totalNotes++;

            switch (quality)
            {
                case HitQuality.Perfect:
                    perfectHits++;
                    AddScore(perfectHitScore);
                    IncrementCombo();
                    break;

                case HitQuality.Good:
                    goodHits++;
                    AddScore(goodHitScore);
                    IncrementCombo();
                    break;

                case HitQuality.Ok:
                    okHits++;
                    AddScore(okHitScore);
                    IncrementCombo();
                    break;

                case HitQuality.Miss:
                    missedHits++;
                    ResetCombo();
                    break;
            }

            CalculateAccuracy();
            OnHitRegistered?.Invoke(quality);

            Debug.Log($"ScoreManager: {quality} hit! Score: {currentScore}, Combo: {currentCombo}");
        }

        private void AddScore(int baseScore)
        {
            int multiplier = GetComboMultiplier();
            int scoreToAdd = baseScore * multiplier;
            currentScore += scoreToAdd;
            OnScoreChanged?.Invoke(currentScore);
        }

        private void IncrementCombo()
        {
            currentCombo++;
            if (currentCombo > maxCombo)
            {
                maxCombo = currentCombo;
            }
            OnComboChanged?.Invoke(currentCombo);
        }

        private void ResetCombo()
        {
            currentCombo = 0;
            OnComboChanged?.Invoke(currentCombo);
        }

        private int GetComboMultiplier()
        {
            int multiplier = 1 + (currentCombo / comboForMultiplier);
            return Mathf.Min(multiplier, maxComboMultiplier);
        }

        private void CalculateAccuracy()
        {
            if (totalNotes == 0)
            {
                accuracy = 100f;
                return;
            }

            float totalWeight = (perfectHits * 1.0f) + (goodHits * 0.7f) + (okHits * 0.4f);
            accuracy = (totalWeight / totalNotes) * 100f;
        }

        public void ResetScore()
        {
            currentScore = 0;
            currentCombo = 0;
            maxCombo = 0;
            perfectHits = 0;
            goodHits = 0;
            okHits = 0;
            missedHits = 0;
            totalNotes = 0;
            accuracy = 100f;

            OnScoreChanged?.Invoke(currentScore);
            OnComboChanged?.Invoke(currentCombo);
        }

        public ScoreData GetScoreData()
        {
            return new ScoreData
            {
                score = currentScore,
                maxCombo = maxCombo,
                accuracy = accuracy,
                perfectHits = perfectHits,
                goodHits = goodHits,
                okHits = okHits,
                missedHits = missedHits,
                totalNotes = totalNotes
            };
        }
    }

    public enum HitQuality
    {
        Perfect,
        Good,
        Ok,
        Miss
    }

    [System.Serializable]
    public struct ScoreData
    {
        public int score;
        public int maxCombo;
        public float accuracy;
        public int perfectHits;
        public int goodHits;
        public int okHits;
        public int missedHits;
        public int totalNotes;
    }
}
