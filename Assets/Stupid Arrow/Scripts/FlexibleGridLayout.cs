using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : MonoBehaviour {

	void Start () {
		Vector2 newSize;
		float width = this.gameObject.GetComponent<RectTransform>().rect.width;
		if(this.gameObject.GetComponent<RectTransform>().rect.height > width) {
			newSize = new Vector2(width / 3 - 5, width / 3 - 5);
		}else {
			newSize = new Vector2(width / 3 - 120, width / 3 - 120);
		}
		this.gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
		GetComponent<RectTransform> ().offsetMin = new Vector2 (0, width - (width * 2.2f));
	}
}
