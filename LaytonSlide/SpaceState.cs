using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaytonPeg
{
    internal struct SpaceState
    {
        private string _takenBy;
        
        public SpaceState(string takenBy)
        {
            _takenBy = takenBy;
        }

        private static SpaceState s_outOfBounds = new SpaceState();
        private static SpaceState s_empty = new SpaceState("_empty");

        public static SpaceState OutOfBounds { get { return s_outOfBounds; } }
        public static SpaceState Empty { get { return s_empty; } }

        public override string ToString()
        {
            if(_takenBy == null)
            {
                return "#";
            }
            else if(_takenBy == "_empty")
            {
                return ".";
            }
            else
            {
                return _takenBy.Substring(0, 1);
            }
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is SpaceState other && this.Equals(other);
        }

        public bool Equals(SpaceState other)
        {
            return this._takenBy == other._takenBy;
        }

        public override int GetHashCode()
        {
            if(_takenBy == null)
            {
                return 0;
            }
            else
            {
                return _takenBy.GetHashCode();
            }
        }

        public static bool operator ==(SpaceState lhs, SpaceState rhs)
        {
            return lhs._takenBy == rhs._takenBy;
        }

        public static bool operator !=(SpaceState lhs, SpaceState rhs)
        {
            return !(lhs == rhs);
        }
    }
}
