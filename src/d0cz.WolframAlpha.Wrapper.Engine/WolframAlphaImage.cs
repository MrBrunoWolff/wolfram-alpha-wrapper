using System;

namespace d0cz.WolframAlpha.Wrapper.Engine
{
    public class WolframAlphaImage
    {
        public WolframAlphaImage() { }

        public WolframAlphaImage(Uri location, int width, int heigth, string title, string hoverText)
        {
            Location = location;
            Width = width;
            Height = heigth;
            Title = title;
            HoverText = hoverText;
        }

        public Uri Location { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Title { get; set; }

        public string HoverText { get; set; }
    }
}