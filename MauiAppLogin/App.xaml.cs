namespace MauiAppLogin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Inicializa com uma página "vazia" ou splash
            MainPage = new ContentPage
            {
                Content = new ActivityIndicator
                {
                    IsRunning = true,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                }
            };
        }

        protected override async void OnStart()
        {
            // Recupera o usuário logado
            string? usuario_logado = await SecureStorage.Default.GetAsync("usuario_logado");

            if (string.IsNullOrEmpty(usuario_logado))
            {
                // Se não há usuário logado, vai para a tela de login
                MainPage = new Login();
            }
            else
            {
                // Se há usuário logado, vai direto para a tela protegida
                MainPage = new Protegida();
            }
        }
    }
}