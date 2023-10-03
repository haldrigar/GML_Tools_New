using System;
using System.IO;
using System.Xml;

namespace GML_Tools.xsd
{
    internal class XsdUrlResolverReplace : XmlUrlResolver
    {
        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            Uri absoluteUri = base.ResolveUri(baseUri, relativeUri);

            if (!absoluteUri.IsFile)
            {
                string directory = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\xsd\\" + absoluteUri.Host + Path.GetDirectoryName(absoluteUri.LocalPath.Replace('/', '\\'));
                string file = Path.GetFileName(absoluteUri.LocalPath);

                string absolutePath = directory + "\\" + file;

                absoluteUri = new Uri(absolutePath);
            }

            return absoluteUri;
        }
    }
}
