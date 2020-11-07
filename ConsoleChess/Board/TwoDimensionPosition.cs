namespace Board
{
    /// <summary>
    /// Represents a position in a two-dimensional space, such as a 2D matrix.
    /// </summary>
    struct TwoDimensionPosition
    {
        /// <value>The x-coordinate.</value>
        public int X;
        /// <value>The y-coordinate.</value>
        public int Y;

        /// <summary>
        /// Intantiates a new TwoDimensionPosition.
        /// </summary>
        /// <param name="x">X-coordinate of this position.</param>
        /// <param name="y">Y-coordinate of this position.</param>
        public TwoDimensionPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns the ordered pair of this instance.
        /// </summary>
        /// <returns>Ordered pair of this instance.</returns>
        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
