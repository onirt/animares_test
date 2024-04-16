using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AnimaresTest.Behaviours
{
    public class SwitchBehaviour : MonoBehaviour
    {
        [SerializeField] private UnityEvent _on;
        [SerializeField] private UnityEvent _off;
        [SerializeField] private bool _isOn;

        public void Switch()
        {
            if (_isOn)
            {
                On();
            }
            else
            {
                Off();
            }
        }

        public void On()
        {
            _on?.Invoke();
            _isOn = true;
        }
        public void Off()
        {
            _off?.Invoke();
            _isOn = false;
        }
    }
}
