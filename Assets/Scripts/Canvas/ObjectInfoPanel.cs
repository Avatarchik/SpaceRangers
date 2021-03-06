﻿using UnityEngine;
using UnityEngine.UI;

namespace Canvas
{
    public class ObjectInfoPanel : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private GameObject infoPanel;
        [SerializeField] private Text title;
        [SerializeField] private GameObject image;
        [SerializeField] private Text data;
        private Collider2D lastTarget;

        private void Update()
        {
            var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            var collider = hit.collider;
            if (collider == null)
            {
                infoPanel.SetActive(false);
                lastTarget = null;
                data.text = "";
            }

            if (collider != null)
            {
                if (collider == lastTarget)
                {
                    return;
                }
                lastTarget = collider;
                var targetPos = collider.gameObject.transform.position;
                infoPanel.transform.position = new Vector3(targetPos.x + 0.6f, targetPos.y, 0f);
                infoPanel.SetActive(true);
                title.text = collider.gameObject.name;
                image.GetComponent<Image>().sprite = 
                    collider.gameObject.transform.Find("Graphics").GetComponent<SpriteRenderer>().sprite;
                var objectData = collider.gameObject.GetComponent<IObjectData>();
                if (objectData != null)
                {
                    data.text = objectData.GetObjectData();
                }
            }
        }
    }
}