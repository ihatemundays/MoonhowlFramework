using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moonhowl.Framework.Ecs;
using UnityEngine;

namespace Moonhowl.Platform.Unity.Utility {
    public static class GameObjectExtensions {
        private static readonly Dictionary<GameObject, Entity> Entities = new Dictionary<GameObject, Entity>();

        public static GameObject GetGameObject(this Entity entity) =>
            Entities.FirstOrDefault(keyValuePair => keyValuePair.Value == entity).Key;
        
        public static void SetEntity(this GameObject gameObject, Entity entity) { 
            Entities[gameObject] = entity;
        }

        public static Entity GetEntity(this GameObject gameObject) => Entities[gameObject];
    }    
}
