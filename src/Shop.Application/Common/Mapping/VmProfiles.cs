using AutoMapper;
using ShoesShop.Application.Requests.Addresses.OutputVMs;
using ShoesShop.Application.Requests.Authentication.OutputVMs;
using ShoesShop.Application.Requests.FavoriteItems.OutputVMs;
using ShoesShop.Application.Requests.Images.OutputVMs;
using ShoesShop.Application.Requests.Models.OutputVMs;
using ShoesShop.Application.Requests.ModelsSizes.OutputVMs;
using ShoesShop.Application.Requests.ModelsVariants.OutputVMs;
using ShoesShop.Application.Requests.Reviews.OutputVMs;
using ShoesShop.Application.Requests.Sales.OutputVMs;
using ShoesShop.Application.Requests.ShopCartsItems.OutputVMs;
using ShoesShop.Application.Requests.Users.OutputVMs;

namespace ShoesShop.Application.Common.Mapping
{
    public static class VmProfiles
    {
        public readonly static Dictionary<Type, Profile> Profiles = new()
        {
            {typeof(AddressVm), new AddressVmProfile()},
            {typeof(AuthenticatedDataVm), new AuthenticatedDataVmProfile()},
            {typeof(FavoriteItemVm), new FavoriteItemVmProfile()},
            {typeof(ImageVm), new ImageVmProfile()},
            {typeof(ModelVm), new ModelVmProfile()},
            {typeof(ModelSizeVm), new ModelSizeVmProfile()},
            {typeof(ModelVariantVm), new ModelVariantVmProfile()},
            {typeof(ReviewVm), new ReviewVmProfile()},
            {typeof(SaleVm), new SaleVmProfile()},
            {typeof(ShopCartItemVm), new ShopCartItemsProfile()},
            {typeof(UserVm), new UserVmProfile()},
        };

        public static IEnumerable<Profile> AllProfiles => Profiles.Values;
    }
}
