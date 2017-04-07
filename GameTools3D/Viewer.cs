using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using GameTools3D.Formats;

namespace GameTools3D {
    public class Viewer : GameWindow {

        int screenWidth = 0, screenHeight = 0, virtualWidth = 1920, virtualHeight = 1080;
        float targetAspectRatio;
        Camera Camera;

        private Model objX = null;

        public Viewer(int width, int height) : base(width, height, OpenTK.Graphics.GraphicsMode.Default, "GT-Viewer")
        {
            screenWidth = width; // Doesn't really matter, gets changed later anyway
            screenHeight = height;

            Camera = new Camera(this, new Vector3(0f, 0f, 0f));

            /*
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 1.0f, 1.0f, -0.5f });
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.3f, 0.3f, 0.3f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.LightModel(LightModelParameter.LightModelAmbient, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
            GL.LightModel(LightModelParameter.LightModelTwoSide, 1);
            GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            */

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal); //Or LessEqual

            //GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Input.Initialize(this);
            //this.KeyPress += HandleKeyPress;
            this.KeyDown += HandleKeyDown;
        }

        public void LoadModel(Model objX) {
            objX.Write3DS();
            foreach(Texture texture in objX.textures) {
                texture.LoadToOpenGL();
            }

            this.objX = objX;
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e) {
            base.OnUpdateFrame(e);

            if (Input.MousePress(OpenTK.Input.MouseButton.Left)) {
                //Console.WriteLine("Click " + Mouse.X + " " + Mouse.Y);
                //Vector2 pos = new Vector2(Mouse.X, Mouse.Y) - new Vector2(this.Width, this.Height) / 2f;
                //pos = view.ToWorld(pos);
            }

            Input.Update();
        }

        float rX = 0;
        float rY = 0;
        int x = 0;
        int y = 0;
        int z = 0;

        bool TextureEnable = true;
        bool WireFrameEnable = true;

        protected override void OnRenderFrame(FrameEventArgs e) {
            base.OnRenderFrame(e);

            GL.ClearColor(Color.SkyBlue);

            //--

            FixBoundingBox();
            float midX = (objX.boundingXmax + objX.boundingXmin) / 2f;
            float midY = (objX.boundingYmax + objX.boundingYmin) / 2f;
            float midZ = (objX.boundingZmax + objX.boundingZmin) / 2f;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadIdentity();

            GL.LoadMatrix(ref Camera.CameraMatrix); 

            /*
            GL.Translate(-midX, -midY, -midZ); //Attempt to center the model
            GL.Translate(x/10f, y / 10f, z / 10f);
            GL.Rotate(rX / 50f, Vector3.UnitX);
            GL.Rotate(rY / 50f, Vector3.UnitY);
            */

            GL.Color3(Color.Blue);

            if (objX != null && objX.meshInfo != null) {
                if (TextureEnable) {
                    GL.Color3(Color.White);
                    foreach (MeshInfo meshInfo in objX.meshInfo) {
                        foreach (Mesh mesh in meshInfo.meshTable) {
                            GL.BindTexture(TextureTarget.Texture2D, mesh.texid + 1);
                            GL.Begin(PrimitiveType.Triangles);
                            //GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
                            foreach(int[] face in mesh.faceData) {
                                if (mesh.normalData.Count > 0)
                                    GL.Normal3(mesh.normalData[face[0]][0], mesh.normalData[face[0]][1], mesh.normalData[face[0]][2]);
                                GL.TexCoord2(mesh.uvData[face[0]][0], mesh.uvData[face[0]][1]);
                                GL.Vertex3(mesh.vertData[face[0]][0], mesh.vertData[face[0]][1], mesh.vertData[face[0]][2]);

                                if (mesh.normalData.Count > 0)
                                    GL.Normal3(mesh.normalData[face[1]][0], mesh.normalData[face[1]][1], mesh.normalData[face[1]][2]);
                                GL.TexCoord2(mesh.uvData[face[1]][0], mesh.uvData[face[1]][1]);
                                GL.Vertex3(mesh.vertData[face[1]][0], mesh.vertData[face[1]][1], mesh.vertData[face[1]][2]);

                                if (mesh.normalData.Count > 0)
                                    GL.Normal3(mesh.normalData[face[2]][0], mesh.normalData[face[2]][1], mesh.normalData[face[2]][2]);
                                GL.TexCoord2(mesh.uvData[face[2]][0], mesh.uvData[face[2]][1]);
                                GL.Vertex3(mesh.vertData[face[2]][0], mesh.vertData[face[2]][1], mesh.vertData[face[2]][2]);
                            }
                            GL.End();
                            GL.BindTexture(TextureTarget.Texture2D, 0);
                        }
                    }
                }

                if (WireFrameEnable) {
                    GL.Color3(Color.Black);
                    foreach (MeshInfo meshInfo in objX.meshInfo) {
                        foreach (Mesh mesh in meshInfo.meshTable) {
                            GL.Begin(PrimitiveType.Lines);
                            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                            GL.PointSize(5f);
                            GL.LineWidth(5f);
                            foreach(int[] face in mesh.faceData) {
                                GL.Vertex3(mesh.vertData[face[0]][0], mesh.vertData[face[0]][1], mesh.vertData[face[0]][2]);
                                GL.Vertex3(mesh.vertData[face[1]][0], mesh.vertData[face[1]][1], mesh.vertData[face[1]][2]);
                                GL.Vertex3(mesh.vertData[face[2]][0], mesh.vertData[face[2]][1], mesh.vertData[face[2]][2]);

                                GL.Vertex3(mesh.vertData[face[0]][0], mesh.vertData[face[0]][1], mesh.vertData[face[0]][2]);
                            }
                            GL.End();
                        }
                    }
                }
            }

            this.SwapBuffers();
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);

