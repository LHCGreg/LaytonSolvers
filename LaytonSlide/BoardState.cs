using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaytonPeg;

namespace LaytonSlide
{
    internal class BoardState
    {
        private List<List<SpaceState>> _spaces;
        private IDictionary<string, PiecePosition> _pieces;
        private List<Move> _history;
        private PiecePosition _winningPiecePosition;
        private int? _cachedHash;

        public PiecePosition WinningPiecePosition { get { return _winningPiecePosition; } }
        public ReadOnlyCollection<Move> History { get { return _history.AsReadOnly(); } }

        public bool IsWinningState()
        {
            return _pieces[_winningPiecePosition.Name].Equals(_winningPiecePosition);
        }

        public BoardState()
        {
            _pieces = new Dictionary<string, PiecePosition>()
            {
                { "Orb", new PiecePosition("Orb", ImmutableHashSet.Create(new BoardLocation(0, 2), new BoardLocation(0, 3), new BoardLocation(1, 2), new BoardLocation(1, 3))) },
                { "cross", new PiecePosition("cross", ImmutableHashSet.Create(new BoardLocation(2, 1), new BoardLocation(3, 0), new BoardLocation(3, 1), new BoardLocation(3, 2), new BoardLocation(4, 1))) },
                { "darkBlueL", new PiecePosition("darkBlueL", ImmutableHashSet.Create(new BoardLocation(2, 4), new BoardLocation(3, 4), new BoardLocation(3, 5))) },
                { "_", new PiecePosition("_", ImmutableHashSet.Create(new BoardLocation(4, 4), new BoardLocation(4, 5))) },
                { "orangeL", new PiecePosition("orangeL", ImmutableHashSet.Create(new BoardLocation(5, 1), new BoardLocation(5, 2), new BoardLocation(6, 2))) },
                { "I", new PiecePosition("I", ImmutableHashSet.Create(new BoardLocation(5, 3), new BoardLocation(6, 3))) },
                { "lightBlueL", new PiecePosition("lightBlueL", ImmutableHashSet.Create(new BoardLocation(5, 4), new BoardLocation(5, 5), new BoardLocation(6, 4))) }
            };

            _winningPiecePosition = new PiecePosition("Orb", ImmutableHashSet.Create(new BoardLocation(7, 2), new BoardLocation(7, 3), new BoardLocation(8, 2), new BoardLocation(8, 3)));
            _history = new List<Move>();
            _cachedHash = null;

            _spaces = new()
            {
                new()
                {
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                },
                new()
                {
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                },
                new()
                {
                    SpaceState.OutOfBounds,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.OutOfBounds,
                },
                new()
                {
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.OutOfBounds,
                },
                new()
                {
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.OutOfBounds,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                },
                new()
                {
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.OutOfBounds,
                },
                new()
                {
                    SpaceState.OutOfBounds,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.OutOfBounds,
                },
                new()
                {
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                },
                new()
                {
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.Empty,
                    SpaceState.Empty,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                },
            };

            foreach(PiecePosition piece in _pieces.Values)
            {
                foreach(BoardLocation spaceOccupied in piece.SpacesOccupied)
                {
                    _spaces[spaceOccupied.RowIndex][spaceOccupied.ColIndex] = new SpaceState(piece.Name);
                }
            }
        }

        private BoardState(List<List<SpaceState>> spaces, IDictionary<string, PiecePosition> pieces, List<Move> history, PiecePosition winningPiecePosition)
        {
            _spaces = spaces;
            _pieces = pieces;
            _history = history;
            _winningPiecePosition = winningPiecePosition;
            _cachedHash = null;
        }

        public override bool Equals(object? obj)
        {
            return obj is BoardState other && this.Equals(other);
        }

        public bool Equals(BoardState other)
        {
            return this._pieces.Count == other._pieces.Count && this._pieces.All(kvp =>
            {
                PiecePosition value2;
                return other._pieces.TryGetValue(kvp.Key, out value2) && kvp.Value.Equals(value2);
            });
        }

        public override int GetHashCode()
        {
            if (_cachedHash != null)
            {
                return _cachedHash.Value;
            }

            HashCode hash = new HashCode();
            foreach(PiecePosition piece in _pieces.Values)
            {
                hash.Add(piece);
            }
            int finalHash = hash.ToHashCode();
            _cachedHash = finalHash;
            return finalHash;
        }

