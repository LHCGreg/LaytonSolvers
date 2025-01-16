using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaytonSlide
{
    internal class Move
    {
        public PiecePosition OldPosition { get; }
        public PiecePosition NewPosition { get; }
        public string Direction { get; }

        public Move(PiecePosition oldPosition, PiecePosition newPosition, string direction)
        {
            OldPosition = oldPosition;
            NewPosition = newPosition;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"{OldPosition.Name} {Direction}";
        }
    }
}
