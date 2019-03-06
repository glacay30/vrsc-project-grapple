using UnityEngine;
using System.Collections;
using Valve.VR;

public class InputTest : MonoBehaviour
{
    public SteamVR_Action_Boolean spawn;
    public SteamVR_Action_Single shrink;

    private GameObject sphere = null;

    private void Start()
    {

    }


    private void Update()
    {
        bool bSpawn = spawn.GetStateDown(SteamVR_Input_Sources.Any);
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

        float size = shrink.GetAxis(SteamVR_Input_Sources.Any);
        if (size > 0.1f && sphere != null)
        {
            Vector3 targetScale = new Vector3(size, size, size);
            sphere.transform.localScale = targetScale;
        }
        else if (size < 0.1f && sphere != null)
        {
            sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
}