namespace MauiAppLogin
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                // 🔎 Validação básica antes de tudo
                if (string.IsNullOrWhiteSpace(txt_usuario?.Text) ||
                    string.IsNullOrWhiteSpace(txt_senha?.Text))
                {
                    await DisplayAlert("Atenção", "Preencha usuário e senha!", "OK");
                    return;
                }

                var listaUsuarios = new List<DadosUsuario>
                {
                    new DadosUsuario { Usuario = "cleberson", Senha = "123" },
                    new DadosUsuario { Usuario = "ggzinha", Senha = "321" }
                };

                var usuarioDigitado = txt_usuario.Text.Trim();
                var senhaDigitada = txt_senha.Text.Trim();

                var usuarioEncontrado = listaUsuarios
                    .FirstOrDefault(u =>
                        u.Usuario.Equals(usuarioDigitado, StringComparison.OrdinalIgnoreCase)
                        && u.Senha == senhaDigitada);

                if (usuarioEncontrado != null)
                {
                    try
                    {
                        await SecureStorage.Default.SetAsync("usuario_logado", usuarioEncontrado.Usuario);
                    }
                    catch (Exception secureEx)
                    {
                        await DisplayAlert("Erro SecureStorage",
                            secureEx.Message,
                            "OK");
                        return;
                    }

                    Application.Current.MainPage = new Protegida();
                }
                else
                {
                    await DisplayAlert("Ops", "Usuário ou senha inválidos!", "Fechar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro detectado",
                    $"Tipo: {ex.GetType().FullName}\n\nMensagem: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}",
                    "Fechar");

                Console.WriteLine("===== ERRO COMPLETO =====");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}