

namespace ExpressedEngine.ExpressedEngine
{

    /// <summary>
    /// Convert X,Y values to Victors
    /// </summary>
    public class Vector2
    {
        #region Proberties
        /// <summary>
        /// X for Vector ..
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// Y for Vector ..
        /// </summary>
        public float Y { get; set; }
        #endregion
        #region Init
        /// <summary>
        /// Defult Constructor
        /// </summary>
        public Vector2()
        {
            Vector2 vector2Def = Zero();
            this.X = vector2Def.X; this.Y = vector2Def.Y;
        }
        /// <summary>
        /// Predefined Constructor by User
        /// </summary>
        /// <param name="xValue">X value</param>
        /// <param name="yValue">Y Value</param>
        public Vector2(float xValue,float yValue)
        {
            this.X = xValue; this.Y = yValue;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Vector has an origin value (0,0)
        /// </summary>
        public static Vector2 Zero()
        {
            return new Vector2(0, 0);
        }
        #endregion
    }

}
