using marioRPGMinigame.Essentials;
using marioRPGMinigame.StatesLogics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace marioRPGMinigame.UIs
{
    public class MainUI: UI
    {

        private SpriteFont font;
        private UIItem_text scoreText;


        public MainUI(Vector2 origin, float w, float h) : base(origin, w,h)
        {
            scoreText = new UIItem_text(origin, this, 50, 10, 0, "score");
            items.Add(scoreText);
        }

        public override void Update()
        {
            scoreText.text = "score:" + MainGame_Logic.score.ToString();
            base.Update();
        }


    }
}
