using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoDino {
    public class Input {

        private static KeyboardState prevKState;
        private static KeyboardState currentKState;

        private static MouseState prevMState;
        private static MouseState currentMState;

        private static Rectangle mouseRectangle;

        public static Rectangle getMouseRectangle() => Input.mouseRectangle;

        public static bool IsKeyDown(Keys k) => Input.prevKState.IsKeyUp(k) && Input.currentKState.IsKeyDown(k);

        public static bool IsKeyHold(Keys k) => Input.currentKState.IsKeyDown(k);

        public static bool IsLeftClickDown() => Input.prevMState.LeftButton == ButtonState.Released && Input.currentMState.LeftButton == ButtonState.Pressed;

        public static void Update() {

            Input.prevKState = Input.currentKState;
            Input.currentKState = Keyboard.GetState();

            Input.prevMState = Input.currentMState;
            Input.currentMState = Mouse.GetState();

            Input.mouseRectangle = new Rectangle(Input.currentMState.X, Input.currentMState.Y, 1, 1);
        }
    }
}