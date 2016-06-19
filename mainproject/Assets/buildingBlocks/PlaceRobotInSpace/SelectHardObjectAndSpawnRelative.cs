using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class SelectHardObjectAndSpawnRelative : MonoBehaviour
{
	[SerializeField]
	private GameObject _Visualizer;
	[SerializeField] private readonly Vector3 offsetFromTarget = new Vector3(.5f, 0f, 0f);

	private GestureRecognizer recognizer;
	public GameObject FocusedObject { get {return _Visualizer;} }

	// Use this for initialization
	private void OnEnable()
	{
		recognizer = new GestureRecognizer();
		recognizer.TappedEvent += RecognizerOnTappedEvent;
		recognizer.SetRecognizableGestures(GestureSettings.Tap);
		recognizer.StartCapturingGestures();
        RecognizerOnTappedEvent(InteractionSourceKind.Other, 1, new Ray(Camera.main.transform.position, Camera.main.transform.forward));
    }

	private void OnDisable()
	{
		recognizer.StopCapturingGestures();
	}
	
	private void RecognizerOnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
	{
		//Debug.Log("Recognized Gesture headray:"+JsonUtility.ToJson(headRay));
		RaycastHit hit;
		//if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
		//30.0f, SpatialMapping.PhysicsRaycastMask))
		//layerMask:SpatialMapping.PhysicsRaycastMask
		
		//FakeSpawn();
		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 30.0f, SpatialMapping.PhysicsRaycastMask))
			spawn(hit.point + offsetFromTarget);
	}

	private void spawn(Vector3 positionToSpawnAt)
	{
		//if(_Visualizer == null) Instantiate(_prefabToSpawn);
		//_Visualizer = Instantiate(_prefabToSpawn);//GameObject.CreatePrimitive(PrimitiveType.Cube);
		_Visualizer.transform.localScale = 0.25f*Vector3.one;
		_Visualizer.transform.position = positionToSpawnAt;
		_Visualizer.transform.LookAt(Camera.main.transform);
	}
}