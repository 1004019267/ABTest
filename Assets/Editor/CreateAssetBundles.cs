using UnityEditor;
using System.IO;
public class CreateAssetBundles 
{
    // 在菜单 Tools下的Build AssetBundles
    [MenuItem("Tools/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        //出包路径
        string dir = "AssetBundles";
        //判断路径是否存在dir
        if (Directory.Exists("dir")==false)
        {
            //在指定路径中创建所有目录和子目录，除非它们已经存在。
            Directory.CreateDirectory(dir);
        }
        //全部打包 参数为 出包路径 空 打包环境(win64)
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.ChunkBasedCompression,BuildTarget.StandaloneWindows64);
    }
}
