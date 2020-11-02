using Pieces.Enum;

namespace Pieces
{
    /// <summary>
    /// Represents the Queen in chess.
    /// </summary>
    sealed class Queen : Piece
    {
        sealed protected override string Symbol => "Q";

        /// <summary>
        /// Contructor for a new <see cref="Queen"/>./>
        /// </summary>
        /// <param name="team"><inheritdoc/></param>
        public Queen(Team team)
            : base(team) { }
    }
}
