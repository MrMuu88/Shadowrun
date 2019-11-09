namespace Shadowrun.DataLoader.ViewModels {
    public interface ITabVM {
        INavigationVM NavVM { get; set; }
        IDetailVM DetailVM { get; set; }
        void Load();
    }
}
