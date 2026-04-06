using UnityEngine;
namespace RubikStudio_3Q_Crash.UI
{
    public class Light_VFX : MonoBehaviour
    {
        public Vector3 Rotation = Vector3.one;

        private Transform _transform;

        void Awake() 
        {
            _transform = transform;
        }

        void LateUpdate() 
        {
            _transform.Rotate(Rotation);
        }
    }
}