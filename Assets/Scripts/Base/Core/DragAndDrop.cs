using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quan_Utility
{
    public class DragAndDrop : MonoBehaviour
    {
        private bool isDragging = false;
        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        public void OnMouseDown()
        {
            isDragging = true;
        }

        public void OnMouseUp()
        {
            isDragging = false;
        }

        void Update()
        {
            if (isDragging)
            {
                Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                transform.Translate(mousePosition);
            }
        }
    }
}