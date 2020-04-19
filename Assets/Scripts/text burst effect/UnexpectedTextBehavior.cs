using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnexpectedTextBehavior : MonoBehaviour
{
    public float deleteDistance;
    public float distance;

    private Text text;
    private Rigidbody2D rb;
    private RectTransform tr;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        text.text = "" + (char)Random.Range(0, 255);
        text.color = new Color(Random.value, Random.value, Random.value, Random.value);
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<RectTransform>();
        rb.velocity = Random.Range(1f, 5f) * new Vector2(-tr.position.x, -tr.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Sqrt(Mathf.Pow(tr.position.x, 2) + Mathf.Pow(tr.position.y, 2));
        if(distance > deleteDistance)
        {
            Destroy(gameObject);
        }
    }
}
