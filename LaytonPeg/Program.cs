using System.Linq;

namespace LaytonPeg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BoardState initialState = new BoardState();
            List<Tuple<BoardState, Move?>>? winningMoves = Solve(initialState);
            if(winningMoves == null)
            {
                Console.WriteLine("Impossible initial position!");
            }
            else
            {
                foreach(Tuple<BoardState, Move?> stateAndMove in winningMoves)
                {
                    BoardState state = stateAndMove.Item1;
                    Move? move = stateAndMove.Item2;

                    Console.WriteLine(state);
                    if(move != null)
                    {
                        Console.WriteLine(move);
                    }
                }
            }
        }

        static List<Tuple<BoardState, Move?>>? Solve(BoardState initialState)
        {
            HashSet<BoardState> statesSeen = new HashSet<BoardState>() { initialState };

            Stack<BoardState> positionsToEvaluate = new Stack<BoardState>();
            positionsToEvaluate.Push(initialState);

            while(positionsToEvaluate.Count > 0)
            {
                BoardState currentPosition = positionsToEvaluate.Pop();
                statesSeen.Add(currentPosition);

                if(currentPosition.IsWinningState)
                {
                    List<Tuple<BoardState, Move?>> winningMoves = new List<Tuple<BoardState, Move?>>();
                    foreach(Tuple<BoardState, Move> priorMove in currentPosition.PriorHistory)
                    {
                        winningMoves.Add(new Tuple<BoardState, Move?>(priorMove.Item1, priorMove.Item2));
                    }
                    winningMoves.Add(new Tuple<BoardState, Move?>(currentPosition, null));
                    return winningMoves;
                }

                List<Move> possibleMoves = currentPosition.GetPossibleMoves();
                foreach (Move move in possibleMoves)
                {
                    BoardState newPosition = currentPosition.MakeMove(move);
                    if(!statesSeen.Contains(newPosition))
                    {
                        positionsToEvaluate.Push(newPosition);
                    }
                }
            }

            return null;
        }
    }
}
