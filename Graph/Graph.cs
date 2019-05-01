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
            // ab, ah
            List<string> firstsPaths = new List<string>();

            //bc, cb, cd, de, hg, gf, fe
            List<string> possiblePaths = new List<string>();

            //caiu fora : ba, da

            var allConections = Links;

            foreach (var connection in allConections)
            {
                if (connection.Source.Equals(source))
                {
                    //Takes both paths that contains source->"a"
                    //{a -> b} AND {a -> h}
                    firstsPaths.Add(connection.ToString());

                    
                }
                else
                {
                    //Takes all the remaining nodes
                    possiblePaths.Add(connection.ToString());

                    if (connection.Target.Equals(source))
                    {
                        possiblePaths.Remove(connection.ToString());
                    }                   
                }
                
            }

           

         

            //abcd de
            return null;
        }
               
    }
}

