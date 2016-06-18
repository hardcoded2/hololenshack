using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class SelectHardObjectAndSpawnRelative : MonoBehaviour
{
	[SerializeField] private GameObject _prefabToSpawn;
	private GameObject _spawnedPrefab;
	[SerializeField] private readonly Vector3 offsetFromTarget = new Vector3(.5f, 0f, 0f);

	private GestureRecognizer recognizer;
	public GameObject FocusedObject { get {return _spawnedPrefab;} }

	// Use this for initialization
	private void Start()
	{
		recognizer = new GestureRecognizer();
		recognizer.TappedEvent += RecognizerOnTappedEvent;
		recognizer.SetRecognizableGestures(GestureSettings.Tap);
		recognizer.StartCapturingGestures();
		//FakeSpawn();
	}

	[ContextMenu("FakeSpawn")]
	void FakeSpawn()
	{
		//note: transform.up not working the way I expect
		var camDirection = Camera.main.transform.up *0.91f + new Vector3(0f, 0f, -9.59f);
		spawn(camDirection);
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
	/*
	 * 
		Debug.Log("Spawn");
		var spawned = _spawnedPrefab ?? Instantiate(_prefabToSpawn);
		spawned.transform.position = positionToSpawnAt;
		spawned.transform.LookAt(Camera.main.transform.position);
		_spawnedPrefab = spawned;
	 */
	private void spawn(Vector3 positionToSpawnAt)
	{
		if(_spawnedPrefab != null) Destroy(_spawnedPrefab);

		_spawnedPrefab = Instantiate(_prefabToSpawn);//GameObject.CreatePrimitive(PrimitiveType.Cube);
		_spawnedPrefab.transform.localScale = 0.25f*Vector3.one;
		_spawnedPrefab.transform.position = positionToSpawnAt;
	}
}