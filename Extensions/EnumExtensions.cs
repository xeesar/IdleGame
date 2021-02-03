using Enums;

namespace Extensions
{
    public static class EnumExtensions
    {
        public static string ToFriendlyName(this DynamicParameterType dynamicParameterType)
        {
            switch (dynamicParameterType)
            {
                case DynamicParameterType.ArtistsCount:
                    return "add_artist";
                case DynamicParameterType.BuildingIncome:
                    return "increase_income";
                case DynamicParameterType.DrawingSpeed:
                    return "drawing_speed";
                case DynamicParameterType.ProductionSpeed:
                    return "speed_up_income";
                case DynamicParameterType.RunningSpeed:
                    return "running_speed";
                case DynamicParameterType.SprayBottleCapacity:
                    return "capacity_of_cans";
                case DynamicParameterType.RespectIncomePerBlock:
                    return "more_per_block";
                default:
                    return "none";
            }
        }
    }
}

