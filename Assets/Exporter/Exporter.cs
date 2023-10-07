
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Globalization;
public class Exporter
{
    [MenuItem("CISS115/Export")]
    public static void Export()
    {
        ClearConsole();

        const string aDir = "Assets";
        const string psDir = "ProjectSettings";

        CultureInfo culture = new CultureInfo("en-US");

        string cd = Directory.GetCurrentDirectory();

        string[] aDirs = Directory.GetDirectories(cd + "/" + aDir);

        string[] psFiles = Directory.GetFiles(cd + "/" + psDir);

       // Debug.Log("psFiles Length = " + psFiles.Length + "adirs Length = " + aDirs.Length);
        string[] projectContent = new string[
            psFiles.Length + aDirs.Length - 1];

        Debug.Log("Export started at "
                   + System.DateTime.Now.ToString(culture));

        Debug.LogFormat("Exporting {0} files, {1} directories.",
            psFiles.Length, aDirs.Length);

        int n = 0;

        for (int i = 0; i < aDirs.Length; ++i)
        {
            DirectoryInfo di = new DirectoryInfo(aDirs[i]);

            if (di.Name != "Exporter")
            {
                string dirName = aDir + "/" + di.Name;
                projectContent[n++] = dirName;
            }
        }

        for (int i = 0; i < psFiles.Length; ++i)
        {
            FileInfo fi = new FileInfo(psFiles[i]);
            projectContent[n++] = psDir + "/" + fi.Name;
        }

        AssetDatabase.ExportPackage(
            projectContent,
            "FullProject.unitypackage",
            ExportPackageOptions.Recurse);

#if TEST
        foreach (string path in projectContent)
        {
            Debug.Log(path);
        }
#endif

        Debug.Log("Project Exported at "
                   + System.DateTime.Now.ToString(culture));
    }

    public static void ClearConsole()
    {
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}