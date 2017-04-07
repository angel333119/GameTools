using System.Drawing;

namespace GameTools3D.Formats {
    public abstract class Texture {

        protected string file;
        protected string safefilename;
        protected int width, height;
        protected Bitmap bitmap;
        protected int textureID;

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public Bitmap Bitmap { get { return bitmap; } }
        public int TextureID { get { return textureID; } }
        public string FileID { get { return safefilename.Split('.')[0]; } }

        public abstract void SavePNG(string path = "");

        public void LoadToOpenGL() {
            textureID = ContentPipe.LoadTexture(bitmap);
        }
    }
}
