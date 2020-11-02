using Pieces.Enum;

namespace Pieces
{
    /// <summary>
    /// Represents the King in chess.
    /// </summary>
    sealed class King : Piece
    {
        sealed protected override string Symbol => "K";

        /// <summary>
        /// Contructor for a new <see cref="King"/>./>
        /// </summary>
        /// <param name="team"><inheritdoc/></param>
        public King(Team team)
            : base(team) { }
    }
}
