using Moonhowl.Framework.Ecs;
using Moonhowl.Platform.Unity.Utility;
using UnityEngine;

namespace Moonhowl.Platform.Unity.Ecs {
  public abstract class EntitySystemBehavior: MonoBehaviour {
    protected Entity Entity;

    private void Start() {
      Entity = gameObject.GetEntity();
    }

    public void Destroy() {
      Destroy(this);
    }
  }
}
