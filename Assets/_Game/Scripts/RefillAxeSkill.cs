using System;
using System.Collections;
using UnityEngine;

namespace _Game.Scripts
{
    public class RefillAxeSkill : MonoBehaviour
    {
       [field: SerializeField] public float Cooldown;
       
       private AxeAmmunition _axeAmmunition;
       
       private bool _canActivate = true;
       
       public event Action<float> CooldownChanged;

       private void Awake()
       {
           _axeAmmunition = GetComponent<AxeAmmunition>();
       }

       private void Update()
       {
           if (Input.GetKeyDown(KeyCode.Alpha1))
           {
               Use();
           }
       }

       private void Use()
       {
           if (!_canActivate)
               return;

           _canActivate = false;
           _axeAmmunition.Add();
           
           StartCoroutine(StartReload());
       }
       
       private IEnumerator StartReload()
       {
           float elapsedTime = 0f;

           while (elapsedTime < Cooldown)
           {
               elapsedTime += Time.deltaTime;
               CooldownChanged?.Invoke(elapsedTime);
               yield return null;
           }

           _canActivate = true;
       }
    }
}