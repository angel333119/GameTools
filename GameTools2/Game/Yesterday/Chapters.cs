using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameTools2.Game.Yesterday
{
    class ChapterList
    {
        public static List<Chapter> Chapters = new List<Chapter>() {
            new Chapter('A', "John (Training)", new Dictionary<string, string>() {
	            {"00", "Items"},
	            {"01", "Scene (Outside Plant)"},
	            {"02", "Scene (Outside Nest)"},
	            {"03", "Scene (Outside Bell)"},
	            {"05", "Scene (Inside)"}
            }),
            new Chapter('B', "John (Warehouse)", new Dictionary<string, string>() {
	            {"00", "Items"},
	            {"01", "Scene (Outside)"},
	            {"02", "Scene (Outside Lower)"},
	            {"03", "Scene (Outside Train)"},
	            {"04", "Scene (Outside Upper)"},
	            {"05", "Scene (Inside 2)"},
	            {"06", "Scene (Control Panel)"},
	            {"07", "Scene (Inside 1)"},
	            {"11", "Scene (Unrelated)"},
            }),
            new Chapter('C', "Henry (Subway)", new Dictionary<string, string>() {
	            {"00", "Items"},
	            {"02", "Scene (Entry)"},
	            {"03", "Scene (Stairs)"},
	            {"04", "Scene (Train)"},
	            {"05", "Scene (Gate)"},
	            {"06", "Scene (Lair)"},
            }),
            new Chapter('D', "Cooper (Subway)", new Dictionary<string, string>() {
	            {"00", "Items"},
	            {"01", "Scene (Outside)"},
	            {"02", "Scene (Entry)"},
	            {"03", "Scene (Stairs)"},
	            {"04", "Scene (Train)"},
	            {"06", "Scene (Lair)"},
	            {"07", "Scene (Storage)"},
	            {"11", "Scene (Unrelated)"},
            }),
            new Chapter('E', "John (Hotel)", new Dictionary<string, string>() {
	            {"00", "Items"},
	            {"01", "Scene (Main)"},
	            {"02", "Scene (Bathroom)"},
	            {"03", "Scene (Outside)"},
            }),
            new Chapter('F', "John (Store)", new Dictionary<string, string>() {
	            {"00", "Items"},
	            {"01", "Scene (Front)"},
	            {"02", "Scene (Back)"},
	            {"04", "Scene (Cross)"},
            }),
            new Chapter('H', "John (Scotland)", new Dictionary<string, string>() {
	            {"00", "Items"},
	            {"01", "Scene Overview"},
	            {"02", "Scene (Camp)"},
	            {"03", "Scene (Cross)"},
	            {"10", "Scene (Underground Past)"},
	            {"11", "Scene (Underground Start)"},
	            {"13", "Scene (Underground End)"},
            })
        };

        public static string FolderName(string ext) {
            string ret = "UNKNOWN_" + ext;

            if(ext == "IFZ") {
                ret = "Interface";   
            } else if(ext[0] == 'X') {
                ret = "X" + "\\" + "RESOURCE_" + ext;
            } else {
                foreach(Chapter c in Chapters) {
                    if(c.id == ext[0] && c.scene.ContainsKey(ext.Substring(1,2))) {
                        ret = c.id + " - " + c.name + "\\" + ext.Substring(1,2) + " " + c.scene[ext.Substring(1,2)];
                        break;
                    }
                }
            }

            if (ret == "UNKNOWN_" + ext)
                Console.WriteLine();

            return ret;
        }
    }

    class Chapter
    {
        public char id;
        public string name;
        public Dictionary<string, string> scene;

        public Chapter(char i, string n, Dictionary<string, string> s) {
            id = i;
            name = n;
            scene = s;
        }
    }
}
