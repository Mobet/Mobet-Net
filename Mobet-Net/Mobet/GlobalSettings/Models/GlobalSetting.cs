﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Mobet.Domain.Entities;
using Newtonsoft.Json;
namespace Mobet.GlobalSettings.Models
{
    /// <summary>
    /// Defines a setting.
    /// A setting is used to configure and change behavior of the application.
    /// </summary>
    public class GlobalSetting : Entity<Guid>
    {
        public GlobalSetting()
        {
            this.Scope = GlobalSettingScope.Application;
        }

        /// <summary>
        /// Primary key.
        /// </summary>
        [JsonIgnore]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id { get; set; }
        /// <summary>
        /// Unique name of the setting.
        /// </summary>
        [Required]
        [MaxLength(256)]
        public virtual string Name { get; set; }
        /// <summary>
        /// Display Name
        /// </summary>
        [Required]
        [MaxLength(256)]
        public virtual string DisplayName { get; set; }
        /// <summary>
        /// Value of the setting.
        /// </summary>
        [MaxLength(2000)]
        public virtual string Value { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [MaxLength(2000)]
        public virtual string Description { get; set; }
        /// <summary>
        /// Scopes of this setting.
        /// Default value: <see cref="GlobalSettingScope.Application"/>.
        /// </summary>
        public virtual GlobalSettingScope Scope { get; set; }
        /// <summary>
        /// GlobalSetting Group
        /// </summary>
        public virtual string Group { get; set; }
    }
}
