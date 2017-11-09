using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvDependencyGrapher
{
    class MapNode
    {
        public long Id { get; set; }
        public HashSet<MapNode> TransfersToNodes { get; } = new HashSet<MapNode>();
        public HashSet<MapNode> TransfersFromNodes { get; } = new HashSet<MapNode>();
    }
}
