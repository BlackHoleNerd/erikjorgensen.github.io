using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    public Rigidbody Player;
    public Transform X;
    public Transform Y; 
    public Transform Z; 
    private Vector3 torq;
    private Vector3 AngMoment;
    void Start()
    {
        torq = new Vector3(0, 0, 0);
        AngMoment = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        torq = new Vector3 (0, 0, 0);
        Vector3 X0 = 0.1f * (X.position - transform.position).normalized;
        Vector3 Y0 = 0.1f * (Y.position - transform.position).normalized;
        Vector3 Z0 = 0.1f * (Z.position - transform.position).normalized;
        if (Input.GetKey(KeyCode.Q))
        {
            torq += Z0;
        }
        if (Input.GetKey(KeyCode.E))
        {
            torq += -Z0;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            torq += Y0;
        }
        if (Input.GetKey(KeyCode.X))
        {
            torq += -Y0;
        }

        if (Input.GetKey(KeyCode.C))
        {
            torq += X0;
        }
        if (Input.GetKey(KeyCode.V))
        {
            torq += -X0;
        }
        Player.AddTorque(torq);




        
    }
}
