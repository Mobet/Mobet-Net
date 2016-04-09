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
            var model = GlobalSettingRepository.FirstOrDefault(x => x.Name == setting.Name);
            if (model == null)
            {
                return Task.FromResult(GlobalSettingRepository.Add(setting));
            }

            model.Name = setting.Name;
            model.Group = setting.Group;
            model.Scope = setting.Scope;
            model.Description = setting.Description;
            model.Value = setting.Value;
            model.DisplayName = setting.DisplayName;

            return Task.FromResult(GlobalSettingRepository.Update(model));
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
