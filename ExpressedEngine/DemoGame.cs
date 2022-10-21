using ExpressedEngine.ExpressedEngine;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ExpressedEngine
{

    class DemoGame : ExpressedEngine.ExpressedEngine
    {
        readonly string[,] Map =
        {
            {"g","g","g","g","g","g","b","g",".","g","g","g","g","g","b","g",".","g"},
            {"g","c","g","c",".",".","g","c",".","c","g","c",".",".","g","c",".","g"},
            {"g",".",".",".","E","c","c",".","c",".",".",".","E","c","c",".","c","g"},
            {"g",".","P",".",".",".",".",".","g",".","g",".",".",".",".",".","g","g"},
            {"g",".",".",".","b","g",".",".",".",".",".",".","b","g",".",".",".","g"},
            {"g","g","g","E",".",".",".",".","c","g","g","E",".",".",".",".","c","g"},
            {"g","c",".",".",".","E","g","c",".","c",".",".",".","E","g","c",".","g"},
            {"g","g","g","g","g","g","g","b","g","g","g","g","g","g","g","b","g","g"}
        };
        bool UpDirection, DwnDirection, LeftDirection, RightDirecton;
        int HorizIncrementer, VerticIncrementer;
        Sprite2D Player = null;
         

        Vector2 LastPositon = Vector2.Zero();
        float StepSize = 64/10f;
       


        //Player senarios..
        //Def-state
        Bitmap Player_Ref = EngineHelpers.LoadImageInMemory("Assets/Sprites/Player/player_11.png");
        //Right-state-step1
        Bitmap Player_Ref_Right_Step1 = EngineHelpers.LoadImageInMemory("Assets/Sprites/Player/player_11.png");
        //Right-state-step2
        Bitmap Player_Ref_Right_Step2 = EngineHelpers.LoadImageInMemory("Assets/Sprites/Player/player_12.png");
        //Left-state-step1
        Bitmap Player_Ref_Left_Step1 = EngineHelpers.LoadImageInMemory("Assets/Sprites/Player/player_14.png");
        //Left-state-step2
        Bitmap Player_Ref_Left_Step2 = EngineHelpers.LoadImageInMemory("Assets/Sprites/Player/player_15.png");
        //Up-state-step1
        Bitmap Player_Ref_Up_Step1 = EngineHelpers.LoadImageInMemory("Assets/Sprites/Player/player_03.png");
        //Up-state-step2
        Bitmap Player_Ref_Up_Step2 = EngineHelpers.LoadImageInMemory("Assets/Sprites/Player/player_04.png");
        //Dwn-state-step1
        Bitmap Player_Ref_Dwn_Step1 = EngineHelpers.LoadImageInMemory("Assets/Sprites/Player/player_06.png");
        //Dwn-state-step2
        Bitmap Player_Ref_Dwn_Step2 = EngineHelpers.LoadImageInMemory("Assets/Sprites/Player/player_07.png");




        // This constructor will call BaseClass.BaseClass(int i)
        public DemoGame() : base(new Vector2(1300,700),"Express Engine - Demo")
        {
        }

        public override void OnLoad()
        {
            Log.Info("ONLOAD::All Resources::Sprites Game Objects UI any thing you need to load before Engine start");
            //Load images..
            //Assets/Sprites/{directory}.png
            Bitmap SolidBlocks_Ref = EngineHelpers.LoadImageInMemory("Assets/Sprites/Blocks/block_06.png");
            Bitmap SemSolidBlocks_Ref = EngineHelpers.LoadImageInMemory("Assets/Sprites/Blocks/block_05.png");
            Bitmap Ground_Ref = EngineHelpers.LoadImageInMemory("Assets/Sprites/Ground/ground_01.png");
            Bitmap EnemyBomb_Ref = EngineHelpers.LoadImageInMemory("Assets/Sprites/Enemies/bomb.png");
            Bitmap CoinGold_Ref = EngineHelpers.LoadImageInMemory("Assets/Sprites/Environment/environment_12.png");
           

            Log.Info($"Engine Started With Map :Row {Map.GetLength(0)} & Column {Map.GetLength(1)}");
            this.BacKgroundColour = Color.DarkSlateGray;
            Vector2 PlayerPostion = null;
       
        
            for (int i = 0; i < Map.GetLength(0); i++)
               
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == "g")
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(64, 64), SolidBlocks_Ref, "SolidBlock");
                    else if (Map[i, j] == "b")
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(64, 64), SemSolidBlocks_Ref, "SemiSolidBlock");
                    else if (Map[i, j] == "P")
                    {
                        PlayerPostion = new Vector2(j * 64, i * 64);
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(64, 64), Ground_Ref, "Ground");
                    }
                    else if (Map[i, j] == "E")
                    {
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(64, 64), Ground_Ref, "Ground");
                        new Sprite2D(new Vector2(j * 64+64/4, i * 64+64/4), new Vector2(64/2, 64/2), EnemyBomb_Ref, "EnemyBomb");
                    }
                    else if (Map[i, j] == ".")
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(64, 64), Ground_Ref, "Ground");
                    else if (Map[i, j] == "c")
                    {
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(64, 64), Ground_Ref, "Ground");
                        new Sprite2D(new Vector2(j * 64, i * 64), new Vector2(64, 64), CoinGold_Ref, "CoinGold");
                    }

                }
            }
            Player = new Sprite2D(PlayerPostion, new Vector2(64-StepSize, 64-StepSize), Player_Ref, "Player");

        }

        public override void OnDraw()
        {
             
        }
        
        public override void OnUpdate()
        {

            if (UpDirection)
            {
                VerticIncrementer++;
                Player.Position.Y -= StepSize;
                if (VerticIncrementer % 2 == 0)
                {
                    Player.ReUpdateSprite2D(Player_Ref_Up_Step1);
                }
                else
                {
                    Player.ReUpdateSprite2D(Player_Ref_Up_Step2);
                }
                UpDirection = false;
            }
            if (DwnDirection)
            {
                VerticIncrementer--;
                Player.Position.Y += StepSize;
                if (VerticIncrementer % 2 == 0)
                {
                    Player.ReUpdateSprite2D(Player_Ref_Dwn_Step1);
                }
                else
                {
                    Player.ReUpdateSprite2D(Player_Ref_Dwn_Step2);
                }
                DwnDirection = false;
            }
            if (RightDirecton)
            {
                HorizIncrementer++;
                Player.Position.X += StepSize;
                if (HorizIncrementer % 2 == 0)
                {
                    Player.ReUpdateSprite2D(Player_Ref_Right_Step1);
                }
                else
                {
                    Player.ReUpdateSprite2D(Player_Ref_Right_Step2);
                }
                RightDirecton = false;
            }
            if (LeftDirection)
            {
                HorizIncrementer--;
                Player.Position.X -= StepSize;
                if (HorizIncrementer % 2 == 0)
                {
                    Player.ReUpdateSprite2D(Player_Ref_Left_Step1);
                }
                else
                {
                    Player.ReUpdateSprite2D(Player_Ref_Left_Step2);
                }
                LeftDirection = false;
            }


            Sprite2D coin = Player.IsColliding("CoinGold");
            Sprite2D bomb = Player.IsColliding("EnemyBomb");
            if (coin != null)
            {
                Log.Info("Player is colliding Coin!");
                coin.DestroySelf();
            }
          
            if (bomb != null)
            {
                Log.Info("Player is colliding with Bomb!, You Dead");
               // MessageBox.Show("Game Over","You dead");

            }
            Sprite2D solidblock = Player.IsColliding("SolidBlock");
            if (solidblock != null)
            {
                Log.Info("Player is colliding with SolidBlcok!");
                Player.Position.X = LastPositon.X;
                Player.Position.Y = LastPositon.Y;
            }
            else
            {
               
              //  Log.Info("Player is not colliding to SolidBlcok!");
                LastPositon.X = Player.Position.X;
                LastPositon.Y = Player.Position.Y;
            }
            
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W|| e.KeyCode == Keys.Up)
            {
                Player.ReUpdateSprite2D("/Player/player_02");
                UpDirection = true;
            }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                Player.ReUpdateSprite2D("/Player/player_05");
                DwnDirection = true;
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                Player.ReUpdateSprite2D("/Player/player_14");
                LeftDirection = true;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                Player.ReUpdateSprite2D("/Player/player_11");
                RightDirecton = true;
            }

        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                UpDirection = false;
            if (e.KeyCode == Keys.S)
                DwnDirection = false;
            if (e.KeyCode == Keys.A)
                LeftDirection = false;
            if (e.KeyCode == Keys.D)
                RightDirecton = false;
        }
    }
}
