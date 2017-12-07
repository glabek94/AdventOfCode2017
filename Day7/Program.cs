using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile = File.OpenText("../../input.txt");
            string input = inputFile.ReadToEnd().Trim();

            List<Node> allNodes = new List<Node>();

            foreach (string line in input.Split('\n').ToList())
            {
                var curLine = line.Trim().Split(' ');

                string name = curLine[0];
                int weight = int.Parse(curLine[1].Trim('(', ')'));

                Node currNode;
                currNode = allNodes.Find(c => c.name == name);
                if (currNode == null)
                {
                    currNode = new Node(name, weight, null);
                    allNodes.Add(currNode);
                }
                currNode.weight = weight;

                if (curLine.Length > 2) // line with childs
                {
                    for (int i = 3; i < curLine.Length; i++)
                    {
                        string childName = curLine[i].TrimEnd(',');
                        Node currChild = allNodes.Find(c => c.name == childName);
                        if (currChild == null)
                        {
                            currChild = new Node(childName, 0, currNode);
                            allNodes.Add(currChild);
                        }
                        currChild.parent = currNode;
                        currNode.children.Add(currChild);
                    }
                }
            }

            Node root = allNodes.Find(c => c.parent == null);
            Console.WriteLine($"Root: {root.name}");

            while (true)
            {
                Dictionary<Node, long> weights = new Dictionary<Node, long>();
                foreach (var child in root.children)
                {
                    weights.Add(child, CountWeight(child));
                }
                var oth = weights.GroupBy(a => a.Value).Where(group => !(group.Skip(1).Any())).SelectMany(g => g).ToList(); //OMG
                if (oth.Count != 0)
                {
                    root = oth[0].Key;
                }
                else
                {
                    break;
                }
            }

            long siblingsWeight = CountWeight(root);
            foreach(var child in root.parent.children)
            {
                if(CountWeight(child) != CountWeight(root))
                {
                    siblingsWeight = CountWeight(child);
                    break;
                }
            }

            long newWeight = root.weight + (siblingsWeight - CountWeight(root));
            Console.WriteLine($"{root.name}\t{root.weight} -> {newWeight}");
        }

        static long CountWeight(Node node, long weight = 0)
        {
            weight += node.weight;
            foreach (var child in node.children)
            {
                weight += CountWeight(child);
            }

            return weight;
        }
    }

    public class Node
    {
        public string name { get; set; }
        public int weight { get; set; }
        public Node parent { get; set; }
        public List<Node> children { get; set; }

        public Node(string name, int weight, Node parent)
        {
            this.name = name;
            this.weight = weight;
            this.parent = parent;
            this.children = new List<Node>();
        }
    }
}