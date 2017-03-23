using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class Location
    {
        public Location(int id, int? parentId, string name)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.Name = name;
        }
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Name { get; set; }
    }
    class Program
    {
        public static void DisplayTree<T>(IEnumerable<Node<T>> nodes, string display)
        {
            foreach(var node in nodes)
            {
                var displayProperty = node.Value.GetType().GetProperty(display);
                Console.WriteLine(string.Format("{0}{1}", new String('-', node.Level), displayProperty.GetValue(node.Value, null)));
                if (node.Children.Count() > 0)
                {
                    DisplayTree<T>(node.Children, display);
                }
            }
            
            
        }
        static void Main(string[] args)
        {
            //var locations = new List<Location>
            //{
            //    new Location(1, null, "Amsterdam"),
            //    new Location(2, 1, "North"),
            //    new Location(3, 2, "East"),
            //    new Location(4, 2, "West"),
            //    new Location(5, null, "Newyork"),
            //    new Location(6, 5, "London"),
            //};

            var locations = new List<Location>
                {
                    //           id, parentID, name
                    new Location(50, null, "Europe"),
                        new Location(2, 50, "North"),
                        new Location(4, 50, "West"),
                            new Location(20, 4, "The Netherlands"),
                                new Location(11, 20, "Leiden"),
                                new Location(12, 20, "Amsterdam"),
                                new Location(19, 20, "Rotterdam"),
                            new Location(7, 4, "Belgium"),
                        new Location(5, 50, "East"),
                        new Location(30,50, "South"),
                            new Location(31, 30, "Spain"),
                            new Location(31, 30, "Italy"),
                };
            var rootNodes = Node<Location>.CreateTree(locations, l => l.Id, l => l.ParentId);
            var rootNode = rootNodes.Single();
            DisplayTree<Location>(rootNodes, "Name");
            var theNetherlands = rootNode.Descendants.Single(n => n.Value.Name == "The Netherlands");
        }
    }
}
