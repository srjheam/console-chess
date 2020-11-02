using Pieces.Enum;

namespace Pieces
{
    /// <summary>
    /// Represents the Bishop in chess.
    /// </summary>
    sealed class Bishop : Piece
    {
        sealed protected override string Symbol => "B";

        /// <summary>
        /// Contructor for a new <see cref="Bishop"/>./>
        /// </summary>
        /// <param name="team"><inheritdoc/></param>
        public Bishop(Team team)
            : base(team) { }
    }
}
