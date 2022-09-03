using UnityEditor;
using UnityEngine;
using static System.IO.Directory;
using static System.IO.Path;


namespace siratim
{
  public static class ToolsMenu
  {
    [MenuItem("Tools/Setup/CreateDefaultFolders")]
    public static void CreateDefaultFolder()
    {
      MakeDir("_Project",
      "Scenes","Scripts","Materials",
      "Media/Audio",
      "Media/Video",
      "Media/Images"
      );
      AssetDatabase.Refresh();
    }


    private static void MakeDir(string root, params string[] dirs)
    {
      var fullpath = Combine(Application.dataPath, root);
      foreach (var newDir in dirs) CreateDirectory(Combine(fullpath, newDir));
    }
  }
}