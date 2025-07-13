using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;      // drag *parent* prefab here
    [SerializeField] private Rigidbody playerRigidbody;  // drag the Player here

    public float M;
    public float X;
    public float T;
    public float n;

    void Start()
    {
        // Example: spawn ten cubes in a line
        for (int i = 0; i < n; i++)
            SpawnCube(new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))*X, M*1f, new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f))*(X / T) );
        // …spawn more if you like
    }

    // -----------------------------------------------------------------
    private void SpawnCube(Vector3 position, float mass, Vector3 velocity)
    {
        // 1. Instantiate the whole prefab hierarchy (parent + children)
        GameObject clone = Instantiate(cubePrefab, position, Quaternion.identity);

        clone.GetComponent<InitializeVelocity>().velocity = velocity;
        clone.GetComponent<Rigidbody>().mass = mass;
        //clone.GetComponent<Transform>().localScale = Vector3.one * position.magnitude;

        // 2. Find *every* component in that clone (including children, even inactive)
        var receivers = clone.GetComponentsInChildren<IRequiresPlayerRigidbody>(true);

        // 3. Inject the player rigidbody
        foreach (var r in receivers)
            r.SetPlayerRigidbody(playerRigidbody);
    }
}
