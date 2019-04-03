using UnityEngine;
using System.Collections;
using Valve.VR;

public class InputTest : MonoBehaviour
{
    public SteamVR_Action_Boolean spawn;
    public SteamVR_Action_Single shrink;

    private Rigidbody rb;
    private Vector3 forceDirection;

    [SerializeField] private float maxMagnitude = 20f;
    [SerializeField] private float friction = 1f;
    [SerializeField] private float strength = 100f;

    private GameObject sphere = null;
    private  SteamVR_Input_Sources source;

    private void Start()
    {
        if (GetComponent<SteamVR_Behaviour_Pose>() == null)
        {
            Debug.LogError("No SteamVR_Behaviour_Pose set", gameObject);
        }

        source = GetComponent<SteamVR_Behaviour_Pose>().inputSource;
        switch (source)
        {
            case SteamVR_Input_Sources.Any:
                Debug.Log("Found device Any for " + ToString());
                break;
            case SteamVR_Input_Sources.LeftHand:
                Debug.Log("Found device LeftHand for " + ToString());
                break;
            case SteamVR_Input_Sources.RightHand:
                Debug.Log("Found device RightHand for " + ToString());
                break;
        }

        rb = gameObject.GetComponentInParent<Rigidbody>();
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        rb.drag = friction;
        SpawningSphere();
        UpdatingSphereSize();

    }

    private void SpawningSphere()
    {
        bool bSpawn = spawn.GetStateDown(source);
        if (bSpawn == true)
        {
            if (sphere != null)
            {
                Destroy(sphere);

            }
            else if (sphere == null)
            {
                sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.parent = this.transform;
                sphere.transform.localPosition = new Vector3(0f, 0f, 2f);
                sphere.transform.localRotation = Quaternion.identity;
            }
        }
    }

    private void UpdatingSphereSize()
    {
        float forceStrength = shrink.GetAxis(source);
        //float size = shrink.GetAxis(source);
        forceDirection = Vector3.Normalize(transform.position - (transform.parent.transform.position + new Vector3(0f, 0.5f, 0f)));
        forceDirection = Vector3.Normalize(transform.parent.GetChild(2).transform.position - transform.position);
        if (forceStrength > 0.2f && rb.velocity.magnitude < maxMagnitude)
        {
            rb.AddForce(forceDirection * forceStrength * strength);
        }

        //if (size > 0.1f && sphere != null)
        //{
        //    Vector3 targetscale = new Vector3(size, size, size);
        //    sphere.transform.localScale = targetscale;
        //}
        //else if (size < 0.1f && sphere != null)
        //{
        //    sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //}
    }
}