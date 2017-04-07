using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameTools3D {
    class Camera {

        public Vector3 Position;
        public float X {
            get { return Position.X; }
            set { Position.X = value; }
        }
        public float Y {
            get { return Position.Y; }
            set { Position.Y = value; }
        }
        public float Z {
            get { return Position.Z; }
            set { Position.Z = value; }
        }
        public Vector3 Up = Vector3.UnitY;
        public Matrix4 CameraMatrix = Matrix4.Identity;
        public float Pitch = 0;
        public float Facing = 0;
        public float HorizontalSensitivity = 3;
        public float VerticalSensitivity = 6;
        public float Fog = 10000;
        public Point ScreenCenter { get { return new Point(viewer.Bounds.Left + (viewer.Bounds.Width / 2), viewer.Bounds.Top + (viewer.Bounds.Height / 2)); } }
        public Point WindowCenter { get { return new Point(viewer.Width / 2, viewer.Height / 2); } }
        public Point MouseDelta { get; private set; }
        private Viewer viewer;

        public Camera() { }
        public Camera(Viewer window, float x, float y, float z) : this(window, new Vector3(x, y, z)) { }
        public Camera(Viewer window, Vector3 position) : this(window, position, Vector3.UnitY) { }
        public Camera(Viewer window, Vector3 position, Vector3 up) {
            viewer = window;
            Position = position;
            Up = up;

            MouseDelta = new Point();

            viewer.Resize += HandleResize;
            viewer.UpdateFrame += HandleUpdateFrame;
        }

        private float MoveVelocity = 0.1f;

        public void MoveForwards() {
            X += (float)Math.Cos(Facing) * MoveVelocity;
            Z += (float)Math.Sin(Facing) * MoveVelocity;
            Y += (float)Math.Sin(Pitch) * MoveVelocity;
        }

        public void MoveBackwards() {
            X -= (float)Math.Cos(Facing) * MoveVelocity;
            Z -= (float)Math.Sin(Facing) * MoveVelocity;
            Y -= (float)Math.Sin(Pitch) * MoveVelocity;
        }

        public void MoveLeft() {
            X -= (float)Math.Cos(Facing + Math.PI / 2) * MoveVelocity;
            Z -= (float)Math.Sin(Facing + Math.PI / 2) * MoveVelocity;
        }

        public void MoveRight() {
            X += (float)Math.Cos(Facing + Math.PI / 2) * MoveVelocity;
            Z += (float)Math.Sin(Facing + Math.PI / 2) * MoveVelocity;
        }

        public void TurnLeft() {
            Facing -= 0.1f;
        }

        public void TurnRight() {
            Facing += 0.1f;
        }

        public void TiltUp() {
            Pitch += 0.1f;
        }

        public void TiltDown() {
            Pitch -= 0.1f;
        }

        public void MoveUp() {
            Y += MoveVelocity;
        }

        public void MoveDown() {
            Y -= MoveVelocity;
        }

        public void SpeedUp() {
            MoveVelocity += 0.1f;
        }
        public void SpeedDown() {
            if(MoveVelocity > 0.1f)
                MoveVelocity -= 0.1f;
        }

        public void Reset() {
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
            MoveVelocity = 0.1f;
        }

        bool mouseLock = false;
        public void ToggleMouseLock() {
            mouseLock = !mouseLock;

            if(mouseLock) {
                viewer.UpdateFrame += HandleUpdateFrameMouse;
                Cursor.Hide();
            } else {
                viewer.UpdateFrame -= HandleUpdateFrameMouse;
                Cursor.Show();
            }
        }
        
        //---

        private void HandleResize(object sender, EventArgs a) {
            Cursor.Position = ScreenCenter;

            GL.Viewport(viewer.ClientRectangle.X, viewer.ClientRectangle.Y, viewer.ClientRectangle.Width, viewer.ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, viewer.Width / (float)viewer.Height, 1f, Fog);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        private void HandleUpdateFrame(object sender, EventArgs a) {
            if (Pitch < -1.5f) Pitch = -1.5f;
            if (Pitch > 1.5f) Pitch = 1.5f;
            Vector3 lookatPoint = new Vector3((float)Math.Cos(Facing), (float)Math.Tan(Pitch), (float)Math.Sin(Facing));
            CameraMatrix = Matrix4.LookAt(Position, Position + lookatPoint, Up);
        }

        private void HandleUpdateFrameMouse(object sender, EventArgs a) {
            MouseDelta = new Point(viewer.Mouse.X - WindowCenter.X, viewer.Mouse.Y - WindowCenter.Y);
            Point p = Cursor.Position;
            p.X -= MouseDelta.X;
            p.Y -= MouseDelta.Y;
            Cursor.Position = p;
            Facing += MouseDelta.X / (1000 - (float)(HorizontalSensitivity * 100));
            Pitch -= MouseDelta.Y / (1000 - (float)(VerticalSensitivity * 100));
        }
    }
}