using System.Collections;
using System.Collections.Generic;
using AnimaresTest.Transitions;
using UnityEngine;

namespace AnimaresTest.UI
{
    public class View : MonoBehaviour
    {
        [SerializeField] private GameObject _content;

        private ITransition _transition;

        [ContextMenu("OnEnter")]
        public virtual void OnEnter()
        {
            _content.SetActive(true);

            if (_transition == null)
            {
                _transition = GetComponentInChildren<ITransition>();
                if (_transition != null)
                {
                    //_transition.OnOut.AddListener(() => _content.SetActive(false));
                }
                else
                {
                    Debug.LogError($"[No][Animation][Detected]: {name}");
                    return;
                }
            }

            _transition?.In();
        }
        [ContextMenu("OnExit")]
        public virtual void OnExit()
        {
            if (_transition != null)
            {
                _transition?.Out();
            }
            else
            {
                _content.SetActive(false);
            }
        }
    }
}
