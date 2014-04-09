namespace TeamMentor.CoreLib
{
	public class TMConsts
	{
        public static string AUTH_TOKEN_REQUEST_VAR_NAME      = "auth";
		public static string PATH_HTML_PAGE_UNAVAILABLE       = "/Html_Pages/Gui/Pages/unavailable.html";      

        public static string DEFAULT_ARTICLE_FOLDER_NAME      = "Articles";
        public static string VIRTUAL_PATH_MAPPING			  = "..\\..";					  
        public static string XML_DATABASE_VIRTUAL_PATH		  = "TeamMentor";
		public static string XML_DATABASE_VIRTUAL_PATH_LEGACY = "Library_Data\\XmlDatabase"; // for legacy support (pre 3.3)
                
        public static string DEFAULT_TM_LOCALHOST_SERVER_URL  = "http://localhost";
	    public static string APPLICATION_LOGS_FOLDER_NAME     = "Application_Logs";

        public static string USERDATA_FIRST_SCRIPT_TO_INVOKE  = "H2Scripts//FirstScriptToInvoke.h2";
        public static string USERDATA_PATH_WEB_ROOT_FILES     = "WebRoot_Files";

        public static int    FIREBASE_SUBMIT_QUEUE_MAX_WAIT    = 60000;
        public static string FIREBASE_AREA_DEBUG_MSGS          = "debugMsgs";
        public static string FIREBASE_AREA_ACTIVITIES          = "activities";
        public static string FIREBASE_AREA_REQUESTS_URLS       = "requestUrls";
        
        public static string EMAIL_SUBJECT_NEW_USER_WELCOME   = "Welcome to TeamMentor";
        public static string EMAIL_TEST_SUBJECT               = "Test Email";
        public static string EMAIL_TEST_MESSAGE               = "From TeamMentor";

        public static string EMAIL_BODY_NEW_USER_WELCOME =
@"Hello,

It's a pleasure to confirm that a new TeamMentor account has been created for you and that you'll now be able to access
the entire set of guidance available in the TM repository.

To access the service:

- Go to {0} and login at the top right-hand corner of the page.
- Use your username : {1}.

Thanks,

";
        public static string EMAIL_DEFAULT_FOOTER =
@"
Sent by TeamMentor. 

";        
	


    public static string EMAIL_BODY_NEW_USER_NEEDS_APPROVAL =
@"Hello,

Thanks for creating an account in TeamMentor. 

The account is being reviewed and you will be notified if it is approved.

Thanks,

";

    }
}