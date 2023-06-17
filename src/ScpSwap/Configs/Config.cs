using Exiled.API.Interfaces;
using PlayerRoles;
using System.Collections.Generic;
using System.ComponentModel;

namespace ScpSwap.Configs
{
    public sealed class Config : IConfig
    {
        [Description("Включен ли плагин?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Включен ли режим отладки?")]
        public bool Debug { get; set; } = false;

        [Description("Текст подсказки с информацией по плагину.")]
        public string InfoText { get; set; } = "<line-height=95%><voffset=-16em><color=FFCF48>Ты можешь сменить свой класс с помощью команды <b>.force</b>." +
            "\nТы можешь сделать это в течение {0} секунд</color></voffset>";

        [Description("Длительность подсказки.")]
        public float InfoDuration { get; set; } = 20;

        [Description("Длительность возможности перехода")]
        public ushort SwapDuration { get; set; } = 60;

        [Description("SCP которым разрешено менять роль.")]
        public static List<RoleTypeId> AllowedScps { get; set; } = new(8)
        {
            RoleTypeId.Scp096,
            RoleTypeId.Scp049,
            RoleTypeId.Scp173,
            RoleTypeId.Scp939,
            RoleTypeId.Scp106,
            RoleTypeId.Scp079
        };

        [Description("Количество слотов за данного SCP.")]
        public static Dictionary<RoleTypeId, int> Slots { get; set; } = new(8)
        {
            { RoleTypeId.Scp096, 1 },
            { RoleTypeId.Scp049, 2 },
            { RoleTypeId.Scp173, 1 },
            { RoleTypeId.Scp939, 1 },
            { RoleTypeId.Scp106, 1 },
            { RoleTypeId.Scp079, 1 },
        };
    }
}
