

using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;

namespace ExpressedEngine.ExpressedEngine
{

    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Canvas));
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.ClientSize = new Size(284, 261);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Canvas";
            this.ResumeLayout(false);

        }
    }

    public abstract class ExpressedEngine
    {
        
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title="No Title";
        private Canvas Window = null;
        private Thread GameLoopThread = null;

        public Color BacKgroundColour = Color.Beige;

        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0.0f;
        public Vector2 CameraScale = new Vector2(1, 1);

        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();
        public ExpressedEngine(Vector2 screensize,string titlewind)
        {

            Log.Info("Engine Started..");
            this.ScreenSize = screensize;
            this.Title = titlewind;
            this.Window = new Canvas();
            this.Window.Size = new Size((int)this.ScreenSize.X,(int)this.ScreenSize.Y);
            this.Window.Text = this.Title ;
            //Add a Redraw Method for first time only but not work now only if window visible
            this.Window.Paint += Renderer;
            this.Window.KeyDown += Window_KeyDown;
            this.Window.KeyUp += Window_KeyUp;
            this.Window.FormClosing += Window_FormClosing;
            //bug when windows move no solution untill now!
            //Looping redrawing in thread
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            Application.Run(Window);
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameLoopThread.Abort();
            
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        void GameLoop() {

            //Load all reaources first
            OnLoad();

            while (GameLoopThread.IsAlive)
            {

                try
                {
                    //Draw new shapes
                    OnDraw();

                    //redraw here but we don't have graphics object so 
                    //we will search for the method responsible for refresh inside it paint method
                    this.Window.BeginInvoke((MethodInvoker)delegate { this.Window.Refresh(); });
                    //Adding physics, moveming 
                    OnUpdate();

                    Thread.Sleep(1);
                }
                catch
                {
                    Log.Error("Window Not Found!.. Waiting..");
                }
            }
        
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BacKgroundColour);


            g.RotateTransform(CameraAngle);
            g.TranslateTransform(CameraPosition.X, CameraPosition.Y);
            g.ScaleTransform(CameraScale.X,CameraScale.Y);

            try
            {
                foreach (Shape2D sh in AllShapes)
                {
                    g.FillRectangle(new SolidBrush(Color.Red), sh.Position.X, sh.Position.Y, sh.Scale.X, sh.Scale.Y);
                }
                foreach (Sprite2D sp in AllSprites)
                {
                    g.DrawImage(sp.spriteBitmap, sp.Position.X, sp.Position.Y, sp.Scale.X, sp.Scale.Y);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// Load all reaources first 
        /// </summary>
        public abstract void OnLoad();
        /// <summary>
        /// Adding physics, moveming 
        /// </summary>
        public abstract void OnUpdate();
        /// <summary>
        /// Draw new shapes
        /// </summary>
        public abstract void OnDraw();
        /// <summary>
        /// Add Shape to current list of shapes to be drawn
        /// </summary>
        /// <param name="shape2D">Current Shape2D</param>
        public static void RegisterShape(Shape2D shape2D)
        {
            ExpressedEngine.AllShapes.Add(shape2D);
        }
        /// <summary>
        /// Remove Shape from current list of shapes to be drawn
        /// </summary>
        /// <param name="shape2D">Current Shape2D</param>
        public static void UnRegisterShape(Shape2D shape2D)
        {
            ExpressedEngine.AllShapes.Remove(shape2D);
        }
        /// <summary>
        /// Add Sprite to current list of shapes to be drawn
        /// </summary>
        /// <param name="sprite2D">Current Sprite2D</param>
        public static void RegisterSprite(Sprite2D sprite2D)
        {
            ExpressedEngine.AllSprites.Add(sprite2D);
        }
        /// <summary>
        /// Remove Sprite from current list of shapes to be drawn
        /// </summary>
        /// <param name="sprite2D">Current Sprite2D</param>
        public static void UnRegisterSprite(Sprite2D sprite2D)
        {
            ExpressedEngine.AllSprites.Remove(sprite2D);
        }
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);
    }
}
