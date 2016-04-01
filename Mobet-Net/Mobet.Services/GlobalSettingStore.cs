using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mobet.Domain.Repositories;
using Mobet.GlobalSettings.Models;
using Mobet.GlobalSettings.Store;
using Mobet.Dependency;
using Mobet.Domain.Services;
using Mobet.Domain.UnitOfWork;

namespace Mobet.Services
{
    public class GlobalSettingStore : IGlobalSettingStore, IDependency
    {
        public IGlobalSettingRepository GlobalSettingRepository { get; set; }

        public GlobalSettingStore(IGlobalSettingRepository globalSettingRepository)
        {
            GlobalSettingRepository = globalSettingRepository;
        }
        [UnitOfWork]
        public virtual Task<GlobalSetting> GetSettingAsync(string name)
        {
            return Task.FromResult(GlobalSettingRepository.FirstOrDefault(x => x.Name == name));
        }
        [UnitOfWork]
        public virtual Task<GlobalSetting> AddOrUpdateSettingAsync(GlobalSetting setting)
        {
            if (setting.Id == Guid.Empty)
            {
                return Task.FromResult(GlobalSettingRepository.Add(setting));
            }
            return Task.FromResult(GlobalSettingRepository.Update(setting));
        }
        [UnitOfWork]
        public virtual Task<GlobalSetting> DeleteSettingAsync(string name)
        {
            return Task.FromResult(GlobalSettingRepository.Remove(GlobalSettingRepository.FirstOrDefault(x => x.Name == name)));
        }
        [UnitOfWork]
        public virtual Task<List<GlobalSetting>> GetAllSettingsAsync()
        {
            var models = GlobalSettingRepository.Models.ToList();

            return Task.FromResult(models);
        }
    }
}
