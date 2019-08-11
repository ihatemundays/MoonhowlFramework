namespace Moonhowl.Framework.Ecs {
  public class Match<T> where T: IEntityComponent {
    public static Matcher operator ~(Match<T> match) {
      return new Matcher(entity => entity.GetComponent<T>() != null);
    }
    
    public static Matcher operator !(Match<T> match) {
      return new Matcher(entity => entity.GetComponent<T>() == null);
    }
    
    public static Matcher operator +(Match<T> match) {
      return new Matcher(entity => entity.CheckComponent<T>() == EntityComponentState.Added);
    }
    
    public static Matcher operator -(Match<T> match) {
      return new Matcher(entity => entity.CheckComponent<T>() == EntityComponentState.Removed);
    }
  }
}
