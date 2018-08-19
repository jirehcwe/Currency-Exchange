using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteDisplay : MonoBehaviour {
	public Image image;

	public Note note;
	public void StartNote(Note note) {
		this.note = note;
		image = this.gameObject.GetComponent<Image>();
		image.sprite = note.image;
	}
	

}
