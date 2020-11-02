using Pieces.Enum;

namespace Pieces
{
    /// <summary>
    /// Represents the Pawn in chess.
    /// </summary>
    sealed class Pawn : Piece
    {
        sealed protected override string Symbol => "P";

        /// <summary>
        /// Contructor for a new <see cref="Pawn"/>./>
        /// </summary>
        /// <param name="team"><inheritdoc/></param>
        public Pawn(Team team)
            : base(team) { }
    }
}
