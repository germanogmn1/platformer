using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Foundation.ScreenManager
{
    public class ScreenManager : DrawableGameComponent
    {
        private Dictionary<Type, Screen> screensDict = new Dictionary<Type, Screen>();
        private Screen currentScreen;
        
        public ScreenManager(Game game) : base(game) { }
        
        public void AddScreen(Screen screen)
        {
            Debug.Assert(!screensDict.ContainsKey(screen.GetType()), "ScreenManager can't handle multiple screens of the same type.");
            
            screen.Manager = this;

            screensDict.Add(screen.GetType(), screen);

            if (screensDict.Count == 1)
                EnterScreen(screen);
        }

        public void EnterScreen(Screen newScreen)
        {
            Debug.Assert(screensDict.ContainsValue(newScreen), "ScreenManager don't contains this screen instance. Add the screen before using it.");

            if (currentScreen != null)
                currentScreen.Leave();

            newScreen.Enter();
            currentScreen = newScreen;
        }

        public void EnterScreen<T>() where T : Screen
        {
            EnterScreen(screensDict[typeof(T)]);
        }

        protected override void LoadContent()
        {
            foreach (var scr in screensDict.Values)
                scr.LoadContent(Game.Content);
        }

        public override void Initialize()
        {
            Debug.Assert(screensDict.Count > 0, "Can not initialize empty ScreenManager. Add screens before initialization");
            base.Initialize();

            foreach (var scr in screensDict.Values)
                scr.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (currentScreen != null)
                currentScreen.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (currentScreen != null)
                currentScreen.Draw(gameTime);
        }
    }
}
