using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using AnimaresTest.Channels;
using UnityEngine.Events;

namespace AnimaresTest.Behaviours
{
    public class SelectedBehaviour : MonoBehaviour
    {
        [SerializeField] private BoolChannel _selectedChannel;
        [SerializeField] private BoolChannel _restartChannel;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _center;

        [SerializeField] private UnityEvent _onSelected;
        [SerializeField] private UnityEvent _onRestart;

        [SerializeField] private bool _selected;

        private void OnEnable()
        {
            _restartChannel.Action += Restart;
        }
        private void OnDisable()
        {
            _restartChannel.Action -= Restart;
        }

        private void OnMouseDown()
        {
            if (_selected)
            {
                return;
            }
            transform.SetParent(_center);
            _selected = true;
            _onSelected?.Invoke();
            _selectedChannel.Trigger(true);
            transform.DOLocalMove(Vector3.zero, 1);
        }

        public void Restart(bool restart)
        {
            Debug.Log("[Sceen2][Restart]");
            if (_selected)
            {
                transform.SetParent(_parent);
                _selected = false;
                _onRestart?.Invoke();
                transform.DOLocalMove(Vector3.zero, 1);
            }
        }
    }
}