namespace Moonhowl.Framework.Ecs {
  public class Match<T> where T: IEntityComponent {
    public static Matcher operator ~(Match<T> match) => Exists();
    public static Matcher operator !(Match<T> match) => NotExists();
    private static Matcher Exists() =>
      new Matcher(entity => entity.TryComponent<T>());
    private static Matcher NotExists() => 
      new Matcher(entity => !entity.TryComponent<T>());

    public static Matcher operator +(Match<T> match) => Added();
    public static Matcher operator -(Match<T> match) => Removed();
    private static Matcher Added() => 
      new Matcher(entity => entity.GetComponentState<T>() == EntityComponentState.Added);
    private static Matcher Removed() => 
      new Matcher(entity => entity.GetComponentState<T>() == EntityComponentState.Removed);
    
    public static implicit operator Matcher(Match<T> match) => Exists();
  }
}
