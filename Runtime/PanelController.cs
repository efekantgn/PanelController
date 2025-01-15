using UnityEngine;
using DG.Tweening;
using System;

namespace Efekan.Systems.PanelController
{
    /// <summary>
    /// A system to control the movement, scaling, and fading of a UI panel in Unity.
    /// </summary>
    public class PanelController : MonoBehaviour
    {
        /// <summary>
        /// The UI panel to be controlled.
        /// </summary>
        [SerializeField] private RectTransform panel;

        /// <summary>
        /// CanvasGroup used for alpha transitions.
        /// </summary>
        [SerializeField] private CanvasGroup canvasGroup;

        /// <summary>
        /// The panel's original position on the screen.
        /// </summary>
        private Vector3 originalPosition;

        /// <summary>
        /// The position where the panel will move off-screen.
        /// </summary>
        private Vector3 offScreenPosition;

        /// <summary>
        /// Duration for movement transitions in seconds.
        /// </summary>
        public float MovementDuration = 0.5f;

        /// <summary>
        /// Duration for scaling transitions in seconds.
        /// </summary>
        public float ScaleDuration = 0.5f;

        /// <summary>
        /// Duration for fading transitions in seconds.
        /// </summary>
        public float FadeDuration = 0.5f;

        // Action events

        /// <summary>
        /// Invoked when the panel starts showing.
        /// </summary>
        public event Action OnShowPanelStart;

        /// <summary>
        /// Invoked when the panel finishes showing.
        /// </summary>
        public event Action OnShowPanelComplete;

        /// <summary>
        /// Invoked when the panel starts hiding.
        /// </summary>
        public event Action OnHidePanelStart;

        /// <summary>
        /// Invoked when the panel finishes hiding.
        /// </summary>
        public event Action OnHidePanelComplete;

        /// <summary>
        /// Invoked when the panel starts scaling in.
        /// </summary>
        public event Action OnScaleInStart;

        /// <summary>
        /// Invoked when the panel finishes scaling in.
        /// </summary>
        public event Action OnScaleInComplete;

        /// <summary>
        /// Invoked when the panel starts scaling out.
        /// </summary>
        public event Action OnScaleOutStart;

        /// <summary>
        /// Invoked when the panel finishes scaling out.
        /// </summary>
        public event Action OnScaleOutComplete;

        /// <summary>
        /// Invoked when the panel starts fading in.
        /// </summary>
        public event Action OnFadeInStart;

        /// <summary>
        /// Invoked when the panel finishes fading in.
        /// </summary>
        public event Action OnFadeInComplete;

        /// <summary>
        /// Invoked when the panel starts fading out.
        /// </summary>
        public event Action OnFadeOutStart;

        /// <summary>
        /// Invoked when the panel finishes fading out.
        /// </summary>
        public event Action OnFadeOutComplete;

        private void Awake()
        {
            if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
            if (panel == null) panel = GetComponent<RectTransform>();

            // Store the panel's original position.
            originalPosition = panel.anchoredPosition;

            // Calculate the position where the panel will move off-screen.
            offScreenPosition = new Vector3(originalPosition.x, originalPosition.y + Screen.height, 0);
        }

        /// <summary>
        /// Shows the panel by moving it to its original position.
        /// </summary>
        public void ShowPanel()
        {
            OnShowPanelStart?.Invoke();
            panel.gameObject.SetActive(true);
            panel.DOAnchorPos(originalPosition, MovementDuration).SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    OnShowPanelComplete?.Invoke();
                });
        }

        /// <summary>
        /// Hides the panel by moving it off-screen.
        /// </summary>
        public void HidePanel()
        {
            OnHidePanelStart?.Invoke();
            panel.DOAnchorPos(offScreenPosition, MovementDuration).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    panel.gameObject.SetActive(false);
                    OnHidePanelComplete?.Invoke();
                });
        }

        /// <summary>
        /// Scales the panel in from zero to its full size.
        /// </summary>
        public void ScaleIn()
        {
            OnScaleInStart?.Invoke();
            panel.localScale = Vector3.zero;
            panel.gameObject.SetActive(true);
            panel.DOScale(Vector3.one, ScaleDuration).SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    OnScaleInComplete?.Invoke();
                });
        }

        /// <summary>
        /// Scales the panel out from its full size to zero.
        /// </summary>
        public void ScaleOut()
        {
            OnScaleOutStart?.Invoke();
            panel.DOScale(Vector3.zero, ScaleDuration).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    panel.gameObject.SetActive(false);
                    OnScaleOutComplete?.Invoke();
                });
        }

        /// <summary>
        /// Fades the panel in by increasing its alpha to 1.
        /// </summary>
        public void FadeIn()
        {
            OnFadeInStart?.Invoke();
            canvasGroup.alpha = 0;
            panel.gameObject.SetActive(true);
            canvasGroup.DOFade(1, FadeDuration)
                .OnComplete(() =>
                {
                    OnFadeInComplete?.Invoke();
                });
        }

        /// <summary>
        /// Fades the panel out by decreasing its alpha to 0.
        /// </summary>
        public void FadeOut()
        {
            OnFadeOutStart?.Invoke();
            canvasGroup.DOFade(0, FadeDuration)
                .OnComplete(() =>
                {
                    panel.gameObject.SetActive(false);
                    OnFadeOutComplete?.Invoke();
                });
        }
    }
}
