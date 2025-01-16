using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaytonSlide
{
    internal struct PiecePosition
    {        
        public string Name { get; }
        public IImmutableSet<BoardLocation> SpacesOccupied { get; }

        public PiecePosition(string name, IImmutableSet<BoardLocation> spacesOccupied)
        {
            Name = name;
            SpacesOccupied = spacesOccupied;
        }

        public PiecePosition Up()
        {
            var builder = ImmutableHashSet.CreateBuilder<BoardLocation>();
            foreach(BoardLocation currentSpaceOccupied in SpacesOccupied)
            {
                BoardLocation newSpaceOccupied = currentSpaceOccupied.Up();
                builder.Add(newSpaceOccupied);
            }
            return new PiecePosition(Name, builder.ToImmutable());
        }

        public PiecePosition Right()
        {
            var builder = ImmutableHashSet.CreateBuilder<BoardLocation>();
            foreach (BoardLocation currentSpaceOccupied in SpacesOccupied)
            {
                BoardLocation newSpaceOccupied = currentSpaceOccupied.Right();
                builder.Add(newSpaceOccupied);
            }
            return new PiecePosition(Name, builder.ToImmutable());
        }

        public PiecePosition Down()
        {
            var builder = ImmutableHashSet.CreateBuilder<BoardLocation>();
            foreach (BoardLocation currentSpaceOccupied in SpacesOccupied)
            {
                BoardLocation newSpaceOccupied = currentSpaceOccupied.Down();
                builder.Add(newSpaceOccupied);
            }
            return new PiecePosition(Name, builder.ToImmutable());
        }

        public PiecePosition Left()
        {
            var builder = ImmutableHashSet.CreateBuilder<BoardLocation>();
            foreach (BoardLocation currentSpaceOccupied in SpacesOccupied)
            {
                BoardLocation newSpaceOccupied = currentSpaceOccupied.Left();
                builder.Add(newSpaceOccupied);
            }
            return new PiecePosition(Name, builder.ToImmutable());
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is PiecePosition other && this.Equals(other);
        }

        public bool Equals(PiecePosition other)
        {
            return this.Name == other.Name && this.SpacesOccupied.SetEquals(other.SpacesOccupied);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Name);
            foreach(BoardLocation spaceOccupied in SpacesOccupied)
            {
                hash.Add(spaceOccupied);
            }
            return hash.ToHashCode();
        }
    }
}
