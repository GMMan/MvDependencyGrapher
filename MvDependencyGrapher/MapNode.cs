using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvDependencyGrapher
{
    class MapNode
    {
        public long Id { get; set; }
        public List<MapNode> TransfersToNodes { get; } = new List<MapNode>();
        public List<MapNode> TransfersFromNodes { get; } = new List<MapNode>();
    }
}
