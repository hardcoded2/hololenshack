using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SignWhatIsDictated : MonoBehaviour
{
	private const int messageLength = 60;
	// Using an empty string specifies the default microphone. 
	private static readonly string deviceName = string.Empty;

	private DictationRecognizer dictationRecognizer;
	private int samplingRate;

	// Use this for initialization
	private void Start()
	{
		
		//StartRecording();
	}

	public void OnEnable()
	{
		dictationRecognizer = new DictationRecognizer(ConfidenceLevel.High, DictationTopicConstraint.Dictation);
		dictationRecognizer.Start();
		dictationRecognizer.DictationResult += DictationRecognizerOnDictationResult;

		//asink: I have no idea why I should do this
		// Query the maximum frequency of the default microphone. Use 'unused' to ignore the minimum frequency.
		int unused;
		Microphone.GetDeviceCaps(deviceName, out unused, out samplingRate);
	}

	public void OnDisable()
	{
		dictationRecognizer.DictationResult -= DictationRecognizerOnDictationResult;
		dictationRecognizer.Stop();
	}

	private void DictationRecognizerOnDictationResult(string text, ConfidenceLevel confidence)
	{
		Debug.Log("Got dictation result:" + text);
		try
		{
			foreach (var speechManager in FindObjectsOfType<BlendLegacyAnimations>())
			{
				speechManager.SayWordFromVoiceRecgonition(text);
			}
		}
		catch (Exception e)
		{
			Debug.LogError("Error when oding speech:" + e.Message);
		}
	}

	/// <summary>
	///     Turns on the dictation recognizer and begins recording audio from the default microphone.
	/// </summary>
	/// <returns>The audio clip recorded from the microphone.</returns>
	public AudioClip StartRecording()
	{
		Debug.Log("Startrecording");
		// Shutdown the PhraseRecognitionSystem. This controls the KeywordRecognizers
		PhraseRecognitionSystem.Shutdown();

		// Start dictationRecognizer
		dictationRecognizer.Start();

		Debug.Log("Dictation is starting. It may take time to display your text the first time, but begin speaking now...");

		// Start recording from the microphone for 10 seconds
		var clip = Microphone.Start(deviceName, false, messageLength, samplingRate);
		Debug.Log("GOT Audio clip");
		return clip;
		;
	}

	public void StopRecording()
	{
		// Check if dictationRecognizer.Status is Running and stop it if so
		if(dictationRecognizer.Status == SpeechSystemStatus.Running)
			dictationRecognizer.Stop();

		Microphone.End(deviceName);
	}
}