using UnityEngine;
using DG.Tweening;
using System;
namespace Efekan.Systems.PanelController
{
    public class PanelController : MonoBehaviour
    {
        [SerializeField] private RectTransform panel; // Kontrol edilecek UI paneli
        [SerializeField] private CanvasGroup canvasGroup; // Alfa geçişleri için CanvasGroup

        private Vector3 originalPosition;
        private Vector3 offScreenPosition;

        public float MovementDuration = 0.5f;
        public float ScaleDuration = 0.5f;
        public float FadeDuration = 0.5f;

        // Aksiyonlar
        public Action OnShowPanelStart;
        public Action OnShowPanelComplete;
        public Action OnHidePanelStart;
        public Action OnHidePanelComplete;
        public Action OnScaleInStart;
        public Action OnScaleInComplete;
        public Action OnScaleOutStart;
        public Action OnScaleOutComplete;
        public Action OnFadeInStart;
        public Action OnFadeInComplete;
        public Action OnFadeOutStart;
        public Action OnFadeOutComplete;

        private void Awake()
        {
            if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
            if (panel == null) panel = GetComponent<RectTransform>();

            // Panelin orijinal pozisyonunu kaydet
            originalPosition = panel.anchoredPosition;

            // Panelin ekrandan dışarı çıkacağı pozisyonu hesapla
            offScreenPosition = new Vector3(originalPosition.x, originalPosition.y + Screen.height, 0);
        }

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
