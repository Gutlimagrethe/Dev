﻿using System.Web.Mvc;

namespace TeamMentor.Website
{
	public static class MarkdownDeepExtensions
	{        
		public static string renderMarkdown(this string markdown)
		{						
		    var md = new MarkdownDeep.Markdown
		                 {
                             SafeMode = true,       // was false in the MarkdownDeep demo app
                             ExtraMode = false,     // was true in the MarkdownDeep demo app
                             AutoHeadingIDs = true, 
                             MarkdownInHtml = true, 
                             NewWindowForExternalLinks = true
		                 };
            return md.Transform(markdown);            
		}
	}
}