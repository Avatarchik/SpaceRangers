using UnityEngine;
using UnityEngine.UI;

public class ObjectInfoPanel : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Text title;
    [SerializeField] private GameObject image;
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
        }

        if (collider != null)
        {
            if (collider == lastTarget)
            {
                return;
            }
            lastTarget = collider;
            infoPanel.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
            infoPanel.SetActive(true);
            title.text = collider.gameObject.name;
            image.GetComponent<Image>().sprite = 
                collider.gameObject.transform.Find("Graphics").GetComponent<SpriteRenderer>().sprite;

        }
    }
}