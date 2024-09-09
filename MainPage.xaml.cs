namespace TestToolbarItem;

public partial class MainPage : ContentPage
{
    private int _count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        CounterBtn.Text = $"Clicked {++_count} time{(_count > 1 ? "s" : "")}";
        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}

