using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace RobotNavigationProblem
{
    public class Map
    {
        private int[] _dimensions = new int[2] { 0, 0 };
        private byte[,] _mapValues;
        private Position[] _goal = new Position[2];
        private Position _startPos;

        public Map(string fileName)
        {
            loadFile(fileName);
        }

        public enum MapCodec : byte
        {
            Empty = 0,
            Wall = 1,
            Player = 2,
            Goal = 3
        }

        public int[] Dimensions
        {
            get => _dimensions;
            set => _dimensions = value;
        }

        public byte[,] MapValues
        {
            get => _mapValues;
            set => _mapValues = value;
        }

        public Position[] Goal
        {
            get => _goal;
            set => _goal = value;
        }

        public Position StartPos
        {
            get => _startPos;
            set => _startPos = value;
        }

        public void loadFile(string fileName)
        {
            //Place all lines of the file onto the parsed list
            StreamReader reader = new StreamReader(fileName);
            try
            {
                string line = reader.ReadLine();
                //Read file in order
                //We know what must be trimmed in each order (as the file is saved in a particular format)
                //First we need to set the dimensions of the field/map and then create it
                string[] coords = line.Trim('[', ']').Split(',');
                Dimensions[0] = coords[1].rInt();
                Dimensions[1] = coords[0].rInt();

                MapValues = new byte[Dimensions[0], Dimensions[1]];

                //Now we have created map with a size set in the first line of the file being read.
                //Inputting the player's co-ordinates onto the map.
                line = reader.ReadLine();
                coords = line.Trim('(', ')').Split(',');
                MapValues[coords[0].rInt(), coords[1].rInt()] = 2;
                StartPos = new Position(coords[0].rInt(), coords[1].rInt());

                //Adding the goal states in
                line = reader.ReadLine();
                string[] sets = line.Replace(" ", string.Empty).Split('|');
                for (int i = 0; i < sets.Length; i++)
                {
                    coords = sets[i].Trim('(', ')').Split(',');
                    MapValues[coords[0].rInt(), coords[1].rInt()] = 3;
                    //TODO Need to add error checking here.
                    Goal[i] = new Position(coords[0].rInt(), coords[1].rInt());
                }

                //Reading boundaries/walls now, in a loop
                while(!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    coords = line.Trim('(', ')').Split(',');

                    for (int i = 0; i < coords[2].rInt(); i++)
                    {
                        for(int j = 0; j < coords[3].rInt(); j++)
                        {
                            MapValues[coords[0].rInt() + i, coords[1].rInt() + j] = 1;
                        }
                    }
                }
            }
            finally
            {
                reader.Close();
            }

            //Now we are no longer needing the file itself and have all the values (in string form) in the parsed list
            //Go through the list and parse the values -- some are in an expected arrangement of fixed length (e.g. Grid size, initial state, 2 goal states)
            //Only non-fixed element is the number of walls expressed in tuples

            //Just a quick way of drawing the loaded table into the debug console, can be commented out later
            #region consoleDraw
            Debug.WriteLine("File " + fileName + " has been loaded.");
            Debug.Write("+");
            for (int i = 0; i < ((Dimensions[0] * 3) - 9); i++)
            {
                Debug.Write("=");
            }
            Debug.Write("+");
            Debug.WriteLine("");
            for (int i = 0; i < Dimensions[1]; i++)
            {
                Debug.Write("+ ");
                for (int j = 0; j < Dimensions[0]; j++)
                {
                    Debug.Write(MapValues[j, i] + " ");
                }
                Debug.Write(" +");
                Debug.WriteLine("");
            }
            Debug.Write("+");
            for (int i = 0; i < ((Dimensions[0] * 3) - 9); i++)
            {
                Debug.Write("=");
            }
            Debug.Write("+");
            Debug.WriteLine(" ");
            #endregion
        }

        public List<Node> GetNeighbouringNodes(Node node)
        {
            List<Node> neighbouringNodes = new List<Node>();

            int xCheck;
            int yCheck;

            //Check NORTH
            xCheck = node.Position.X;
            yCheck = node.Position.Y + 1;
            if (xCheck >= 0 && xCheck < Dimensions[0])
            {
                if (yCheck >= 0 && yCheck < Dimensions[1])
                {
                    neighbouringNodes.Add(new Node(new Position(xCheck, yCheck)));
                }
            }

            //Check EAST
            xCheck = node.Position.X + 1;
            yCheck = node.Position.Y;
            if (xCheck >= 0 && xCheck < Dimensions[0])
            {
                if (yCheck >= 0 && yCheck < Dimensions[1])
                {
                    neighbouringNodes.Add(new Node(new Position(xCheck, yCheck)));
                }
            }

            //Check WEST
            xCheck = node.Position.X - 1;
            yCheck = node.Position.Y;
            if (xCheck >= 0 && xCheck < Dimensions[0])
            {
                if (yCheck >= 0 && yCheck < Dimensions[1])
                {
                    neighbouringNodes.Add(new Node(new Position(xCheck, yCheck)));
                }
            }

            //Check SOUTH
            xCheck = node.Position.X;
            yCheck = node.Position.Y - 1;
            if (xCheck >= 0 && xCheck < Dimensions[0])
            {
                if (yCheck >= 0 && yCheck < Dimensions[1])
                {
                    neighbouringNodes.Add(new Node(new Position(xCheck, yCheck)));
                }
            }

            return neighbouringNodes;
        }

        //3 Methods for checking the validity of the path being made by an algorithm, can be used (or scrapped) later if not required
        public bool IsValid(Position p)
        {
            if(IsNotWall(p) && IsInBounds(p))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsNotWall(Position p)
        {
            if(MapValues[p.X, p.Y] == (byte)MapCodec.Wall)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsInBounds(Position p)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= Dimensions[0] || p.Y >= Dimensions[1])
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
