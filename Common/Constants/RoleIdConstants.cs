namespace Fanex.Razorbill.Module.Common.Constants
{
    public class RoleIdConstants
    {
        public const int Super = 4;
        public const int Master = 3;
        public const int Agent = 2;
        public const int Member = 1;

        public static string RoleName(int roleId)
        {
            switch (roleId)
            {
                case Super:
                    return "Super";
                case Master:
                    return "Master";
                case Agent:
                    return "Agent";
                default:
                    return "Member";
            }
        }
    }
}