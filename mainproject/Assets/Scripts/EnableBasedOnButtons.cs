using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Windows.Speech;

public class EnableBasedOnButtons : MonoBehaviour
{
	[SerializeField] private GameObject listenForMeGO; //robot
	[SerializeField]
	private GameObject teachMeToSignGO; //
	Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
	KeywordRecognizer keywordRecognizer;

    public GameObject canvasGO;
    public GameObject cursorGO;

	//listen for me
	//hear for me
	public void OnEnable()
	{
		keywords.Add("listen for me", ListenForMe);
		keywords.Add("teach me to sign", TeachMeToSign);

		// Tell the KeywordRecognizer about our keywords.
		keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

		// Register a callback for the KeywordRecognizer and start recognizing!
		keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
		keywordRecognizer.Start();
	}
	private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		Debug.Log("keyword recognized:"+args.text);
		System.Action keywordAction;
		if(keywords.TryGetValue(args.text, out keywordAction))
		{
            keywordAction.Invoke();
		}
	}
    public void shutdown()
    {
        if (keywordRecognizer != null) {
        keywordRecognizer.OnPhraseRecognized -= KeywordRecognizer_OnPhraseRecognized;
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
        PhraseRecognitionSystem.Shutdown();
        gameObject.SetActive(false);
        canvasGO.SetActive(false);
        cursorGO.SetActive(false);
    }
	public void ListenForMe()
	{
        shutdown();
        teachMeToSignGO.SetActive(false);
		listenForMeGO.SetActive(true);
	}

	public void TeachMeToSign()
	{
        shutdown();
        teachMeToSignGO.SetActive(true);
		listenForMeGO.SetActive(false);
	}
	public void OnDisable()
	{
		if(keywordRecognizer != null)
		{
			keywordRecognizer.OnPhraseRecognized -= KeywordRecognizer_OnPhraseRecognized;
			keywordRecognizer.Stop();
			keywordRecognizer.Dispose();
		}
		
	}
}
