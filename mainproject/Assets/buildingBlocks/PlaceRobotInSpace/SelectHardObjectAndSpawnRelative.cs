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
		recognizer.StartCapturingGestures();
		spawn(new Vector3(0, 0.91f, -9.59f));
	}

	private void RecognizerOnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
	{
		var hit = new RaycastHit();
		//if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
		//30.0f, SpatialMapping.PhysicsRaycastMask))
		//layerMask:SpatialMapping.PhysicsRaycastMask
		if(Physics.Raycast(headRay, out hit))
			spawn(hit.transform.position + offsetFromTarget);
	}

	private void spawn(Vector3 positionToSpawnAt)
	{
		var spawned = _spawnedPrefab ?? Instantiate(_prefabToSpawn);
		spawned.transform.position = positionToSpawnAt;
		spawned.transform.LookAt(Camera.main.transform.position);
		_spawnedPrefab = spawned;
	}
}