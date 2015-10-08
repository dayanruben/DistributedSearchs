// Developed by doiTTeam => devdoiTTeam@gmail.com

namespace DistributedSearchs.MVVM
{
    public interface IViewModel<TView>
    {
        TView View { get; }
    }
}