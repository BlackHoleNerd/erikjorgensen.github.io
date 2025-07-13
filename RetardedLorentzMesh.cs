using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LightTransport;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(MeshFilter))]
public class RetardedLorentzMesh : MonoBehaviour////, IRequiresPlayerRigidbody   // ① implement
{
    ////[HideInInspector]                     // keeps Inspector tidy
    ////public Rigidbody playerRigidbody;     // ② field for the reference

    // ③ helper required by the interface
    /// <summary>
    /// /public void SetPlayerRigidbody(Rigidbody rb) => playerRigidbody = rb;
    /// </summary>

    /* ——— everything else in your script stays the same ——— */

    public Rigidbody Player;
    public Rigidbody Object;
    //float c = 1f;//30.44f;


    public Mesh originalMesh;
    private Mesh displacedMesh;

    private Vector3[] originalVertices;
    private Vector3[] displacedVertices;

    


    void Start()
    {

        // 
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        displacedMesh = Instantiate(originalMesh);
        GetComponent<MeshFilter>().mesh = displacedMesh;

        originalVertices = originalMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];

        originalMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10000f); // or whatever size fits your needs

        if (!Object && transform.parent)
            Object = transform.parent.GetComponent<Rigidbody>();

        Renderer rend = GetComponent<Renderer>();
        rend.forceRenderingOff = false;
        rend.enabled = true;


    }

    void OnRenderObject()
    {
        Graphics.DrawMeshNow(GetComponent<MeshFilter>().mesh, transform.localToWorldMatrix);
    }


    void FixedUpdate()
    {
        ////if (playerRigidbody == null || Object == null) return;
        // 
        float c = CGHscale.c;
        // Velocity and Lorentz inverse boost
        Vector3 vPlayer0 = -Player.linearVelocity;//.GetComponent<PlayerMover>().velocityWorld;
        Vector3 vObject0 = -Object.linearVelocity;//.GetComponent<ObjectMover>().velocityWorld;
        float vPlayer2 = vPlayer0.x * vPlayer0.x + vPlayer0.y * vPlayer0.y + vPlayer0.z * vPlayer0.z;
        float EPlayer1 = Mathf.Sqrt(1 + vPlayer2);
        float vObject2 = vObject0.x * vObject0.x + vObject0.y * vObject0.y + vObject0.z * vObject0.z;
        float EObject1 = Mathf.Sqrt(1 + vObject2);
        Vector3 vPlayer = vPlayer0 / EPlayer1;
        Vector3 vObject = vObject0 / EObject1;
        Matrix4x4 Lplayer = LorentzMatrix.GetInverseBoost(vPlayer, c);
        Matrix4x4 Lplayer_inv = LorentzMatrix.GetInverseBoost(-vPlayer, c);
        Matrix4x4 Lobject = LorentzMatrix.GetInverseBoost(vObject, c);
        Matrix4x4 Lobject_inv = LorentzMatrix.GetInverseBoost(-vObject, c);

        Vector3 PlayerPos = Player.worldCenterOfMass;//Player.worldCenterOfMass;//Player.position;

        for (int i = 0; i < originalVertices.Length; i++)
        {

            //Quaternion parentRotation = Object.rotation;
            //Quaternion invRotation = Quaternion.Inverse(parentRotation);


            // Lorentz Transform relative Velocity between Object and Player
            //Vector4 ObjectVel = Lobject * new Vector4(1f,0f,0f,0f);
            //Vector4 RelVel = Lplayer_inv * ObjectVel;

            // Convert local to world vertex
            Vector3 localPos = originalVertices[i];
            Vector3 ObjectPos = localPos;// transform.localToWorldMatrix.MultiplyPoint3x4(transform.TransformPoint(localPos));//;


            
            //transform.localToWorldMatrix.MultiplyPoint3x4

            // Velocity of Object
            Vector3 ObjX = vObject * Time.time;

            // Relative Position(world frame at rest)
            Vector3 deltaX = ObjectPos - PlayerPos;

            // Compute intersection of Object with Player's Past Light Cone
            Vector4 deltaXobject = new Vector4(-deltaX.magnitude,deltaX.x,deltaX.y,deltaX.z);

            // Go back to world frame
            Vector4 deltaXworld = Lobject * deltaXobject;

            // Go to player's frame
            Vector4 deltaXplayer = Lplayer_inv * deltaXworld;

            // Add players 3 - postion in world frame
            Vector4 displaced = new Vector3(deltaXplayer.y, deltaXplayer.z, deltaXplayer.w);// + PlayerPos;




            //Player.rotation = invRotation;

            // Apply this to the final displaced vector before assigning:

            //Vector3 Displaced = transform.worldToLocalMatrix.MultiplyPoint3x4(displaced);
            //displacedVertices[i] = displaced;
            displacedVertices[i] = displaced;
        }

        
        // Update mesh with displaced vertices
        displacedMesh.vertices = displacedVertices;
        displacedMesh.RecalculateNormals();

        displacedMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000f);
    }
}