        public List<Move> GetPossibleMoves()
        {
            List<Move> possibleMoves = new List<Move>();
            foreach(PiecePosition piece in _pieces.Values)
            {
                bool upMoveValid = true;
                bool rightMoveValid = true;
                bool downMoveValid = true;
                bool leftMoveValid = true;

                // Up: Row index - 1
                foreach (BoardLocation currentSpaceOccupied in piece.SpacesOccupied)
                {
                    if (currentSpaceOccupied.RowIndex == 0)
                    {
                        upMoveValid = false;
                        break;
                    }
                    SpaceState candidateSpace = _spaces[currentSpaceOccupied.RowIndex - 1][currentSpaceOccupied.ColIndex];
                    if(candidateSpace != SpaceState.Empty && candidateSpace != new SpaceState(piece.Name))
                    {
                        upMoveValid = false;
                        break;
                    }
                }

                // Right: Col index + 1
                foreach (BoardLocation currentSpaceOccupied in piece.SpacesOccupied)
                {
                    if (currentSpaceOccupied.ColIndex + 1 >= _spaces[currentSpaceOccupied.RowIndex].Count)
                    {
                        rightMoveValid = false;
                        break;
                    }
                    SpaceState candidateSpace = _spaces[currentSpaceOccupied.RowIndex][currentSpaceOccupied.ColIndex + 1];
                    if (candidateSpace != SpaceState.Empty && candidateSpace != new SpaceState(piece.Name))
                    {
                        rightMoveValid = false;
                        break;
                    }
                }

                // Down: Row index + 1
                foreach(BoardLocation currentSpaceOccupied in piece.SpacesOccupied)
                {
                    if(currentSpaceOccupied.RowIndex + 1 >= _spaces.Count)
                    {
                        downMoveValid = false;
                        break;
                    }
                    SpaceState candidateSpace = _spaces[currentSpaceOccupied.RowIndex + 1][currentSpaceOccupied.ColIndex];
                    if(candidateSpace != SpaceState.Empty && candidateSpace != new SpaceState(piece.Name))
                    {
                        downMoveValid = false;
                        break;
                    }
                }

                // Left: Col index - 1
                foreach(BoardLocation currentSpaceOccupied in piece.SpacesOccupied)
                {
                    if(currentSpaceOccupied.ColIndex == 0)
                    {
                        leftMoveValid = false;
                        break;
                    }
                    SpaceState candidateSpace = _spaces[currentSpaceOccupied.RowIndex][currentSpaceOccupied.ColIndex - 1];
                    if(candidateSpace != SpaceState.Empty && candidateSpace != new SpaceState(piece.Name))
                    {
                        leftMoveValid = false;
                        break;
                    }
                }

                if(upMoveValid)
                {
                    PiecePosition newPiecePosition = piece.Up();
                    possibleMoves.Add(new Move(piece, newPiecePosition, "up"));
                }
                if(rightMoveValid)
                {
                    PiecePosition newPiecePosition = piece.Right();
                    possibleMoves.Add(new Move(piece, newPiecePosition, "right"));
                }
                if(downMoveValid)
                {
                    PiecePosition newPiecePosition = piece.Down();
                    possibleMoves.Add(new Move(piece, newPiecePosition, "down"));
                }
                if(leftMoveValid)
                {
                    PiecePosition newPiecePosition = piece.Left();
                    possibleMoves.Add(new Move(piece, newPiecePosition, "left"));
                }
            }

            return possibleMoves;
        }

        public BoardState MakeMove(Move move)
        {
            List<List<SpaceState>> newSpaces = new List<List<SpaceState>>(_spaces.Count);
            foreach(List<SpaceState> row in _spaces)
            {
                List<SpaceState> rowCopy = row.ToList();
                newSpaces.Add(rowCopy);
            }

            foreach(BoardLocation previouslyOccupiedPosition in move.OldPosition.SpacesOccupied)
            {
                newSpaces[previouslyOccupiedPosition.RowIndex][previouslyOccupiedPosition.ColIndex] = SpaceState.Empty;
            }

            foreach(BoardLocation newOccupiedPosition in move.NewPosition.SpacesOccupied)
            {
                newSpaces[newOccupiedPosition.RowIndex][newOccupiedPosition.ColIndex] = new SpaceState(move.NewPosition.Name);
            }

            Dictionary<string, PiecePosition> newPieces = new Dictionary<string, PiecePosition>(_pieces);
            newPieces[move.NewPosition.Name] = move.NewPosition;

            List<Move> newHistory = _history.ToList();
            newHistory.Add(move);

            return new BoardState(newSpaces, newPieces, newHistory, _winningPiecePosition);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int rowIndex = 0; rowIndex < _spaces.Count; rowIndex++)
            {
                for (int colIndex = 0; colIndex < _spaces[rowIndex].Count; colIndex++)
                {
                    SpaceState space = _spaces[rowIndex][colIndex];
                    builder.Append(space);
                }
                builder.Append("\n");
            }
            return builder.ToString();
        }
    }
}
