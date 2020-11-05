using Pieces.Enum;

namespace Pieces
{
    /// <summary>
    /// The <c>Piece</c> class represents a generic piece.
    /// </summary>
    abstract class Piece
    {
        /// <summary>
        /// Team of the piece.
        /// </summary>
        /// <value>Gets the team of the piece.</value>
        public readonly Team Team;
        
        /// <summary>
        /// Symbol of the piece on console.
        /// </summary>
        /// <value>Gets the symbol of the piece.</value>
        protected abstract string Symbol { get; }

        /// <summary>
        /// Base contructor for a new generic piece.
        /// </summary>
        /// <param name="team">Team of the piece.</param>
        protected Piece(Team team)
        {
            Team = team;
        }

        /// <summary>
        /// Converts the <c>Piece</c> of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance, consisting of the <see cref="Symbol"/>.</returns>
        sealed public override string ToString()
        {
            return Symbol;
        }
    }
}
