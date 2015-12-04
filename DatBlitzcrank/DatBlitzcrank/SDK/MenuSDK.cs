using EloBuddy.SDK.Menu;

namespace DatBlitzcrank.SDK
{
    public static class MenuSDK
    {
        private const string MenuName = "SDK";
        public static Menu Menu { get; private set; }

        static MenuSDK()
        {
            Menu = MainMenu.AddMenu(MenuName, "SDKmenu");
            Menu.AddGroupLabel("BanSharp SDK");
            
        }
        public static void Initialize()
        {
        }
    }
}
