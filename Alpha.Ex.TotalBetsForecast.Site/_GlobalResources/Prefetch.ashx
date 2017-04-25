<%@ WebHandler Language="C#" Class="Resources_PreLoad" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;


/*
* Created 20100306@Lee - Read all resources (javascripts, images and css) indicated in Web.res and render ajax script to pre-load it in browser cache.
* Revision ?@? - ... 
*
*/

public class Resources_PreLoad : IHttpHandler, IRequiresSessionState
{
    private List<string> staticResources;
    private const string JSRESOURCESARRAY = "AGE.AGENT.JSRESOURCESARRAY";
    private const string DOTJS = ".js";
    private const string DOTCSS = ".css";
    private const string PATH = "../Web.res";
    private const char QUESTION = '?';
    
    private Cache Cache
    {
        get 
        {
            return HttpRuntime.Cache;
        }
    }
    
    private string JsResourcesArray
    {
        get
        {
            if (Cache[JSRESOURCESARRAY] != null)
                return (string)(Cache[JSRESOURCESARRAY]);
            else
                return null;
        }
        set
        {
            Cache[JSRESOURCESARRAY] = value;
        }
    }

    private void LoadStaticResources()
    {
        if (JsResourcesArray == null)
        {
            staticResources = new List<string>();
            // Load list of static resources
            string[] paths = File.ReadAllLines(HttpContext.Current.Server.MapPath(PATH));
            int c = paths.Length;
            for (int i = 0; i < c; i++)
            {
                string[] pd = paths[i].Split(new char[] { QUESTION });
                string p = pd[0];
                string d = pd[1];
                
                GetUrlFiles(p, d.Split(new char[]{','}), true);
            }

            c = staticResources.Count;
            string basePath = HttpContext.Current.Server.MapPath("../");
            StringBuilder jsArray = new StringBuilder();
            jsArray.Append("[");
            if (c >= 1)
            {
                staticResources[0] = RevertPath(basePath, staticResources[0]);

                if (staticResources[0].EndsWith(DOTJS) || staticResources[0].EndsWith(DOTCSS))
                    staticResources[0] = staticResources[0] + GetUrlVersion();
                
                jsArray.Append("'" + staticResources[0] + "'");
            }
            for (int i = 1; i < c; i++)
            {
                staticResources[i] = RevertPath(basePath, staticResources[i]);

                if (staticResources[i].EndsWith(DOTJS) || staticResources[i].EndsWith(DOTCSS))
                    staticResources[i] = staticResources[i] + GetUrlVersion();
                
                jsArray.Append(",'" + staticResources[i] + "'");
            }
            jsArray.Append("]");

            JsResourcesArray = jsArray.ToString();        
        }
    }

    private string GetUrlVersion()
    {
        if (string.IsNullOrEmpty(TplCore.TplSettings.UrlVersion)) return null;
        else return QUESTION + TplCore.TplSettings.UrlVersion;
    }

    private void GetUrlFiles(string path, string[] exts, bool searchSubDir)
    {
        HttpServerUtility server = HttpContext.Current.Server;
        int c = exts.Length;
        string[] files;
        for(int i=0; i<c; i++)
        {
            files = Directory.GetFiles(server.MapPath("../" + path), exts[i], SearchOption.AllDirectories);
            staticResources.AddRange(files);
        }
    }

    private string RevertPath(string basePath, string path)
    {
        int i = path.IndexOf(basePath);
        if (-1 == i) return null;

        string vPath = path.Substring(i + basePath.Length);
        vPath = vPath.Replace('\\', '/');

        return vPath;
    }

    /// <summary>
    /// Return array of static files url (excluding domain url)
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Cache.SetNoStore();

        LoadStaticResources();
        context.Response.Write(JsResourcesArray);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}