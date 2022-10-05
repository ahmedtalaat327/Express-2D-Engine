using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressedEngine.ExpressedEngine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
      
        public string Tag = "";
        public Bitmap spriteBitmap = null;
        public Sprite2D(Vector2 position, Vector2 scale, Bitmap btm,string tag)
        {
            this.Position = position;
            this.Scale = scale;
            
            this.Tag = tag;

            this.spriteBitmap = btm;
            

            ExpressedEngine.RegisterSprite(this);
            Log.Info($"[Sprite2D]({Tag}) - Has Been Registered..");
        }
        public void DestroySelf()
        {

            ExpressedEngine.UnRegisterSprite(this);
            Log.Info($"[Sprite2D]({Tag}) - Has Been Unregistered..");
        }
        public void ReUpdateSprite2D(string newDirectory)
        {
            Image spriteImage = null;
            try
            {
                spriteImage = Image.FromFile($"Assets/Sprites/{newDirectory}.png");
            }
            catch (Exception imgNotFound)
            {
                Log.Error($"{imgNotFound.StackTrace}");
            }
            finally
            {
                spriteBitmap = new Bitmap(spriteImage);
            }
        }

        public void ReUpdateSprite2D(Bitmap bitmap)
        {
          
            
                spriteBitmap = bitmap;
            
        }

        public Sprite2D IsColliding(Sprite2D ASprite,Sprite2D BSprite)
        {
             
            if (ASprite.Position.X < BSprite.Position.X + BSprite.Scale.X &&
                ASprite.Position.X + ASprite.Scale.X > BSprite.Position.X &&
                ASprite.Position.Y < BSprite.Position.Y + BSprite.Scale.Y &&
                ASprite.Position.Y + ASprite.Scale.Y > BSprite.Position.Y )
            {
                return BSprite;
            }

            return null;

        }
        public Sprite2D IsColliding(string tag)
        {
             
            foreach(Sprite2D BSprite in ExpressedEngine.AllSprites)
            {
                if (BSprite.Tag == tag)
                {
                    if (this.Position.X < BSprite.Position.X + BSprite.Scale.X &&
                                   this.Position.X + this.Scale.X > BSprite.Position.X &&
                                   this.Position.Y < BSprite.Position.Y + BSprite.Scale.Y &&
                                   this.Position.Y + this.Scale.Y > BSprite.Position.Y)
                    {
                        return BSprite;
                    }
                }
            }
            return null;
        }
    }
}