            screenWidth = Size.Width;
            screenHeight = Size.Height;

            GL.Viewport(0, 0, screenWidth, screenHeight);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                 (float)Math.PI / 4.0f,
                 screenWidth / (float)screenHeight, 0.01f, 500.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        /*
        static void HandleKeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == '`') {
                GameConsole.Toggle();
            } else if(GameConsole.IsOpen()) {
                GameConsole.OnUpdateFrame(e.KeyChar);
            } else {
                //Send it to the game
            }
        }
        */

        void HandleKeyDown(object sender, OpenTK.Input.KeyboardKeyEventArgs e) {
            if (e.Key == OpenTK.Input.Key.F4) {
                this.Exit();
                Environment.Exit(0);
            } else if (e.Key == OpenTK.Input.Key.Number1)
                TextureEnable = !TextureEnable;
            else if (e.Key == OpenTK.Input.Key.Number2)
                WireFrameEnable = !WireFrameEnable;
            else if (e.Key == OpenTK.Input.Key.Tab)
                Camera.ToggleMouseLock();
            else if (e.Key == OpenTK.Input.Key.R)
                Camera.Reset();
            else if (e.Key == OpenTK.Input.Key.W)
                Camera.MoveForwards();
            else if (e.Key == OpenTK.Input.Key.S)
                Camera.MoveBackwards();
            else if (e.Key == OpenTK.Input.Key.A)
                Camera.MoveLeft();
            else if (e.Key == OpenTK.Input.Key.D)
                Camera.MoveRight();
            else if (e.Key == OpenTK.Input.Key.X)
                Camera.MoveDown();
            else if (e.Key == OpenTK.Input.Key.Space)
                Camera.MoveUp();
            else if (e.Key == OpenTK.Input.Key.Q)
                Camera.TurnLeft();
            else if (e.Key == OpenTK.Input.Key.E)
                Camera.TurnRight();
            else if (e.Key == OpenTK.Input.Key.T)
                Camera.TiltUp();
            else if (e.Key == OpenTK.Input.Key.G)
                Camera.TiltDown();
            else if (e.Key == OpenTK.Input.Key.C)
                Camera.SpeedDown();
            else if (e.Key == OpenTK.Input.Key.V)
                Camera.SpeedUp();
        }

        public void FixBoundingBox() {
            objX.GenBoundingBox();

            float midX = objX.boundingXmax - objX.boundingXmin;
            float midY = objX.boundingYmax - objX.boundingYmin;
            float midZ = objX.boundingZmax - objX.boundingZmin;
        }

    }
}
