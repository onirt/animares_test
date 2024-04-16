using System;
using System.Collections;
using AnimaresTest.Channels;
using AnimaresTest.Controls;
using AnimaresTest.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace AnimaresTest.Managers
{
    /**
     * This class control the navigation between scenes. Trigger the scene events 
     **/
    public class NavigationManager : MonoBehaviour
    {
        [SerializeField] private BoolChannel _onSelected;
        [SerializeField] private BoolChannel _onRestart;
        [SerializeField] private SceneManager _sceneManager;
        [SerializeField] private SceneControl _scene1Control;
        [SerializeField] private SceneControl _scene2Control;
        [SerializeField] private SceneControl _scene3Control;


        [SerializeField] private UnityEvent _onScene1Active;
        [SerializeField] private UnityEvent _onScene2Active;
        [SerializeField] private UnityEvent _onScene3Active;

        [SerializeField] private float _fadeDuration = 1;

        private void Start()
        {
            _scene2Control.Deactivated = true;
            _scene3Control.Deactivated = true;
            GotoScene1();
        }

        private void OnEnable()
        {
            _onSelected.Action += Selected;
        }

        private void OnDisable()
        {
            _onSelected.Action -= Selected;
        }

        private void Selected(bool selected)
        {
            GotoScene3();
        }

        public void GotoScene1()
        {
            _onScene1Active?.Invoke();
            bool needsDelay = false;
            if (_scene2Control.IsActive)
            {
                _onRestart.Trigger(true);
                _scene2Control.Deactivating(_fadeDuration);
                needsDelay = true;
            }
            if (_scene3Control.IsActive)
            {
                _scene3Control.Deactivating(_fadeDuration);
                needsDelay = true;
            }
            if (needsDelay)
            {
                StartCoroutine(AfterFade(() =>
                {
                    _sceneManager.UnloadScene(AppUtils.SCENE2);
                    _sceneManager.UnloadScene(AppUtils.SCENE3);
                    _scene1Control.Activating(_fadeDuration);
                }));
            }
            else
            {
                _scene1Control.Activating(_fadeDuration);
            }
        }
        public void GotoScene2()
        {
            _onScene2Active?.Invoke();
            if (_scene3Control.IsActive)
            {
                _scene3Control.Deactivating(_fadeDuration);

                StartCoroutine(AfterFade(() =>
                {
                    _sceneManager.UnloadScene(AppUtils.SCENE3);
                    if (_scene2Control.IsActive)
                    {
                        _onRestart.Trigger(true);
                        _scene2Control.Activating(_fadeDuration);
                    }
                    _sceneManager.LoadScene(AppUtils.SCENE2);
                }));
            }
            else if (_scene1Control.IsActive)
            {
                _scene1Control.Deactivating(_fadeDuration);

                StartCoroutine(AfterFade(() =>
                {
                    _sceneManager.LoadScene(AppUtils.SCENE2);
                }));
            }
            else
            {
                _sceneManager.LoadScene(AppUtils.SCENE2);
            }
        }
        public void GotoScene3()
        {
            _onScene3Active?.Invoke();
            if (_scene2Control.IsActive)
            {
                _scene2Control.Deactivating(_fadeDuration);

                StartCoroutine(AfterFade(() =>
                {
                    _sceneManager.LoadScene(AppUtils.SCENE3);
                    _scene2Control.Deactivated = false;
                }));
            }
            else if (_scene1Control.IsActive)
            {
                _scene1Control.Deactivating(_fadeDuration);

                StartCoroutine(AfterFade(() =>
                {
                    _sceneManager.LoadScene(AppUtils.SCENE3);
                }));
            }
            else
            {
                _sceneManager.LoadScene(AppUtils.SCENE3);
            }
        }
        public void SceneLoaded(string scene)
        {
            switch (scene)
            {
                case AppUtils.SCENE2:
                    _onRestart.Trigger(true);
                    _scene2Control.Activating(_fadeDuration);
                    break;
                case AppUtils.SCENE3:
                    _scene3Control.Activating(_fadeDuration);
                    break;
            }
        }
        public void SceneUnloaded(string scene)
        {
            switch (scene)
            {
                case AppUtils.SCENE2:
                    _scene2Control.Deactivated = true;
                    break;
                case AppUtils.SCENE3:
                    _scene3Control.Deactivated = true;
                    break;
            }
        }
        private IEnumerator AfterFade(Action action)
        {
            yield return new WaitForSeconds(_fadeDuration);
            action?.Invoke();
        }
    }
}
