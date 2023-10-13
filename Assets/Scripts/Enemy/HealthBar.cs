using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class HealthBar: MonoBehaviour
    {
        [SerializeField] private Slider _slider;
     
        private Camera _camera;
        private float _time;
        private float _maxValues;

        public void Initialize(float maxValue,float durationTime)
        {
            _camera = Camera.main;
            _maxValues = maxValue;
            _time = durationTime;
            SetMaxValues();

        }
        private void Update()
        {
            _slider.transform.LookAt(transform.position + _camera.transform.forward);
        }
        private void OnDisable()
        {
            transform.DOKill(this);
        }


        private void Hide()
        {
            _slider.gameObject.SetActive(false);
        }

        private void Show()
        {
            _slider.gameObject.SetActive(true);
        }
        private void SetMaxValues()
        {
            _slider.maxValue = _maxValues;
            _slider.value = _maxValues;
            Show();
        }

        public void GetDamage()
        {
            _slider.DOValue(0, _time)
                   .OnComplete(() =>
                   {
                       Hide();
                   });
        }
    }
}