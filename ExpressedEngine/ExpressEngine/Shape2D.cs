

namespace ExpressedEngine.ExpressedEngine
{
    public class Shape2D
    {

        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Tag = "";

        public Shape2D(Vector2 position,Vector2 scale,string tag)
        {
            this.Position = position;
            this.Scale = scale;
            this.Tag = tag;

            
            ExpressedEngine.RegisterShape(this);
            Log.Info($"[Shape2D]({Tag}) - Has Been Registered..");
        }
        public void DestroySelf()
        {
            
            ExpressedEngine.UnRegisterShape(this);
            Log.Info($"[Shape2D]({Tag}) - Has Been Unregistered..");
        }
    }
}
