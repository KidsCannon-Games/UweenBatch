# UweenBatch

Batch executer for [oingkms/uween](https://github.com/oinkgms/uween)

## Requirements

- uween

## Installation

```
$ cd /your/unity/project
$ git submodule add -b release git@github.com:KidsCannon-Games/UweenBatch.git Assets/UweenBatch
```

## Getting Started

## Parallel

```csharp
using UnityEngine;
using System.Collections.Generic;

using Uween;

public class Example : MonoBehaviour {
  public List<GameObject> spriteRenderers;

  void Start () {
    var batch = new UweenBatch.TweenBatch<UweenBatch.ParallelExecuter>();

    spriteRenderers.ForEach(go => {
      Tween tw = TweenY.Add(go, Random.Range(0.5f, 1.5f), Camera.main.orthographicSize));
      batch.Register(tw);
    });

    batch.Execute().Then(() => {
      spriteRenderers.ForEach(go => {
        TweenY.Add(go, 0.5f, 0).Delay(0.2f);
      });
    });
  }
}
```

![paralell.gif](https://i.gyazo.com/bd7a1cddce22902d3952604fc71297cb.gif)

## Series

```csharp
using UnityEngine;
using System.Collections.Generic;

using Uween;

public class Example : MonoBehaviour {
  public List<GameObject> spriteRenderers;

  void Start () {
    var batch = new UweenBatch.TweenBatch<UweenBatch.SeriesExecuter>();

    spriteRenderers.ForEach(go => {
      Tween tw = TweenY.Add(go, Random.Range(0.5f, 1.5f), Camera.main.orthographicSize));
      batch.Register(tw);
    });

    batch.Execute().Then(() => {
      spriteRenderers.ForEach(go => {
        TweenY.Add(go, 0.5f, 0).Delay(0.2f);
      });
    });
  }
}
```

![series.gif](https://i.gyazo.com/a9dd428dac22c7131509022e0fe8bc60.gif)
