using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace FliqloWPF
{
    public static class UIElementClone
    {
        public static UIElement Clone(this UIElement element)
        {
            string s = XamlWriter.Save(element);

            StringReader stringReader = new StringReader(s);

            XmlReader xmlReader = XmlTextReader.Create(stringReader, new XmlReaderSettings());

            return (UIElement)XamlReader.Load(xmlReader);
        }
    }
}
