using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBot
{
    public class SFX
    {
        public string filename { get; set; }
        public string path { get; set; }
        public string format { get; set; }
        public string description { get; set; }
        public string[] tag { get; set; }
    }
}
