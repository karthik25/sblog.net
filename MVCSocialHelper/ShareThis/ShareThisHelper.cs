#region License
//Copyright (c) 2011 Nandip Makwana
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation 
//files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, 
//modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the 
//Software is furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the 
//Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
//COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
//ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MVCSocialHelper
{
    /// <summary>
    /// Enable developer to render predefined and custom ShareThis widget.
    /// </summary>
    public class ShareThisHelper
    {
        private static Dictionary<string, string> _buttonLabel = new Dictionary<string, string>();

        static ShareThisHelper()
        {
            ShareThisHelper.ButtonLabel.Add("Amazon_Wishlist", "amazon_wishlist");
            ShareThisHelper.ButtonLabel.Add("Bebo", "bebo");
            ShareThisHelper.ButtonLabel.Add("Blogger", "blogger");
            ShareThisHelper.ButtonLabel.Add("Delicious", "delicious");
            ShareThisHelper.ButtonLabel.Add("Digg", "digg");
            ShareThisHelper.ButtonLabel.Add("dotnet_Shoutout", "dotnetshoutout");
            ShareThisHelper.ButtonLabel.Add("DZone", "dzone");
            ShareThisHelper.ButtonLabel.Add("Email", "email");
            ShareThisHelper.ButtonLabel.Add("Facebook", "facebook");
            ShareThisHelper.ButtonLabel.Add("Google_Buzz", "gbuzz");
            ShareThisHelper.ButtonLabel.Add("Google", "google");
            ShareThisHelper.ButtonLabel.Add("Google_Bookmarks", "google_bmarks");
            ShareThisHelper.ButtonLabel.Add("Google_Reader", "google_reader");
            ShareThisHelper.ButtonLabel.Add("Google_Translate", "google_translate");
            ShareThisHelper.ButtonLabel.Add("LinkedIn", "linkedin");
            ShareThisHelper.ButtonLabel.Add("Messenger", "messenger");
            ShareThisHelper.ButtonLabel.Add("MySpace", "myspace");
            ShareThisHelper.ButtonLabel.Add("Orkut", "orkut");
            ShareThisHelper.ButtonLabel.Add("Reddit", "reddit");
            ShareThisHelper.ButtonLabel.Add("ShareThis", "sharethis");
            ShareThisHelper.ButtonLabel.Add("StumbleUpon", "stumbleupon");
            ShareThisHelper.ButtonLabel.Add("Tumblr", "tumblr");
            ShareThisHelper.ButtonLabel.Add("Twitter", "twitter");
            ShareThisHelper.ButtonLabel.Add("WordPress", "wordpress");
            ShareThisHelper.ButtonLabel.Add("Yahoo", "yahoo");
            ShareThisHelper.ButtonLabel.Add("Yahoo_Bookmarks", "yahoo_bmarks");
        }

        /// <summary>
        /// Render custom ShareThis widget.
        /// </summary>
        /// <param name="shareThis">ShareThisBuilder object configured with user customization.</param>
        /// <returns></returns>
        public static MvcHtmlString Render(ShareThisBuilder shareThis)
        {
            string strScript = string.Empty;
            string strButtonStyle = string.Empty;

            if (shareThis.CounterStyle == ShareThisCounterStyle.None)
            {
                if (shareThis.ButtonStyle == ShareThisButtonStyle.Medium_32X32)
                {
                    strButtonStyle = "_large";
                    shareThis.ShowLabel = false;
                }
                else if (shareThis.ButtonStyle == ShareThisButtonStyle.Rectangle)
                { 
                    strButtonStyle = "_button";
                    shareThis.ShowLabel = true;
                }
            }
            else if (shareThis.CounterStyle == ShareThisCounterStyle.Horizontal)
            {
                strButtonStyle = "_hcount";
                shareThis.ShowLabel = true;
            }
            else
            { 
                strButtonStyle = "_vcount";
                shareThis.ShowLabel = true;
            }

            ShareThisHelper.WidgetStyle = shareThis.WidgetStyle;

            for (int counter = 0; counter < shareThis.Buttons.Count; counter++)
            {
                strScript += ShareThisHelper.GetButtonPlaceholder(shareThis.Buttons[counter], strButtonStyle, shareThis.ShowLabel, shareThis.ButtonLabelDictionaty[shareThis.Buttons[counter].ToString()]);
            }

            strScript += ShareThisHelper.GetCommonScript;

            return new MvcHtmlString(strScript);
        }

        /// <summary>
        /// Render predefined MultiChannel widget.
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString MultiChannel()
        {
            ShareThisBuilder shareThis = new ShareThisBuilder();

            shareThis.AddButton(ShareThisButton.Twitter);
            shareThis.AddButton(ShareThisButton.Facebook);
            shareThis.AddButton(ShareThisButton.Yahoo);
            shareThis.AddButton(ShareThisButton.Google_Buzz);
            shareThis.AddButton(ShareThisButton.Email);
            shareThis.AddButton(ShareThisButton.ShareThis);

            return ShareThisHelper.Render(shareThis);
        }

        /// <summary>
        /// Render predefined VerticalCounters widget.
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString VerticalCounters()
        {
            ShareThisBuilder shareThis = new ShareThisBuilder();

            shareThis.AddButton(ShareThisButton.Twitter);
            shareThis.AddButton(ShareThisButton.Facebook);
            shareThis.AddButton(ShareThisButton.Email);
            shareThis.AddButton(ShareThisButton.ShareThis);

            shareThis.CounterStyle = ShareThisCounterStyle.Vertical;

            return ShareThisHelper.Render(shareThis);
        }

        /// <summary>
        /// Render predefined HorizontalCounters widget.
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString HorizontalCounters()
        {
            ShareThisBuilder shareThis = new ShareThisBuilder();

            shareThis.AddButton(ShareThisButton.Twitter);
            shareThis.AddButton(ShareThisButton.Facebook);
            shareThis.AddButton(ShareThisButton.Email);
            shareThis.AddButton(ShareThisButton.ShareThis);

            shareThis.CounterStyle = ShareThisCounterStyle.Horizontal;

            return ShareThisHelper.Render(shareThis);
        }

        /// <summary>
        /// Render predefined Classic Button. 
        /// </summary>
        /// <param name="displayText">Optional label text to use with Classic Button.</param>
        /// <returns></returns>
        public static MvcHtmlString Classic(string displayText = "")
        {
            string strScript = string.Empty;

            if (displayText == "")
                displayText = "ShareThis";

            strScript += ShareThisHelper.GetButtonPlaceholder(ShareThisButton.ShareThis, "_button", true, displayText);
            strScript += ShareThisHelper.GetCommonScript;

            return new MvcHtmlString(strScript);
        }
        
        #region Private Member

        private static string GetButtonPlaceholder(ShareThisButton button, string buttonStyle = "_large", bool renderLabel = true, string displayText = "")
        {
            string strPlaceHolder = string.Empty;

            strPlaceHolder += "<span  class='st_" + ShareThisHelper.ButtonLabel[button.ToString()] + buttonStyle + "'";

            if (renderLabel)
            {
                if (displayText.Trim() != "")
                    strPlaceHolder += " displayText='" + displayText + "'";               
                    
            }

            strPlaceHolder += "></span>";

            return strPlaceHolder;
        }

        private static Dictionary<string, string> ButtonLabel
        {
            get { return ShareThisHelper._buttonLabel; }
            set { ShareThisHelper._buttonLabel = value; }
        }

        private static string GetCommonScript
        {
            get
            {
                return ShareThisHelper.GetWidgetStyleJS + ShareThisHelper.GetButtonJS + ShareThisHelper.GetInitJS;
            }
        }

        private static ShareThisWidgetStyle WidgetStyle
        {           
            get;
            set;
        }

        private static string GetWidgetStyleJS
        {
            get
            {
                string strWidget = string.Empty;
                if (ShareThisHelper.WidgetStyle == ShareThisWidgetStyle.Oauth)
                    strWidget += "true;";
                else
                    strWidget += "false;";

                return "<script type=\"text/javascript\">var switchTo5x=" + strWidget + "</script>";
            }
        }

        private static string GetButtonJS
        {
            get
            {
                return "<script type=\"text/javascript\" src=\"http://w.sharethis.com/button/buttons.js\"></script>";
            }
        }

        private static string GetInitJS
        {
            get
            {
                return "<script type=\"text/javascript\">stLight.options({publisher:'" + ShareThisHelper.GetPublisherId + "'});</script>";
            }
        }

        private static string GetPublisherId
        {
            get
            {
                return "714a3daa-6213-4de6-bb9f-eaaaca632490";
            }
        }
        #endregion
    }

    /// <summary>
    /// ShareThisBuilder is used to customize ShareThis widget.
    /// </summary>
    public class ShareThisBuilder
    {
        private Dictionary<string, string> _buttonLabelDictionaty = new Dictionary<string, string>();
        private List<ShareThisButton> _buttons = new List<ShareThisButton>();

        /// <summary>
        /// 
        /// </summary>
        public ShareThisBuilder()
        {
            this.ButtonLabelDictionaty.Add("Amazon_Wishlist", "Amazon Wishlist");
            this.ButtonLabelDictionaty.Add("Bebo", "Bebo");
            this.ButtonLabelDictionaty.Add("Blogger", "Blogger");
            this.ButtonLabelDictionaty.Add("Delicious", "Delicious");
            this.ButtonLabelDictionaty.Add("Digg", "Digg");
            this.ButtonLabelDictionaty.Add("dotnet_Shoutout", ".net Shoutout");
            this.ButtonLabelDictionaty.Add("DZone", "DZone");
            this.ButtonLabelDictionaty.Add("Email", "Email");
            this.ButtonLabelDictionaty.Add("Facebook", "Facebook");
            this.ButtonLabelDictionaty.Add("Google_Buzz", "Buzz");
            this.ButtonLabelDictionaty.Add("Google", "Google");
            this.ButtonLabelDictionaty.Add("Google_Bookmarks", "Google Bookmarks");
            this.ButtonLabelDictionaty.Add("Google_Reader", "Google Reader");
            this.ButtonLabelDictionaty.Add("Google_Translate", "Google Translate");
            this.ButtonLabelDictionaty.Add("LinkedIn", "LinkedIn");
            this.ButtonLabelDictionaty.Add("Messenger", "Messenger");
            this.ButtonLabelDictionaty.Add("MySpace", "MySpace");
            this.ButtonLabelDictionaty.Add("Orkut", "Orkut");
            this.ButtonLabelDictionaty.Add("Reddit", "Reddit");
            this.ButtonLabelDictionaty.Add("ShareThis", "ShareThis");
            this.ButtonLabelDictionaty.Add("StumbleUpon", "StumbleUpon");
            this.ButtonLabelDictionaty.Add("Tumblr", "Tumblr");
            this.ButtonLabelDictionaty.Add("Twitter", "Twitter");
            this.ButtonLabelDictionaty.Add("WordPress", "WordPress");
            this.ButtonLabelDictionaty.Add("Yahoo", "Yahoo!");
            this.ButtonLabelDictionaty.Add("Yahoo_Bookmarks", "Yahoo Bookmarks");
        }

        /// <summary>
        /// Hold the button label dictionary. Button label will be used with button whenver it is required.
        /// </summary>
        internal Dictionary<string, string> ButtonLabelDictionaty
        {
            get { return _buttonLabelDictionaty; }
            set { _buttonLabelDictionaty = value; }
        }
        
        /// <summary>
        /// Hold the button label list to include with ShareThis widget.
        /// </summary>
        internal List<ShareThisButton> Buttons
        {
            get { return _buttons; }
            set { _buttons = value; }
        }

        /// <summary>
        /// Add new ShareThis button to the list of selected buttons.
        /// </summary>
        /// <param name="button">ShareThisButton instance.</param>
        /// <param name="displayText">Override default label text to use with button.</param>
        public void AddButton(ShareThisButton button, string displayText = "")
        {
            this.Buttons.Add(button);

            if (displayText.Trim() != "")
                this.ButtonLabelDictionaty[button.ToString()] = displayText;
        }
        
        /// <summary>
        /// Get or Set widget style.
        /// </summary>
        public ShareThisWidgetStyle WidgetStyle
        {
            get;
            set;
        }

        /// <summary>
        /// Get or Set button style.
        /// </summary>
        public ShareThisButtonStyle ButtonStyle
        {
            get;
            set;
        }

        /// <summary>
        /// Get or Set counter style.
        /// </summary>
        public ShareThisCounterStyle CounterStyle
        {
            get;
            set;
        }

        /// <summary>
        /// True or False whether Label should display with button or not.
        /// </summary>
        /// <remarks>This properties is dependent on ButtonStyle.</remarks>
        public bool ShowLabel
        {
            get;
            set;
        }
    }

    /// <summary>
    /// List available Counter Style.
    /// </summary>
    public enum ShareThisCounterStyle
    {
        /// <summary>
        /// No Counter.
        /// </summary>
        None,
        /// <summary>
        /// Horizontal Counter style.
        /// </summary>
        Horizontal,
        /// <summary>
        /// Vertical Counter style.
        /// </summary>
        Vertical
    }

    /// <summary>
    /// List available Button Style.
    /// </summary>
    public enum ShareThisButtonStyle
    {
        /// <summary>
        /// 32 X 32 button style.
        /// </summary>
        Medium_32X32,
        /// <summary>
        /// 16 X 16 button style.
        /// </summary>
        Small_16X16,
        /// <summary>
        /// Rectangle button style.
        /// </summary>
        Rectangle
    }

    /// <summary>
    /// List available buttons to use with ShareThis.
    /// </summary>
    public enum ShareThisButton
    {
        /// <summary>
        /// Amazon Wishlist button.
        /// </summary>
        Amazon_Wishlist,
        /// <summary>
        /// Bebo button.
        /// </summary>
        Bebo,
        /// <summary>
        /// Blogger button.
        /// </summary>
        Blogger,
        /// <summary>
        /// Delicious button.
        /// </summary>
        Delicious,
        /// <summary>
        /// Digg button.
        /// </summary>
        Digg,
        /// <summary>
        /// .NET Shoutout button.
        /// </summary>
        dotnet_Shoutout,
        /// <summary>
        /// DZone button.
        /// </summary>
        DZone,
        /// <summary>
        /// Email button.
        /// </summary>
        Email,
        /// <summary>
        /// Facebook button.
        /// </summary>
        Facebook,
        /// <summary>
        /// Google Buzz button.
        /// </summary>
        Google_Buzz,
        /// <summary>
        /// Google button.
        /// </summary>
        Google,
        /// <summary>
        /// Google Bookmark button.
        /// </summary>
        Google_Bookmarks,
        /// <summary>
        /// Google Reader button.
        /// </summary>
        Google_Reader,
        /// <summary>
        /// Google Translate button.
        /// </summary>
        Google_Translate,
        /// <summary>
        /// LinkedIn button.
        /// </summary>
        LinkedIn,
        /// <summary>
        /// Messenger button.
        /// </summary>
        Messenger,
        /// <summary>
        /// MySpace button.
        /// </summary>
        MySpace,
        /// <summary>
        /// Orkut button.
        /// </summary>
        Orkut,
        /// <summary>
        /// Reddit button.
        /// </summary>
        Reddit,
        /// <summary>
        /// ShareThis button.
        /// </summary>
        ShareThis,
        /// <summary>
        /// StumbleUpon button.
        /// </summary>
        StumbleUpon,
        /// <summary>
        /// Tumblr button.
        /// </summary>
        Tumblr,
        /// <summary>
        /// Twitter button.
        /// </summary>
        Twitter,
        /// <summary>
        /// WordPress button.
        /// </summary>
        WordPress,
        /// <summary>
        /// Yahoo button
        /// </summary>
        Yahoo,
        /// <summary>
        /// Yahoo Bookmarks button
        /// </summary>
        Yahoo_Bookmarks
    }

    /// <summary>
    /// List available Widget Style.
    /// </summary>
    public enum ShareThisWidgetStyle
    {
        /// <summary>
        /// Oauth widget.
        /// </summary>
        Oauth,
        /// <summary>
        /// Classic widget.
        /// </summary>
        Classic
    }
}