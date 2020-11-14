using MyShop.Domain.Models;
using MyShop.Infrastructure.Services;

namespace MyShop.Infrastructure.Lazy.Proxies
{
    /*This will allow the proxy to intercept the calls to the property and load the data when necessary. 
     * This is a commonly applied pattern and things like into the Entity Framework. 
     * 
     * The proxy can override any of the virtual properties and methods on the base type.
     * 
     * Many object-relational mapping frameworks such as Entity Framework has built in support for lazy loading.
     */
    public class CustomerProxy : Customer
    {
        public override byte[] ProfilePicture
        {
            get
            {
                if (base.ProfilePicture == null)
                {
                    base.ProfilePicture = ProfilePictureService.GetFor(Name);
                }
                return base.ProfilePicture;
            }
        }
    }
}
