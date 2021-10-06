using UnityEngine;

namespace Quan_Utility {
    public class DragMouseOrbit : MonoBehaviour
    {
        public Transform target;
        public float pinchSpeed = 1f;
        public float distance = 2.0f;
        public float xSpeed = 20.0f;
        public float ySpeed = 20.0f;
        public float yMinLimit = -90f;
        public float yMaxLimit = 90f;
        public float distanceMin = 10f;
        public float distanceMax = 10f;
        public float smoothTime = 2f;
        float rotationYAxis = 0.0f;
        float rotationXAxis = 0.0f;
        float velocityX = 0.0f;
        float velocityY = 0.0f;

        public void Register(Transform t) => target = t;

        void Start()
        {
            Vector3 angles = transform.eulerAngles;
            rotationYAxis = angles.y;
            rotationXAxis = angles.x;
            // Make the rigid body not change rotation
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().freezeRotation = true;
            }
        }

        void Update()
        {
            if (target)
            {
                DragMouse();
                //  Occlusion();
            }
        }

        //void Occlusion()
        //{
        //    RaycastHit raycastHit;

        //    if(Physics.Linecast(target.transform.position ,transform.position, out raycastHit))
        //    {
        //        if(!raycastHit.transform.IsChildOf(target))
        //            transform.position = raycastHit.point;
        //    }

        //}

        void DragMouse()
        {
            if (Input.GetMouseButton(0))
            {
                velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
                velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
            }

            rotationYAxis += velocityX;
            rotationXAxis -= velocityY;
            rotationXAxis = QuanMathf.ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
            Quaternion fromRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
            Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
            Quaternion rotation = toRotation;

            float newDistance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            distance = Mathf.Lerp(distance, newDistance, Time.deltaTime * pinchSpeed);

            //RaycastHit hit;
            //if (Physics.Linecast(target.position, transform.position, out hit))
            //{
            //    distance -= hit.distance;
            //}
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;

            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
        }


    }
    
}