﻿using NUnit.Framework;
using FluentSharp.CoreLib;
using TeamMentor.CoreLib;
using TeamMentor.FileStorage;

namespace TeamMentor.UnitTests.QA
{
    [TestFixture]
    public class Test_LoadLibraries_FromExternalSource
    {
        TM_FileStorage  tmFileStorage;
        TM_Xml_Database tmXmlDatabase;

        [SetUp]
        public void setup()
        {
            if (Tests_Consts.offline)
                Assert.Ignore("Ignoring Test because we are offline");   

            admin.assert(()=>{
                                tmFileStorage = new TM_FileStorage(false);
            
                                TM_FileStorage.Custom_WebRoot = "Custom_WebRoot".tempDir();

                                tmFileStorage.set_WebRoot()
                                             .set_Path_XmlDatabase()
                                             .tmConfig_Load()
                                             .set_Path_XmlLibraries();

                                tmXmlDatabase = tmFileStorage.TMXmlDatabase;
                            });

            
        }

        [TearDown]
        public void tearDown()
        {
            admin.assert(()=>{
                                TM_FileStorage.Custom_WebRoot = null;
                                tmFileStorage.delete_Database();
                             });
            
        }
        [Test]//[Ignore("SEE Fix added in 3.4")]
        public void DownloadAndInstallLibraryFromZip()
        {     
            //UserGroup.Editor.assert();

            var tmLibraries_Before = tmXmlDatabase.tmLibraries();            

           // Install_LibraryFromZip_TopVulns();
           // Install_LibraryFromZip_TopVulns();            //2nd time should skip install
            tmFileStorage.install_LibraryFromZip_Docs();
            tmFileStorage.install_LibraryFromZip_Docs();    //2nd time should skip install

            UserGroup.Editor.assert();
            Assert.IsEmpty(tmLibraries_Before, "No Libraries should be there before install");
            Assert.IsNotEmpty(tmXmlDatabase.tmLibraries(), "After install, no Libraries");
            Assert.IsNotEmpty(tmXmlDatabase.tmViews(), "After install, no Views");
            //Assert.IsNotEmpty(tmXmlDatabase.tmFolders(), "After install, no Folders");
            Assert.IsNotEmpty(tmXmlDatabase.tmGuidanceItems(), "After install, no Articles");

            tmFileStorage.install_LibraryFromZip_OWASP();
            UserGroup.Editor.assert();

            Assert.AreEqual  (2, tmXmlDatabase.tmLibraries().size() , "After OWASP install, there should be 2");
            Assert.IsNotEmpty(tmXmlDatabase.tmViews(), "After OWASP install, no Views");
            Assert.IsNotEmpty(tmXmlDatabase.tmFolders(), "After OWASPinstall, no Folders");
            Assert.IsNotEmpty(tmXmlDatabase.tmGuidanceItems(), "After OWASP install, no Articles");

            UserRole.None.assert();
        }
        
    }
}
