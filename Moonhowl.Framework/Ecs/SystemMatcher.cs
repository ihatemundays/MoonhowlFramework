using System.Linq;
using System.Threading.Tasks;

namespace Moonhowl.Framework.Ecs {
  public class SystemMatcher<T> where T: IEntitySystem {
    public readonly T[] EntitySystems = {};

    public async Task MatchSystems(Entity entity) {
      var tasks = EntitySystems.Select(entitySystem => {
        var task = new Task(() => {
          if (entitySystem.GetMatcher().Match(entity)) {
            entitySystem.OnMatch(entity);
          } else {
            entitySystem.OnNotMatch(entity);
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
