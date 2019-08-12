using Moonhowl.Framework.Ecs;
using Moonhowl.Platform.Unity.Utility;
using System;
using UnityEngine;

namespace Moonhowl.Platform.Unity.Ecs {
    public abstract class EntitySystem: MonoBehaviour, IEntitySystem {
        protected Entity Entity;
        
        public Matcher GetMatcher() {
            throw new NotImplementedException();
        }

        public void OnMatch(Entity entity) {
            var gameObject = entity.GetGameObject();
            var type = GetType();
            var component = gameObject.GetComponent(type);
            
            if (component != null) {
                return;
            }

            gameObject.AddComponent(type);
        }

        public void OnNotMatch(Entity entity) {
            var gameObject = entity.GetGameObject();
            var type = GetType();
            var component = gameObject.GetComponent(type);
            
            if (component == null) {
                return;
            }

            Destroy(component);
        }
        
        private void Start() {
            Entity = gameObject.GetEntity();
        }
    }
}
