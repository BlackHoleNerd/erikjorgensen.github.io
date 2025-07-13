using UnityEngine;

public class PlayerScaleController : MonoBehaviour
{

    public Rigidbody rb;
    public Transform player;
    public float thrust = 0.0216f;
    //public float scale;
    //Vector3 x0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //x0 = player.position;
        
        if (Input.GetKey(KeyCode.T))
        {
            thrust *= 1.1f;
        }
        if (Input.GetKey(KeyCode.G))
        {
            thrust *= 1f / 1.1f;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            player.localScale *= 1.1f;
        }
        if (Input.GetKey(KeyCode.H))
        {
            player.localScale *= 1f / 1.1f;
        }
        if (Input.GetKey(KeyCode.U))
        {
            rb.mass *= 1.331f;
        }
        if (Input.GetKey(KeyCode.J))
        {
            rb.mass *= 1.0f / 1.331f;
        }
    }
}
