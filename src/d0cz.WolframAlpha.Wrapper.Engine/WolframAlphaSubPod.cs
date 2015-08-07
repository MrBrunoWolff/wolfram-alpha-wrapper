namespace d0cz.WolframAlpha.Wrapper.Engine
{
    public class WolframAlphaSubPod
    {
        public WolframAlphaSubPod() { }

        public WolframAlphaSubPod(string title, string podText, WolframAlphaImage podImage)
        {
            Title = title;
            PodText = podText;
            PodImage = podImage;
        }

        public string Title { get; set; }

        public string PodText { get; set; }

        public WolframAlphaImage PodImage { get; set; }
    }
}