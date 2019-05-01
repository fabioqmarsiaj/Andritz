using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Graph
{
    public interface IGraph<T>
    {
        IObservable<IEnumerable<T>> RoutesBetween(T source, T target);
    }

    public class Graph<T> : IGraph<T>
    {
        private IEnumerable<ILink<T>> Links;

        public Graph(IEnumerable<ILink<T>> links) => this.Links = links;



        public IObservable<IEnumerable<T>> RoutesBetween(T source, T target)
        {

            var allConections = Links;

            // ab, ah
            ObservableCollection<ILink<T>> firstsPaths = new ObservableCollection<ILink<T>>();
            //bc, cb, cd, de, hg, gf, fe
            ObservableCollection<ILink<T>> possiblePaths = new ObservableCollection<ILink<T>>();

            //caiu fora : ba, da

            foreach (var connection in allConections)
            {
                if (connection.Source.Equals(source))
                {
                    //Takes both paths that contains source->"a"
                    //{a -> b} AND {a -> h}
                    firstsPaths.Add(connection);
                    
                }
                else
                {
                    //Takes all the remaining links
                    possiblePaths.Add(connection);

                    if (connection.Target.Equals(source))
                    {
                        //Remove the links that have the parameter source as TARGET
                        // We don't want to make the path backwards.
                        possiblePaths.Remove(connection);
                    }                   
                }
                
            }

            
            
         

            //abcd de
            return firstsPaths;
        }

        public class TestClass : IObservable<IEnumerable<T>>
        {
     
            public IDisposable Subscribe(IObserver<IEnumerable<T>> observer)
            {
                throw new NotImplementedException();
            }
        }

    }
}

