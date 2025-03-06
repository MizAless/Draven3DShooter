using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts
{
    public class RefillAxeSkillView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private RefillAxeSkill _refillAxeSkill;

        private void OnEnable()
        {
            _refillAxeSkill.CooldownChanged += OnCooldownChanged;
        }
        
        private void OnDisable()
        {
            _refillAxeSkill.CooldownChanged -= OnCooldownChanged;
        }


        private void OnCooldownChanged(float value)
        {
            _slider.value = value / _refillAxeSkill.Cooldown;
        }
    }
}