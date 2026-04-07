using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Zombie : MonoBehaviour
{
    private int baseRange = 10;
    private int num1;
    private int num2;
    private int sign;
    private Transform player;
    public int answer {get; private set;}
    public float speed = 2f;
    public TextMeshPro equation;
    private Rigidbody rb;
    private Animator anim;
    private bool dead=false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        anim = GetComponent<Animator>();
        num1 = Random.Range(1, baseRange);
        num2 = Random.Range(1, baseRange);
        sign = Random.Range(0,2);
        if (sign == 0)
        {
            equation.SetText(num1 + " + " + num2);
            answer = num1 + num2;
        } else if (sign == 1)
        {
            equation.SetText(num1 + " - " + num2);
            answer = num1 - num2;
        }
    }

    
    void Update()
    {
        if (!dead)
        {
            rb.linearVelocity = new Vector3(-speed, rb.linearVelocity.y, rb.linearVelocity.z);
            player = GameObject.FindWithTag("Player").transform;
            float distance = (transform.position - player.position).magnitude;
            if (distance <= 2.5f)
            {
                Death();
                ResourceController.instance.TakeDamage();
                DamageRecorder.instance.AddRecord(num1, num2, sign);
            }
            if (distance <= 5f)
            {
                Vector3 direction = (transform.position - player.transform.position).normalized;
                float angleInRadians = Mathf.Atan2(direction.y, direction.x);
                // Calculate the X and y components using sine and cosine
                float xComponent = Mathf.Cos(angleInRadians) * -speed;
                float yComponent = Mathf.Sin(angleInRadians) * -speed;
                Vector3 velocityVector = new Vector3(xComponent, yComponent, rb.linearVelocity.z);
                rb.linearVelocity = velocityVector;
            }
        }
    }

    public void Death()
    {
        dead = true;
        rb.linearVelocity = new Vector3(0,0,0);
        equation.SetText("");
        anim.SetTrigger("Death");
        Destroy(gameObject, 1f);
    }

    public void SetStatByLevel(int level)
    {
        int currentRange = baseRange + (level-1)*4;
        this.baseRange = currentRange;

        float currentSpeed = speed + (level - 1) * 0.1f;
        speed = Mathf.Min(currentSpeed, 1.4f);
    }
}
