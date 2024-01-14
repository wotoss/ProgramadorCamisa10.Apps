using Todo.ProgramadorCamisa10.App.Views;
using Todo.ProgramadorCamisa10.Views;

namespace Todo.ProgramadorCamisa10.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new PaginaInicial();

            #region exemplo (padronizar ou forçar) tema (escuro ou claro).
            //vou (padronizar ou forçar) para que ele sempre use o tema claro do meu aplicativo
            //poderiamos como opção colocar Dark aí ficaria sempre escuro.
            //faço para neste momento não precisar pensar em temas e sim em um só no caso claro.
            //App.Current.UserAppTheme = AppTheme.Light;
            #endregion

        }
    }
}