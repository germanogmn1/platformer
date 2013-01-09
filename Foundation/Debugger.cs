using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Foundation
{
    public class Debugger : GameComponent
    {
        private static DebuggerForm form;

        public static void Debug(string key, object value)
        {
            form.Debug(key, value.ToString());
        }

        public Debugger(Game game) : base(game)
        {
            form = new DebuggerForm();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHelper.IsKeyPressed(Keys.F12))
            {
                if (form.Visible)
                    form.Hide();
                else
                    form.Show();
            }
        }
    }
}
