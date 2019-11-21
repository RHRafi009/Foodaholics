using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foodaholics.Models
{
    public class BlogDetails
    {
        public int id { get; set; }
        public string Title { get; set; }

        public String CoverPicturePath { get; set; }
        public String Content { get; set; }

        public HtmlString htmlContent { get;  set;}

        public DateTime Posted { get; set; }
        public int Watch { get; set; }

        public string WriterName { get; set; }

        public void ContentProcess()
        {
            BoldProcess();
            ItalicProcess();
            LinkProcess();
            PictureProcess();
            ParagraphProcess();
            htmlContent = new HtmlString(Content);
        }

        private void BoldProcess()
        {
            string temp = Content;
            string[] arr = temp.Split(new string[] { "*BOLD*" }, StringSplitOptions.None);
            if (arr.Length > 1)
            {
                Content = "";
                int tagOpen = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (tagOpen == 0)
                    {
                        Content += arr[i] + "<b>";
                        tagOpen = 1;
                    }
                    else if (tagOpen == 1)
                    {
                        Content += arr[i] + "</b>";
                        tagOpen = 0;
                    }

                }
            }
            
        }

        private void ItalicProcess()
        {
            string temp = Content;
            string[] arr = temp.Split(new string[] { "*I*" }, StringSplitOptions.None);
            if (arr.Length > 1)
            {
                Content = "";
                int tagOpen = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (tagOpen == 0)
                    {
                        Content += arr[i] + "<i>";
                        tagOpen = 1;
                    }
                    else if (tagOpen == 1)
                    {
                        Content += arr[i] + "</i>";
                        tagOpen = 0;
                    }

                }
            }
            
        }

        private void LinkProcess()
        {
            string temp = Content;
            string[] sArr = temp.Split(new string[] { "*L*" }, StringSplitOptions.None);
            int tagOpen = 0;
           
            if(sArr.Length > 1)
            {
                Content = "";
                for (int i = 0; i < sArr.Length; i++)
                {
                    if (tagOpen == 0)
                    {
                        Content += sArr[i] + "<a href=\"";
                        string[] addArr = sArr[i + 1].Split(new String[] { "*A*" }, StringSplitOptions.None);
                        Content += addArr[1] + "\">";
                        tagOpen = 1;
                    }
                    else if (tagOpen == 1)
                    {
                        string[] addArr = sArr[i].Split(new String[] { "*A*" }, StringSplitOptions.None);
                        Content += addArr[0] + "</a>";
                        tagOpen = 0;
                        i++;
                    }
                }
                Content += sArr[sArr.Length - 1];
            }
            
        }

        private void ParagraphProcess()
        {
            string[] arr = Content.Split(new string[] { "*P*" }, StringSplitOptions.None);
            if (arr.Length > 1)
            {
                Content = "";
                for (int i = 0; i < arr.Length; i++)
                {
                    Content += "<p>" + arr[i] + "</p>";
                }
            }
            
        }

        private void PictureProcess()
        {
            string temp = Content;
            string[] sArr = temp.Split(new string[] { "*image*" }, StringSplitOptions.None);
            //int tagOpen = 0;
            if (sArr.Length > 1)
            {
                Content = "";
                for (int i = 0; i < sArr.Length - 1; i++)
                {
                    Content += sArr[i] + "<p><img src=\"/Images/Blog/";
                    string[] addArr = sArr[i + 1].Split(new String[] { "[Do not edit]" }, StringSplitOptions.None);
                    Content += addArr[1].Trim() + "\" width='80%' height='400' class=\"img-thumbnail\"\"></p>";
                    i++;
                }
                Content += sArr[sArr.Length - 1];
            }
            
        }

    }
}