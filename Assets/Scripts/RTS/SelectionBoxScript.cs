using UnityEngine.UI;
using UnityEngine;

public class SelectionBoxScript : MonoBehaviour {
    private new RectTransform transform;
    private Vector2 startPoint;
    private Image image;

    private static readonly int MOUSE_LEFT = 0;
    
    private void Start () {
        transform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    private void Update () {

        if (Input.GetMouseButtonDown(MOUSE_LEFT))
            SelectionStart();

        if (Input.GetMouseButtonUp(MOUSE_LEFT))
            SelectionEnd();

        if (image.enabled)
            SelectionMove();
    }

    private void SelectionStart()
    {
        Vector2 currentPosition = Input.mousePosition;

        startPoint = currentPosition;
        image.enabled = true;
    }

    private void SelectionEnd()
    {
        image.enabled = false;
    }

    private void SelectionMove()
    {
        Vector2 currentPosition = Input.mousePosition;

        Vector2 position = new Vector2(startPoint.x, startPoint.y);

        if (currentPosition.y > startPoint.y)
            position.y = currentPosition.y;

        if (currentPosition.x > startPoint.x)
            position.x = currentPosition.x;

        transform.position = position;

        float width = Mathf.Abs(startPoint.x - currentPosition.x);
        float height = Mathf.Abs(startPoint.y - currentPosition.y);

        transform.sizeDelta = new Vector2(width, height);
    }
    
}