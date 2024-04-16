using DG.Tweening;
using UnityEngine.Events;

namespace AnimaresTest.Transitions
{

    public interface ITransition<T> : ITransition
    {
        float Duration { get; set; }

        T StartValue { get; set; }
        T EndValue { get; set; }

    }
    public interface ITransition
    {

        Tween Tween { get; set; }

        UnityEvent OnIn { get; set; }
        UnityEvent OnOut { get; set; }

        void In();
        void Out();
    }
}
