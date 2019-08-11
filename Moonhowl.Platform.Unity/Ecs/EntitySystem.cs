using Moonhowl.Framework.Ecs;
using Moonhowl.Platform.Unity.Utility;
using System;

namespace Moonhowl.Platform.Unity.Ecs {
    public abstract class EntitySystem<T>: IEntitySystem where T: EntitySystemBehavior {
        public Matcher GetMatcher() {
            throw new NotImplementedException();
        }

        public void OnMatch(Entity entity) {
            var gameObject = entity.GetGameObject();
            
            if (gameObject.GetComponent<T>() != null) {
                return;
            }

            gameObject.AddComponent<T>();
        }

        public void OnNotMatch(Entity entity) {
            var gameObject = entity.GetGameObject();
            var component = gameObject.GetComponent<T>();
            
            if (component == null) {
                return;
            }

            gameObject.GetComponent<T>().Destroy();
        }
    }
}
