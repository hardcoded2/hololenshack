using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayAnimationWhenRecognizedPhraseHeard : MonoBehaviour
{
	public List<WrapAnim> animsSupported;
	private KeywordRecognizer keywordRecognizer;
	private readonly Dictionary<string, string> stringToTriggerName = new Dictionary<string, string>();
	[SerializeField] private Animation toPlayOn;
	// Use this for initialization
	private void Start()
	{
		foreach (var wrapAnim in animsSupported)
		{
			stringToTriggerName.Add(wrapAnim.phrase, wrapAnim.triggerToUse);
		}
		// Tell the KeywordRecognizer about our keywords.
		keywordRecognizer = new KeywordRecognizer(stringToTriggerName.Keys.ToArray());
		// Register a callback for the KeywordRecognizer and start recognizing!
		keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
		keywordRecognizer.Start();
	}


	private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
	{
		string trigger;
		if(stringToTriggerName.TryGetValue(args.text, out trigger))
			toPlayOn.GetComponent<Animator>().SetTrigger(trigger);
	}

	[Serializable]
	public class WrapAnim
	{
		public string phrase;
		public string triggerToUse;
	}
}