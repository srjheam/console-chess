namespace Extensions
{
    /// <summary>
    /// TODO: DOC
    /// </summary>
    static class BoolExtensions
    {
        /// <summary>
        /// Given two bool[*,*], it returns the intersection between them where each element is the OR operator applied in both arrays elements in the same index.
        /// </summary>
        /// <param name="obj">Original array.</param>
        /// <param name="toMerge">Array to merge.</param>
        /// <returns>A bidimentional array of booleans that is intersection between them where each element is the OR operator applied in both arrays elements in the same index.</returns>
        public static bool[,] Merge(this bool[,] obj, bool[,] toMerge)
        {
            var result = new bool[(obj.GetLength(0) < toMerge.GetLength(0)) ? obj.GetLength(0) : toMerge.GetLength(0), (obj.GetLength(1) < toMerge.GetLength(1)) ? obj.GetLength(1) : toMerge.GetLength(1)];
            
            for (int row = 0; row < result.GetLength(0); row++)
            {
                for (int column = 0; column < result.GetLength(1); column++)
                {
                    result[row, column] = obj[row, column] || toMerge[row, column];
                }
            }

            return result;
        }
    }
}
