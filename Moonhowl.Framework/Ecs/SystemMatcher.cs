using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moonhowl.Framework.Ecs {
  public class SystemMatcher<T> where T: IEntitySystem {
    private readonly Dictionary<T, Matcher> _systemMatchers = new Dictionary<T, Matcher>();

    public SystemMatcher(IEnumerable<T> systems) {
      _systemMatchers = systems.Select(system => system).ToDictionary(x => x, x => x.GetMatcher());
    }

    public async Task Match(Entity entity) {
      var tasks = _systemMatchers.Select(systemMatcher => {
        var system = systemMatcher.Key;
        var matcher = systemMatcher.Value;
        
        var task = new Task(() => {
          if (matcher.Match(entity)) {
            entity.AddSystem(system);
            system.OnMatch(entity);
          } else {
            entity.RemoveSystem(system);
            system.OnNotMatch(entity);
          }
        });
        task.Start();
        
        return task;
      });

      await Task.WhenAll(tasks);
      entity.ClearComponentStates();  // Clear the states.
    }
  }
}
