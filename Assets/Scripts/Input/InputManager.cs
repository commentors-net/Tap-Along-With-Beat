using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TapAlongWithBeat.Core;

namespace TapAlongWithBeat.Input
{
    /// <summary>
    /// Handles all input for the rhythm game including tap detection and timing.
    /// Supports both touch (mobile) and mouse input.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        private static InputManager _instance;
        public static InputManager Instance => _instance;

        [Header("Input Settings")]
        [SerializeField] private bool enableTouchInput = true;
        [SerializeField] private bool enableMouseInput = true;
        [SerializeField] private LayerMask interactableLayer;

        public event Action<Vector2> OnTapDetected;
        public event Action<Vector2> OnTapReleased;
        public event Action<Vector2, float> OnTapHold;

        private Vector2 lastTapPosition;
        private float tapStartTime;
        private bool isTapping = false;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Update()
        {
            if (GameManager.Instance != null && GameManager.Instance.IsPaused)
                return;

            HandleInput();
        }

        private void HandleInput()
        {
            // Handle touch input for mobile
            if (enableTouchInput && UnityEngine.Input.touchCount > 0)
            {
                HandleTouchInput();
            }
            // Handle mouse input for testing in editor
            else if (enableMouseInput)
            {
                HandleMouseInput();
            }
        }

        private void HandleTouchInput()
        {
            Touch touch = UnityEngine.Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTapStart(touch.position);
                    break;

                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    if (isTapping)
                    {
                        float holdDuration = Time.time - tapStartTime;
                        OnTapHold?.Invoke(touch.position, holdDuration);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    OnTapEnd(touch.position);
                    break;
            }
        }

        private void HandleMouseInput()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnTapStart(UnityEngine.Input.mousePosition);
            }
            else if (UnityEngine.Input.GetMouseButton(0))
            {
                if (isTapping)
                {
                    float holdDuration = Time.time - tapStartTime;
                    OnTapHold?.Invoke(UnityEngine.Input.mousePosition, holdDuration);
                }
            }
            else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                OnTapEnd(UnityEngine.Input.mousePosition);
            }
        }

        private void OnTapStart(Vector2 position)
        {
            // Check if tapping on UI
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            isTapping = true;
            lastTapPosition = position;
            tapStartTime = Time.time;

            OnTapDetected?.Invoke(position);

            // Raycast to detect hit objects
            DetectTapOnGameObject(position);
        }

        private void OnTapEnd(Vector2 position)
        {
            if (!isTapping)
                return;

            isTapping = false;
            OnTapReleased?.Invoke(position);
        }

        private void DetectTapOnGameObject(Vector2 screenPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, interactableLayer);

            if (hit.collider != null)
            {
                ITappable tappable = hit.collider.GetComponent<ITappable>();
                if (tappable != null)
                {
                    tappable.OnTap();
                }
            }
        }

        public Vector2 GetWorldPosition(Vector2 screenPosition)
        {
            return Camera.main.ScreenToWorldPoint(screenPosition);
        }

        public float GetTapDuration()
        {
            return isTapping ? Time.time - tapStartTime : 0f;
        }

        public Vector2 GetLastTapPosition()
        {
            return lastTapPosition;
        }
    }

    /// <summary>
    /// Interface for objects that can be tapped
    /// </summary>
    public interface ITappable
    {
        void OnTap();
    }
}
