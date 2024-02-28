using marioRPGMinigame.Essentials;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace marioRPGMinigame.Menus
{
    public class MainMenu: Essentials.Menu
    {

        public MainMenu(Vector2 origin): base(origin)
        {
            buttons.Add(new MenuItem(new Vector2(10, 10), this, 40, 20, 0));
            buttons.Add(new MenuItem(new Vector2(10, 60), this, 40, 20, 0));
        }

        
        public override void Update()
        {
            
            if (buttons[0].pressed == true)
            {
                Game1.changeState("mainGame");
                buttons[0].body.color = Color.Red;
            }
            else
            {
                buttons[0].body.color = Color.Black;
            }
            
            if (buttons[1].pressed == true)
            {
                
                buttons[1].body.color = Color.Blue;
            }
            else
            {
                buttons[1].body.color = Color.Black;
            }



            base.Update();
        }

    }
}
