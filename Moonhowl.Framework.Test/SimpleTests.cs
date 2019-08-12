using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using Moonhowl.Framework.Ecs;
using NUnit.Framework;

namespace Moonhowl.Framework.Test {
  struct TestComponentA: IEntityComponent { }
  struct TestComponentB: IEntityComponent { }
  struct TestComponentC: IEntityComponent { }
  struct TestComponentD: IEntityComponent { }

  abstract class TestSystem {
    public void OnMatch(Entity entity) { }
    public void OnNotMatch(Entity entity) { }
  }
  
  class TestSystemA: TestSystem, IEntitySystem {
    public Matcher GetMatcher() => new Match<TestComponentA>();   
  }
  
  class TestSystemB: TestSystem, IEntitySystem {
    public Matcher GetMatcher() => new Match<TestComponentB>();   
  }
  
  class TestSystemC: TestSystem, IEntitySystem {
    public Matcher GetMatcher() => new Match<TestComponentC>();   
  }
  
  class TestSystemD: TestSystem, IEntitySystem {
    public Matcher GetMatcher() => new Match<TestComponentD>();   
  }
  
  class TestSystemAandB: TestSystem, IEntitySystem {
    public Matcher GetMatcher() => (Matcher) new Match<TestComponentA>() & new Match<TestComponentB>();   
  }
  
  class TestSystemCandD: TestSystem, IEntitySystem {
    public Matcher GetMatcher() => (Matcher) new Match<TestComponentC>() & new Match<TestComponentD>();   
  }
  
  class TestSystemAandD: TestSystem, IEntitySystem {
    public Matcher GetMatcher() => (Matcher) new Match<TestComponentA>() & new Match<TestComponentD>();   
  }
  
  class TestSystemBandC: TestSystem, IEntitySystem {
    public Matcher GetMatcher() => (Matcher) new Match<TestComponentB>() & new Match<TestComponentC>();   
  }
  
  [TestFixture] public class Tests {
    private List<Entity> _entities;
    private SystemMatcher<IEntitySystem> _systemMatcher;

    [SetUp] public void Setup() {
      _entities = new List<Entity> {
        new Entity(new IEntityComponent[] {
          new TestComponentA()
        }),
        new Entity(new IEntityComponent[] {
          new TestComponentB()
        }),
        new Entity(new IEntityComponent[] {
          new TestComponentC()
        }),
        new Entity(new IEntityComponent[] {
          new TestComponentD()
        }),
        new Entity(new IEntityComponent[] {
          new TestComponentA(), 
          new TestComponentB(),
          new TestComponentC(),
          new TestComponentD() 
        }),
        new Entity(new IEntityComponent[] {
          new TestComponentA(), 
          new TestComponentB()
        }),
        new Entity(new IEntityComponent[] {
          new TestComponentC(), 
          new TestComponentD()
        }),
        new Entity(new IEntityComponent[] {
          new TestComponentA(), 
          new TestComponentD()
        }),
        new Entity(new IEntityComponent[] {
          new TestComponentB(), 
          new TestComponentC()
        })
      };
      
      _systemMatcher = new SystemMatcher<IEntitySystem>(new IEntitySystem[] {
        new TestSystemA(), 
        new TestSystemB(),
        new TestSystemC(),
        new TestSystemD(),
        new TestSystemAandB(), 
        new TestSystemCandD(),
        new TestSystemAandD(),
        new TestSystemBandC() 
      });      
    }

    [Test] public async void Test1() {
      var matched =
        from entity in _entities
        select entity;

      foreach (var entity in matched) {
        await _systemMatcher.Match(entity);
      }
    }
  }
}