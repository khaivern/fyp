using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    Vector2 direction;
    float speed;

    public int damage = 100;

    private void OnEnable()
    {
        Invoke("SetInactive", 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 25f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }

    }

    public void SetMoveDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    void SetInactive()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
