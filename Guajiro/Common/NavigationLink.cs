using System.Windows.Input;

namespace Guajiro.Common
{
    public class NavigationLink
    {
        #region Variables

        public string Label { get; }
        public string Url { get; }
        public ICommand Open { get; }

        #endregion

        #region Constructores

        public NavigationLink(NavigationLinkType type, string url) : this(type, url, null) { }

        public NavigationLink(NavigationLinkType type, string url, string label)
        {

        }

        #endregion

        #region Procedimientos



        #endregion
    }
}
