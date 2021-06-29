using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CircuitDrawer : MonoBehaviour
    {
        public Race race;
        public LineRenderer lineRenderer;

        public float roadWidth = 5;
        public int textureQuality = 1024;

        public RawImage rawImage;
        public Camera cam;
        
        [ContextMenu("Create line")]
        public void Init()
        {
            var frames = race.road.bezierSpline.RotationMinimisingFrames;
            Vector3[] positions = new Vector3[frames.Count];
            lineRenderer.positionCount = frames.Count;

            Transform lineT = lineRenderer.transform;
            float minX = float.MaxValue, maxX = float.MinValue, minZ = float.MaxValue, 
                maxZ = float.MinValue, maxY = float.MinValue;
            Vector3 topPoint = Vector3.zero;
            for (var index = 0; index < frames.Count; index++)
            {
                Vector3 pos = frames[index].LocalOrigin;
                positions[index] = pos;
                pos = lineT.TransformPoint(pos);
                if (pos.x > maxX) maxX = pos.x;
                if (pos.x < minX) minX = pos.x;
                if (pos.z > maxZ) maxZ = pos.z;
                if (pos.z < minZ) minZ = pos.z;
                if (pos.y > maxY)
                {
                    maxY = pos.y;
                    topPoint = pos;
                }
            }

            lineRenderer.widthMultiplier = roadWidth;
            lineRenderer.SetPositions(positions);

            RenderTexture texture = new RenderTexture(textureQuality, textureQuality, 100);
            cam.targetTexture = texture;
            cam.orthographicSize = Mathf.Max(maxX - minX, maxZ - minZ)/2 + 100;
            
            cam.transform.position = new Vector3((maxX + minX) / 2, topPoint.y + roadWidth * 2, (maxZ + minZ) / 2);
            bool notAligned = true;
            while (notAligned)
            {
                notAligned = false;
                foreach (var position in positions)
                {
                    Vector3 globalPos = lineT.TransformPoint(position);
                    Vector2 viewportPoint = cam.WorldToViewportPoint(globalPos);
                    if (viewportPoint.x < 0 || viewportPoint.x > 1 || viewportPoint.y < 0 || viewportPoint.y > 1)
                    {
                        ++topPoint.y;
                        cam.transform.position = new Vector3((maxX + minX) / 2, topPoint.y + roadWidth, (maxZ + minZ) / 2);
                        notAligned = true;
                        break;
                    }
                }
            }
            cam.transform.position = new Vector3((maxX + minX) / 2, topPoint.y + roadWidth * 2, (maxZ + minZ) / 2);
            rawImage.texture = texture;
        }
    }
}
