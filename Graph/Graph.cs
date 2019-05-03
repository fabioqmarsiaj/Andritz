using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Graph
{
    public interface IGraph<T>
    {
        List<string> RoutesBetween(T source, T target);
    }

    public class Graph<T> : IGraph<T>
    {
        private IEnumerable<ILink<T>> Links;

        public Graph(IEnumerable<ILink<T>> links) => this.Links = links;



        public List<string> RoutesBetween(T source, T target)
        {

            var allConections = Links;

            // ab, ah
            ObservableCollection<ILink<T>> firstsPaths = new ObservableCollection<ILink<T>>();
            //({b, c}, {c, b}, {c, d}, {d, e}, {h, g}, {g, f}, {f, e})
            ObservableCollection<ILink<T>> possiblePaths = new ObservableCollection<ILink<T>>();

            //({a, b}, {b, c})
            ObservableCollection<ILink<T>> newPath = new ObservableCollection<ILink<T>>();

            //({a, h}, {h, g})
            ObservableCollection<ILink<T>> newPath2 = new ObservableCollection<ILink<T>>();

            //({a, b}, {b, c}, {c, d})
            //({a, b}, {b, c}, {c, b})
            ObservableCollection<ILink<T>> intermediatePath = new ObservableCollection<ILink<T>>();

            //({a, h}, {h, g}, {g, f})
            ObservableCollection<ILink<T>> intermediatePath2 = new ObservableCollection<ILink<T>>();

            //({a, b}, {b, c}, {c, d}, {d, e})
            ObservableCollection<ILink<T>> finalPath = new ObservableCollection<ILink<T>>();

            //({a, h}, {h, g}, {g, f}, {f, e})
            ObservableCollection<ILink<T>> finalPath2 = new ObservableCollection<ILink<T>>();

            //removed : ba, da

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

            newPath.Add(firstsPaths[0]);
            newPath2.Add(firstsPaths[1]);

            foreach (var connection in possiblePaths.ToList())
            {
                if (connection.Source.Equals(firstsPaths[0].Target))
                {
           
                    newPath.Add(connection);
                    possiblePaths.Remove(connection);
  
                }

                else if(connection.Source.Equals(firstsPaths[1].Target))
                {                                      
                    newPath2.Add(connection);
                    possiblePaths.Remove(connection);
                   
                }               
            }

            foreach (var connection in newPath)
            {
                intermediatePath.Add(connection);
            }
            foreach (var connection in newPath2)
            {
                intermediatePath2.Add(connection);
            }

            foreach (var connection in possiblePaths.ToList())
            {
                if (connection.Source.Equals(newPath[1].Target))
                {   
                    intermediatePath.Add(connection);
                    possiblePaths.Remove(connection);               
                }
                
                else if (connection.Source.Equals(newPath2[1].Target))
                { 
                    intermediatePath2.Add(connection);
                    possiblePaths.Remove(connection);      
                }              
            }    

            foreach (var connection in intermediatePath)
            {
                finalPath.Add(connection);
            }

            foreach (var connection in intermediatePath2)
            {
                finalPath2.Add(connection);
            }

            foreach (var connection in possiblePaths.ToList())
            {
                if (connection.Source.Equals(intermediatePath[3].Target))
                {
                    
                    finalPath.Add(connection);
                    possiblePaths.Remove(connection);
                    

                }
                else if (connection.Source.Equals(intermediatePath2[2].Target))
                {
                    
                    finalPath2.Add(connection);
                    possiblePaths.Remove(connection);                    
                }             
            }

            for (int i = 3; i < finalPath.ToList().Count(); i++)
            {
                var con = finalPath.ElementAt(i);
                for (int j = 2; j < finalPath.ToList().Count(); j++)
                {
                    if (finalPath.ElementAt(i).Source.Equals(finalPath.ElementAt(j).Source))
                    {
                        finalPath.Remove(finalPath.ElementAt(j));
                        break;
                    }
                }
                
            }
            List<string> path = new List<string>();

            foreach (var connection in finalPath)
            {
                var oneSource = connection.Source.ToString();
                path.Add(oneSource);

                if (connection.Target.Equals(target))
                {
                    var finalTarget = target.ToString();
                    path.Add(finalTarget);
                }
            }

            List<string> path2 = new List<string>();

            foreach (var connection in finalPath2)
            {
                var oneSource = connection.Source.ToString();
                path2.Add(oneSource);

                if (connection.Target.Equals(target))
                {
                    var finalTarget = target.ToString();
                    path2.Add(finalTarget);
                }
            }

            var finalPathString = string.Join("", path);
            var finalPath2String = string.Join("", path2);

            List<string> finalList = new List<string>();
            finalList.Add(finalPathString);
            finalList.Add(finalPath2String);

            return finalList;
        }
    }
}

