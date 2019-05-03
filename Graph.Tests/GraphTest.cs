using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Reactive.Linq;

namespace Graph.Tests
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void TestRoutesBetweenTwoPoints()
        {
            var links = new ILink<string>[]
            {
                new Link<string>("a","b"),
                new Link<string>("b","c"),
                new Link<string>("c","b"),
                new Link<string>("b","a"),
                new Link<string>("c","d"),
                new Link<string>("d","e"),
                new Link<string>("d","a"),
                new Link<string>("a","h"),
                new Link<string>("h","g"),
                new Link<string>("g","f"),
                new Link<string>("f","e"),
            };
            

            var graph = new Graph<string>(links);
            var paths = graph.RoutesBetween("a", "e");

            var list = paths.ToList();
            Assert.AreEqual(list.ToList().Count(), 2);

            Assert.AreEqual("abcde", list[0]);
            Assert.AreEqual("ahgfe", list[1]);          
        }
    }
}
