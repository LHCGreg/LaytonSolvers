using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaytonSlide
{
    internal struct BoardLocation
    {
        public int RowIndex { get; }
        public int ColIndex { get; }

        public BoardLocation(int rowIndex, int colIndex)
        {
            RowIndex = rowIndex;
            ColIndex = colIndex;
        }

        public BoardLocation Up()
        {
            return new BoardLocation(RowIndex - 1, ColIndex);
        }

        public BoardLocation Right()
        {
            return new BoardLocation(RowIndex, ColIndex + 1);
        }

        public BoardLocation Down()
        {
            return new BoardLocation(RowIndex + 1, ColIndex);
        }

        public BoardLocation Left()
        {
            return new BoardLocation(RowIndex, ColIndex - 1);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is BoardLocation other && this.Equals(other);
        }

        public bool Equals(BoardLocation other)
        {
            return this.RowIndex == other.RowIndex && this.ColIndex == other.ColIndex;
        }

        public override int GetHashCode()
        {
            return (RowIndex, ColIndex).GetHashCode();
        }
    }
}
