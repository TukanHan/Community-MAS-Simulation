using SpaceGeneration.UnityPort;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CameraMovement
{
    public class CameraMovement : SingletonBehaviour<CameraMovement>
    {
        public float speed = 2;

        public float maxCameraSize = 3;
        public float minCameraSize = 1.5f;

        private int mapWidth;
        private int mapHeight;

        void Start()
        {
            mapHeight = SpaceGenerator.instance.radius*3;
            mapWidth = SpaceGenerator.instance.radius*3;
        }

        void Update()
        {
            Vector2? movement;
            if ((movement = Move()).HasValue)
            {
                movement *= speed * Time.deltaTime * Camera.main.orthographicSize;
                SetCameraPosition(movement.Value + (Vector2)transform.position);
            }

            float? zoom;
            if ((zoom = Zoom()).HasValue)
            {
                SetCameraSize(zoom.Value);
            }
        }

        private void SetCameraSize(float change, bool deselect = true)
        {
            float size = Camera.main.orthographicSize -= change;
            Camera.main.orthographicSize = Mathf.Clamp(size, minCameraSize, maxCameraSize);

            SetCameraPosition(transform.position, deselect);
        }

        private void SetCameraPosition(Vector2 target, bool deselect = true)
        {
            float vertExtent = Camera.main.orthographicSize;
            float horzExtent = vertExtent * Screen.width / Screen.height;

            float minX = -mapWidth + horzExtent;
            float maxX = mapWidth - horzExtent;
            float minY = -mapHeight * 0.875f + vertExtent;
            float maxY = mapHeight * 0.875f - vertExtent;

            transform.position = new Vector3(Mathf.Clamp(target.x, minX, maxX), Mathf.Clamp(target.y, minY, maxY), transform.position.z);
        }

        private Vector2? Move()
        {
            if (Input.GetMouseButton(0) && CalculateDistance(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) > 0.2f)
            {
                return new Vector2(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
            }

            return null;
        }

        private float? Zoom()
        {
            float zoom = Input.GetAxis("Mouse ScrollWheel");
            if (zoom != 0)
                return zoom;

            return null;
        }

        private float CalculateDistance(float x, float y)
        {
            return new Vector2(x, y).sqrMagnitude;
        }
    }
}