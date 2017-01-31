namespace Ex04.Menus.Interfaces
{
    public class MainMenu : Menu
    {
        private const string k_CloseMenuStr = "Exit";

        public MainMenu(string i_Title) : base(i_Title)
        {
        }

        protected override string CloseMenuStr
        {
            get
            {
                return k_CloseMenuStr;
            }
        }
    }
}
