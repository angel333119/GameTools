using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GameTools3D.Formats {
    public class ColladaExporter {

        // Borrows a lot from: https://github.com/xdanieldzd/N3DSCmbViewer/blob/master/N3DSCmbViewer/Cmb/ExportCollada.cs

        private Model model;
        private static bool enableNormals = false;

        public ColladaExporter(Model model) {
            this.model = model;

            XmlTextWriter xw = new XmlTextWriter("GT-TS2-Export/" + model.GetSafeFileName + ".dae", Encoding.UTF8);
            xw.Formatting = Formatting.Indented;
            xw.Indentation = 2;
            xw.WriteStartDocument(false);
            xw.WriteStartElement("COLLADA");
            xw.WriteAttributeString("xmlns", "http://www.collada.org/2005/11/COLLADASchema");
            xw.WriteAttributeString("version", "1.4.1");
            {
                SectionAsset(xw);
                SectionLibraryTextures(xw);
                SectionLibraryGeometry(xw);
                SectionScene(xw);
            }
            xw.WriteEndElement();

            xw.Close();
        }

        private void SectionLibraryTextures(XmlTextWriter xw) {
            #region Images
            xw.WriteStartElement("library_images");
            {
                foreach (Texture tex in model.textures) {
                    tex.SavePNG("GT-TS2-Export/Textures/");

                    string texId = "image-" + tex.FileID;
                    xw.WriteStartElement("image");
                    xw.WriteAttributeString("id", texId);
                    {
                        xw.WriteStartElement("init_from");
                        xw.WriteString("Textures/" + tex.FileID + ".png");
                        xw.WriteEndElement();
                    }
                    xw.WriteEndElement();
                }
            }
            xw.WriteEndElement();
            #endregion

            #region Effects
            xw.WriteStartElement("library_effects");
            {
                string defaultID = "effect-default";

                xw.WriteStartElement("effect");
                xw.WriteAttributeString("id", defaultID);
                xw.WriteAttributeString("name", defaultID);
                {
                    xw.WriteStartElement("profile_COMMON");
                    {
                        xw.WriteStartElement("technique");
                        xw.WriteAttributeString("sid", "COMMON");
                        {
                            xw.WriteStartElement("phong");
                            {
                                xw.WriteStartElement("diffuse");
                                {
                                    xw.WriteStartElement("color");
                                    xw.WriteString("1.0 1.0 1.0 1.0");
                                    xw.WriteEndElement();
                                }
                                xw.WriteEndElement();
                            }
                            xw.WriteEndElement();
                        }
                        xw.WriteEndElement();
                    }
                    xw.WriteEndElement();
                }
                xw.WriteEndElement();

                foreach (Texture tex in model.textures) {
                    string effectID = "effect-" + tex.FileID;

                    xw.WriteStartElement("effect");
                    xw.WriteAttributeString("id", effectID);
                    xw.WriteAttributeString("name", effectID);
                    {
                        xw.WriteStartElement("profile_COMMON");
                        {
                            xw.WriteStartElement("newparam");
                            xw.WriteAttributeString("sid", "surface-" + tex.FileID);
                            {
                                xw.WriteStartElement("surface");
                                xw.WriteAttributeString("type", "2D");
                                {
                                    xw.WriteStartElement("init_from");
                                    xw.WriteString("image-" + tex.FileID);
                                    xw.WriteEndElement();
                                }
                                xw.WriteEndElement();
                            }
                            xw.WriteEndElement();

                            xw.WriteStartElement("newparam");
                            xw.WriteAttributeString("sid", "sampler-" + tex.FileID);
                            {
                                xw.WriteStartElement("sampler2D");
                                {
                                    xw.WriteElementString("source", "surface-" + tex.FileID);
                                    xw.WriteElementString("wrap_s", "WRAP");
                                    xw.WriteElementString("wrap_t", "WRAP");
                                    xw.WriteElementString("minfilter", "LINEAR");
                                    xw.WriteElementString("magfilter", "LINEAR");
                                }
                                xw.WriteEndElement();
                            }
                            xw.WriteEndElement();

                            xw.WriteStartElement("technique");
                            xw.WriteAttributeString("sid", "COMMON");
                            {
                                xw.WriteStartElement("phong");
                                {
                                    xw.WriteStartElement("diffuse");
                                    {
                                        xw.WriteStartElement("texture");
                                        xw.WriteAttributeString("texture", "sampler-" + tex.FileID);
                                        xw.WriteAttributeString("texcoord", "TEXCOORD0");
                                        xw.WriteEndElement();
                                    }
                                    xw.WriteEndElement();
                                }
                                xw.WriteEndElement();
                            }
                            xw.WriteEndElement();
                        }
                        xw.WriteEndElement();
                    }
                    xw.WriteEndElement();
                }
            }
            xw.WriteEndElement();
            #endregion

            #region Materials
            xw.WriteStartElement("library_materials");
            {
                xw.WriteStartElement("material");
                xw.WriteAttributeString("id", "material-default");
                xw.WriteStartElement("instance_effect");
                xw.WriteAttributeString("url", "#effect-default");
                xw.WriteEndElement();
                xw.WriteEndElement();

                foreach (Texture texture in model.textures) {
                    string effectID = "effect-" + texture.FileID;

                    xw.WriteStartElement("material");
                    xw.WriteAttributeString("id", "material-" + texture.FileID);
                    {
                        xw.WriteStartElement("instance_effect");
                        xw.WriteAttributeString("url", "#" + effectID);
                        xw.WriteEndElement();
                    }
                    xw.WriteEndElement();
                }
            }
            xw.WriteEndElement();
            #endregion
        }

        private void SectionAsset(XmlTextWriter xw) {
            xw.WriteStartElement("asset");
            {
                xw.WriteStartElement("contributor");
                {
                    xw.WriteElementString("author", "GameTools3D Library - ColladaExporter");
                    xw.WriteElementString("authoring_tool", System.Windows.Forms.Application.ProductName);
                    xw.WriteElementString("source_data", model.GetFileName);
                }
                xw.WriteEndElement();
                xw.WriteElementString("created", DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture));
                xw.WriteElementString("modified", DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture));

                xw.WriteStartElement("unit");
                xw.WriteAttributeString("meter", "0.01");
                xw.WriteAttributeString("name", "centimeter");
                xw.WriteEndElement();

                xw.WriteStartElement("up_axis");
                xw.WriteString("Y_UP");
                xw.WriteEndElement();
            }
            xw.WriteEndElement();
        }

        private void SectionLibraryGeometry(XmlTextWriter xw) {
            xw.WriteStartElement("library_geometries");
            {
                int numID = 0;
                foreach (MeshInfo meshInfo in model.meshInfo) {
                    foreach (Mesh mesh in meshInfo.meshTable) {
                        string name = "Mesh" + (numID++);

                        xw.WriteStartElement("geometry");
                        xw.WriteAttributeString("id", "geom-" + name);
                        xw.WriteAttributeString("name", name);
                        {
                            xw.WriteStartElement("mesh");
                            {
                                #region Positions (Verticies)
                                xw.WriteStartElement("source");
                                xw.WriteAttributeString("id", "geom-" + name + "-positions");
                                {
                                    xw.WriteStartElement("float_array");
                                    xw.WriteAttributeString("id", "geom-" + name + "-positions-array");
                                    xw.WriteAttributeString("count", (mesh.vertCount * 3).ToString());
                                    StringBuilder sbVert = new StringBuilder();
                                    for (int i = 0; i < mesh.vertData.Count; i++) {
                                        sbVert.AppendFormat("{0:0.000000} ", mesh.vertData[i][0]);
                                        sbVert.AppendFormat("{0:0.000000} ", mesh.vertData[i][1]);
                                        sbVert.AppendFormat("{0:0.000000} ", mesh.vertData[i][2]);
                                    }
                                    xw.WriteString(sbVert.ToString());
                                    xw.WriteEndElement();

                                    xw.WriteStartElement("technique_common");
                                    {
                                        xw.WriteStartElement("accessor");
                                        xw.WriteAttributeString("source", "#geom-" + name + "-positions-array");
                                        xw.WriteAttributeString("count", mesh.vertCount.ToString());
                                        xw.WriteAttributeString("stride", "3");
                                        {
                                            xw.WriteStartElement("param");
                                            xw.WriteAttributeString("name", "X");
                                            xw.WriteAttributeString("type", "float");
                                            xw.WriteEndElement();

                                            xw.WriteStartElement("param");
                                            xw.WriteAttributeString("name", "Y");
                                            xw.WriteAttributeString("type", "float");
                                            xw.WriteEndElement();

                                            xw.WriteStartElement("param");
                                            xw.WriteAttributeString("name", "Z");
                                            xw.WriteAttributeString("type", "float");
                                            xw.WriteEndElement();
                                        }
                                        xw.WriteEndElement();
                                    }
                                    xw.WriteEndElement();
                                }
                                xw.WriteEndElement();
                                #endregion

                                if (enableNormals) {
                                    #region Normals
                                    xw.WriteStartElement("source");
                                    xw.WriteAttributeString("id", "geom-" + name + "-normals");
                                    {
                                        xw.WriteStartElement("float_array");
                                        xw.WriteAttributeString("id", "geom-" + name + "-normals-array");
                                        xw.WriteAttributeString("count", (mesh.normalData.Count * 3).ToString());
                                        StringBuilder sbNormal = new StringBuilder();
                                        for (int i = 0; i < mesh.normalData.Count; i++) {
                                            sbNormal.AppendFormat("{0:0.00} ", mesh.normalData[i][0]);
                                            sbNormal.AppendFormat("{0:0.00} ", mesh.normalData[i][1]);
                                            sbNormal.AppendFormat("{0:0.00} ", mesh.normalData[i][2]);
                                        }
                                        xw.WriteString(sbNormal.ToString());
                                        xw.WriteEndElement();

                                        xw.WriteStartElement("technique_common");
                                        {
                                            xw.WriteStartElement("accessor");
                                            xw.WriteAttributeString("source", "#geom-" + name + "-normals-array");
                                            xw.WriteAttributeString("count", mesh.normalData.Count.ToString());
                                            xw.WriteAttributeString("stride", "3");
                                            {
                                                xw.WriteStartElement("param");
                                                xw.WriteAttributeString("name", "X");
                                                xw.WriteAttributeString("type", "float");
                                                xw.WriteEndElement();

                                                xw.WriteStartElement("param");
                                                xw.WriteAttributeString("name", "Y");
                                                xw.WriteAttributeString("type", "float");
                                                xw.WriteEndElement();

                                                xw.WriteStartElement("param");
                                                xw.WriteAttributeString("name", "Z");
                                                xw.WriteAttributeString("type", "float");
                                                xw.WriteEndElement();
                                            }
                                            xw.WriteEndElement();
                                        }
                                        xw.WriteEndElement();
                                    }
                                    xw.WriteEndElement();
                                    #endregion
                                }

                                #region UV Map
                                xw.WriteStartElement("source");
                                xw.WriteAttributeString("id", "geom-" + name + "-map1");
                                {
                                    xw.WriteStartElement("float_array");
                                    xw.WriteAttributeString("id", "geom-" + name + "-map1-array");
                                    xw.WriteAttributeString("count", (mesh.uvData.Count * 2).ToString());
                                    StringBuilder sbUV = new StringBuilder();
                                    for (int i = 0; i < mesh.uvData.Count; i++) {
                                        sbUV.AppendFormat("{0:0.00} ", mesh.uvData[i][0]); //S = u
                                        sbUV.AppendFormat("{0:0.00} ", 1.0f - mesh.uvData[i][1]); //T = 1 - v
                                    }
                                    xw.WriteString(sbUV.ToString());
                                    xw.WriteEndElement();

                                    xw.WriteStartElement("technique_common");
                                    {
                                        xw.WriteStartElement("accessor");
                                        xw.WriteAttributeString("source", "#geom-" + name + "-map1-array");
                                        xw.WriteAttributeString("count", mesh.uvData.Count.ToString());
                                        xw.WriteAttributeString("stride", "2");
                                        {
                                            xw.WriteStartElement("param");
                                            xw.WriteAttributeString("name", "S");
                                            xw.WriteAttributeString("type", "float");
                                            xw.WriteEndElement();

                                            xw.WriteStartElement("param");
                                            xw.WriteAttributeString("name", "T");
                                            xw.WriteAttributeString("type", "float");
                                            xw.WriteEndElement();

                                            /*
                                            xw.WriteStartElement("param");
                                            xw.WriteAttributeString("name", "P");
                                            xw.WriteAttributeString("type", "float");
                                            xw.WriteEndElement();
                                            */
                                        }
                                        xw.WriteEndElement();
                                    }
                                    xw.WriteEndElement();
                                }
                                xw.WriteEndElement();
                                #endregion

                                #region Verticies
                                xw.WriteStartElement("vertices");
                                xw.WriteAttributeString("id", "geom-" + name + "-vertices");
                                {
                                    xw.WriteStartElement("input");
                                    xw.WriteAttributeString("semantic", "POSITION");
                                    xw.WriteAttributeString("source", "#geom-" + name + "-positions");
                                    xw.WriteEndElement();

                                    if (enableNormals) {
                                        xw.WriteStartElement("input");
                                        xw.WriteAttributeString("semantic", "NORMAL");
                                        xw.WriteAttributeString("source", "#geom-" + name + "-normals");
                                        xw.WriteEndElement();
                                    }

                                    xw.WriteStartElement("input");
                                    xw.WriteAttributeString("semantic", "TEXCOORD");
                                    xw.WriteAttributeString("source", "#geom-" + name + "-map1");
                                    xw.WriteEndElement();
                                }
                                xw.WriteEndElement();
                                #endregion

                                #region Triangles
                                xw.WriteStartElement("triangles");
                                if (mesh.texid < model.textures.Count)
                                    xw.WriteAttributeString("material", "material-" + model.textures[(int)mesh.texid].FileID + "-sym");
                                xw.WriteAttributeString("count", mesh.faceData.Count.ToString());
                                {
                                    xw.WriteStartElement("input");
                                    xw.WriteAttributeString("semantic", "VERTEX");
                                    xw.WriteAttributeString("source", "#geom-" + name + "-vertices");
                                    xw.WriteAttributeString("offset", "0");
                                    xw.WriteEndElement();

                                    xw.WriteStartElement("p");
                                    StringBuilder sbTri = new StringBuilder();
                                    for (int i = 0; i < mesh.faceData.Count; i++) {
                                        sbTri.Append(mesh.faceData[i][0].ToString() + " ");
                                        sbTri.Append(mesh.faceData[i][1].ToString() + " ");
                                        sbTri.Append(mesh.faceData[i][2].ToString() + " ");
                                    }
                                    xw.WriteString(sbTri.ToString());
                                    xw.WriteEndElement();
                                }
                                xw.WriteEndElement();
                                #endregion
                            }
                            xw.WriteEndElement();
                        }
                        xw.WriteEndElement();
                    } //for
                } //for
            }
            xw.WriteEndElement();
        }

        private void SectionScene(XmlTextWriter xw) {
            xw.WriteStartElement("library_visual_scenes");
            {
                xw.WriteStartElement("visual_scene");
                xw.WriteAttributeString("id", "default");
                {
                    int numID = 0;
                    foreach (MeshInfo meshInfo in model.meshInfo) {
                        foreach (Mesh mesh in meshInfo.meshTable) {
                            string name = "Mesh" + (numID++);

                            string nodeId = string.Format("node-{0:X8}", mesh.GetHashCode());
                            xw.WriteStartElement("node");
                            xw.WriteAttributeString("id", nodeId);
                            xw.WriteAttributeString("name", nodeId);
                            {
                                xw.WriteStartElement("translate");
                                xw.WriteString("0.0 0.0 0.0");
                                xw.WriteEndElement();

                                xw.WriteStartElement("rotate");
                                xw.WriteString("0.0 0.0 1.0 0.0");
                                xw.WriteEndElement();

                                xw.WriteStartElement("rotate");
                                xw.WriteString("0.0 1.0 0.0 0.0");
                                xw.WriteEndElement();

                                xw.WriteStartElement("rotate");
                                xw.WriteString("1.0 0.0 0.0 0.0");
                                xw.WriteEndElement();

                                xw.WriteStartElement("scale");
                                xw.WriteString("1.0 1.0 1.0");
                                xw.WriteEndElement();

                                xw.WriteStartElement("instance_geometry");
                                xw.WriteAttributeString("url", "#geom-" + name);
                                {
                                    xw.WriteStartElement("bind_material");
                                    {
                                        xw.WriteStartElement("technique_common");
                                        {
                                            xw.WriteStartElement("instance_material");
                                            if (mesh.texid < model.textures.Count) {
                                                xw.WriteAttributeString("symbol", "material-" + model.textures[(int)mesh.texid].FileID + "-sym");
                                                xw.WriteAttributeString("target", "#material-" + model.textures[(int)mesh.texid].FileID);
                                            } else {
                                                xw.WriteAttributeString("symbol", "material-default-sym");
                                                xw.WriteAttributeString("target", "#material-default");
                                            }
                                            xw.WriteEndElement();
                                        }
                                        xw.WriteEndElement();
                                    }
                                    xw.WriteEndElement();
                                }
                                xw.WriteEndElement();
                            }
                            xw.WriteEndElement();
                        }
                    }
                }
                xw.WriteEndElement();
            }
            xw.WriteEndElement();

            //--

            xw.WriteStartElement("scene");
            {
                xw.WriteStartElement("instance_visual_scene");
                xw.WriteAttributeString("url", "#default");
                xw.WriteEndElement();
            }
            xw.WriteEndElement();
        }
    }
}
