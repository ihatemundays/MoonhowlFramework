namespace Moonhowl.Framework.Ecs {
  public class Match<T> where T: IEntityComponent {
    public static Matcher operator ~(Match<T> match) => Exists(match);
    public static Matcher operator !(Match<T> match) => NotExists(match);

    private static Matcher Exists(Match<T> match) =>
      new Matcher(entity => entity.GetComponent<T>() != null);
    private static Matcher NotExists(Match<T> match) => 
      new Matcher(entity => entity.GetComponent<T>() == null);

    public static Matcher operator +(Match<T> match) => Added(match);
    public static Matcher operator -(Match<T> match) => Removed(match);
    private static Matcher Added(Match<T> match) => 
      new Matcher(entity => entity.CheckComponent<T>() == EntityComponentState.Added);
    private static Matcher Removed(Match<T> match) => 
      new Matcher(entity => entity.CheckComponent<T>() == EntityComponentState.Removed);

    public static implicit operator Matcher(Match<T> match) => Exists(match);
  }
}
