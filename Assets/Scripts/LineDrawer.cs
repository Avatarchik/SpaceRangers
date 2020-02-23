using UnityEngine;

namespace DefaultNamespace
{
    public class LineDrawer : MonoBehaviour
    {
        [SerializeField]private GameObject player;
        [SerializeField]private GameObject target;
        private LineRenderer lineRenderer;
        private GameManager gameManager;

        public void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.widthMultiplier = 0.01f;
        }

        public void Update()
        {
            lineRenderer.SetPosition(0, player.transform.position);
            lineRenderer.SetPosition(1, target.transform.position);

            /*var bezierP1 = player.transform.position;
            var bezierP2 = player.transform.position;
            
            Vector3 relative;
            relative = player.transform.InverseTransformDirection(target.transform.position);
            Debug.Log(relative);*/

            /*Vector3 aToB = target.transform.position - player.transform.position;
            Vector3 relativePosition = player.transform.InverseTransformPoint(aToB);
            Debug.Log(relativePosition);*/
            
            //DrawQuadraticBezierCurve(player.transform.position, bezierP1, target.transform.position);
            //DrawCubicBezierCurve(player.transform.position, bezierP1, bezierP2, target.transform.position);
        }
        
        void DrawQuadraticBezierCurve(Vector3 point0, Vector3 point1, Vector3 point2)
        {
            lineRenderer.positionCount = 50;
            float t = 0f;
            Vector3 B = new Vector3(0, 0, 0);
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
                lineRenderer.SetPosition(i, B);
                t += (1 / (float)lineRenderer.positionCount);
            }
        }
        
        void DrawCubicBezierCurve(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3)
        {

            lineRenderer.positionCount = 200;
            float t = 0f;
            Vector3 B = new Vector3(0, 0, 0);
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                B = (1 - t) * (1 - t) * (1 - t) * point0 + 3 * (1 - t) * (1 - t) * 
                    t * point1 + 3 * (1 - t) * t * t * point2 + t * t * t * point3;
        
                lineRenderer.SetPosition(i, B);
                t += (1 / (float)lineRenderer.positionCount);
            }
        }
    }
}