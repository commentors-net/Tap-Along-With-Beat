using UnityEngine;
using System.Collections;

namespace TapAlongWithBeat.Effects
{
    /// <summary>
    /// Manages visual effects like particle systems, animations, and feedback.
    /// </summary>
    public class EffectsManager : MonoBehaviour
    {
        private static EffectsManager _instance;
        public static EffectsManager Instance => _instance;

        [Header("Particle Effects")]
        [SerializeField] private GameObject perfectHitParticle;
        [SerializeField] private GameObject goodHitParticle;
        [SerializeField] private GameObject missParticle;

        [Header("Screen Effects")]
        [SerializeField] private bool enableScreenShake = true;
        [SerializeField] private float shakeIntensity = 0.1f;
        [SerializeField] private float shakeDuration = 0.1f;

        private Camera mainCamera;
        private Vector3 originalCameraPosition;
        private bool isShaking = false;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            mainCamera = Camera.main;
            if (mainCamera != null)
            {
                originalCameraPosition = mainCamera.transform.localPosition;
            }
        }

        public void PlayHitEffect(Vector3 position, Gameplay.HitQuality quality)
        {
            GameObject particlePrefab = null;

            switch (quality)
            {
                case Gameplay.HitQuality.Perfect:
                    particlePrefab = perfectHitParticle;
                    if (enableScreenShake)
                    {
                        StartCoroutine(ScreenShake(shakeIntensity * 0.5f, shakeDuration * 0.5f));
                    }
                    break;

                case Gameplay.HitQuality.Good:
                    particlePrefab = goodHitParticle;
                    break;

                case Gameplay.HitQuality.Miss:
                    particlePrefab = missParticle;
                    break;
            }

            if (particlePrefab != null)
            {
                GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);
                Destroy(particle, 2f);
            }
        }

        private IEnumerator ScreenShake(float intensity, float duration)
        {
            if (isShaking || mainCamera == null)
                yield break;

            isShaking = true;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * intensity;
                float y = Random.Range(-1f, 1f) * intensity;

                mainCamera.transform.localPosition = originalCameraPosition + new Vector3(x, y, 0);

                elapsed += Time.deltaTime;
                yield return null;
            }

            mainCamera.transform.localPosition = originalCameraPosition;
            isShaking = false;
        }

        public void FlashScreen(Color color, float duration)
        {
            StartCoroutine(FlashScreenCoroutine(color, duration));
        }

        private IEnumerator FlashScreenCoroutine(Color color, float duration)
        {
            // TODO: Implement screen flash using UI overlay
            yield return new WaitForSeconds(duration);
        }

        public void ShowComboText(int combo, Vector3 position)
        {
            // TODO: Implement floating combo text
            Debug.Log($"Combo: {combo}");
        }
    }
}
