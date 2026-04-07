using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameObject target;
    public float speed = 12f;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        rb.linearVelocity = transform.right * speed;

        if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
        {
            Zombie zombie = target.GetComponent<Zombie>();
            if (zombie != null)
            {
                zombie.Death();
            }

            // Destroy(target);
            Destroy(gameObject);
            LevelScoreController.Instance.AddScore();
        }
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("Bullet collided with: " + other.gameObject.name);
    //     if (other.gameObject == target)
    //     {
    //         Debug.Log("Bullet hit the target!");
    //         Destroy(target);
    //         Destroy(gameObject);
    //     }
    // }
}
