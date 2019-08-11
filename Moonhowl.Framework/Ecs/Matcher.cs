namespace Moonhowl.Framework.Ecs {
    public class Matcher {
        public delegate bool MatchFunc(Entity entity);
        private readonly MatchFunc _localMatchFunc;

        public Matcher() {
            _localMatchFunc = entity => true;
        }
        
        public Matcher(MatchFunc matchFunc) {
            _localMatchFunc = matchFunc;
        }

        public static Matcher operator &(Matcher leftMatcher, Matcher rightMatcher) {
            return new Matcher(entity => leftMatcher._localMatchFunc(entity) && rightMatcher._localMatchFunc(entity));    
        }
        
        public static Matcher operator |(Matcher leftMatcher, Matcher rightMatcher) {
            return new Matcher(entity => leftMatcher._localMatchFunc(entity) || rightMatcher._localMatchFunc(entity));    
        }

        public bool Match(Entity entity) {
            return _localMatchFunc(entity);
        }
    }
}
