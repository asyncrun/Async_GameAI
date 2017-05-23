//------------------------------------------------------------------------
//
//  Name:		KeywordReplace.cs
//
//  Author:		LIWEI\Administrator
//
//  Date:		05/23/2017 18:42:25
//
//  Project:	Async_GameAI
//
//  Desc:       自定义初始脚本，添加文件信息
//
//------------------------------------------------------------------------

using System;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using UnityEditor;
using UnityEngine;

namespace AsyncRun.Editor
{
    public class KeywordReplace : UnityEditor.AssetModificationProcessor
    {
        private const string NameSpace = "AsyncRun";
        private const string TemplatesFilePath = "Assets/Editor/_ProjectPref/_Templates/BasicScript.txt";

        public static void OnWillCreateAsset(string path)
        {
            //文件夹路径 path ："Assets\xx"
            //原始路径 path : "Assets\_Scripts\xxxx.cs.meta"
            path = path.Replace(".meta", "");
            //替换后路径 path : "Assets\_Scripts\xxxx.cs"

            int extensionStartIndex = path.LastIndexOf(".", StringComparison.Ordinal);
            if (extensionStartIndex == -1)
            {
                //创建的是文件夹
                return;
            }

            string extension = path.Substring(extensionStartIndex);
            if (extension != ".cs")
            {
                //如果后缀是其他的不去做替换处理
                return;
            }

            string fileName = Path.GetFileName(path);
            string ScriptName = fileName.Split('.')[0];

            //read
            extensionStartIndex = Application.dataPath.LastIndexOf("Assets", StringComparison.Ordinal);
            string projectAssetPath = Application.dataPath.Substring(0, extensionStartIndex);
            string fullTemplatesFilePath = Path.Combine(projectAssetPath, TemplatesFilePath);
            string templatesFileString = File.ReadAllText(fullTemplatesFilePath);

            //KeywordReplace
            templatesFileString = templatesFileString.Replace("#SCRIPTNAME#", ScriptName);
            templatesFileString = templatesFileString.Replace("#NAMESPACE#", NameSpace);
            templatesFileString = templatesFileString.Replace("#CREATIONDATE#", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            templatesFileString = templatesFileString.Replace("#PROJECTNAME#", PlayerSettings.productName);
            templatesFileString = templatesFileString.Replace("#DEVELOPERNAME#", WindowsIdentity.GetCurrent().Name);

            //Debug.Log(templatesFileString);

            //write
            File.WriteAllText(path, templatesFileString);
            AssetDatabase.Refresh();
        }
    }
}

