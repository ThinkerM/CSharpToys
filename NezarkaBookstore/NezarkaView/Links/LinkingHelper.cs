using System.IO;

namespace NezarkaView.Links
{
    internal static class LinkingHelper
    {
        public static string ConstructPath(params string[] segments) 
            => Path.Combine(segments).Replace('\\', '/');
    }
}
