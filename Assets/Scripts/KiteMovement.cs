using UnityEngine;

public class KiteMovement : MonoBehaviour
{
    public float speed = 3f;
    private Vector3 direction = Vector3.right;

    private SpriteRenderer sr;
    private float screenLeft, screenRight;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        Camera cam = Camera.main;
        if (cam == null) return;

        float camDistance = Mathf.Abs(cam.transform.position.z - transform.position.z);

        Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, camDistance));
        Vector3 leftEdge  = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, camDistance));

        screenRight = rightEdge.x;
        screenLeft  = leftEdge.x;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Edge detection (same as balloon)
        if (transform.position.x > screenRight)
        {
            direction = Vector3.left;
            if (sr != null) sr.flipX = true;
        }
        else if (transform.position.x < screenLeft)
        {
            direction = Vector3.right;
            if (sr != null) sr.flipX = false;
        }
    }
}