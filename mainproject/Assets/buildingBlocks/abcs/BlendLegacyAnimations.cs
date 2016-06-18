using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendLegacyAnimations : MonoBehaviour
{
	private Animation _animation;
	public string textToSay = "ok";

	[SerializeField] private WordSayer youGotTold;

	// Use this for initialization
	private void Start()
	{
		_animation = GetComponent<Animation>();
		youGotTold.Init();
		StartCoroutine(youGotTold.SayWord(textToSay, _animation));
	}

	[Serializable]
	public class AnimationToLetter
	{
		public Animation anim;
		public char letter;
	}

	[Serializable]
	public class WordSayer
	{
		public readonly Dictionary<char, Animation> charToAnimation = new Dictionary<char, Animation>();

		[SerializeField] private List<AnimationToLetter> animToLetters;

		[SerializeField] private readonly float fadeLengthBetweenLetters = 0.3f;

		[SerializeField] private readonly float fadeLengthBetweenWords = 0.5f;

		[SerializeField] private readonly float pauseBetweenWords = 0.5f;

		[SerializeField] private readonly float targetWeight = 1f;

		public void Init()
		{
			/*
			foreach (var animationToLetter in animToLetters)
			{
				charToAnimation.Add(animationToLetter.letter, animationToLetter.anim);
			}*/
		}

		public IEnumerator SayWord(string word, Animation whatToPlayOn)
		{
			var lastLetterWasSpace = false;
			foreach (var letter in word)
			{
				if(letter == ' ')
				{
					yield return new WaitForSeconds(pauseBetweenWords);
					lastLetterWasSpace = true;
					continue;
				}
				var fadeLength = lastLetterWasSpace ? fadeLengthBetweenWords : fadeLengthBetweenLetters;
				//charToAnimation[letter].name
				string letterToSay = (letter + "").ToUpper();
				Debug.Log("SayingLetter:"+letterToSay);
				whatToPlayOn.CrossFade(letterToSay,fadeLength); //(charToAnimation[letter].name, targetWeight, fadeLength);
				yield return new WaitForSeconds(fadeLength);
			}
		}
	}
}