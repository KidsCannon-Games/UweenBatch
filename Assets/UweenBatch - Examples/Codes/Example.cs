using UnityEngine;
using System.Collections.Generic;

using Uween;

namespace UweenBatchExamples {
	public class Example : MonoBehaviour {
		public List<GameObject> spriteRenderers;	

		void Start () {
		}

		public void OnParallelButtonDown() {
			var batch = new UweenBatch.TweenBatch<UweenBatch.ParallelExecuter>();

			spriteRenderers.ForEach(go => {
				Tween tw = TweenY.Add(go, Random.Range(0.5f, 1.5f), Camera.main.orthographicSize);
				batch.Register(tw);
			});

			batch.Execute().Then(() => {
				spriteRenderers.ForEach(go => {
					TweenY.Add(go, 0.5f, 0).Delay(0.2f);
				});
			});
		}

		public void OnSeriesButtonDown() {
			var batch = new UweenBatch.TweenBatch<UweenBatch.SeriesExecuter>();

			spriteRenderers.ForEach(go => {
				Tween tw = TweenY.Add(go, Random.Range(0.5f, 1.5f), Camera.main.orthographicSize);
				batch.Register(tw);
			});

			batch.Execute().Then(() => {
				spriteRenderers.ForEach(go => {
					TweenY.Add(go, 0.5f, 0).Delay(0.2f);
				});
			});
		}
	}
}
