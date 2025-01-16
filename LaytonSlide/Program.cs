using System.Collections.ObjectModel;

namespace LaytonSlide
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BoardState initialState = new BoardState();

            BoardState? winningPosition = Solve(initialState);
            if(winningPosition == null)
            {
                Console.WriteLine("Impossible initial position!");
            }
            else
            {
                ReadOnlyCollection<Move> winningMoves = winningPosition.History;
                Console.WriteLine($"{winningMoves.Count} moves to win.");
                Console.WriteLine(initialState);
                BoardState intermediatePosition = initialState;
                foreach (Move move in winningMoves)
                {
                    Console.WriteLine(move);
                    intermediatePosition = intermediatePosition.MakeMove(move);
                    Console.WriteLine(intermediatePosition);
                }
            }
        }

        static BoardState? Solve(BoardState initialState)
        {
            if(initialState.IsWinningState())
            {
                return initialState;
            }
            
            HashSet<BoardState> statesSeen = new HashSet<BoardState>() { initialState };

            Queue<BoardState> positionsToEvaluate = new Queue<BoardState>();
            positionsToEvaluate.Enqueue(initialState);

            long positionsEvaluated = 0;

            while (positionsToEvaluate.Count > 0)
            {
                BoardState currentPosition = positionsToEvaluate.Dequeue();

                List<Move> possibleMoves = currentPosition.GetPossibleMoves();
                foreach (Move move in possibleMoves)
                {
                    BoardState newPosition = currentPosition.MakeMove(move);
                    
                    if(move.OldPosition.Name.Equals(currentPosition.WinningPiecePosition.Name) && newPosition.IsWinningState())
                    {
                        return newPosition;
                    }
                    
                    if (!statesSeen.Contains(newPosition))
                    {
                        positionsToEvaluate.Enqueue(newPosition);
                        statesSeen.Add(newPosition);
                    }
                }

                positionsEvaluated++;
                if (positionsEvaluated % 1000 == 0)
                {
                    Console.WriteLine($"{positionsEvaluated} positions evaluated.");
                }
            }

            return null;
        }
    }
}
