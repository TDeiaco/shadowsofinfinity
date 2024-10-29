using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ShadowsOfInfinity.Orchestrator
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel()
        {
            RendererItems = new ObservableCollection<ComboboxItem>
            {
                new ComboboxItem(){Id=0, Value = "Mandelbrot" },
                new ComboboxItem(){Id=1, Value = "Buddhabrot" },
                new ComboboxItem(){Id=2, Value = "Visagebrot" },
                new ComboboxItem(){Id=3, Value = "Nebulabrot" }
            };

            SelectedRenderer = new ComboboxItem() { Id = 0, Value = "Mandelbrot" };
            SelectedRendererChangedCommand = new RelayCommand(SelectedRendererChanged);
            AddRenderConfigCommand = new RelayCommand(AddRenderConfig);
            ExecuteCommand = new RelayCommand(Execute);

            RenderConfigs = new ObservableCollection<IRenderConfig>();
        }

        private void Execute(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddRenderConfig(object obj)
        {
            switch (SelectedRenderer.Id)
            {
                case 0:
                    RenderConfigs.Add(new MandelbrotRenderConfig()
                    {
                        Iterations = Iterations
                    });
                    break;
                case 1:
                    RenderConfigs.Add(new BuddhabrotRenderConfig()
                    {
                        Iterations = Iterations,
                        Samples = Samples,
                        Repeats = Repeats
                    });

                    break;
                case 2:
                    RenderConfigs.Add(new VisagebrotRenderConfig()
                    {
                        Samples = Samples,
                        Band = Band,
                        Cycles = Cycles,
                        Repeats = Repeats
                    });
                    break;
                case 3:
                    RenderConfigs.Add(new NebulabrotRenderConfig()
                    {
                        Samples = Samples,
                        Order = Order,
                        Repeats = Repeats
                    });
                    break;
            }
        }

        private void SelectedRendererChanged(object obj)
        {
            ShowBuddhabrot = ShowMandelbrot = ShowNebulabrot = ShowVisagebrot = false;
            switch (SelectedRenderer.Id)
            {
                case 0: ShowMandelbrot = true; break;
                case 1: ShowBuddhabrot = true; break;
                case 2: ShowVisagebrot = true; break;
                case 3: ShowNebulabrot = true; break;
            }
        }

        public ICommand SelectedRendererChangedCommand { get; set; }
        public ICommand AddRenderConfigCommand { get; set; }
        public ICommand ExecuteCommand { get; set; }

        public string WindowTitle => "Shadows of Infinity";

        public ObservableCollection<ComboboxItem> RendererItems { get; set; }

        public ObservableCollection<IRenderConfig> RenderConfigs { get; set; }

        [ObservableProperty]
        private ComboboxItem _selectedRenderer;

        [ObservableProperty]
        private bool _showBuddhabrot;

        [ObservableProperty]
        private bool _showNebulabrot;

        [ObservableProperty]
        private bool _showVisagebrot;

        [ObservableProperty]
        private bool _showMandelbrot;

        [ObservableProperty]
        private int _iterations;

        [ObservableProperty]
        private int _samples;

        [ObservableProperty]
        private int _rangeStart;

        [ObservableProperty]
        private int _rangeEnd;

        [ObservableProperty]
        private string _order;

        [ObservableProperty]
        private int _band;

        [ObservableProperty]
        private int _cycles;

        [ObservableProperty]
        private int _repeats;

    }
}
