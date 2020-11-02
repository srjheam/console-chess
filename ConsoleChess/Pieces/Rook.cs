using Pieces.Enum;

namespace Pieces
{
    /// <summary>
    /// Represents the Rook in chess.
    /// </summary>
    sealed class Rook : Piece
    {
        sealed protected override string Symbol => "R";

        /// <summary>
        /// Contructor for a new <see cref="Rook"/>./>
        /// </summary>
        /// <param name="team"><inheritdoc/></param>
        public Rook(Team team)
            : base(team) { }
    }
}
