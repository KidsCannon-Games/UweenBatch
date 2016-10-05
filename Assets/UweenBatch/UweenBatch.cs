using Uween;
using System.Collections.Generic;

public class UweenBatch {
	private List<Tween> tweens = new List<Tween>();
	private bool executing = false;
	public event Uween.Callback OnComplete;

	public UweenBatch Register(Tween tw) {
			tweens.Add(tw);
			tw.enabled = false;
			return this;
	}

	public UweenBatch Execute() {
		if (executing) {
			OnComplete();
		} else {
			executing = true;
			var finishCount = 0;

			tweens.ForEach(tw => {
				tw.enabled = true;
				tw.Then(() => {
					finishCount++;
					if (finishCount == tweens.Count) {
						executing = false;
						OnComplete();
					}
				});
			});
		}
		return this;
	}

	public UweenBatch Then(Uween.Callback callback) {
		OnComplete += callback;
		return this;
	}
}
