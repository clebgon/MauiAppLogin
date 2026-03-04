namespace MauiAppLogin;

public partial class Protegida : ContentPage
{
    public Protegida()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Recupera o usuário logado
        string? usuario_logado = await SecureStorage.Default.GetAsync("usuario_logado");

        // Atualiza o label
        lbl_boasvindas.Text = $"Bem-vindo(a) {usuario_logado}";
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        bool confirmacao = await DisplayAlert("Tem certeza?", "Sair do App?", "Sim", "Não");
        if (confirmacao)
        {
            SecureStorage.Default.Remove("usuario_logado");
            App.Current.MainPage = new Login();
        }
    }
}