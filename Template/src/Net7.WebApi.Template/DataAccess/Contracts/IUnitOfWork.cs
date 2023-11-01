namespace Net7.WebApi.Template.DataAccess.Contracts
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves data to database
        /// </summary>
        void Save();

        /// <summary>
        /// Asynchronosley saves data to database
        /// </summary>
        Task SaveAsync();

        ///<inheritdoc/>
        void AutoDetectChangesEnabled(bool enable);

        ///<inheritdoc/>
        void ClearContext();
    }
}