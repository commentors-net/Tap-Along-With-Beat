using UnityEngine;
using TapAlongWithBeat.Input;
using TapAlongWithBeat.Gameplay;
using TapAlongWithBeat.Core;

namespace TapAlongWithBeat.Gameplay
{
    /// <summary>
    /// Represents a single note/beat that the player must tap.
    /// Handles timing, hit detection, and visual feedback.
    /// </summary>
    public class NoteController : MonoBehaviour, ITappable
    {
        [Header("Note Settings")]
        [SerializeField] private float noteSpeed = 5f;
        [SerializeField] private NoteType noteType = NoteType.Tap;
        
        [Header("Timing Windows (in seconds)")]
        [SerializeField] private float perfectWindow = 0.05f;
        [SerializeField] private float goodWindow = 0.1f;
        [SerializeField] private float okWindow = 0.15f;
        
        [Header("Visual")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color hitColor = Color.green;

        private float spawnTime;
        private float targetHitTime;
        private bool isHit = false;
        private bool hasPassed = false;
        private Vector3 targetPosition;
        private Vector3 startPosition;

        public float TargetHitTime => targetHitTime;
        public bool IsActive => !isHit && !hasPassed;

        private void Awake()
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        private void Start()
        {
            spawnTime = Time.time;
            if (spriteRenderer != null)
            {
                spriteRenderer.color = normalColor;
            }
        }

        private void Update()
        {
            if (isHit || hasPassed)
                return;

            MoveNote();
            CheckMiss();
        }

        public void Initialize(Vector3 start, Vector3 target, float hitTime)
        {
            startPosition = start;
            targetPosition = target;
            targetHitTime = hitTime;
            transform.position = startPosition;
        }

        private void MoveNote()
        {
            // Move note towards target position
            float journey = (Time.time - spawnTime) * noteSpeed;
            transform.position = Vector3.Lerp(startPosition, targetPosition, journey);
        }

        private void CheckMiss()
        {
            float currentTime = AudioManager.Instance != null ? AudioManager.Instance.MusicTime : Time.time;
            
            if (currentTime > targetHitTime + okWindow)
            {
                Miss();
            }
        }

        public void OnTap()
        {
            if (isHit || hasPassed)
                return;

            float currentTime = AudioManager.Instance != null ? AudioManager.Instance.MusicTime : Time.time;
            float timingDifference = Mathf.Abs(currentTime - targetHitTime);

            HitQuality quality = EvaluateHit(timingDifference);
            RegisterHit(quality);
        }

        private HitQuality EvaluateHit(float timingDifference)
        {
            if (timingDifference <= perfectWindow)
            {
                return HitQuality.Perfect;
            }
            else if (timingDifference <= goodWindow)
            {
                return HitQuality.Good;
            }
            else if (timingDifference <= okWindow)
            {
                return HitQuality.Ok;
            }
            else
            {
                return HitQuality.Miss;
            }
        }

        private void RegisterHit(HitQuality quality)
        {
            isHit = true;

            // Update score
            ScoreManager.Instance?.RegisterHit(quality);

            // Play appropriate sound
            switch (quality)
            {
                case HitQuality.Perfect:
                    AudioManager.Instance?.PlayPerfectHitSound();
                    break;
                case HitQuality.Good:
                case HitQuality.Ok:
                    AudioManager.Instance?.PlayTapSound();
                    break;
            }

            // Visual feedback
            ShowHitEffect(quality);

            // Destroy note after a short delay
            Destroy(gameObject, 0.2f);
        }

        private void Miss()
        {
            hasPassed = true;
            ScoreManager.Instance?.RegisterHit(HitQuality.Miss);
            AudioManager.Instance?.PlayMissSound();
            
            // Fade out and destroy
            Destroy(gameObject, 0.5f);
        }

        private void ShowHitEffect(HitQuality quality)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = hitColor;
            }

            // TODO: Add particle effects, animations, etc.
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("HitZone"))
            {
                // Note entered hit zone
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("HitZone") && !isHit)
            {
                // Note left hit zone without being hit
                Miss();
            }
        }
    }

    public enum NoteType
    {
        Tap,
        Hold,
        Slide,
        Double
    }
}
