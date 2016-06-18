using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Windows.Speech;
using Object = UnityEngine.Object;

public class SignWhatIsDictated : MonoBehaviour {
	private int samplingRate;
	// Using an empty string specifies the default microphone. 
	private static string deviceName = string.Empty;

	private DictationRecognizer dictationRecognizer;
	private const int messageLength = 10;

	// Use this for initialization
	void Start () {
		dictationRecognizer = new DictationRecognizer(ConfidenceLevel.High,DictationTopicConstraint.Dictation);
		dictationRecognizer.Start();
		dictationRecognizer.DictationResult += DictationRecognizerOnDictationResult;

		//asink: I have no idea why I should do this
		// Query the maximum frequency of the default microphone. Use 'unused' to ignore the minimum frequency.
		int unused;
		Microphone.GetDeviceCaps(deviceName, out unused, out samplingRate);
	}

	private void DictationRecognizerOnDictationResult(string text, ConfidenceLevel confidence)
	{
		try
		{
			foreach(var speechManager in Object.FindObjectsOfType<BlendLegacyAnimations>())
			{
				speechManager.SayWordFromVoiceRecgonition(text);
			}
		}catch(Exception e) { Debug.LogError("Error when oding speech:"+e.Message);}
		
	}
	/// <summary>
	/// Turns on the dictation recognizer and begins recording audio from the default microphone.
	/// </summary>
	/// <returns>The audio clip recorded from the microphone.</returns>
	public AudioClip StartRecording()
	{
		// Shutdown the PhraseRecognitionSystem. This controls the KeywordRecognizers
		PhraseRecognitionSystem.Shutdown();

		// Start dictationRecognizer
		dictationRecognizer.Start();

		Debug.Log( "Dictation is starting. It may take time to display your text the first time, but begin speaking now...");

		// Start recording from the microphone for 10 seconds
		return Microphone.Start(deviceName, false, messageLength, samplingRate);
	}
	public void StopRecording()
	{
		// Check if dictationRecognizer.Status is Running and stop it if so
		if(dictationRecognizer.Status == SpeechSystemStatus.Running)
		{
			dictationRecognizer.Stop();
		}

		Microphone.End(deviceName);
	}
}
