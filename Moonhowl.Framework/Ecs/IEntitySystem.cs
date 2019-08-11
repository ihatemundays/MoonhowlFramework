using System.Threading.Tasks;

namespace Moonhowl.Framework.Ecs {
  public interface IEntitySystem {
    Matcher GetMatcher();
    void OnMatch(Entity entity);
    void OnNotMatch(Entity entity);
  }
}
