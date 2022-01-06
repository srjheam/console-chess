using Board;
using Board.Enums;

using Chess.Pieces;

using Extensions;

using Screen;
using static Screen.GraphicEngine;

using System;

namespace Chess
{
    /// <summary>
    /// Handles a chess match.
    /// </summary>
    class ChessMatch
    {
        /// <value>Gets the number of moves made in the game.</value>
        private int movementsMade;
        /// <value>Gets this match's chess board.</value>
        public ChessBoard Board { get; }
        /// <value>Gets the selected piece, if one is selected.</value>
        public ChessPiece SelectedPiece { get; private set; }
        /// <value>Gets the turn the chess game is on.</value>
        public int Turn
        {
            get => (int)Math.Ceiling(movementsMade / 2.0);
        }
        /// <value>Get which team is playing now.</value>
        public Team TeamPlaying
        {
            get => (Team)(movementsMade % 2);
        }

        /// <summary>
        /// Creates a new chess match.
        /// </summary>
        public ChessMatch()
        {
            movementsMade = 0;
            Board = new ChessBoard(this);
        }

        /// <summary>
        /// Starts a new chess match.
        /// </summary>
        public void Start()
        {
            do
            {
                NextTurn();
                StartTurn();
            } while (true);
        }

        /// <summary>
        /// Starts a new chess game turn.
        /// </summary>
        private void StartTurn()
        {
            Console.Clear();
            PrintMatchSummary(this);

            SelectPiece();

            Console.Clear();
            PrintMatchSummary(this);

            var selectedTarget = SelectSelectedPieceTarget();

            Board.MovePiece(new BoardPosition(SelectedPiece.GetPosition(), Board), new BoardPosition(selectedTarget, Board));
        }

        /// <summary>
        /// Moves the match to the next turn.
        /// </summary>
        private void NextTurn()
        {
            movementsMade++;
            SelectedPiece = null;
        }

        /// <summary>
        /// Prompts the user to select a piece.
        /// </summary>
        private void SelectPiece()
        {
            try
            {
                var userPiece = Board.GetPiece(UserInput.RequestInput("Select a Piece").ToArrayPosition(Board)) as ChessPiece;

                if (userPiece is null)
                    throw new ArgumentNullException("You cannot select an empty space.", innerException: null);
                else if (userPiece.Team != TeamPlaying)
                    throw new ArgumentException("You cannot select an enemy piece now.");
                else if (!userPiece.PossibleTargets().HasTrue())
                    throw new ArgumentException("The selected piece has no movements available.");

                SelectedPiece = userPiece;
            }
            catch (Exception e)
            {
                ShowOneLineError(e);

                SelectPiece();
            }
        }

        /// <summary>
        /// Prompts the user to select the selected piece's target.
        /// </summary>
        private TwoDimensionPosition SelectSelectedPieceTarget()
        {
            try
            {
                var userTarget = UserInput.RequestInput("Select a target").ToArrayPosition(Board);

                if (!SelectedPiece.PossibleTargets()[userTarget.Y, userTarget.X])
                    throw new ArgumentException("The informed position isn't a possible target for the selected piece.");

                return userTarget;
            }
            catch (Exception e)
            {
                ShowOneLineError(e);

                return SelectSelectedPieceTarget();
            }
        }
    }
}
