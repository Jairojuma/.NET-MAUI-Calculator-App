namespace Calculator;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MainPage();
	}
}
protected override async void OnStart()
{
    base.OnStart();

    // Load the history data
    await historyViewModel.LoadHistoryAsync();
}

protected override async void OnSleep()
{
    base.OnSleep();

    // Save the history data
    await historyViewModel.SaveHistoryAsync();
}
