namespace FeatureOne
{
    public interface IFeatureStore
    {
        IEnumerable<IFeature> FindStartsWith(string key);

        IEnumerable<IFeature> GetAll();
    }
}