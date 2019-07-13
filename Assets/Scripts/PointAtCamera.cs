using UnityEngine;

namespace AncientArmory
{
    public class PointAtCamera : MonoBehaviour
    {
        /// <summary>
        /// Continuously manipulate transform every frame.
        /// </summary>
        public bool updateEveryFrame = false;

        /// <summary>
        /// If this is null, will assume main camera.
        /// </summary>
        [Tooltip("If this is null, will assume main camera.")]
        public Transform cameraTransform;
        
        /// <summary>
        /// Cached transform attached to this gameObject.
        /// </summary>
        private Transform myTransform;//cache of transform on this object

        // Start is called before the first frame update
        void Start()
        {
            if (!cameraTransform)//if one has not bee provided
            {
                cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;//assume main camera
            }

            myTransform = this.gameObject.transform;//cache ref

            DoPointAtCamera();
        }

        private void OnEnable()
        {
            DoPointAtCamera();
        }

        // Update is called once per frame
        void Update()
        {
            if (updateEveryFrame)
            {
                myTransform.LookAt(cameraTransform);
            }
        }

        /// <summary>
        /// One-time transform manipulation.  updateEveryFrame to true for continuous.
        /// </summary>
        public void DoPointAtCamera()
        {
            myTransform.LookAt(cameraTransform);
        }

    }
}