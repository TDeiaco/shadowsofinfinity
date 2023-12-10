using System.ComponentModel;

namespace ShadowsOfInfinity.Orchestrator
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string WindowTitle => "Shadows of Infinity";



        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
