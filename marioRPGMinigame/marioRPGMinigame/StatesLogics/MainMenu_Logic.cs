using marioRPGMinigame.Essentials;
using marioRPGMinigame.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marioRPGMinigame.StatesLogics
{
    public class MainMenu_Logic: StateLogic
    {

        Menu mainMenu;
        float menuWidth;
        float menuHeight;

        public MainMenu_Logic(float width, float height)
        {
            menuWidth = width;
            menuHeight = height;
        }

        public override void Setup()
        {
            mainMenu = new MainMenu(new Vector2(menuWidth / 2 - 100, menuHeight / 2 - 100));
            Game1.menus.Add(mainMenu);
        }

        public override void Clear()
        {
            Game1.menus.Remove(mainMenu);
            mainMenu = null;
        }

    }
}
