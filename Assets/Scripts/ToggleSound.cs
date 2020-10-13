using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSound : MonoBehaviour {
	[SerializeField] private Sprite musicFull;
	[SerializeField] private Sprite musicHalf;
	[SerializeField] private Sprite musicOff;
	[SerializeField] private Image musicImage;

	// Use this for initialization
	void Start () {
		// Set sprite at start of each level
		musicImage.sprite = (PlayerSettingsController.CurrSoundLevel == MusicLevel.FULL) ? musicFull :
								((PlayerSettingsController.CurrSoundLevel == MusicLevel.HALF) ? musicHalf : musicOff);
	}
	

	// Toggles music sprite and volume
	public void toggle () 
	{
		switch (PlayerSettingsController.CurrSoundLevel) {
			case MusicLevel.OFF:
				PlayerSettingsController.CurrSoundLevel = MusicLevel.FULL;
				musicImage.sprite = musicFull;
				break;
			case MusicLevel.HALF:
				PlayerSettingsController.CurrSoundLevel = MusicLevel.OFF;
				musicImage.sprite = musicOff;
				break;
			case MusicLevel.FULL:
				PlayerSettingsController.CurrSoundLevel = MusicLevel.HALF;
				musicImage.sprite = musicHalf;
				break;
		}
	}

}
