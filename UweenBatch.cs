using System;
using System.Collections.Generic;

using Uween;

namespace UweenBatch
{
	public interface IExecuter
    {
        void Execute(List<Tween> tweens, Uween.Callback callback);
    }

    public class TweenBatch<T> where T : IExecuter, new()
    {
        private List<Tween> tweens = new List<Tween>();
		private IExecuter executer;
        private bool executing = false;
        private event Uween.Callback OnComplete;

		public TweenBatch()
    	{
			this.executer = new T();
    	}

        public TweenBatch<T> Register(Tween tw)
        {
            tw.enabled = false;
			tweens.Add(tw);
            return this;
        }

        public TweenBatch<T> Execute()
        {
            if (!executing)
            {
				executing = true;
                executer.Execute(this.tweens, () =>
                {
                    executing = false;
					if (OnComplete != null) {
						var callback = OnComplete;
						OnComplete = null;
						callback();
					}
                });
            }
            return this;
        }

		public TweenBatch<T> Then(Uween.Callback callback)
		{
			OnComplete += callback;
			return this;
		}
    }

	// 1 ->    |
	// 2 --->  |
	// 3 -->   | ==> callback()
	// 4 ----> |
	// 5 ->    |
    public class ParallelExecuter : IExecuter
    {
        public void Execute(List<Tween> tweens, Uween.Callback callback)
        {
            var finishedCount = 0;
            tweens.ForEach(tw =>
            {
                tw.enabled = true;
                tw.Then(() =>
                {
                    finishedCount++;
                    if (finishedCount == tweens.Count)
                    {
                        callback();
                    }
                });
            });
        }
    }

	// 1 -> 2 ---> 3 --> 4 ----> 5 -> | ==> callback()
    public class SeriesExecuter : IExecuter
    {
        public void Execute(List<Tween> tweens, Uween.Callback callback)
        {
			Action<int> exec = null;
			exec = (index) =>
            {
                tweens[index].enabled = true;
                tweens[index].Then(() =>
                {
                    if (index < tweens.Count-1)
                    {
                        exec(index + 1);
                    }
                    else
                    {
                        callback();
                    }
                });
            };

            exec(0);
        }
    }
}
