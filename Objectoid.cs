using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrigorievC_tema04
{
    internal class Objectoid
    {
        Randomizer  _randomizer = new Randomizer();

        private bool visibility;
        private bool isGravityBound;
        private Color color;
        private int size;

        private List<Vector3> coordList;

        private const int GRAVITY_OFFSET = 1;

        public Objectoid(bool gravity_status)
        {
            // initierea proprietatilor entitatii
            this.visibility = true;
            this.isGravityBound = gravity_status;
            this.color = _randomizer.getRandomColor();
            this.size = _randomizer.GetRandomOffset(2, 10);

            // se genereaza aleatoriu inaltimea si raza de pozitionare a entitatii
            int heightOffset = _randomizer.GetRandomOffset(20, 50);
            int radialOffset = _randomizer.GetRandomOffset(5, 40);

            // lista cu coordonate pentru modelul entitatii
            this.coordList = new List<Vector3>();
            this.coordList.Add(new Vector3(0 + radialOffset, 0 + heightOffset, this.size + radialOffset));
            this.coordList.Add(new Vector3(0 + radialOffset, 0 + heightOffset, 0 + radialOffset));
            this.coordList.Add(new Vector3(this.size + radialOffset, 0 + heightOffset, this.size + radialOffset));
            this.coordList.Add(new Vector3(this.size + radialOffset, 0 + heightOffset, 0 + radialOffset));
            this.coordList.Add(new Vector3(this.size + radialOffset, this.size + heightOffset, this.size + radialOffset));
            this.coordList.Add(new Vector3(this.size + radialOffset, this.size + heightOffset, 0 + radialOffset));
            this.coordList.Add(new Vector3(0 + radialOffset, this.size + heightOffset, this.size + radialOffset));
            this.coordList.Add(new Vector3(0 + radialOffset, this.size + heightOffset, 0 + radialOffset));
            this.coordList.Add(new Vector3(0 + radialOffset, 0 + heightOffset, this.size + radialOffset));
            this.coordList.Add(new Vector3(0 + radialOffset, 0 + heightOffset, 0 + radialOffset));

            // pentru debug se afiseaza pozitia si marimea obiectului in consola
            System.Console.WriteLine($"spawned entity at x={this.coordList.Min(v => v.X)}, y={this.coordList.Min(v => v.Y)}, z={this.coordList.Min(v => v.Z)}, size={this.size}");
        }

        public void Draw()
        {
            if (this.visibility)
            {
                GL.Color3(this.color);

                GL.Begin(PrimitiveType.QuadStrip);
                foreach (Vector3 coord in this.coordList)
                {
                    GL.Vertex3(coord);
                }
                GL.End();
            }
        }

        public void UpdatePosition(bool gravity_status)
        {
            if(this.visibility && gravity_status && !GroundCollisionDetected())
            {
                for(int i = 0; i < this.coordList.Count; i++)
                {
                    this.coordList[i] = new Vector3(this.coordList[i].X, this.coordList[i].Y - GRAVITY_OFFSET, this.coordList[i].Z);
                }
            }
        }

        // metoda pentru detectarea coliziunii intre entitate si grid
        public bool GroundCollisionDetected()
        {
            foreach(Vector3 coord in this.coordList)
            {
                if (coord.Y <= 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void ToggleVisibility()
        {
            this.visibility = !this.visibility;
        }
    }
}
