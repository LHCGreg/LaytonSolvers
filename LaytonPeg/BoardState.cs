using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaytonPeg
{
    class BoardState
    {
        private List<List<SpaceState>> _spaces;
        private int _numFilled;
        private List<Tuple<BoardState, Move>> _priorHistory;
        private int? _cachedHash;

        public ReadOnlyCollection<Tuple<BoardState, Move>> PriorHistory { get { return _priorHistory.AsReadOnly(); } }

        private BoardState(List<List<SpaceState>> spaces, int numFilled, List<Tuple<BoardState, Move>> priorHistory)
        {
            _spaces = spaces;
            _numFilled = numFilled;
            _priorHistory = priorHistory;
            _cachedHash = null;
        }

        public BoardState()
        {
            // [row, column]
            _spaces = new()
            {
                new(){
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds
                },
                new(){
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds
                },
                new(){
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled
                },
                new(){
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Empty,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled
                },
                new(){
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled
                },
                new(){
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds
                },
                new(){
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.Filled,
                    SpaceState.OutOfBounds,
                    SpaceState.OutOfBounds
                }
                //new(){
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds,
                //    SpaceState.Empty,
                //    SpaceState.Filled,
                //    SpaceState.Empty,
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds
                //},
                //new(){
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds
                //},
                //new(){
                //    SpaceState.Empty,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Empty
                //},
                //new(){
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Empty,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled
                //},
                //new(){
                //    SpaceState.Empty,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Empty
                //},
                //new(){
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds
                //},
                //new(){
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds,
                //    SpaceState.Empty,
                //    SpaceState.Filled,
                //    SpaceState.Empty,
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds
                //}
                //new(){
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds,
                //    SpaceState.Empty,
                //    SpaceState.Empty,
                //    SpaceState.Empty,
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds
                //},
                //new(){
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds,
                //    SpaceState.Empty,
                //    SpaceState.Empty,
                //    SpaceState.Empty,
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds
                //},
                //new(){
                //    SpaceState.Empty,
                //    SpaceState.Empty,
                //    SpaceState.Empty,
                //    SpaceState.Empty,
                //    SpaceState.Empty,
                //    SpaceState.Empty,
                //    SpaceState.Empty
                //},
                //new(){
                //    SpaceState.Empty,
                //    SpaceState.Empty,
                //    SpaceState.Filled,
                //    SpaceState.Empty,
                //    SpaceState.Filled,
                //    SpaceState.Empty,
                //    SpaceState.Empty
                //},
                //new(){
                //    SpaceState.Empty,
                //    SpaceState.Empty,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Empty,
                //    SpaceState.Empty
                //},
                //new(){
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds
                //},
                //new(){
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.Filled,
                //    SpaceState.OutOfBounds,
                //    SpaceState.OutOfBounds
                //}
            };
            _numFilled = _spaces.SelectMany(x => x).Count(space => space == SpaceState.Filled);
            _priorHistory = new List<Tuple<BoardState, Move>>();
            _cachedHash = null;
        }

        public bool IsWinningState { get { return _numFilled == 1; } }

        public override bool Equals(object? obj)
        {
            BoardState? other = obj as BoardState;
            if(other == null)
            {
                return false;
            }
            return Equals(other);
        }

        public bool Equals(BoardState other)
        {
            if (this._spaces.Count != other._spaces.Count)
            {
                return false;
            }
            for(int i = 0; i < this._spaces.Count; i++)
            {
                if (this._spaces[i].Count != other._spaces[i].Count)
                {
                    return false;
                }
                for(int j = 0; j < this._spaces[i].Count; j++)
                {
                    if (this._spaces[i][j] != other._spaces[i][j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            if(_cachedHash != null)
            {
                return _cachedHash.Value;
            }

            int hash = 0;
            for(int i = 0; i < _spaces.Count; i++)
            {
                for(int j = 0; j < _spaces[i].Count; j++)
                {
                    SpaceState spaceState = _spaces[i][j];
                    int spaceStateValue;
                    if(spaceState == SpaceState.Filled)
                    {
                        spaceStateValue = 11;
                    }
                    else if(spaceState == SpaceState.Empty)
                    {
                        spaceStateValue = 5;
                    }
                    else
                    {
                        spaceStateValue = 23;
                    }
                    int spaceValue = i * 3 * j * 7 * spaceStateValue;
                    hash += spaceValue;
                }
            }

            _cachedHash = hash;
            return hash;
        }

        public List<Move> GetPossibleMoves()
        {
            List<Move> possibleMoves = new List<Move>();
            for(int rowIndex = 0; rowIndex < _spaces.Count; rowIndex++)
            {
                for(int colIndex = 0; colIndex < _spaces[rowIndex].Count; colIndex++)
                {
                    if (_spaces[rowIndex][colIndex] != SpaceState.Filled)
                    {
                        continue;
                    }
                    // Up: dest row of rowIndex - 2, same dest col.
                    // rowIndex - 1 must exist and be filled. rowIndex - 2 must exist and be empty
                    if(rowIndex - 2 >= 0 && _spaces[rowIndex - 1][colIndex] == SpaceState.Filled && _spaces[rowIndex - 2][colIndex] == SpaceState.Empty)
                    {
                        Move move = new Move(rowIndex, colIndex, rowIndex - 2, colIndex, rowIndex - 1, colIndex);
                        possibleMoves.Add(move);
                    }
                    // Right: dest row of rowIndex, dest col of colIndex + 2
                    // colIndex + 1 must exist and be filled. colIndex + 2 must exist and be empty
                    if(colIndex + 2 < _spaces[rowIndex].Count && _spaces[rowIndex][colIndex + 1] == SpaceState.Filled && _spaces[rowIndex][colIndex + 2] == SpaceState.Empty)
                    {
                        Move move = new Move(rowIndex, colIndex, rowIndex, colIndex + 2, rowIndex, colIndex + 1);
                        possibleMoves.Add(move);
                    }
                    // Down: dest row of rowIndex + 2, same dest col
                    // rowIndex + 1 must exist and be filled. rowIndex + 2 must exist and be empty
                    if(rowIndex + 2 < _spaces.Count && _spaces[rowIndex + 1][colIndex] == SpaceState.Filled && _spaces[rowIndex + 2][colIndex] == SpaceState.Empty)
                    {
                        Move move = new Move(rowIndex, colIndex, rowIndex + 2, colIndex, rowIndex + 1, colIndex);
                        possibleMoves.Add(move);
                    }
                    // Left: dest row of rowIndex, dest col of colIndex - 2
                    // colIndex - 1 must exist and be filled. colIndex - 2 must exist and be empty
                    if(colIndex - 2 >= 0 && _spaces[rowIndex][colIndex - 1] == SpaceState.Filled && _spaces[rowIndex][colIndex - 2] == SpaceState.Empty)
                    {
                        Move move = new Move(rowIndex, colIndex, rowIndex, colIndex - 2, rowIndex, colIndex - 1);
                        possibleMoves.Add(move);
                    }
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

            newSpaces[move.SourceRow][move.SourceCol] = SpaceState.Empty;
            newSpaces[move.DestRow][move.DestCol] = SpaceState.Filled;
            newSpaces[move.RemovedRow][move.RemovedCol] = SpaceState.Empty;

            List<Tuple<BoardState, Move>> newBoardHistory = _priorHistory.ToList();
            newBoardHistory.Add(new Tuple<BoardState, Move>(this, move));

            return new BoardState(newSpaces, _numFilled - 1, newBoardHistory);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for(int rowIndex = 0; rowIndex < _spaces.Count; rowIndex++)
            {
                for(int colIndex = 0; colIndex < _spaces[rowIndex].Count; colIndex++)
                {
                    SpaceState space = _spaces[rowIndex][colIndex];
                    if(space == SpaceState.OutOfBounds)
                    {
                        builder.Append(" ");
                    }
                    else if(space == SpaceState.Empty)
                    {
                        builder.Append(".");
                    }
                    else
                    {
                        builder.Append("O");
                    }
                }
                builder.Append("\n");
            }
            return builder.ToString();
        }
    }
}
