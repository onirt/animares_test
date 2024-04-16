using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using AnimaresTest.Channels;

namespace AnimaresTest.Transitions
{
    public abstract class TransitionBehaviour<T> : MonoBehaviour, ITransition<T>
    {
        [SerializeField] protected FloatChannel FadeInChannel;
        [SerializeField] protected FloatChannel FadeOutChannel;

        [field: SerializeField] public float Duration { get; set; }
        [field: SerializeField] public bool SyncEnable { get; set; }

        [field: SerializeField] public T StartValue { get; set; }
        [field: SerializeField] public T EndValue { get; set; }

        [field: SerializeField] public Tween Tween { get; set; }

        [field: SerializeField] public UnityEvent OnIn { get; set; }
        [field: SerializeField] public UnityEvent OnOut { get; set; }

        private void OnDestroy()
        {
            if (Tween != null)
            {
                Tween.Kill();
            }
        }
        private void OnEnable()
        {
            if (FadeInChannel)
            {
                FadeInChannel.Action += In;
            }
            if (FadeOutChannel)
            {
                FadeOutChannel.Action += Out;
            }
            if (SyncEnable)
            {
                Restart();
                In();
            }
        }

        private void OnDisable()
        {
            if (FadeInChannel)
            {
                FadeInChannel.Action -= In;
            }
            if (FadeOutChannel)
            {
                FadeOutChannel.Action -= Out;
            }
            if (SyncEnable)
            {
                Out();
            }
        }
        private void In(float duration)
        {
            Duration = duration;
            In();
        }
        private void Out(float duration)
        {
            Duration = duration;
            Out();
        }

        public virtual void In()
        {
            Tween.OnComplete(() =>
            {
                OnIn?.Invoke();
            }).Play();
        }

        public virtual void Out()
        {
            Tween.OnComplete(() =>
            {
                OnOut?.Invoke();
            }).Play();
        }

        public abstract void Restart();
    }
}
