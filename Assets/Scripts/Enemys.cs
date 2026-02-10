using UnityEngine;

public class Enemys : MonoBehaviour
{
    public float speed2 = 1f;
    public Transform[] pointss;

    private int i2;
    private SpriteRenderer sspriteRenderer;

    void Start()
    {
       sspriteRenderer = GetComponent<SpriteRenderer>();   
    }


    void Update()
    {
        if (Vector2.Distance(transform.position, pointss[i2].position) < 0.25f)
        {
            i2++;
            if (i2 == pointss.Length)
            {
                i2 = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, pointss[i2].position, speed2 * Time.deltaTime);

        sspriteRenderer.flipX = (transform.position.x - pointss[i2].position.x) < 0f;
    }
}
