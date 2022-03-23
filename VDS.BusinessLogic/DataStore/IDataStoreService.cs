namespace VDS.BusinessLogic.DataStore
{
    public interface IDataStoreService
    {
        void ClearAndImportDataStore(string csv);
    }
}