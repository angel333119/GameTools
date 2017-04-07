using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace GT_KyleHyde
{
    static class Tools
    {
        public static ushort SwapBytes(ushort x)
        {
            return (ushort)((ushort)((x & 0xff) << 8) | ((x >> 8) & 0xff));
        }

        public static long ByteArrayToLong(byte[] byteArray, bool flip = true)
        {
            if (flip)
                Array.Reverse(byteArray);

            if (byteArray.Length == 2)
                return ((byteArray[0] & 0xFF) << 0 | (byteArray[1] & 0xFF) << 8);
            else if (byteArray.Length == 4)
                return ((byteArray[0] & 0xFF) << 0 | (byteArray[1] & 0xFF) << 8 | (byteArray[2] & 0xFF) << 16 | (byteArray[3] & 0xFF) << 24);
            else
                return BitConverter.ToInt64(byteArray, 0);
        }

        public static void DirectoryToTree(string dir, TreeView tree)
        {
            tree.Nodes.Clear();
            //http://stackoverflow.com/questions/6239544/c-sharp-how-to-populate-treeview-with-file-system-directory-structure
            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(dir);
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                }
                foreach (var file in directoryInfo.GetFiles())
                {
                    if (file.Extension.ToLower() == ".wpf")
                        currentNode.Nodes.Add(new TreeNode(file.Name));
                }
            }

            tree.Nodes.Add(node);

            tree.ExpandAll();
            tree.SelectedNode = tree.Nodes[0];
        }

    }
}
