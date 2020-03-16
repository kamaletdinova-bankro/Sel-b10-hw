namespace Sel_b10_hw
{
    public class NavigationHelper : HelperBase
    {
        private readonly string baseURL;

        public NavigationHelper(ApplicationManager app, string baseURL)
            : base(app) => this.baseURL = baseURL;

        public void Go2Url(string uri, bool isFullPath=false)
        {            
            if (isFullPath)
            {
                app.Browser.Url = uri;
            }
            else
            {
                //string s = baseURL + uri;
                app.Browser.Url = baseURL + uri;
            }
        }
        public void GoShopHome()
        {
            app.Browser.Url = baseURL + @"en/";
        }
    }
}
