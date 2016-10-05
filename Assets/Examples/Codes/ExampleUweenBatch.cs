using UnityEngine;
using System.Collections.Generic;

using Uween;

public class ExampleUweenBatch : MonoBehaviour {
	public List<GameObject> spriteRenderers;	

	void Start () {
		var batch = new UweenBatch();

		spriteRenderers.ForEach(go => {
			batch.Register(TweenY.Add(go, Random.Range(0.5f, 1.5f), Camera.main.orthographicSize).Delay(Random.Range(0.5f, 1.5f)));
		});

		batch.Execute().Then(() => {
			spriteRenderers.ForEach(go => {
				TweenY.Add(go, 0.5f, 0).Delay(0.2f);
			});
		});
	}
}
