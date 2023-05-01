using System.Collections.ObjectModel;
using System.ComponentModel;
using CalculatorApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace CalculatorApp.ViewModels
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Calculation> history;
        public ObservableCollection<Calculation> History
        {
            get { return history; }
            set
            {
                if (history != value)
                {
                    history = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(History)));
                }
            }
        }

        public HistoryViewModel()
        {
            History = new ObservableCollection<Calculation>();
        }

        public void AddCalculation(Calculation calculation)
        {
            History.Add(calculation);
        }
    }
    public async Task LoadHistoryAsync()
{
    try
    {
        var file = await FileSystem.Current.LocalStorage.GetFileAsync("history.json");
        var json = await file.ReadAllTextAsync();
        History = JsonConvert.DeserializeObject<List<Calculation>>(json);
    }
    catch
    {
        // Ignore errors
    }
}

using System;

namespace Calculator.Models
{
    public class CalculationHistory
    {
        public int Id { get; set; }
        public string Calculation { get; set; }
        public DateTime DateTime { get; set; }
    }
}


public async Task SaveHistoryAsync()
{
    try
    {
        var file = await FileSystem.Current.LocalStorage.CreateFileAsync("history.json", CreationCollisionOption.ReplaceExisting);
        var json = JsonConvert.SerializeObject(History);
        await file.WriteAllTextAsync(json);
    }
    catch
    {
        // Ignore errors
    }
}

using Microsoft.EntityFrameworkCore;

namespace Calculator.Data
{
    public class CalculationContext : DbContext
    {
        public DbSet<CalculationHistory> CalculationHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=CalculationHistory.db");
        }
    }
}

using (var db = new CalculationContext())
{
    var calculationHistory = new CalculationHistory
    {
        Calculation = $"{firstNumber} {mathOperator} {secondNumber} = {result.ToTrimmedString(decimalFormat)}",
        DateTime = DateTime.Now
    };

    db.CalculationHistories.Add(calculationHistory);
    db.SaveChanges();
}

using Microsoft.EntityFrameworkCore;

namespace Calculator
{
    public class CalculatorContext : DbContext
    {
        public DbSet<Calculation> CalculationHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=calculator.db");
    }
}


}
private List<Calculation> history;
public List<Calculation> History
{
    get { return history; }
    set { SetProperty(ref history, value); }
}
public HistoryPage()
{
    InitializeComponent();

    BindingContext = new HistoryViewModel();
}
