using GrigorievC_tema04;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK_Template
{
    internal class Window3D : GameWindow
    {
        Axes axes;
        Camera3DIsometric camera;

        KeyboardState previousKeyboard;
        MouseState previousMouse;

        Grid grid;

        List<Objectoid> rainOfObjects;
        bool GRAVITY = true;

        public Window3D() : base(800, 600, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            grid = new Grid();
            axes = new Axes();
            camera = new Camera3DIsometric(80, 80, 80, 0, 0, 0, 0, 1, 0);

            rainOfObjects = new List<Objectoid>();

            displayHelp();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.FromArgb(49, 50, 51));

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, this.Width, this.Height);

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)Width / (float)Height, 1, 256);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState thisKeyboard = Keyboard.GetState();
            MouseState thisMouse = Mouse.GetState();

            if (thisKeyboard[Key.Escape])
            {
                Exit();
            }

            if (thisKeyboard[Key.A])
            {
                camera.MoveLeft();
            }

            if (thisKeyboard[Key.D])
            {
                camera.MoveRight();
            }

            if (thisKeyboard[Key.W])
            {
                camera.MoveForward();
            }

            if (thisKeyboard[Key.S])
            {
                camera.MoveBackward();
            }

            if (thisKeyboard[Key.ShiftLeft])
            {
                camera.MoveDown();
            }

            if (thisKeyboard[Key.Space])
            {
                camera.MoveUp();
            }

            if (thisMouse[MouseButton.Left] && !previousMouse[MouseButton.Left])
            {
                rainOfObjects.Add(new Objectoid(GRAVITY));
            }

            if (thisMouse[MouseButton.Right] && !previousMouse[MouseButton.Right])
            {
                rainOfObjects.Clear();
            }

            if (thisKeyboard[Key.G] && !previousKeyboard[Key.G])
            {
                GRAVITY = !GRAVITY;
            }

            if (thisKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                Console.Clear();
                displayHelp();
            }

            previousKeyboard = thisKeyboard;
            previousMouse = thisMouse;
        }

        // metoda pentru afisarea meniului de ajutor
        public void displayHelp()
        {
            System.Console.WriteLine("Help menu :");
            System.Console.WriteLine("H - display help menu;");
            System.Console.WriteLine("ESC - close scene");
            System.Console.WriteLine("WASD - moving around scene;");
            System.Console.WriteLine("SHIFT - moving down;");
            System.Console.WriteLine("SPACE - moving up;");
            System.Console.WriteLine("LEFT CLICK - spawn a objectoid;");
            System.Console.WriteLine("RIGHT CLICK - reset scene;");
            System.Console.WriteLine("G - toggle gravity;");
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            camera.SetCamera();
            grid.Draw();
            axes.Draw();

            foreach(Objectoid obj in rainOfObjects)
            {
                obj.Draw();
                obj.UpdatePosition(GRAVITY);
            }

            SwapBuffers();
        }
    }
}