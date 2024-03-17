using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class Curve : ScriptableObject
    {
        [SerializeField] private AnimationCurve _curve;

        public float Evaluate(float value) => _curve.Evaluate(value);
    }
}