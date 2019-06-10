using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
public class LoadAB : MonoBehaviour
{
    AssetBundle ab;
    AssetBundle ab1;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        ////同步加载
        //ab1 = AssetBundle.LoadFromFile("AssetBundles/mat.ab");
        //ab = AssetBundle.LoadFromFile("AssetBundles/cube.ab"); 

        ////加载所有
        //Object[] objs = ab.LoadAllAssets();
        //foreach (Object item in objs)
        //{
        //    Instantiate(item);
        //}

        ////从缓存中同步读取
        //ab = AssetBundle.LoadFromMemory(File.ReadAllBytes("AssetBundles/mat.ab"));

        ////从缓存中异步读取
        //AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes("AssetBundles/cube.ab"));
        //yield return request;
        //ab1 = request.assetBundle;


        ////从本地异步读取
        //AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync("AssetBundles/cube.ab");
        //ab1 = request.assetBundle;
        //yield return request;

        ////www加载
        ////没下载好就
        //while (Caching.ready == false)
        //{
        //    //暂停一帧
        //    yield return null;
        //}
        ////参数为路径 版本号 crc是核对ab包里依赖一个标识是否下载完整 不完整重新下载
        //WWW www = WWW.LoadFromCacheOrDownload(@"file://D:\UnityAss\AssetBundleProject\AssetBundles\cube.ab", 1);
        //yield return www;
        //if (!string.IsNullOrEmpty(www.error))
        //{
        //    Debug.Log(www.error);
        //    yield break;
        //}
        //ab1 = www.assetBundle;

        //UnityWebRequest加载 using UnityEngine.Networking;

        string uri = @"http://localhost/AssetBundles/cube.ab";
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
        // File.WriteAllBytes("",request.downloadHandler.data); 保存到本地文件
        //直到 开始与远程服务器通信。才开始下载
        yield return request.SendWebRequest();

        // ab1 = DownloadHandlerAssetBundle.GetContent(request);

        ab1 = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;

        //使用资源
        GameObject wallPrefab = ab1.LoadAsset<GameObject>("Wall");
        Instantiate(wallPrefab);

        AssetBundle manifestAB = AssetBundle.LoadFromFile("AssetBundles/AssetBundles");
        AssetBundleManifest manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //所有包名
        foreach (var item in manifest.GetAllAssetBundles())
        {
            print(item);
        }
        //得到依赖这个包的包名
        string[] strs = manifest.GetAllDependencies("cube.ab");      
        foreach (var item in strs)
        {
            print(item);
            AssetBundle.LoadFromFile("AssetBundles/" + item);
        }
       
    }

}
