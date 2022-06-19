using CodeBase.Infrastructure.AssetManagment;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private IAssetProvider _assets;

        public UIFactory(IAssetProvider assets)
        {
            _assets = assets;
        }
        public void CreateRootUI()
        {
            _assets.Instantiate("Assets/Resources/UI/RootUI.prefab");
        }
        public void CreateShop()
        {
            throw new System.NotImplementedException();
        }
    }
}