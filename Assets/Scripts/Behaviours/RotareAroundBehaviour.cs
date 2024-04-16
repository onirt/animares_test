using UnityEngine;

namespace AnimaresTest.Behaviours
{
    public class RotareAroundBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _axis = Vector3.up;
        [SerializeField] private float _rotationSpeed = 2;

        void Update()
        {
            transform.RotateAround(_target.position, _axis, _rotationSpeed * Time.deltaTime);
        }
    }
}
