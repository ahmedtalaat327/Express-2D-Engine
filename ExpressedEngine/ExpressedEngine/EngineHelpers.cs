using System;
using System.Drawing;

namespace ExpressedEngine.ExpressedEngine
{
    public  class EngineHelpers
    {
        private static Bitmap spriteBitmap = null;
        private static Image spriteImage = null;

        public static Bitmap LoadImageInMemory(string directory)
        {
         
            try
            {
                spriteImage = Image.FromFile($"{directory}");
               
            }
            catch (Exception imgNotFound)
            {
                Log.Error($"{imgNotFound.StackTrace}");
                
            }
            finally
            {
                spriteBitmap = new Bitmap(spriteImage);
              
            }
            return spriteBitmap;
        
        }
    }
}
