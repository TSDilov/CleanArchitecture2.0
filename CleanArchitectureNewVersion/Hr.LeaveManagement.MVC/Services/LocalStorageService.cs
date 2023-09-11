using Hanssens.Net;
using Hr.LeaveManagement.MVC.Contracts;

namespace Hr.LeaveManagement.MVC.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private LocalStorage storage;
        public LocalStorageService()
        {
            var config = new LocalStorageConfiguration()
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "HR.LEAVEMGMT"
            };
            this.storage = new LocalStorage(config);
        }
        public void Clearstorage(List<string> keys)
        {
            foreach (var key in keys) 
            {
                this.storage.Remove(key);
            }
        }

        public bool Exists(string key)
        {
            return this.storage.Exists(key);
        }

        public T GetStorageValue<T>(string key)
        {
            return this.storage.Get<T>(key);
        }

        public void SetStorageValue<T>(string key, T value)
        {
            this.storage.Store(key, value);
            this.storage.Persist();
        }
    }
}
