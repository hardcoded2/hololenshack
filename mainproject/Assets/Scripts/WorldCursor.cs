using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class WorldCursor : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private GestureRecognizer recognizer;

    public GameObject teachQuad;
    public GameObject listenQuad;

    EnableBasedOnButtons switchGO;

    // Use this for initialization
    void Start()
    {
        // Grab the mesh renderer that's on the same object as this script.
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();

        switchGO = FindObjectOfType<EnableBasedOnButtons>();

        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += Recognizer_TappedEvent;

        recognizer.StartCapturingGestures();
    }

    private void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        RaycastHit hitInfo;
	    Debug.Log("Tapped");
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo))
        {
			Debug.Log("Hit");
            GameObject hitObj = hitInfo.collider.gameObject;

            if(hitObj)
            {
                if (hitObj.GetInstanceID() == teachQuad.GetInstanceID())
                {
                    Debug.Log("raycast hit teach quad");
                    switchGO.TeachMeToSign();
                } else if (hitObj.GetInstanceID() == listenQuad.GetInstanceID())
                {
                    Debug.Log("raycast hit listenQuad");
                    switchGO.ListenForMe();
                } 
                //Debug.Log("raycast hit something else");
            }
            else
            {
                //Debug.Log("raycast missed");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram...

            // Display the cursor mesh.
            meshRenderer.enabled = true;
            // Move the cursor to the point where the raycast hit.
            this.transform.position = hitInfo.point;
            // Rotate the cursor to hug the surface of the hologram.
            this.transform.rotation =
                Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            // If the raycast did not hit a hologram, hide the cursor mesh.
            meshRenderer.enabled = false;
        }
    }

    public void OnDisable()
    {
        if (recognizer != null)
        {
            recognizer.TappedEvent -= Recognizer_TappedEvent;
            recognizer.StopCapturingGestures();
            recognizer.Dispose();
        }

    }
    
}
