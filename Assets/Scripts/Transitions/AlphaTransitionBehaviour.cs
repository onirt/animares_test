using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AnimaresTest.Transitions
{
    public class AlphaTransitionBehaviour : TransitionBehaviour<float>
    {
        [SerializeField] CanvasGroup _canvasGroup;

        public override void In()
        {
            Tween = _canvasGroup.DOFade(EndValue, Duration);
            base.In();
        }

        public override void Out()
        {
            Tween = _canvasGroup.DOFade(StartValue, Duration);
            base.Out();
        }

        public override void Restart()
        {
            _canvasGroup.alpha = StartValue;
        }
    }
}
