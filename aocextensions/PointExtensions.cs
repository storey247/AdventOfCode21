using System.Drawing;

namespace aocextensions;

public static class PointExtensions
{
    /// <summary>
        /// Get all neighbours from current point including diagonals
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point[] GetAllNeighbours(this Point point)
        {
            return new Point[8]
            {
                new(point.X - 1, point.Y),
                new(point.X + 1, point.Y),
                new(point.X, point.Y - 1),
                new(point.X, point.Y + 1),
                new(point.X - 1, point.Y - 1),
                new(point.X + 1, point.Y + 1),
                new(point.X - 1, point.Y + 1),
                new(point.X + 1, point.Y - 1),
            };
        }
        
        /// <summary>
        /// Get direct neighbours from current point excluding diagonals
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Point[] GetNeighbours(this Point point)
        {
            return new Point[4]
            {
                new(point.X - 1, point.Y),
                new(point.X + 1, point.Y),
                new(point.X, point.Y - 1),
                new(point.X, point.Y + 1)
            };
        }

        /// <summary>
        /// Get all neighbour points, excluding diagonals, where x and y are valid dimensions 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        public static Point[] GetValidNeighbours(this Point point, int maxX, int maxY)
        {
            return point.GetNeighbours().Where(x => x.X >= 0 
                                         && x.Y >= 0 
                                         && x.X < maxX
                                         && x.Y < maxY).ToArray();
        }
        
        /// <summary>
        /// Get all neighbour points, including diagonals, where x and y are valid dimensions 
        /// </summary>
        /// <param name="point"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        public static Point[] GetAllValidNeighbours(this Point point, int maxX, int maxY)
        {
            return point.GetAllNeighbours().Where(x => x.X >= 0 
                                                    && x.Y >= 0 
                                                    && x.X < maxX
                                                    && x.Y < maxY).ToArray();
        }
}