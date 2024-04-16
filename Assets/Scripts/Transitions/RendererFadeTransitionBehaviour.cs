
using UnityEngine;
using DG.Tweening;

namespace AnimaresTest.Transitions
{
    public class RendererFadeTransitionBehaviour : TransitionBehaviour<float>
    {
        [SerializeField] Renderer _renderer;

        public override void In()
        {
            Tween = _renderer.material.DOFade(EndValue, Duration);
            base.In();
        }

        public override void Out()
        {
            Tween = _renderer.material.DOFade(StartValue, Duration);
            base.Out();
        }

        public override void Restart()
        {
            _renderer.material.DORestart(false);
        }
    }
}
