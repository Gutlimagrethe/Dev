using FluentSharp.CoreLib;
using FluentSharp.CoreLib.API;
using FluentSharp.NUnit;
using FluentSharp.Web35;
using NUnit.Framework;
using TeamMentor.CoreLib;
using TeamMentor.FileStorage;

namespace TeamMentor.UnitTests.Cassini
{
    public static class NUnitTests_Cassini_TeamMentor_ExtensionMethods
    {
        public static string                        GET(this NUnitTests_Cassini_TeamMentor nUnitTests_Cassini, string virtualPath)
        {
            var fullUri = nUnitTests_Cassini.siteUri.append(virtualPath);
            return fullUri.GET();
        }
        public static NUnitTests_Cassini_TeamMentor tmProxy_Refresh(this NUnitTests_Cassini_TeamMentor nUnitTests_Cassini)
        {            
            nUnitTests_Cassini.GET("").assert_Contains("TeamMentor");     // make a request to trigger Asp.NET pipeline and TM Startup
            nUnitTests_Cassini.tmProxy = nUnitTests_Cassini.tmProxy();          // get a new reference to it
            nUnitTests_Cassini.tmProxy.map_ReferencesToTmObjects();             // map the main TM objects (which should be live after a HTTP request)
            
            return nUnitTests_Cassini;
        }
        public static NUnitTests_Cassini_TeamMentor stop_And_Delete_Temp_XmlDatabase(this NUnitTests_Cassini_TeamMentor nUnitTests_Cassini)
        {
            return nUnitTests_Cassini.delete_Temp_Path_XmlDatabase();
        }
        public static NUnitTests_Cassini_TeamMentor delete_Temp_Path_XmlDatabase    (this NUnitTests_Cassini_TeamMentor nUnitTests_Cassini)
        {            
            var path_XmlDatabase = nUnitTests_Cassini.tmProxy().get_Custom_Path_XmlDatabase()
                                                               .assert_Folder_Exists();
            nUnitTests_Cassini.stop();
            Files.delete_Folder_Recursively(path_XmlDatabase).assert_True();
            path_XmlDatabase.assert_Folder_Not_Exists();
            return nUnitTests_Cassini;
        }
        public static NUnitTests_Cassini_TeamMentor use_Temp_Path_XmlDatabase(this NUnitTests_Cassini_TeamMentor nUnitTests_Cassini)
        {
            var temp_Path_XmlDatabase = "Temp_Path_XmlDatabase".tempDir().assert_Folder_Exists()
                                                                         .assert_Folder_Empty ();
            nUnitTests_Cassini.tmProxy().set_Custom_Path_XmlDatabase(temp_Path_XmlDatabase)
                                        .get_Custom_Path_XmlDatabase(                     ).assert_Is(temp_Path_XmlDatabase);
            return nUnitTests_Cassini;
        }
        public static NUnitTests_Cassini_TeamMentor call_TM_StartUp_Application_Start(this NUnitTests_Cassini_TeamMentor nUnitTests_Cassini)
        {
            var tmProxy = nUnitTests_Cassini.tmProxy();
            tmProxy.invoke_Instance(typeof(TM_StartUp), "get_Version").assert_Not_Null();  // this will call the TM_StartUp .ctor()
            var tmStartUp = tmProxy.get_Current<TM_StartUp>()         .assert_Not_Null();  // get byRef object of TM_StartUp
            tmStartUp.Application_Start();
            tmProxy.get_Current<TM_FileStorage>().assert_Not_Null();
            return nUnitTests_Cassini;
        }
        public static string teamMentor_Root_OnDisk(this NUnitTests_Cassini_TeamMentor nUnitTests_Cassini)
        {            
            //equivalent to TeamMentor.UnitTests WebSite_Root_OnDisk
            var assembly        = nUnitTests_Cassini.type().Assembly;
            var dllLocation     = assembly.CodeBase.subString(8);
            var webApplications = dllLocation.parentFolder().pathCombine(@"\..\..\..\..");
            var tmWebsite       = webApplications.pathCombine(@"TM_Websites\Website_3.5");

            tmWebsite.assert_Folder_Exists("tmWebsite dir not found: {0}".format(tmWebsite));
            Assert.That(dllLocation.fileExists()    , "dllLocation file not found: {0}".format(dllLocation));
            Assert.That(webApplications.dirExists() , "webApplications dir not found: {0}".format(webApplications));

            return tmWebsite;            
        }
        public static TeamMentor_Objects_Proxy tmProxy(this NUnitTests_Cassini_TeamMentor nUnitTests_Cassini)
        {
            return nUnitTests_Cassini.teamMentor_Objects_Proxy();
        }
        public static TeamMentor_Objects_Proxy teamMentor_Objects_Proxy(this NUnitTests_Cassini_TeamMentor nUnitTests_Cassini)
        {
            if (nUnitTests_Cassini.notNull() && nUnitTests_Cassini.apiCassini.notNull())            
                return new TeamMentor_Objects_Proxy(nUnitTests_Cassini.apiCassini)
                                .map_ReferencesToTmObjects();
            return null;
        }

        //TM Objects via Proxy
        public static TM_FileStorage tmFileStorage(this NUnitTests_Cassini_TeamMentor nUnitTests_Cassini)
        {
            return nUnitTests_Cassini.tmProxy().get_Current<TM_FileStorage>();
        }
    }
}