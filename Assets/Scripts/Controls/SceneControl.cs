using System.Collections;
using System.Collections.Generic;
using AnimaresTest.Channels;
using UnityEngine;

namespace AnimaresTest.Controls
{
    [CreateAssetMenu(fileName = "ControlScene", menuName = "Animares/Controls/Scene")]
    public class SceneControl : ScriptableObject
    {
        public enum SceneStatus
        {
            Active,
            Deactivate
        }
        [SerializeField] public FloatChannel _fadeIn;
        [SerializeField] private FloatChannel _fadeOut;

        [SerializeField] private SceneStatus status = SceneStatus.Deactivate;

        public bool IsActive => status == SceneStatus.Active;
        public bool Deactivated {
            set {
                if (value)
                {
                    status = SceneStatus.Deactivate;
                }
                else
                {
                    status = SceneStatus.Active;
                }
            }
        }

        public void Activating(float fadeDuration)
        {
            _fadeIn.Trigger(fadeDuration);
            status = SceneStatus.Active;
        }
        public void Deactivating(float fadeDuration)
        {
            _fadeOut.Trigger(fadeDuration);
            status = SceneStatus.Deactivate;
        }
    }
}
