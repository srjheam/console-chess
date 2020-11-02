using Pieces.Enum;

namespace Pieces
{
    /// <summary>
    /// Represents the Knight in chess.
    /// </summary>
    sealed class Knight : Piece
    {
        sealed protected override string Symbol => "N";

        /// <summary>
        /// Contructor for a new <see cref="Knight"/>./>
        /// </summary>
        /// <param name="team"><inheritdoc/></param>
        public Knight(Team team)
            : base(team) { }
    }
}
