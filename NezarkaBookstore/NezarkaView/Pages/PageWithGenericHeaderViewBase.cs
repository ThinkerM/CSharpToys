using System.IO;
using NezarkaModel;
using NezarkaView.Components;
using NezarkaView.Links;

namespace NezarkaView.Pages
{
    public abstract class PageWithGenericHeaderViewBase : PageViewBase
    {
        protected override IView Header { get; }

        protected override int HtmlBodyOffset => 1;

        protected PageWithGenericHeaderViewBase(ICustomer user, ILinkGenerator linkGenerator)
        {
            Header = new NezarkaCommonHeaderView(new NezarkaPageStyleSetup(), new CustomerMenuView(user, linkGenerator));
        }
    }
}
