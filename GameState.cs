using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoDino {
    public abstract class GameState {

        public abstract void LoadContent();

        public abstract void Update(GameTime dt);

        public abstract void Draw(SpriteBatch b);
    }
}